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
        
        public Form1()
        {
            InitializeComponent();
        }
        
        public void notify(object data)
        {
            if (data.GetType() == typeof(List<AllDataTable>))
            {
                List<AllDataTable> rw = (List<AllDataTable>)data;


                int m = rw.Count();
                int n = 3;
                dataGridView1.ColumnCount = n;
                dataGridView1.RowCount = m;
                for (int i = 0; i < m; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = rw.ElementAt(i).user;
                    dataGridView1.Rows[i].Cells[1].Value = rw.ElementAt(i).host;
                    dataGridView1.Rows[i].Cells[2].Value = rw.ElementAt(i).po;
                }
            }
            if (data.GetType() == typeof(MarcovitsModelState))
            {
                MarcovitsModelState state = (MarcovitsModelState)data;
                //Вывод данных о доходности
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.AddXY(0, (state.income*100));
                chart1.Series[0].Points.AddXY(0, 100- (state.income * 100));
                chart1.Series[0].Points.ElementAt(1).Color = Color.Black;
                //Вывод данных о риске
                chart2.Series[0].Points.Clear();
                chart2.Series[0].Points.AddXY(0, (state.risk[0,0] * 100));
                chart2.Series[0].Points.AddXY(0, 100 - (state.income * 100));
                chart2.Series[0].Points.ElementAt(0).Color = Color.Red;
                chart2.Series[0].Points.ElementAt(1).Color = Color.Black;
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
        }

        CommandsStore commandsStore = new ConcreteCommandStore();
        Model model;

        private void button1_Click(object sender, EventArgs e)
        {
            model = new AllLoadModel("D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb", "Information");
            model.subscribe(this);
            commandsStore.executeCommand(new LoadDataCommand(model));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            model = new MarcovitsModel("D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb", "Information");
            model.subscribe(this);
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand(model)); 
            model.calculationStatistics();
        }
    }
}
