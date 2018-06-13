using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.Views;
using Analytics.MarcovitsComponent.Converters;
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
            textBox1.Text = "D:\\Files\\MsVisualProjects\\Diplom\\AnaliticsMath\\rules2.txt";
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

        private void button2_Click(object sender, EventArgs e)
        {
            marcovitsView.button2_Click();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            modelingView = new ModelingView(this);
            modelingView.button1_Click();
        }
    }
}
