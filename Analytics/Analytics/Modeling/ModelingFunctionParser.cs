using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie
{
    class ModelingFunctionParser
    {
        //функция парсинга математических функций, на вход подается сама функция и класс всех 
        //объектов, так как функция может содержать СЧА
        public static string go_parse(ModelingState modeling_objects, string function, 
            int id_tranzakts)
        {
            bool function_was_counting = false;
            while (function_was_counting == false)//пока есть что считать
            {
                //поиск самых вложенных скобок
                int index_of_left_parenthesis = -1;
                int index_of_right_parenthesis = -1;
                for (int i = 0; i < function.Count(); i++)
                {
                    //если встретилась открывающая скобка-запись ее индекса
                    if (function.ElementAt(i) == '(')
                    {
                        index_of_left_parenthesis = i;
                        index_of_left_parenthesis++;//чтобы потом не захватить скобку
                        continue;
                    }
                    //если встретилась закрывающая скобка-запись ее индекса, это
                    //гарантирует, что внутри нее больше не будет скобок
                    if (function.ElementAt(i) == ')')
                    {                                
                        index_of_right_parenthesis = i;
                        break;
                    }
                }
                //проверка на ошибку синтаксиса
                if ((index_of_right_parenthesis == -1 & index_of_left_parenthesis != -1) | 
                    (index_of_right_parenthesis != -1 & index_of_left_parenthesis == -1))
                {
                    return "syntaxis_error";
                }
                //проверка, возможно, больше нет(не было скобок)
                if (index_of_right_parenthesis == -1 & index_of_left_parenthesis == -1)
                {//отправляю всю функцию в расчет
                    function_was_counting = true;
                    function = counting_function(function, modeling_objects, id_tranzakts);
                    if (function == "syntaxis_error")
                    {
                        return function;
                    }
                }
                else//иначе отправляю только часть функции в расчет
                {
                    string counting_part_function = counting_function(function.Substring(
                        index_of_left_parenthesis, (index_of_right_parenthesis - 
                        index_of_left_parenthesis)), modeling_objects, id_tranzakts);
                    if (counting_part_function == "syntaxis_error")
                    {
                        return counting_part_function;
                    }
                    //удаление выражения в скобах и замена его на рассчитанное значение
                    function = function.Remove((index_of_left_parenthesis - 1), ((
                        index_of_right_parenthesis - index_of_left_parenthesis) + 2));
                    function = function.Insert((index_of_left_parenthesis - 1), 
                        counting_part_function);
                }
            }
            return function;
        }




        //функция рассчет части или целой функции в случае отсутствия скобок
        private static string counting_function(string part_or_full_function, 
            ModelingState modeling_objects, int id_tranzakts)
        {
            //проверка приоритетных знаков(умножение, деление, возведение в степень)
            //таких знаков может быть несколько
            bool priority_mark_not_search = false;
            while (priority_mark_not_search == false)
            {
                //если последним символом будет стоять какое-либо действие, то это значит 
                //отсутствие правого оператора
                if (part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '*' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '/' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '+' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '-' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '^')
                {
                    return "syntaxis_error";
                }

                for (int i = 0; i < part_or_full_function.Count(); i++)
                {
                    //запоминаю действие, чтобы потом обпять не обращаться по индексу
                    string action = part_or_full_function.ElementAt(i).ToString();

                    //если нашли приоритетное действие
                    if (action == "*" | action == "/" | action == "^")
                    {
                        //оно не стоит на первом месте(перед ним должно стоять что - либо)
                        if (i > 0)
                        {
                            //числа или СЧА
                            string a = "";
                            string b = "";
                            //индексы рассчитываемого выражения, когда рассчитаю выражение, на 
                            //место него в строку запишу ответ
                            int left = 0;
                            int right = 0;
                            //проверка на неправильный синтаксис, для случая, когда несколько 
                            //действий стоит рядом
                            //проверять слева нет смысла, поиск выражений идет слева на право и, 
                            //если два действия будут стоять радом, это обнаружится еще при 
                            //исследовании левого
                            if (part_or_full_function.ElementAt(i + 1) == '*' | 
                                part_or_full_function.ElementAt(i + 1) == '/' | 
                                part_or_full_function.ElementAt(i + 1) == '^' | 
                                part_or_full_function.ElementAt(i + 1) == '+' | 
                                part_or_full_function.ElementAt(i + 1) == '-')
                            {
                                return "syntaxis_error";
                            }
                            //записываем все числа или сча слева от приоритетного знака
                            for (int n = (i - 1); n >= 0; n--)
                            {
                                //если встретится хотя бы один ттакой символ, то это будет хначить, 
                                //что число или строка А заполенна(о)
                                if (part_or_full_function.ElementAt(n) == '*' | 
                                    part_or_full_function.ElementAt(n) == '/' | 
                                    part_or_full_function.ElementAt(n) == '+' | 
                                    part_or_full_function.ElementAt(n) == '-' | 
                                    part_or_full_function.ElementAt(n) == '^')
                                {
                                    break;
                                }
                                a = part_or_full_function.ElementAt(n) + a;
                                left = n;
                            }
                            //записываем все числа или сча справа от приоритетного знака
                            for (int n = (i + 1); n < part_or_full_function.Count(); n++)
                            {
                                //если встретится хотя бы один ттакой символ, то это будет хначить, 
                                //что число или строка А заполенна(о)
                                if (part_or_full_function.ElementAt(n) == '*' | 
                                    part_or_full_function.ElementAt(n) == '/' | 
                                    part_or_full_function.ElementAt(n) == '+' | 
                                    part_or_full_function.ElementAt(n) == '-' | 
                                    part_or_full_function.ElementAt(n) == '^')
                                {
                                    break;
                                }
                                b += part_or_full_function.ElementAt(n);
                                right = n;
                            }




                            //Удалить блок для возвращения к исходному модулю(добавлена 
                            //распознавание СЧА и др для моделирования)
                            if (a.ElementAt(0) == 'P')//это параметр транзакта
                            {
                                a = modeling_objects.tranzakts.ElementAt(id_tranzakts).
                                    get_parameter(a.Remove(0, 1));
                            }
                            if (b.ElementAt(0) == 'P')//это параметр транзакта
                            {
                                b = modeling_objects.tranzakts.ElementAt(id_tranzakts).
                                    get_parameter(b.Remove(0, 1));
                            }
                            //некоторые индикаторы не однозначные(как параметр транзкта, например) 
                            //а двузначеные(V$-пример)
                            if (a.Count() > 2)
                            {

                                if (a.Remove(2) == "N$")//это кол-во переходов по метке
                                {
                                    a = a.Remove(0, 2);
                                    if (a.Count() == 0)
                                    {
                                        //есть определение перменной, но у нее нет имени
                                        return "syntaxis_error";
                                    }
                                    //поиск метки по имени
                                    for (int g = 0; g < modeling_objects.lables.Count(); g++)
                                    {
                                        if (modeling_objects.lables.ElementAt(g).get_name() == a)
                                        {
                                            a = modeling_objects.lables.ElementAt(g).
                                                get_entries_number().ToString();
                                            break;
                                        }
                                        //не нашли метку
                                        if (g == modeling_objects.lables.Count() - 1)
                                        {
                                            return "syntaxis_error";
                                        }
                                    }
                                }
                            }
                            //некоторые индикаторы не однозначные(как параметр транзкта, например) 
                            //а двузначеные(V$-пример)
                            if (b.Count() > 2)
                            {
                                if (b.Remove(2) == "N$")//это кол-во переходов по метке
                                {
                                    b = b.Remove(0, 2);
                                    if (b.Count() == 0)
                                    {
                                        //есть определение перменной, но у нее нет имени
                                        return "syntaxis_error";
                                    }
                                    //поиск метки по имени
                                    for (int g = 0; g < modeling_objects.lables.Count(); g++)
                                    {
                                        if (modeling_objects.lables.ElementAt(g).get_name() == b)
                                        {
                                            b = modeling_objects.lables.ElementAt(g).
                                                get_entries_number().ToString();
                                            break;
                                        }
                                        //не нашли метку
                                        if (g == modeling_objects.lables.Count() - 1)
                                        {
                                            return "syntaxis_error";
                                        }
                                    }
                                }
                            }
                            //Конец удаляемого блока





                            //определяю что нужно сделать с а и b
                            if (action == "*")
                            {
                                float count = float.Parse(a) * float.Parse(b);
                                //удаляю рассчитанное выражение
                                part_or_full_function = part_or_full_function.Remove(left, (right - 
                                    left + 1));
                                //на его место запишу ответ
                                part_or_full_function = part_or_full_function.Insert(left, 
                                    count.ToString());
                            }
                            if (action == "/")
                            {
                                float count = float.Parse(a) / float.Parse(b);
                                //удаляю рассчитанное выражение
                                part_or_full_function = part_or_full_function.Remove(left, (right - 
                                    left + 1));
                                //на его место запишу ответ
                                part_or_full_function = part_or_full_function.Insert(left, count.
                                    ToString());
                            }
                            if (action == "^")
                            {
                                double count = Math.Pow(double.Parse(a), double.Parse(b));
                                //удаляю рассчитанное выражение
                                part_or_full_function = part_or_full_function.Remove(left, (right - 
                                    left + 1));
                                //на его место запишу ответ
                                part_or_full_function = part_or_full_function.Insert(left, count.
                                    ToString());
                            }
                            break;
                        }
                        else
                        {
                            return "syntaxis_error";
                        }
                    }


                    //если нашли приоритетное действие, то строка проверена и больше(вообще) 
                    //приоритетных знаков не обнаружено
                    if (i == part_or_full_function.Count() - 1)
                    {
                        priority_mark_not_search = true;
                        break;
                    }
                }
            }



            //проверка остальных знаков
            //таких знаков может быть несколько
            bool mark_not_search = false;
            while (mark_not_search == false)
            {
                //если последним символом будет стоять какое-либо действие, то это значит 
                //отсутствие правого оператора
                if (part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '*' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '/' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '+' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '-' | 
                    part_or_full_function.ElementAt(part_or_full_function.Count() - 1) == '^')
                {
                    return "syntaxis_error";
                }
                for (int i = 0; i < part_or_full_function.Count(); i++)
                {
                    //запоминаю действие, чтобы потом обпять не обращаться по индексу
                    string action = part_or_full_function.ElementAt(i).ToString();


                    if (action == "+" | action == "-")//если нашли действие
                    {
                        if (i > 0)//оно не стоит на первом месте(перед ним должно стоять что - либо)
                        {
                            //числа или СЧА
                            string a = "";
                            string b = "";
                            //индексы рассчитываемого выражения, когда рассчитаю выражение, на 
                            //место него в строку запишу ответ
                            int left = 0;
                            int right = 0;
                            //проверка на неправильный синтаксис, для случая, когда несколько 
                            //действий стоит рядом
                            //проверять слева нет смысла, поиск выражений идет слдева на право и, 
                            //если два действия будут стоять радом, это обнаружится еще при 
                            //исследовании левого
                            if (part_or_full_function.ElementAt(i + 1) == '*' | 
                                part_or_full_function.ElementAt(i + 1) == '/' | 
                                part_or_full_function.ElementAt(i + 1) == '^' | 
                                part_or_full_function.ElementAt(i + 1) == '+' | 
                                part_or_full_function.ElementAt(i + 1) == '-')
                            {
                                return "syntaxis_error";
                            }
                            //записываем все числа или сча слева от знака
                            for (int n = (i - 1); n >= 0; n--)
                            {
                                //если встретится хотя бы один ттакой символ, то это будет 
                                //хначить, что число или строка А заполенна(о)
                                if (part_or_full_function.ElementAt(n) == '+' | 
                                    part_or_full_function.ElementAt(n) == '-')
                                {
                                    break;
                                }
                                a = part_or_full_function.ElementAt(n) + a;
                                left = n;
                            }
                            //записываем все числа или сча справа от знака
                            for (int n = (i + 1); n < part_or_full_function.Count(); n++)
                            {
                                //если встретится хотя бы один ттакой символ, то это будет 
                                //хначить, что число или строка А заполенна(о)
                                if (part_or_full_function.ElementAt(n) == '+' | 
                                    part_or_full_function.ElementAt(n) == '-')
                                {
                                    break;
                                }
                                b += part_or_full_function.ElementAt(n);
                                right = n;
                            }




                            //Удалить блок для возвращения к исходному модулю(добавлена 
                            //распознавание СЧА и др для моделирования)
                            if (a.ElementAt(0) == 'P')//это параметр транзакта
                            {
                                a = modeling_objects.tranzakts.ElementAt(id_tranzakts).
                                    get_parameter(a.Remove(0, 1));
                            }
                            if (b.ElementAt(0) == 'P')//это параметр транзакта
                            {
                                b = modeling_objects.tranzakts.ElementAt(id_tranzakts).
                                    get_parameter(b.Remove(0, 1));
                            }
                            //некоторые индикаторы не однозначные(как параметр транзкта, например) 
                            //а двузначеные(V$-пример)
                            if (a.Count() > 2)
                            {
                                if (a.Remove(2) == "N$")//это кол-во переходов по метке
                                {
                                    a = a.Remove(0, 2);
                                    if (a.Count() == 0)
                                    {
                                        //есть определение перменной, но у нее нет имени
                                        return "syntaxis_error";
                                    }
                                    //поиск метки по имени
                                    for (int g = 0; g < modeling_objects.lables.Count(); g++)
                                    {
                                        if (modeling_objects.lables.ElementAt(g).get_name() == a)
                                        {
                                            a = modeling_objects.lables.ElementAt(g).
                                                get_entries_number().ToString();
                                            break;
                                        }
                                        //не нашли метку
                                        if (g == modeling_objects.lables.Count() - 1)
                                        {
                                            return "syntaxis_error";
                                        }
                                    }
                                }
                            }
                            //некоторые индикаторы не однозначные(как параметр транзкта, например) 
                            //а двузначеные(V$-пример)
                            if (b.Count() > 2)
                            {
                                if (b.Remove(2) == "N$")//это кол-во переходов по метке
                                {
                                    b = b.Remove(0, 2);
                                    if (b.Count() == 0)
                                    {
                                        //есть определение перменной, но у нее нет имени
                                        return "syntaxis_error";
                                    }
                                    //поиск метки по имени
                                    for (int g = 0; g < modeling_objects.lables.Count(); g++)
                                    {
                                        if (modeling_objects.lables.ElementAt(g).get_name() == b)
                                        {
                                            b = modeling_objects.lables.ElementAt(g).
                                                get_entries_number().ToString();
                                            break;
                                        }
                                        //не нашли метку
                                        if (g == modeling_objects.lables.Count() - 1)
                                        {
                                            return "syntaxis_error";
                                        }
                                    }
                                }
                            }
                            //Конец удаляемого блока




                            //определяю что нужно сделать с а и b
                            if (action == "+")
                            {
                                float count = float.Parse(a) + float.Parse(b);
                                //удаляю рассчитанное выражение
                                part_or_full_function = part_or_full_function.Remove(left, (right - 
                                    left + 1));
                                //на его место запишу ответ
                                part_or_full_function = part_or_full_function.Insert(left, count.
                                    ToString());
                            }
                            if (action == "-")
                            {
                                float count = float.Parse(a) - float.Parse(b);
                                //удаляю рассчитанное выражение
                                part_or_full_function = part_or_full_function.Remove(left, (right - 
                                    left + 1));
                                //на его место запишу ответ
                                part_or_full_function = part_or_full_function.Insert(left, count.
                                    ToString());
                            }
                            break;
                        }
                        else
                        {
                            return "syntaxis_error";
                        }
                    }

                    //если нашли приоритетное действие, то строка проверена и больше(вообще) 
                    //приоритетных знаков не обнаружено
                    if (i == part_or_full_function.Count() - 1)
                    {
                        mark_not_search = true;
                        break;
                    }
                }
            }
            return part_or_full_function;
        }
    }
}
/*
 * Модуль для парсинга функций(модифицированный для моделирования, для возвращения к стандарту 
 * удалить соответствующие строки)
 * Возвращает строку, на случай, если в дальнейшем потребуется работать не только с числами, но и 
 * строками(например, складывать их) 
 * Возможны ошибки, тестировал на повторяющиеся знаки(8*+2), одиночные скобки, знаки перед числом
 * (/8+2), не умеет распознавать неизвестные символы(просто пишет их так, как есть)
 */
