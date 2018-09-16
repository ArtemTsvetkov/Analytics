using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.ExceptionHandler;
using Analytics.CommonComponents.ExceptionHandler.Concrete;
using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.ExceptionHandler.View;
using Analytics.CommonComponents.Exceptions;
using Analytics.CommonComponents.Views;
using Analytics.MarcovitsComponent.Converters;
using Analytics.Modeling.GroupByTypes;
using Analytics.Navigator;
using Analytics.Navigator.Basic;
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
            try
            {
                InitializeComponent();
                ConcreteExceptionHandlerInitializer.initThisExceptionHandler(
                    ExceptionHandler.getInstance());
                marcovitsView = new MarcovitsView(this);
                modelingView = new ModelingView(this);
                comboBox1.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                Navigator.Navigator.getInstance().addView(marcovitsView);
                Navigator.Navigator.getInstance().addView(modelingView);
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
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

        public TabControl tabControl1Elem
        {
            get { return tabControl1; }
        }

        public TabControl tabControl2Elem
        {
            get { return tabControl2; }
        }

        public ComboBox comboBox1Elem
        {
            get { return comboBox1; }
        }

        public ComboBox comboBox3Elem
        {
            get { return comboBox3; }
        }

        public CheckBox checkBox1Elem
        {
            get { return checkBox1; }
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
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        if (modelingView != null)
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
                        throw new UnknownTimeIntervalType("Unknown time interval type");
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modelingView.getPreviousState();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
                        throw new UnknownTimeIntervalType("Unknown time interval type");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            marcovitsView.getPreviousState();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            modelingView.flagUseCovarChange(checkBox1.Checked);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Navigator.Navigator.getInstance().navigateTo("ModelingView");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.year);
                    }*/
                    break;
                case 1:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.month);
                    }*/
                    break;
                case 2:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.day);
                    }*/
                    tabControl1.SelectTab(2);

                    //Просто для оформления диплома
                    dataGridView4.RowHeadersVisible = false;
                    dataGridView6.RowHeadersVisible = false;



                    DataGridViewTextBoxColumn col1;
                    col1 = new DataGridViewTextBoxColumn();
                    col1.Name = "Название лицензии";
                    col1.HeaderText = "Название лицензии";
                    col1.Width = 158;
                    DataGridViewTextBoxColumn col2;
                    col2 = new DataGridViewTextBoxColumn();
                    col2.Name = "Наличие";
                    col2.HeaderText = "Наличие";
                    col2.Width = 75;
                    DataGridViewTextBoxColumn col3;
                    col3 = new DataGridViewTextBoxColumn();
                    col3.Name = "Новое";
                    col3.HeaderText = "Новое";
                    col3.Width = 75;
                    dataGridView4.Columns.Add(col1);
                    dataGridView4.Columns.Add(col2);
                    dataGridView4.Columns.Add(col3);


                    DataGridViewTextBoxColumn col10;
                    col10 = new DataGridViewTextBoxColumn();
                    col10.Name = "Название лицензии";
                    col10.HeaderText = "Название лицензии";
                    col10.Width = 158;
                    DataGridViewTextBoxColumn col11;
                    col11 = new DataGridViewTextBoxColumn();
                    col11.Name = "Текущий";
                    col11.HeaderText = "Текущий";
                    col11.Width = 75;
                    DataGridViewTextBoxColumn col12;
                    col12 = new DataGridViewTextBoxColumn();
                    col12.Name = "Новый";
                    col12.HeaderText = "Новый";
                    col12.Width = 75;
                    dataGridView6.Columns.Add(col10);
                    dataGridView6.Columns.Add(col11);
                    dataGridView6.Columns.Add(col12);



                    dataGridView4.Rows.Clear();
                    dataGridView4.Rows.Add(5);
                    dataGridView4.Rows.RemoveAt(0);

                    dataGridView6.Rows.Clear();
                    dataGridView6.Rows.Add(5);
                    dataGridView6.Rows.RemoveAt(0);



                    dataGridView4.Rows[0].Cells[0].Value = "85787BDSPRM_F";
                    dataGridView4.Rows[1].Cells[0].Value = "86071BDSPRM_2014_0F";
                    dataGridView4.Rows[2].Cells[0].Value = "86238BDSPRM_2015_0F";
                    dataGridView4.Rows[3].Cells[0].Value = "86451BDSPRM_2016_0F";
                    dataGridView4.Rows[4].Cells[0].Value = "86696BDSPRM_2017_0F";

                    dataGridView4.Rows[0].Cells[1].Value = "8";
                    dataGridView4.Rows[1].Cells[1].Value = "3";
                    dataGridView4.Rows[2].Cells[1].Value = "1";
                    dataGridView4.Rows[3].Cells[1].Value = "4";
                    dataGridView4.Rows[4].Cells[1].Value = "1";

                    dataGridView4.Rows[0].Cells[2].Value = "5";
                    dataGridView4.Rows[1].Cells[2].Value = "3";
                    dataGridView4.Rows[2].Cells[2].Value = "1";
                    dataGridView4.Rows[3].Cells[2].Value = "3";
                    dataGridView4.Rows[4].Cells[2].Value = "1";
         


                    dataGridView6.Rows[0].Cells[0].Value = "85787BDSPRM_F";
                    dataGridView6.Rows[1].Cells[0].Value = "86071BDSPRM_2014_0F";
                    dataGridView6.Rows[2].Cells[0].Value = "86238BDSPRM_2015_0F";
                    dataGridView6.Rows[3].Cells[0].Value = "86451BDSPRM_2016_0F";
                    dataGridView6.Rows[4].Cells[0].Value = "86696BDSPRM_2017_0F";

                    dataGridView6.Rows[0].Cells[1].Value = "0.3";
                    dataGridView6.Rows[1].Cells[1].Value = "0.1";
                    dataGridView6.Rows[2].Cells[1].Value = "0.2";
                    dataGridView6.Rows[3].Cells[1].Value = "0.3";
                    dataGridView6.Rows[4].Cells[1].Value = "0.1";

                    dataGridView6.Rows[0].Cells[2].Value = "0.2";
                    dataGridView6.Rows[1].Cells[2].Value = "0.1";
                    dataGridView6.Rows[2].Cells[2].Value = "0.2";
                    dataGridView6.Rows[3].Cells[2].Value = "0.4";
                    dataGridView6.Rows[4].Cells[2].Value = "0.1";

                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП
                    throw new Exception();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.year);
                    }*/
                    break;
                case 1:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.month);
                    }*/
                    break;
                case 2:
                    /*if (modelingView != null)
                    {
                        modelingView.intervalChange(BasicType.day);
                    }*/
                    tabControl1.SelectTab(2);
                    break;
                default:
                    //ДОБАВИТЬ СЮДА ИСКЛЮЧЕНИЕ - НЕИЗВЕСТНЫЙ ТИП
                    throw new Exception();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            modelingView.numberOfModelingStartsChange(
                int.Parse(numericUpDown1.Value.ToString()));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            marcovitsView.getNextState();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            modelingView.getNextState();
        }
    }
}
