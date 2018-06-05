using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class VariableOperation : BasicOperation
    {
        public VariableOperation(ModelingModel model) : base(model)
        {

        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1 && words[1] == "VARIABLE")
            {
                Variable variable = new Variable(words[0], words[2], "");
                model.getState().variables.Add(variable);
                return new VariableOperation(model);
            }
            //Для случая наличия метки
            if (words.Length > 2 && words[2] == "VARIABLE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                Variable variable = new Variable(words[1], words[3], "");
                model.getState().variables.Add(variable);
                return new VariableOperation(model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                my_place += 1;
        }
    }
}
/*
 * При моделировании последовательность и количество строк исходной модели не меняется
 * Но есть строки модели, которые просто создают необходимые переменные и все, их обрабатывать нет 
 * смысла
 * Класс создан просто как некоторый заполнитель в массиве правил
 */
