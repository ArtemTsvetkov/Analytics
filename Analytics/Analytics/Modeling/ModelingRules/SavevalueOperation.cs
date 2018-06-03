using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie.Modeling.ModelingRules
{
    class SavevalueOperation : BasicOperation
    {
        public SavevalueOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public SavevalueOperation(string name, string value, ModelingModel model) : base(model)
        {
            parameters = new string[2];
            parameters[0] = name;
            parameters[1] = value;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "SAVEVALUE")
            {
                //иначе ищем нужную переменную
                string[] param = words[1].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                return new SavevalueOperation(param[0], param[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "SAVEVALUE")
            {
                Lable lable = new Lable(model.state.newRules.Count, words[0]);//создание метки
                model.state.lables.Add(lable);
                //иначе ищем нужную переменную
                string[] param = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                return new SavevalueOperation(param[0], param[1], model);
            }

            return null;
        }

        public override void processing()
        {
            for (int n = 0; n < model.state.variables.Count; n++)
            {
                if (model.state.variables.ElementAt(n).get_name() == parameters[0])
                {
                    //определение переменной, СЧА длина должна быть больше двух(V$Var-пример)
                    if (parameters[1].Count() > 2)
                    {
                        if (parameters[1].Remove(2) == "V$")
                        {
                            //поиск переменной по ее имени
                            string parametersName = parameters[1].Remove(0, 2);
                            for (int k = 0; k < model.state.variables.Count(); k++)
                            {
                                if (model.state.variables.ElementAt(k).get_name() == parametersName)
                                {
                                    //расчет значения
                                    string value = ModelingFunctionParser.go_parse(model.state, 
                                        model.state.variables.ElementAt(k).get_function(), model.
                                        state.idProcessingTranzact);
                                    if (value != "syntaxis_error")
                                    {
                                        model.state.variables.ElementAt(n).value = value;
                                    }
                                    else//иначе синтаксическая ошибка
                                    {
                                        int id_str = model.state.tranzakts.ElementAt(model.state.
                                            idProcessingTranzact).my_place - 1;
                                        model.state.result = "syntaxis_error " + id_str.ToString() + 
                                            "-ая строка, некорректная функция переменной: " + 
                                            parametersName;
                                        return;
                                    }
                                    break;
                                }
                                if (k == model.state.variables.Count() - 1)//переменная не найдена
                                {
                                    int id_str = model.state.tranzakts.ElementAt(model.state.
                                        idProcessingTranzact).my_place - 1;
                                    model.state.result = "syntaxis_error " + id_str.ToString() + 
                                        "-ая строка, переменная: " + parametersName + "не найдена";
                                    return;
                                }
                            }
                            continue;
                        }
                    }
                    break;
                }
            }
            //передвигаем далее транзакт
            model.state.tranzakts.ElementAt(model.state.idProcessingTranzact).my_place++;
            return;
        }
    }
}

