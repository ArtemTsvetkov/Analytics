using Analytics.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.Exceptions;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithFiles.Load;
using Analytics.Modeling;
using Analytics.Modeling.Config;
using Analytics.Modeling.Converters;
using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.Views
{
    class ModelingView : Observer
    {
        private Form1 form;
        private ModelingModel control;
        CommandsStore<ModelingReport, ModelingConfig> commandsStore =
                new ConcreteCommandStore<ModelingReport, ModelingConfig>();
        private ModelingConfig config;
        int step = 1;
        //При откате модели до предыдущего состояния, элементы вью тоже меняются,
        //но так как они прослушиваются на изменения вью, то это влечет за собой 
        //изменение модели и добавление еще одной команды, а она не нужна, так как мы
        //только что забрали предыдущую
        private bool activateChangeListeners = true;

        public ModelingView(Form1 form)
        {
            this.form = form;
            form.dataGridView1Elem.Rows.Clear();
            form.dataGridView1Elem.Columns.Clear();
            form.dataGridView1Elem.RowHeadersVisible = false;
            //добавили колонки
            DataGridViewTextBoxColumn coefficient;
            coefficient = new DataGridViewTextBoxColumn();
            coefficient.Name = "Название переменной";
            coefficient.HeaderText = "Название переменной";
            coefficient.Width = 150;
            form.dataGridView1Elem.Columns.Add(coefficient);
            DataGridViewTextBoxColumn coefficient2;
            coefficient2 = new DataGridViewTextBoxColumn();
            coefficient2.Name = "Значение";
            coefficient2.HeaderText = "Значение";
            coefficient2.Width = 100;
            form.dataGridView1Elem.Columns.Add(coefficient2);
            //
            form.dataGridView3Elem.Rows.Clear();
            form.dataGridView3Elem.Columns.Clear();
            form.dataGridView3Elem.RowHeadersVisible = false;
            //добавили колонки
            DataGridViewTextBoxColumn coefficient3;
            coefficient3 = new DataGridViewTextBoxColumn();
            coefficient3.Name = "Название метки";
            coefficient3.HeaderText = "Название метки";
            coefficient3.Width = 150;
            form.dataGridView3Elem.Columns.Add(coefficient3);
            DataGridViewTextBoxColumn coefficient4;
            coefficient4 = new DataGridViewTextBoxColumn();
            coefficient4.Name = "Количество переходов";
            coefficient4.HeaderText = "Количество переходов";
            coefficient4.Width = 100;
            form.dataGridView3Elem.Columns.Add(coefficient4);
            //
            form.dataGridView2Elem.Rows.Clear();
            form.dataGridView2Elem.Columns.Clear();
            form.dataGridView2Elem.RowHeadersVisible = false;
            //добавили колонки
            DataGridViewTextBoxColumn coefficient5;
            coefficient5 = new DataGridViewTextBoxColumn();
            coefficient5.Name = "Тип лицензии";
            coefficient5.HeaderText = "Тип лицензии";
            coefficient5.Width = 150;
            form.dataGridView2Elem.Columns.Add(coefficient5);
            DataGridViewTextBoxColumn coefficient6;
            coefficient6 = new DataGridViewTextBoxColumn();
            coefficient6.Name = "Максимальное количество заявок";
            coefficient6.HeaderText = "Максимальное количество заявок";
            coefficient6.Width = 100;
            form.dataGridView2Elem.Columns.Add(coefficient6);
            DataGridViewTextBoxColumn coefficient7;
            coefficient7 = new DataGridViewTextBoxColumn();
            coefficient7.Name = "Среднее количество заявок";
            coefficient7.HeaderText = "Среднее количество заявок";
            coefficient7.Width = 100;
            form.dataGridView2Elem.Columns.Add(coefficient7);
            form.progressBar1Elem.Value = 0;


            control = new ModelingModel();
            control.subscribe(this);
            config = new ModelingConfig(
                "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                BasicType.year);
            config.setWithKovar(false);
            control.setConfig(config);
            control.loadStore();
            form.progressBar1Elem.Value = 0;
        }

        public void button1_Click()
        {
            //form.progressBar1Elem.Value = 0;
            //step = 100 / int.Parse(form.numericUpDown1Elem.Value.ToString())/3;
            commandsStore.executeCommand(new RunModeling<ModelingConfig>(control));
            //form.progressBar1Elem.Value = 100;
        }

        public void intervalChange(GropByType interval)
        {
            if (activateChangeListeners)
            {
                config = control.getConfig();
                config.setInterval(interval);
                commandsStore.executeCommand(
                    new UpdateConfigCommand<ModelingReport, ModelingConfig>(control, config));
                form.progressBar1Elem.Value = 0;
            }
        }

        public void numberOfModelingStartsChange(int number)
        {
            if (activateChangeListeners)
            {
                try
                {
                    config = control.getConfig();
                    config.setNumberOfStartsModeling(number);
                    commandsStore.executeCommand(
                        new UpdateConfigCommand<ModelingReport, ModelingConfig>(control, config));
                    form.progressBar1Elem.Value = 0;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionHandler.getInstance().processing(ex);
                }
            }
        }

        public void flagUseCovarChange(bool flag)
        {
            if (activateChangeListeners)
            {
                config = control.getConfig();
                config.setWithKovar(flag);
                commandsStore.executeCommand(
                    new UpdateConfigCommand<ModelingReport, ModelingConfig>(control, config));
                form.progressBar1Elem.Value = 0;
            }
        }

        public void getPreviousState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            ModelingConfig configWithReset = new ModelingConfig(
                        "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                        config.getInterval());
            configWithReset.setWithKovar(config.getWithKovar());
            control.setConfig(configWithReset);
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        public void notify()
        {
            ModelingReport report = control.getResult();
            //Проверка строк в таблицах
            if (form.dataGridView1Elem.Rows.Count != report.getVariablesValue().Count()
                && report.getVariablesValue().Count() > 0)
            {
                form.dataGridView1Elem.Rows.Clear();
                form.dataGridView1Elem.Rows.Add(report.getVariablesValue().Count());
                form.dataGridView1Elem.Rows.RemoveAt(0);
            }
            if (form.dataGridView2Elem.Rows.Count != report.getAvgTranzactsInQueue().Count()
                && report.getAvgTranzactsInQueue().Count() > 0)
            {
                form.dataGridView2Elem.Rows.Clear();
                form.dataGridView2Elem.Rows.Add(report.getAvgTranzactsInQueue().Count());
                form.dataGridView2Elem.Rows.RemoveAt(0);
            }
            if (form.dataGridView3Elem.Rows.Count != report.getNumberRunTranzactsOnLable().Count()
                && report.getNumberRunTranzactsOnLable().Count() > 0)
            {
                form.dataGridView3Elem.Rows.Clear();
                form.dataGridView3Elem.Rows.Add(report.getNumberRunTranzactsOnLable().Count());
                form.dataGridView3Elem.Rows.RemoveAt(0);
            }


            if (report.getVariablesValue().Count() == 0)
            {
                form.dataGridView1Elem.Rows.Clear();
            }
            if (report.getAvgTranzactsInQueue().Count == 0)
            {
                form.dataGridView2Elem.Rows.Clear();
            }
            if (report.getNumberRunTranzactsOnLable().Count == 0)
            {
                form.dataGridView3Elem.Rows.Clear();
            }

            //Заполнение таблиц
            for (int i = 0; i < report.getVariablesValue().Count(); i++)
            {
                form.dataGridView1Elem.Rows[i].Cells[0].Value =
                    report.getVariablesValue().ElementAt(i).name;
                form.dataGridView1Elem.Rows[i].Cells[1].Value =
                    report.getVariablesValue().ElementAt(i).value;

                form.dataGridView1Elem.Update();
            }
            for (int i = 0; i < report.getAvgTranzactsInQueue().Count(); i++)
            {
                form.dataGridView2Elem.Rows[i].Cells[0].Value =
                    report.getMaxTranzactsInQueue().ElementAt(i).name;
                form.dataGridView2Elem.Rows[i].Cells[1].Value =
                    report.getMaxTranzactsInQueue().ElementAt(i).value;
                form.dataGridView2Elem.Rows[i].Cells[2].Value =
                    report.getAvgTranzactsInQueue().ElementAt(i).value;

                form.dataGridView2Elem.Update();
            }
            for (int i = 0; i < report.getNumberRunTranzactsOnLable().Count(); i++)
            {
                form.dataGridView3Elem.Rows[i].Cells[0].Value =
                    report.getNumberRunTranzactsOnLable().ElementAt(i).name;
                form.dataGridView3Elem.Rows[i].Cells[1].Value =
                    report.getNumberRunTranzactsOnLable().ElementAt(i).value;

                form.dataGridView3Elem.Update();
            }

            //if (form.progressBar1Elem.Value + step < 100)
            //{
                //form.progressBar1Elem.Value += step;
            //}
            //else
            //{
                //form.progressBar1Elem.Value = 0;
            //}

            //Обновление управляющих элементов
            if (report.getConfig() != null)
            {
                switch (report.getConfig().getInterval().getType())
                {
                    case "year":
                        form.comboBox1Elem.SelectedIndex = 0;
                        break;
                    case "month":
                        form.comboBox1Elem.SelectedIndex = 1;
                        break;
                    case "day":
                        form.comboBox1Elem.SelectedIndex = 2;
                        break;
                    case "hour":
                        form.comboBox1Elem.SelectedIndex = 3;
                        break;
                    case "minute":
                        form.comboBox1Elem.SelectedIndex = 4;
                        break;
                    case "second":
                        form.comboBox1Elem.SelectedIndex = 5;
                        break;
                    default:
                        throw new UnknownTimeIntervalType("Unknown time interval type");
                }
                form.numericUpDown1Elem.Value = report.getConfig().getNumberOfStartsModeling();
            }
        }
    }
}
