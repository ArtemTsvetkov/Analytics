using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie.Modeling.ModelingRules
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
                Lable lable = new Lable(model.state.newRules.Count, words[0]);//создание метки
                model.state.lables.Add(lable);
                return new ReleaseOperation(words[2], model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.state.tranzakts.ElementAt(model.state.idProcessingTranzact).my_place++;
            //поиск устройства по имени
            for (int n = 0; n < model.state.devices.Count; n++)
            {
                if (model.state.devices.ElementAt(n).Get_name() == parameters[0])
                {//проверка очереди
                    //если в очереди есть транзакты
                    if (model.state.devices.ElementAt(n).id_tranzaktions_in_device_queue.Count > 0)
                    {//то первый в очереди занимает устройство и разблокируется для движения далее
                     //поиск индекса транзакта по его id
                        int tranzakts_index = -1;
                        for (int a = 0; a < model.state.tranzakts.Count(); a++)
                        {
                            if (model.state.tranzakts.ElementAt(a).get_my_id() == model.state.
                                devices.ElementAt(n).id_tranzaktions_in_device_queue.ElementAt(0))
                            {
                                tranzakts_index = a;
                                break;
                            }
                        }

                        //разблокировал
                        model.state.tranzakts.ElementAt(tranzakts_index).blocked = false;
                        //передвинул по программе дальше
                        model.state.tranzakts.ElementAt(tranzakts_index).my_place++;
                        //удалил из очереди, устройство не разблокируется, потому что при его 
                        //освобождении одним транзактом оно тут же занимается первым транзактом 
                        //из очереди
                        model.state.devices.ElementAt(n).id_tranzaktions_in_device_queue.RemoveAt(0);
                    }
                    else
                    {
                        //то разблокируем устройство, текущий транзакт его покинул, а очередь пуста
                        model.state.devices.ElementAt(n).device_empty = true;
                    }
                    break;
                }
            }
            return;
        }
    }
}