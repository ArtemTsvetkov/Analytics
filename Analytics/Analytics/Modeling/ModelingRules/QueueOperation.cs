using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class QueueOperation : BasicOperation
    {
        public QueueOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public QueueOperation(string name, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "QUEUE")
            {
                Queue queue = new Queue(words[1]);
                model.getState().queues.Add(queue);
                return new QueueOperation(words[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "QUEUE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                Queue queue = new Queue(words[2]);
                model.getState().queues.Add(queue);
                return new QueueOperation(words[2], model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.getState().tranzakts.ElementAt(model.getState().
                idProcessingTranzact).my_place += 1;
            //поиск очереди по имени
            for (int n = 0; n < model.getState().queues.Count; n++)
            {
                if (model.getState().queues.ElementAt(n).get_name() == parameters[0])
                {
                    //после нахождения инкрементирую кол-во тразактов в очереди, очередь-абстракция, 
                    //которая лишь показывает в конкретный момент кол-во тразактов на определенном 
                    //участке реальная очередь находится в устройстве, где существует очередность 
                    //вхождения и куда можно добавить очередность исходя из приоритетов
                    model.getState().queues.ElementAt(n).count_of_tranzactions_in_queue++;
                    model.getState().queues.ElementAt(n).update_tranzacts_in_queue();
                    break;
                }
            }
        }
    }
}
