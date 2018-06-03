using Analytics.CommonComponents.BasicObjects;
using Analytics.MarcovitsComponent.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics
{
    public partial class Form1 : Form, Observer
    {
        BasicModel<MarcovitsModelState, MarcovitsModelState> model;

        public Form1()
        {
            InitializeComponent();
        }
        
        public void notify()
        {
            MarcovitsModelState state = model.getResult();
            //Вывод данных о доходности
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, (state.income*100));
            chart1.Series[0].Points.AddXY(0, 100- (state.income * 100));
            chart1.Series[0].Points.ElementAt(1).Color = Color.Black;
            double plus = state.income * 100;
            label5.Visible = true;
            label5.Text = "Доходность:" + plus.ToString();
            //Вывод данных о риске
            chart2.Series[0].Points.Clear();
            chart2.Series[0].Points.AddXY(0, (state.risk[0,0] * 100));
            chart2.Series[0].Points.AddXY(0, 100 - (state.income * 100));
            chart2.Series[0].Points.ElementAt(0).Color = Color.Red;
            chart2.Series[0].Points.ElementAt(1).Color = Color.Black;
            double mines = state.risk[0, 0] * 100;
            label6.Visible = true;
            label6.Text = "Риск:" + mines.ToString();
            //Вывод данных о распределении бюджета по лицензиям
            chart3.Series[0].Points.Clear();
            for(int i=0;i<state.unicSoftwareNames.Length;i++)
            {
                chart3.Series[0].Points.AddXY(0, (state.percents[i,0] * 100));
                chart3.Series[0].Points.ElementAt(i).Label = state.unicSoftwareNames[i];
                chart3.Legends.ElementAt(0).Title = "Лицензии:";
            }
            //Вывод данных о средней доходности по каждой из лицензий
            chart4.Series[0].Points.Clear();
            for (int i = 0; i < state.unicSoftwareNames.Length; i++)
            {
                chart4.Series[0].Points.AddXY(0, (state.avgDeviationFromPurchasedNumber[i]*100));
                chart4.Series[0].Points.ElementAt(i).Label = state.unicSoftwareNames[i];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommandsStore<MarcovitsModelState, MarcovitsModelState> commandsStore = new ConcreteCommandStore();
            ResultConverter resultConverter = new ResultConverter();
            model = new MarcovitsModel("D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb", "Information");
            model.setResultConverter(resultConverter);
            model.subscribe(this);
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand(model)); 
            model.calculationStatistics();
        }
    }
}
