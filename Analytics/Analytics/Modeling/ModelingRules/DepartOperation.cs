using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie.Modeling.ModelingRules
{
    class DepartOperation : BasicOperation
    {
        public DepartOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public DepartOperation(string name, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "DEPART")
            {
                return new DepartOperation(words[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "DEPART")
            {
                Lable lable = new Lable(model.state.newRules.Count, words[0]);//создание метки
                model.state.lables.Add(lable);
                return new DepartOperation(words[2], model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.state.tranzakts.ElementAt(model.state.idProcessingTranzact).my_place += 1;
            //поиск очереди по имени
            for (int n = 0; n < model.state.queues.Count; n++)
            {
                if (model.state.queues.ElementAt(n).get_name() == parameters[0])
                {
                    //декремент кол-ва заявок в очереди
                    model.state.queues.ElementAt(n).count_of_tranzactions_in_queue--;
                    break;
                }
            }
        }
    }
}