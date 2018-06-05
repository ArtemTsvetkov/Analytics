using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class AdvanceOperation : BasicOperation
    {
        public AdvanceOperation(ModelingModel model):base (model)//Пустой для функции check
        {
            
        }

        public AdvanceOperation(string a, string b, ModelingModel state) : base(state)
        {
            parameters = new string[2];
            parameters[0] = a;
            parameters[1] = b;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "ADVANCE")
            {
                //считывание параметров
                string[] param = words[1].Split(new char[] { ',' }, 
                    StringSplitOptions.RemoveEmptyEntries);
                return new AdvanceOperation(param[0], param[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "ADVANCE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                //считывание параметров
                string[] param = words[2].Split(new char[] { ',' }, 
                    StringSplitOptions.RemoveEmptyEntries);
                return new AdvanceOperation(param[0], param[1], model);
            }

            return null;
        }

        public override void processing()
        {
            //запись задержки в транзакт
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                blocked = true;
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                remaining_time_delay = create_assign(int.Parse(parameters[0]) - 
                int.Parse(parameters[1]), int.Parse(parameters[0]) + int.Parse(parameters[1]), 
                model.getState().rand);
            //проверка, когда задержка оказалась нулевой
            if (model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                remaining_time_delay == 0)
            {
                model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                    blocked = false;
                //передвинул по программе дальше
                model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                    my_place += 1;
            }
        }

        //функция создания времени задержки, принимает разброс возможных значений
        private int create_assign(int min_number, int max_number, Random rand)
        {
            int a = rand.Next(min_number, max_number);
            return rand.Next(min_number, max_number);
        }
    }
}
