using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie.Modeling.ModelingRules
{
    class InitialOperation : BasicOperation
    {
        public InitialOperation(ModelingModel model) : base(model)
        {

        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //определение переменной как ячейки таблицы
            if (words.Length > 0 && words[0] == "INITIAL")
            {
                string[] parameters = words[1].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                Variable variable = new Variable(parameters[0].Remove(0, 2), "", parameters[1]);
                model.state.variables.Add(variable);
                return new InitialOperation(model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "INITIAL")
            {
                Lable lable = new Lable(model.state.newRules.Count, words[0]);//создание метки
                model.state.lables.Add(lable);
                string[] parameters = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                Variable variable = new Variable(parameters[0], "", parameters[1]);
                model.state.variables.Add(variable);
                return new InitialOperation(model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.state.tranzakts.ElementAt(model.state.idProcessingTranzact).my_place += 1;
        }
    }
}
/*
 * При моделировании последовательность и количество строк исходной модели не меняется
 * Но есть строки модели, которые просто создают необходимые переменные и все, их обрабатывать 
 * нет смысла
 * Класс создан просто как некоторый заполнитель в массиве правил
 */
