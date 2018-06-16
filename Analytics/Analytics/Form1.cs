using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.Views;
using Analytics.MarcovitsComponent.Converters;
using Analytics.Modeling.GroupByTypes;
using Modelirovanie;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Analytics
{
    public partial class Form1 : Form
    {
        private MarcovitsView marcovitsView;
        private ModelingView modelingView;
        public int timer = 0;

        public Form1()
        {
            InitializeComponent();
            marcovitsView = new MarcovitsView(this);
            modelingView = new ModelingView(this);
            textBox1.Text = "D:\\Files\\MsVisualProjects\\Diplom\\AnaliticsMath\\rules2.txt";
            comboBox1.SelectedIndex = 0;
        }

        public Chart chart1Elem
        {
            get { return chart1; }
        }

        public Chart chart2Elem
        {
            get { return chart2; }
        }

        public Label label5Elem
        {
            get { return label5; }
        }

        public Label label12Elem
        {
            get { return label12; }
        }

        public Label label6Elem
        {
            get { return label6; }
        }

        public TextBox textBox1Elem
        {
            get { return textBox1; }
        }

        public DataGridView dataGridView1Elem
        {
            get { return dataGridView1; }
        }

        public DataGridView dataGridView3Elem
        {
            get { return dataGridView3; }
        }

        public DataGridView dataGridView2Elem
        {
            get { return dataGridView2; }
        }

        public NumericUpDown numericUpDown1Elem
        {
            get { return numericUpDown1; }
        }

        public ProgressBar progressBar1Elem
        {
            get { return progressBar1; }
        }

        public ComboBox comboBox1Elem
        {
            get { return comboBox1; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            marcovitsView.button2_Click();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modelingView.button1_Click();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //marcovitsView.intervalChange(BasicType.year);
                    if(modelingView!=null)
                    {
                        modelingView.intervalChange(BasicType.year);
                    }
                    break;
                case 1:
                    //marcovitsView.intervalChange(BasicType.month);
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.month);
                    }
                    break;
                case 2:
                    //marcovitsView.intervalChange(BasicType.day);
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.day);
                    }
                    break;
                case 3:
                    //marcovitsView.intervalChange(BasicType.hour);
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.hour);
                    }
                    break;
                case 4:
                    //marcovitsView.intervalChange(BasicType.minute);
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.minute);
                    }
                    break;
                case 5:
                    //marcovitsView.intervalChange(BasicType.second);
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.second);
                    }
                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП ИНТЕРВАЛА
                    throw new Exception();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            marcovitsView.getPreviousState();
            modelingView.getPreviousState();
        }
    }
}
