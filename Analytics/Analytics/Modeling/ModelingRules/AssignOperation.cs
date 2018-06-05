using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class AssignOperation : BasicOperation
    {
        public AssignOperation(string parametersName, string parametersValue, 
            ModelingModel model) : base(model)
        {
            parameters = new string[2];
            parameters[0] = parametersName;
            parameters[1] = parametersValue;
        }

        public AssignOperation(ModelingModel model) : base(model)//Пустой для функции check
        {
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "ASSIGN")
            {
                string[] param = words[1].Split(new char[] { ',' }, 
                    StringSplitOptions.RemoveEmptyEntries);
                parameters = new string[2];
                parameters[0] = param[0];
                parameters[1] = param[1];
                return new AssignOperation(param[0], param[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "ASSIGN")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                string[] param = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                parameters = new string[2];
                parameters[0] = param[0];
                parameters[1] = param[1];
                return new AssignOperation(param[0], param[1], model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                my_place += 1;                                                           
            if (parameters[1] == "AC1")//время нахождения транзакта в системе, СЧА
            {
                model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                    set_parameter(parameters[0], model.getState().tranzakts.ElementAt(model.
                    getState().idProcessingTranzact).time_in_system.ToString());//записал параметр
                return;
            }
            //определение переменной, СЧА длина должна быть больше двух(V$Var-пример)
            if (parameters[1].Count() > 2)
            {   //Если длинна меньше двух, то нет смысла проверять на СЧА
                if (parameters[1].Remove(2) == "V$")
                {
                    //поиск переменной по ее имени
                    string name = parameters[1].Remove(0, 2);
                    for (int k = 0; k < model.getState().variables.Count(); k++)
                    {
                        if (model.getState().variables.ElementAt(k).get_name() == name)
                        {
                            //расчет значения
                            string value = ModelingFunctionParser.go_parse(model.getState(), 
                                model.getState().variables.ElementAt(k).get_function(), 
                                model.getState().idProcessingTranzact);
                            if (value != "syntaxis_error")
                            {
                                model.getState().tranzakts.ElementAt(model.getState().
                                    idProcessingTranzact).set_parameter(parameters[0], value);
                            }
                            else//иначе синтаксическая ошибка
                            {
                                int id_str = model.getState().tranzakts.ElementAt(model.getState().
                                    idProcessingTranzact).my_place - 1;
                                model.getState().result = "syntaxis_error " + id_str.ToString() + 
                                    "-ая строка, некорректная функция переменной: " + name;
                                return;
                            }
                            break;
                        }
                        if (k == model.getState().variables.Count() - 1)//переменная не найдена
                        {
                            int id_str = model.getState().tranzakts.ElementAt(model.getState().
                                idProcessingTranzact).my_place - 1;
                            model.getState().result = "syntaxis_error " + id_str.ToString() + 
                                "-ая строка, переменная: " + name + "не найдена";
                            return;
                        }
                    }
                    return;
                }
            }
            //иначе это обычный параметр, алгоритм сюда дайдет, если он не увидит какие-либо СЧА
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                set_parameter(parameters[0], parameters[1]);//записал параметр
        }

        public override Operation clone()
        {
            return new AssignOperation(parameters[0], parameters[1], model);
        }
    }
}
