using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class ReleaseOperation : BasicOperation
    {
        public ReleaseOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public ReleaseOperation(string name, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "RELEASE")
            {
                return new ReleaseOperation(words[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "RELEASE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                return new ReleaseOperation(words[2], model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).my_place++;
            //поиск устройства по имени
            for (int n = 0; n < model.getState().devices.Count; n++)
            {
                if (model.getState().devices.ElementAt(n).Get_name() == parameters[0])
                {//проверка очереди
                    //если в очереди есть транзакты
                    if (model.getState().devices.ElementAt(n).id_tranzaktions_in_device_queue.
                        Count > 0)
                    {//то первый в очереди занимает устройство и разблокируется для движения далее
                     //поиск индекса транзакта по его id
                        int tranzakts_index = -1;
                        for (int a = 0; a < model.getState().tranzakts.Count(); a++)
                        {
                            if (model.getState().tranzakts.ElementAt(a).get_my_id() == 
                                model.getState().devices.ElementAt(n).
                                id_tranzaktions_in_device_queue.ElementAt(0))
                            {
                                tranzakts_index = a;
                                break;
                            }
                        }

                        //разблокировал
                        model.getState().tranzakts.ElementAt(tranzakts_index).blocked = false;
                        //передвинул по программе дальше
                        model.getState().tranzakts.ElementAt(tranzakts_index).my_place++;
                        //удалил из очереди, устройство не разблокируется, потому что при его 
                        //освобождении одним транзактом оно тут же занимается первым транзактом 
                        //из очереди
                        model.getState().devices.ElementAt(n).id_tranzaktions_in_device_queue.
                            RemoveAt(0);
                    }
                    else
                    {
                        //то разблокируем устройство, текущий транзакт его покинул, а очередь пуста
                        model.getState().devices.ElementAt(n).device_empty = true;
                    }
                    break;
                }
            }
            return;
        }
    }
}