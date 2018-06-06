using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class GenerateOperation : BasicOperation
    {
        public GenerateOperation(ModelingModel model) : base(model)
        {

        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //определение переменной как ячейки таблицы
            if (words.Length > 0 && words[0] == "GENERATE")
            {
                //здесь не все параметры считываются, например, время начальной задержки вообще 
                //пропущено, так как для курсовой это не требуется, если понадобится, изменить 
                //этот if
                string[] parameters = words[1].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                //далее третьим параметром указывается время до следующей транзакции, в данном 
                //случае оно равно нулю, по причине, описанной выше
                //но далее в программе оно используется, при модернизации вместо нуля нужно будет 
                //просто указать считанный параметр, которого сейчас нет
                TranzactionsGenerator tranzactions_generator = new TranzactionsGenerator((
                    int.Parse(parameters[0]) - int.Parse(parameters[1])), (int.
                    Parse(parameters[0]) + int.Parse(parameters[1])), 0, model.getState().
                    newRules.Count, int.Parse(parameters[2]));
                model.getState().tranzation_generators.Add(tranzactions_generator);
                return new GenerateOperation(model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "GENERATE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                //здесь не все параметры считываются, например, время начальной задержки вообще 
                //пропущено, так как для курсовой это не требуется, если понадобится, изменить 
                //этот if
                string[] parameters = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                //далее третьим параметром указывается время до следующей транзакции, в данном 
                //случае оно равно нулю, по причине, описанной выше но далее в программе оно 
                //используется, при модернизации вместо нуля нужно будет просто указать 
                //считанный параметр, которого сейчас нет
                TranzactionsGenerator tranzactions_generator = new TranzactionsGenerator((int.
                    Parse(parameters[0]) - int.Parse(parameters[1])), (int.Parse(parameters[0]) + 
                    int.Parse(parameters[1])), 0, model.getState().newRules.Count, int.
                    Parse(parameters[2]));
                model.getState().tranzation_generators.Add(tranzactions_generator);
                return new GenerateOperation(model);
            }

            return null;
        }

        public override Operation clone()
        {
            return new GenerateOperation(model);
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
 * Но есть строки модели, которые просто создают необходимые переменные и все, их обрабатывать 
 * нет смысла
 * Класс создан просто как некоторый заполнитель в массиве правил
 */
