using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie.Modeling.ModelingRules
{
    class LeaveOperation : BasicOperation
    {
        public LeaveOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        private LeaveOperation(string name, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
        }

        private LeaveOperation(string name, int numberOfReleasedPoints, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
            parameters[1] = numberOfReleasedPoints.ToString();
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "LEAVE")
            {
                string[] param = words[1].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                if (param.Count() == 2)
                {
                    return new LeaveOperation(param[0], int.Parse(param[1]), model);
                }
                if (param.Count() == 1)
                {
                    return new LeaveOperation(words[1], model);
                }
                /* if (param.Count() > 2)
                {
                    //ВЫЗОВ ИСКЛЮЧЕНИЯ-НЕВЕРНЫЙ ФОРМАТ
                }*/
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "LEAVE")
            {
                Lable lable = new Lable(model.state.newRules.Count, words[0]);//создание метки
                model.state.lables.Add(lable);
                string[] param = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                if (param.Count() == 2)
                {
                    return new LeaveOperation(param[0], int.Parse(param[1]), model);
                }
                if (param.Count() == 1)
                {
                    return new LeaveOperation(words[2], model);
                }
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.state.tranzakts.ElementAt(model.state.idProcessingTranzact).my_place++;
            //поиск устройства по имени
            for (int n = 0; n < model.state.storages.Count; n++)
            {
                if (model.state.storages.ElementAt(n).Get_name() == parameters[0])
                {
                    int releasedPoints = 1;
                    //Считывание, сколько мест освобождается
                    if (parameters.Count() == 2)
                    {
                        releasedPoints = int.Parse(parameters[1]);
                    }
                    //Пошагово освобождаем места
                    for (int i = 0; i < releasedPoints; i++)
                    {
                        //проверка очереди
                        //если в очереди есть транзакты
                        if (model.state.storages.ElementAt(n).id_tranzaktions_in_device_queue.Count > 0)
                        {//то первый в очереди занимает устройство и разблокируется для движения далее
                         //поиск индекса транзакта по его id
                            int tranzakts_index = -1;
                            for (int a = 0; a < model.state.tranzakts.Count(); a++)
                            {
                                if (model.state.tranzakts.ElementAt(a).get_my_id() == model.state.
                                    storages.ElementAt(n).id_tranzaktions_in_device_queue.ElementAt(0))
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
                            model.state.storages.ElementAt(n).id_tranzaktions_in_device_queue.RemoveAt(0);
                        }
                        else
                        {
                            //то разблокируем устройство, текущий транзакт его покинул, а очередь пуста
                            model.state.storages.ElementAt(n).leaveStorage(1);
                            break;
                        }
                    }
                    break;
                }
            }
            return;
        }
    }
}