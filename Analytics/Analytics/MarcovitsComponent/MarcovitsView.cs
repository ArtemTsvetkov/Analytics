using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.CommonComponents.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.Exceptions;
using Analytics.MarcovitsComponent.Config;
using Analytics.MarcovitsComponent.Converters;
using Analytics.Modeling.GroupByTypes;
using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.Views
{
    class MarcovitsView : Observer, NavigatorsView
    {
        private Form1 form;
        private BasicStatisticsModel<MarcovitsModelState, MarcovitsConfig> model;
        CommandsStore<MarcovitsModelState, MarcovitsConfig> commandsStore =
                new ConcreteCommandStore<MarcovitsModelState, MarcovitsConfig>();
        //При откате модели до предыдущего состояния, элементы вью тоже меняются,
        //но так как они прослушиваются на изменения вью, то это влечет за собой 
        //изменение модели и добавление еще одной команды, а она не нужна, так как мы
        //только что забрали предыдущую
        private bool activateChangeListeners = true;

        public MarcovitsView(Form1 form)
        {
            this.form = form;
            model = new MarcovitsModel();
            MarcovitsConfig config = new MarcovitsConfig(
                "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                BasicType.year);
            model.setConfig(config);
            model.subscribe(this);
        }

        public void button2_Click()
        {
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand<MarcovitsConfig>(model));
        }

        public void intervalChange(GropByType interval)
        {
            if (activateChangeListeners)
            {
                form.chart1Elem.Series[0].Points.Clear();
                form.chart2Elem.Series[0].Points.Clear();
                form.label5Elem.Text = "";
                form.label6Elem.Text = "";
                form.label5Elem.Visible = false;
                form.label6Elem.Visible = false;
                MarcovitsConfig config = new MarcovitsConfig(
                    "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                    interval);
                commandsStore.executeCommand(
                    new UpdateConfigCommand<MarcovitsModelState, MarcovitsConfig>(model, config));
            }
        }

        public void getPreviousState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        public void getNextState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            commandsStore.rollbackRecoveryModel();
            activateChangeListeners = true;
        }

        public void notify()
        {
            MarcovitsModelState state = model.getResult();
            if (state.income != 0 & state.risk[0, 0] != 0)
            {
                //Вывод данных о доходности
                form.chart1Elem.Series[0].Points.Clear();
                form.chart1Elem.Series[0].Points.AddXY(0, (state.income * 100));
                form.chart1Elem.Series[0].Points.AddXY(0, 100 - (state.income * 100));
                form.chart1Elem.Series[0].Points.ElementAt(1).Color = Color.Black;
                double plus = state.income * 100;
                form.label5Elem.Visible = true;
                string plusStr = plus.ToString();
                if (plusStr.Length > 4)
                {
                    plusStr = plusStr.Remove(5, (plusStr.Length - 5));
                }
                form.label5Elem.Text = "Доходность:" + plusStr.ToString() + "%";
                //Вывод данных о риске
                form.chart2Elem.Series[0].Points.Clear();
                form.chart2Elem.Series[0].Points.AddXY(0, (state.risk[0, 0] * 100));
                form.chart2Elem.Series[0].Points.AddXY(0, 100 - (state.income * 100));
                form.chart2Elem.Series[0].Points.ElementAt(0).Color = Color.Red;
                form.chart2Elem.Series[0].Points.ElementAt(1).Color = Color.Black;
                double mines = state.risk[0, 0] * 100;
                form.label6Elem.Visible = true;
                string minesStr = mines.ToString();
                if (minesStr.Length > 4)
                {
                    minesStr = minesStr.Remove(5, (minesStr.Length - 5));
                }
                form.label6Elem.Text = "Риск:" + minesStr + "%";
            }
            //Обновление управляющих элементов
            switch (state.interval.getType())
            {
                case "year":
                    form.comboBox3Elem.SelectedIndex = 0;
                    break;
                case "month":
                    form.comboBox3Elem.SelectedIndex = 1;
                    break;
                case "day":
                    form.comboBox3Elem.SelectedIndex = 2;
                    break;
                case "hour":
                    form.comboBox3Elem.SelectedIndex = 3;
                    break;
                case "minute":
                    form.comboBox3Elem.SelectedIndex = 4;
                    break;
                case "second":
                    form.comboBox3Elem.SelectedIndex = 5;
                    break;
                default:
                    throw new UnknownTimeIntervalType("Unknown time interval type");
            }
        }


        public void show()
        {
            form.tabControl1Elem.SelectTab(3);
            form.tabControl2Elem.SelectTab(1);
        }

        public string getName()
        {
            return "MarcovitsView";
        }
    }
}
