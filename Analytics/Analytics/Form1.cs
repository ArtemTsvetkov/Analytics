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
            comboBox3.SelectedIndex = 0;
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

        public ComboBox comboBox3Elem
        {
            get { return comboBox3; }
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
                    if(modelingView!=null)
                    {
                        modelingView.intervalChange(BasicType.year);
                    }
                    break;
                case 1:
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.month);
                    }
                    break;
                case 2:
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.day);
                    }
                    break;
                case 3:
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.hour);
                    }
                    break;
                case 4:
                    if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.minute);
                    }
                    break;
                case 5:
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
            modelingView.getPreviousState();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    if (marcovitsView != null)
                    {
                        marcovitsView.intervalChange(BasicType.year);
                    }
                    break;
                case 1:
                    if (marcovitsView != null)
                    {
                        marcovitsView.intervalChange(BasicType.month);
                    }
                    break;
                case 2:
                    if (marcovitsView != null)
                    {
                        marcovitsView.intervalChange(BasicType.day);
                    }
                    break;
                case 3:
                    if (marcovitsView != null)
                    {
                        marcovitsView.intervalChange(BasicType.hour);
                    }
                    break;
                case 4:
                    if (marcovitsView != null)
                    {
                        marcovitsView.intervalChange(BasicType.minute);
                    }
                    break;
                case 5:
                    if (marcovitsView != null)
                    {
                        marcovitsView.intervalChange(BasicType.second);
                    }
                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП ИНТЕРВАЛА
                    throw new Exception();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            marcovitsView.getPreviousState();
        }
    }
}
