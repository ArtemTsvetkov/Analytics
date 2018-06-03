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
        public int timer = 0;

        public Form1()
        {
            InitializeComponent();
            marcovitsView = new MarcovitsView(this);
            timer1.Enabled = true;
            timer1.Interval = timer1.Interval * 2;
            textBox1.Text = "D:\\Files\\MsVisualProjects\\Diplom\\Modeling\\rules2.txt";
        }

        public Chart chart1Elem
        {
            get { return chart1; }
        }

        public Chart chart2Elem
        {
            get { return chart2; }
        }

        public Chart chart3Elem
        {
            get { return chart3; }
        }

        public Chart chart4Elem
        {
            get { return chart4; }
        }

        public Label label5Elem
        {
            get { return label5; }
        }

        public Label label6Elem
        {
            get { return label6; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            marcovitsView.button2_Click();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer++;
            if (timer == 0)
            {
                pictureBox2.Image = Properties.Resources.gear1;
                pictureBox2.Update();
            }
            if (timer == 1)
            {
                pictureBox2.Image = Properties.Resources.gear2;
                pictureBox2.Update();
            }
            if (timer == 2)
            {
                pictureBox2.Image = Properties.Resources.gear3;
                pictureBox2.Update();
            }
            if (timer == 3)
            {
                pictureBox2.Image = Properties.Resources.gear4;
                pictureBox2.Update();
                timer = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> rules = new List<string>();
            //ReadWriteTextFile rwtf = new ReadWriteTextFile();
            rules = ReadWriteTextFile.Read_from_file(textBox1.Text);
            if (rules.ElementAt(0) != "Ошибка чтения, файл не существует или не доступен!")
            {
                ModelingModel control = new ModelingModel();
                ModelingState modeling_objects = new ModelingState();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.RowHeadersVisible = false;
                //добавили колонки
                DataGridViewTextBoxColumn coefficient;
                coefficient = new DataGridViewTextBoxColumn();
                coefficient.Name = "Название переменной";
                coefficient.HeaderText = "Название переменной";
                coefficient.Width = 150;
                dataGridView1.Columns.Add(coefficient);
                DataGridViewTextBoxColumn coefficient2;
                coefficient2 = new DataGridViewTextBoxColumn();
                coefficient2.Name = "Значение";
                coefficient2.HeaderText = "Значение";
                coefficient2.Width = 100;
                dataGridView1.Columns.Add(coefficient2);
                //
                dataGridView3.Rows.Clear();
                dataGridView3.Columns.Clear();
                dataGridView3.RowHeadersVisible = false;
                //добавили колонки
                DataGridViewTextBoxColumn coefficient3;
                coefficient3 = new DataGridViewTextBoxColumn();
                coefficient3.Name = "Название метки";
                coefficient3.HeaderText = "Название метки";
                coefficient3.Width = 150;
                dataGridView3.Columns.Add(coefficient3);
                DataGridViewTextBoxColumn coefficient4;
                coefficient4 = new DataGridViewTextBoxColumn();
                coefficient4.Name = "Количество переходов";
                coefficient4.HeaderText = "Количество переходов";
                coefficient4.Width = 100;
                dataGridView3.Columns.Add(coefficient4);
                //
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.RowHeadersVisible = false;
                //добавили колонки
                DataGridViewTextBoxColumn coefficient5;
                coefficient5 = new DataGridViewTextBoxColumn();
                coefficient5.Name = "Название очереди";
                coefficient5.HeaderText = "Название очереди";
                coefficient5.Width = 150;
                dataGridView2.Columns.Add(coefficient5);
                DataGridViewTextBoxColumn coefficient6;
                coefficient6 = new DataGridViewTextBoxColumn();
                coefficient6.Name = "Максимальное количество заявок";
                coefficient6.HeaderText = "Максимальное количество заявок";
                coefficient6.Width = 100;
                dataGridView2.Columns.Add(coefficient6);
                DataGridViewTextBoxColumn coefficient7;
                coefficient7 = new DataGridViewTextBoxColumn();
                coefficient7.Name = "Среднее количество заявок";
                coefficient7.HeaderText = "Среднее количество заявок";
                coefficient7.Width = 100;
                dataGridView2.Columns.Add(coefficient7);





                //Массивы для хранения информации
                List<Statistics> buf_of_stat_variables = new List<Statistics>();
                List<Statistics> buf_of_stat_queure = new List<Statistics>();
                List<Statistics> buf_of_stat_lable = new List<Statistics>();
                //Для отражения прогресса найду шаг обновления строки прогресса;
                int step = 100 / int.Parse(numericUpDown1.Value.ToString());
                progressBar1.Value = 0;
                for (int i = 0; i < numericUpDown1.Value; i++)//моделирование в соответствии с количеством итераций
                {
                    control.setConfiguration(textBox1.Text);
                    modeling_objects = control.run_simulation();
                    if (modeling_objects.result == "Успех!")
                    {
                        //запоминание результатов моделирования
                        for (int j = 0; j < modeling_objects.variables.Count; j++)//перебор всех перменных
                        {
                            if (modeling_objects.variables.ElementAt(j).get_function() == "")//проверка, чтобы переменаня была определена как ячейка таблицы
                            {
                                Statistics stat_variables = new Statistics();

                                stat_variables.name = modeling_objects.variables.ElementAt(j).get_name();
                                stat_variables.value = modeling_objects.variables.ElementAt(j).value;
                                if (i == 0)//если моделирование происходит впервый раз, то массив с перменными еще пуст и необходимо добавить элементы
                                {
                                    buf_of_stat_variables.Add(stat_variables);
                                    dataGridView1.Rows.Add(buf_of_stat_variables.Count);
                                    dataGridView1.Rows.RemoveAt(0);
                                }
                                else
                                {
                                    //поиск по имени нужной переменной
                                    for (int m = 0; m < buf_of_stat_variables.Count; m++)
                                    {
                                        if (buf_of_stat_variables.ElementAt(m).name == stat_variables.name)
                                        {
                                            double a = double.Parse(buf_of_stat_variables.ElementAt(m).value) + double.Parse(stat_variables.value);
                                            buf_of_stat_variables.ElementAt(m).value = a.ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        //вывод на экран промежуточных значений по переменным
                        for (int m = 0; m < buf_of_stat_variables.Count; m++)
                        {
                            int count = i + 1;
                            string a = count.ToString();
                            dataGridView1.Rows[m].Cells[0].Value = buf_of_stat_variables.ElementAt(m).name;
                            dataGridView1.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_variables.ElementAt(m).value) / float.Parse(a);
                            dataGridView1.Update();
                        }




                        for (int j = 0; j < modeling_objects.queues.Count; j++)//перебор всех очередей
                        {
                            Statistics stat_queure = new Statistics();
                            stat_queure.name = modeling_objects.queues.ElementAt(j).get_name();
                            float value = float.Parse(modeling_objects.queues.ElementAt(j).get_sum_tranzacts_in_queue().ToString()) / float.Parse(modeling_objects.time_of_modeling.ToString());
                            stat_queure.value = value.ToString();
                            stat_queure.value2 = modeling_objects.queues.ElementAt(j).get_max_tranzacts_in_queue().ToString();
                            if (i == 0)//если моделирование происходит впервый раз, то массив с перменными еще пуст и необходимо добавить элементы
                            {
                                dataGridView2.Rows.Add(modeling_objects.queues.Count);
                                dataGridView2.Rows.RemoveAt(0);
                                buf_of_stat_queure.Add(stat_queure);
                            }
                            else
                            {
                                //поиск по имени нужной переменной
                                for (int m = 0; m < buf_of_stat_queure.Count; m++)
                                {
                                    if (buf_of_stat_queure.ElementAt(m).name == stat_queure.name)
                                    {
                                        double a = double.Parse(buf_of_stat_queure.ElementAt(m).value) + double.Parse(stat_queure.value);
                                        buf_of_stat_queure.ElementAt(m).value = a.ToString();
                                        double b = double.Parse(buf_of_stat_queure.ElementAt(m).value2) + double.Parse(stat_queure.value2);
                                        buf_of_stat_queure.ElementAt(m).value2 = b.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                        //вывод на экран промежуточных значений очередей
                        for (int m = 0; m < buf_of_stat_queure.Count; m++)
                        {
                            int count = i + 1;
                            string a = count.ToString();
                            dataGridView2.Rows[m].Cells[0].Value = buf_of_stat_queure.ElementAt(m).name;
                            dataGridView2.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value2) / float.Parse(a);
                            dataGridView2.Rows[m].Cells[2].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value) / float.Parse(a);
                            dataGridView2.Update();
                        }




                        for (int j = 0; j < modeling_objects.lables.Count; j++)//перебор всех меток
                        {
                            Statistics stat_lable = new Statistics();
                            stat_lable.name = modeling_objects.lables.ElementAt(j).get_name();
                            stat_lable.value = modeling_objects.lables.ElementAt(j).get_entries_number().ToString();
                            if (i == 0)//если моделирование происходит впервый раз, то массив с перменными еще пуст и необходимо добавить элементы
                            {
                                dataGridView3.Rows.Add(modeling_objects.lables.Count);
                                dataGridView3.Rows.RemoveAt(0);
                                buf_of_stat_lable.Add(stat_lable);
                            }
                            else
                            {
                                //поиск по имени нужной переменной
                                for (int m = 0; m < buf_of_stat_lable.Count; m++)
                                {
                                    if (buf_of_stat_lable.ElementAt(m).name == stat_lable.name)
                                    {
                                        double a = double.Parse(buf_of_stat_lable.ElementAt(m).value) + double.Parse(stat_lable.value);
                                        buf_of_stat_lable.ElementAt(m).value = a.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                        //вывод на экран промежуточных значений по меткам
                        for (int m = 0; m < buf_of_stat_lable.Count; m++)
                        {
                            int count = i + 1;
                            string a = count.ToString();
                            dataGridView3.Rows[m].Cells[0].Value = buf_of_stat_lable.ElementAt(m).name;
                            dataGridView3.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_lable.ElementAt(m).value) / float.Parse(a);
                            dataGridView3.Update();
                        }
                    }
                    else
                    {
                        label1.Text = "Ошибка моделирования";
                        string message = modeling_objects.result;
                        string caption = "Ошибка";
                        DialogResult result;
                        result = MessageBox.Show(message, caption);
                        break;
                        progressBar1.Value = 100;
                    }
                    progressBar1.Value += step;
                }

                if (label1.Text != "Ошибка моделирования")
                {
                    //вывод на экран результатов
                    //добавление строк
                    progressBar1.Value = 100;
                    label1.Text = "Моделирование завершено";
                    for (int m = 0; m < buf_of_stat_variables.Count; m++)
                    {
                        dataGridView1.Rows[m].Cells[0].Value = buf_of_stat_variables.ElementAt(m).name;
                        dataGridView1.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_variables.ElementAt(m).value) / int.Parse(numericUpDown1.Value.ToString());
                    }
                    for (int m = 0; m < buf_of_stat_queure.Count; m++)
                    {
                        dataGridView2.Rows[m].Cells[0].Value = buf_of_stat_queure.ElementAt(m).name;
                        dataGridView2.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value2) / int.Parse(numericUpDown1.Value.ToString());
                        dataGridView2.Rows[m].Cells[2].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value) / int.Parse(numericUpDown1.Value.ToString());
                    }
                    for (int m = 0; m < buf_of_stat_lable.Count; m++)
                    {
                        dataGridView3.Rows[m].Cells[0].Value = buf_of_stat_lable.ElementAt(m).name;
                        dataGridView3.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_lable.ElementAt(m).value) / int.Parse(numericUpDown1.Value.ToString());
                    }
                    //"D:\\Files\\MsVisualProjects\\Modelirovanie\\rules.txt"
                }
            }
            else
            {
                label1.Text = "Ошибка моделирования";
                string message = "Ошибка чтения файла команд";
                string caption = "Ошибка";
                DialogResult result;
                result = MessageBox.Show(message, caption);
                progressBar1.Value = 100;
            }
        }
    }
}
