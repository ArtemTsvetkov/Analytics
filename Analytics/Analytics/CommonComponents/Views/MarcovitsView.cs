using Analytics.CommonComponents.BasicObjects;
using Analytics.MarcovitsComponent.Config;
using Analytics.MarcovitsComponent.Converters;
using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.Views
{
    class MarcovitsView : Observer
    {
        private Form1 form;
        private BasicModel<MarcovitsModelState, MarcovitsConfig> model;

        public MarcovitsView(Form1 form)
        {
            this.form = form;
        }

        public void button2_Click()
        {
            CommandsStore<MarcovitsModelState, MarcovitsConfig> commandsStore =
                new ConcreteCommandStore<MarcovitsModelState, MarcovitsConfig>();
            model = new MarcovitsModel();
            MarcovitsConfig config = new MarcovitsConfig(
                "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                BasicType.hour);
            model.setConfig(config);
            model.subscribe(this);
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand<MarcovitsConfig>(model));
        }

        public void notify()
        {
            MarcovitsModelState state = model.getResult();
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
            form.label5Elem.Text = "Доходность:" + plusStr.ToString()+"%";
            //Вывод данных о риске
            form.chart2Elem.Series[0].Points.Clear();
            form.chart2Elem.Series[0].Points.AddXY(0, (state.risk[0, 0] * 100));
            form.chart2Elem.Series[0].Points.AddXY(0, 100 - (state.income * 100));
            form.chart2Elem.Series[0].Points.ElementAt(0).Color = Color.Red;
            form.chart2Elem.Series[0].Points.ElementAt(1).Color = Color.Black;
            double mines = state.risk[0, 0] * 100;
            form.label6Elem.Visible = true;
            string minesStr = mines.ToString();
            if(minesStr.Length > 4)
            {
                minesStr = minesStr.Remove(5, (minesStr.Length - 5));
            }
            form.label6Elem.Text = "Риск:" + minesStr+"%";
            /*//Вывод данных о распределении бюджета по лицензиям
            form.chart3Elem.Series[0].Points.Clear();
            for (int i = 0; i < state.unicSoftwareNames.Length; i++)
            {
                form.chart3Elem.Series[0].Points.AddXY(0, (state.percents[i, 0] * 100));
                form.chart3Elem.Series[0].Points.ElementAt(i).Label = state.unicSoftwareNames[i];
                form.chart3Elem.Legends.ElementAt(0).Title = "Лицензии:";
            }*/
            //Вывод данных о средней доходности по каждой из лицензий
            form.chart3Elem.Series[0].Points.Clear();
            for (int i = 0; i < state.unicSoftwareNames.Length; i++)
            {
                form.chart3Elem.Series[0].Points.AddXY(0, (state.avgDeviationFromPurchasedNumber[i] * 100));
                form.chart3Elem.Series[0].Points.ElementAt(i).Label = state.unicSoftwareNames[i];
            }
        }
    }
}
