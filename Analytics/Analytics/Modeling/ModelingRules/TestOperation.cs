using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class TestOperation : BasicOperation
    {
        public TestOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public TestOperation(string type, string a, string b, string point, 
            ModelingModel model) : base(model)
        {
            parameters = new string[4];
            parameters[0] = type;
            parameters[1] = a;
            parameters[2] = b;
            parameters[3] = point;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "TEST")
            {
                string[] param = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                return new TestOperation(words[1], param[0], param[1], param[2], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "TEST")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                string[] param = words[3].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                return new TestOperation(words[2], param[0], param[1], param[2], model);
            }

            return null;
        }

        public override Operation clone()
        {
            return new TestOperation(parameters[0], parameters[1], parameters[2], parameters[3], 
                model);
        }

        public override void processing()
        {
            //Значение операнда А должно быть не равно значению операнда В, иначе переход по метке
            if (parameters[0] == "NE")
            {
                //поддерживаются в качестве операторов только типа int и названия параметра с 
                //приставкой "p"
                string A = "";
                string B = "";
                if (parameters[1].ElementAt(0) == 'P')//если операнд А-это параметр транзакта
                {
                    //удаляю флаг параметра
                    string parameterWithOutFlag = parameters[1].Remove(0, 1);
                    A = model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact)
                        .get_parameter(parameterWithOutFlag);
                }
                //так как сравнивается на равенство, то не имеет значение как сравнивать числа-как 
                //строку, или как число
                else
                {
                    A = parameters[1];
                }
                if (parameters[2].ElementAt(0) == 'P')//если операнд B-это параметр транзакта
                {
                    //удаляю флаг параметра
                    string parameterWithOutFlag = parameters[2].Remove(0, 1);
                    B = model.getState().tranzakts.ElementAt(model.getState().
                        idProcessingTranzact).get_parameter(parameterWithOutFlag);
                }
                //так как сравнивается на равенство, то не имеет значение как сравнивать числа-как 
                //строку, или как число
                else
                {
                    B = parameters[2];
                }
                if (A != B)
                {
                    //если не равно, то просто двигаем транзакт дальше
                    model.getState().tranzakts.ElementAt(model.getState().
                        idProcessingTranzact).my_place++;
                }
                else
                {
                    //иначе ищем нужную метку
                    for (int n = 0; n < model.getState().lables.Count; n++)
                    {
                        if (model.getState().lables.ElementAt(n).get_name() == parameters[3])
                        {
                            model.getState().tranzakts.ElementAt(model.getState().
                                idProcessingTranzact).my_place = 
                                model.getState().lables.ElementAt(n).get_my_plase();
                            break;
                        }
                    }
                }
            }


            //Значение операнда А должно быть меньше значения операнда В, иначе переход по метке
            if (parameters[0] == "L")
            {
                //поддерживаются в качестве операторов только типа int и названия параметра с 
                //приставкой "p"
                float A = 0;
                float B = 0;
                if (parameters[1].ElementAt(0) == 'P')//если операнд А-это параметр транзакта
                {
                    //удаляю флаг параметра
                    string parameterWithOutFlag = parameters[1].Remove(0, 1);
                    A = float.Parse(model.getState().tranzakts.ElementAt(model.getState().
                        idProcessingTranzact).get_parameter(parameterWithOutFlag));
                }
                //так как сравнивается на равенство, то не имеет значение как сравнивать 
                //числа -как строку, или как число
                else
                {
                    A = float.Parse(parameters[1]);
                }
                if (parameters[2].ElementAt(0) == 'P')//если операнд B-это параметр транзакта
                {
                    //удаляю флаг параметра
                    string parameterWithOutFlag = parameters[2].Remove(0, 1);
                    B = float.Parse(model.getState().tranzakts.ElementAt(model.getState().
                        idProcessingTranzact).get_parameter(parameterWithOutFlag));
                }
                //так как сравнивается на равенство, то не имеет значение как сравнивать числа-как 
                //строку, или как число
                else
                {
                    B = float.Parse(parameters[2]);
                }
                if (A < B)
                {
                    //двигаем транзакт дальше
                    model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                        my_place++;
                }
                else
                {
                    //иначе ищем нужную метку
                    for (int n = 0; n < model.getState().lables.Count; n++)
                    {
                        if (model.getState().lables.ElementAt(n).get_name() == parameters[3])
                        {
                            model.getState().tranzakts.ElementAt(model.getState().
                                idProcessingTranzact).
                                my_place = model.getState().lables.ElementAt(n).get_my_plase();
                            break;
                        }
                    }
                }
            }
            //в этой версии у этого оператора есть только модификаторы ne,l, при необходжимости 
            //можно добавить еще if-ы после этого
        }
    }
}