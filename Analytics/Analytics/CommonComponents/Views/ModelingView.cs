using Analytics.Modeling.Converters;
using Modelirovanie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.Views
{
    class ModelingView : Observer
    {
        private Form1 form;
        private ModelingModel control;

        public ModelingView(Form1 form)
        {
            this.form = form;
        }

        public void button1_Click()
        {
            List<string> rules = new List<string>();
            rules = ReadWriteTextFile.Read_from_file(form.textBox1Elem.Text);
            if (rules.ElementAt(0) != "Ошибка чтения, файл не существует или не доступен!")
            {
                control = new ModelingModel();
                ResultConverter resultConverter = new ResultConverter();
                control.setResultConverter(resultConverter);
                ModelingState modeling_objects = new ModelingState();
                form.dataGridView1Elem.Rows.Clear();
                form.dataGridView1Elem.Columns.Clear();
                form.dataGridView1Elem.RowHeadersVisible = false;
                //добавили колонки
                DataGridViewTextBoxColumn coefficient;
                coefficient = new DataGridViewTextBoxColumn();
                coefficient.Name = "Название переменной";
                coefficient.HeaderText = "Название переменной";
                coefficient.Width = 150;
                form.dataGridView1Elem.Columns.Add(coefficient);
                DataGridViewTextBoxColumn coefficient2;
                coefficient2 = new DataGridViewTextBoxColumn();
                coefficient2.Name = "Значение";
                coefficient2.HeaderText = "Значение";
                coefficient2.Width = 100;
                form.dataGridView1Elem.Columns.Add(coefficient2);
                //
                form.dataGridView3Elem.Rows.Clear();
                form.dataGridView3Elem.Columns.Clear();
                form.dataGridView3Elem.RowHeadersVisible = false;
                //добавили колонки
                DataGridViewTextBoxColumn coefficient3;
                coefficient3 = new DataGridViewTextBoxColumn();
                coefficient3.Name = "Название метки";
                coefficient3.HeaderText = "Название метки";
                coefficient3.Width = 150;
                form.dataGridView3Elem.Columns.Add(coefficient3);
                DataGridViewTextBoxColumn coefficient4;
                coefficient4 = new DataGridViewTextBoxColumn();
                coefficient4.Name = "Количество переходов";
                coefficient4.HeaderText = "Количество переходов";
                coefficient4.Width = 100;
                form.dataGridView3Elem.Columns.Add(coefficient4);
                //
                form.dataGridView2Elem.Rows.Clear();
                form.dataGridView2Elem.Columns.Clear();
                form.dataGridView2Elem.RowHeadersVisible = false;
                //добавили колонки
                DataGridViewTextBoxColumn coefficient5;
                coefficient5 = new DataGridViewTextBoxColumn();
                coefficient5.Name = "Название очереди";
                coefficient5.HeaderText = "Название очереди";
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





                //Массивы для хранения информации
                List<Statistics> buf_of_stat_variables = new List<Statistics>();
                List<Statistics> buf_of_stat_queure = new List<Statistics>();
                List<Statistics> buf_of_stat_lable = new List<Statistics>();
                //Для отражения прогресса найду шаг обновления строки прогресса;
                int step = 100 / int.Parse(form.numericUpDown1Elem.Value.ToString());
                form.progressBar1Elem.Value = 0;
                for (int i = 0; i < form.numericUpDown1Elem.Value; i++)//моделирование в соответствии с количеством итераций
                {
                    control.setConfiguration(form.textBox1Elem.Text);
                    control.calculationStatistics();
                    modeling_objects = control.getResult();
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
                                    form.dataGridView1Elem.Rows.Add(buf_of_stat_variables.Count);
                                    form.dataGridView1Elem.Rows.RemoveAt(0);
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
                            form.dataGridView1Elem.Rows[m].Cells[0].Value = buf_of_stat_variables.ElementAt(m).name;
                            form.dataGridView1Elem.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_variables.ElementAt(m).value) / float.Parse(a);
                            form.dataGridView1Elem.Update();
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
                                form.dataGridView2Elem.Rows.Add(modeling_objects.queues.Count);
                                form.dataGridView2Elem.Rows.RemoveAt(0);
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
                            form.dataGridView2Elem.Rows[m].Cells[0].Value = buf_of_stat_queure.ElementAt(m).name;
                            form.dataGridView2Elem.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value2) / float.Parse(a);
                            form.dataGridView2Elem.Rows[m].Cells[2].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value) / float.Parse(a);
                            form.dataGridView2Elem.Update();
                        }




                        for (int j = 0; j < modeling_objects.lables.Count; j++)//перебор всех меток
                        {
                            Statistics stat_lable = new Statistics();
                            stat_lable.name = modeling_objects.lables.ElementAt(j).get_name();
                            stat_lable.value = modeling_objects.lables.ElementAt(j).get_entries_number().ToString();
                            if (i == 0)//если моделирование происходит впервый раз, то массив с перменными еще пуст и необходимо добавить элементы
                            {
                                form.dataGridView3Elem.Rows.Add(modeling_objects.lables.Count);
                                form.dataGridView3Elem.Rows.RemoveAt(0);
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
                            form.dataGridView3Elem.Rows[m].Cells[0].Value = buf_of_stat_lable.ElementAt(m).name;
                            form.dataGridView3Elem.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_lable.ElementAt(m).value) / float.Parse(a);
                            form.dataGridView3Elem.Update();
                        }
                    }
                    else
                    {
                        form.label1Elem.Text = "Ошибка моделирования";
                        string message = modeling_objects.result;
                        string caption = "Ошибка";
                        DialogResult result;
                        result = MessageBox.Show(message, caption);
                        form.progressBar1Elem.Value = 100;
                        break;
                    }
                    form.progressBar1Elem.Value += step;
                }

                if (form.label1Elem.Text != "Ошибка моделирования")
                {
                    //вывод на экран результатов
                    //добавление строк
                    form.progressBar1Elem.Value = 100;
                    form.label1Elem.Text = "Моделирование завершено";
                    for (int m = 0; m < buf_of_stat_variables.Count; m++)
                    {
                        form.dataGridView1Elem.Rows[m].Cells[0].Value = buf_of_stat_variables.ElementAt(m).name;
                        form.dataGridView1Elem.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_variables.ElementAt(m).value) / int.Parse(form.numericUpDown1Elem.Value.ToString());
                    }
                    for (int m = 0; m < buf_of_stat_queure.Count; m++)
                    {
                        form.dataGridView2Elem.Rows[m].Cells[0].Value = buf_of_stat_queure.ElementAt(m).name;
                        form.dataGridView2Elem.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value2) / int.Parse(form.numericUpDown1Elem.Value.ToString());
                        form.dataGridView2Elem.Rows[m].Cells[2].Value = float.Parse(buf_of_stat_queure.ElementAt(m).value) / int.Parse(form.numericUpDown1Elem.Value.ToString());
                    }
                    for (int m = 0; m < buf_of_stat_lable.Count; m++)
                    {
                        form.dataGridView3Elem.Rows[m].Cells[0].Value = buf_of_stat_lable.ElementAt(m).name;
                        form.dataGridView3Elem.Rows[m].Cells[1].Value = float.Parse(buf_of_stat_lable.ElementAt(m).value) / int.Parse(form.numericUpDown1Elem.Value.ToString());
                    }
                }
            }
            else
            {
                form.label1Elem.Text = "Ошибка моделирования";
                string message = "Ошибка чтения файла команд";
                string caption = "Ошибка";
                DialogResult result;
                result = MessageBox.Show(message, caption);
                form.progressBar1Elem.Value = 100;
            }
        }

        public void notify()
        {
            
        }
    }
}
