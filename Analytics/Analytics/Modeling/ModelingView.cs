using Analytics.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.Exceptions;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithFiles.Load;
using Analytics.Modeling;
using Analytics.Modeling.Config;
using Analytics.Modeling.Converters;
using Analytics.Modeling.GroupByTypes;
using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.Views
{
    class ModelingView : Observer, NavigatorsView
    {
        private Form1 form;
        private ModelingModel model;
        private ModelingConfig config;
        int step = 0;

        public ModelingView(Form1 form, ModelingModel model)
        {
            this.form = form;
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


            this.model = model;
            this.model.subscribe(this);
            config = new ModelingConfig(
                "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                BasicType.year);
            config.setWithKovar(false);
            config.setNumberOfStartsModeling(1);
            model.setConfig(config);
            form.progressBar1Elem.Value = 0;
        }

        public void notify()
        {
            form.label12Elem.Text = "";
            if(form.progressBar1Elem.Value>=100)
            {
                form.progressBar1Elem.Value = 0;
            }
            ModelingReport report = model.getResult();
            step = 100 / report.getConfig().getNumberOfStartsModeling();
            if (report.getConfig().getNumberOfStartsModeling() == 
                report.getNumberOfReportsUpdates())
            {
                form.progressBar1Elem.Value = 100;
                form.label12Elem.Text = "Статус: анализ завершен";
            }
            if (report.getConfig().getNumberOfStartsModeling() >
                report.getNumberOfReportsUpdates() & report.getNumberOfReportsUpdates()!=0)
            {
                form.progressBar1Elem.Value += step;
            }
                //Проверка строк в таблицах
                if (form.dataGridView2Elem.Rows.Count != report.getAvgTranzactsInQueue().Count()
                && report.getAvgTranzactsInQueue().Count() > 0)
            {
                form.dataGridView2Elem.Rows.Clear();
                form.dataGridView2Elem.Rows.Add(report.getAvgTranzactsInQueue().Count());
                form.dataGridView2Elem.Rows.RemoveAt(0);
            }

            if (report.getAvgTranzactsInQueue().Count == 0)
            {
                form.dataGridView2Elem.Rows.Clear();
            }

            //Заполнение таблиц
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
                form.checkBox1Elem.Checked = report.getConfig().getWithKovar();
            }
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(3);
            form.tabControl2Elem.SelectTab(0);
        }

        public string getName()
        {
            return "ModelingView";
        }
    }
}
