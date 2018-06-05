using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class SeizeOperation : BasicOperation
    {
        public SeizeOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public SeizeOperation(string name, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "SEIZE")
            {
                Device device = new Device(words[1]);
                model.getState().devices.Add(device);
                return new SeizeOperation(words[1], model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "SEIZE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                Device device = new Device(words[2]);
                model.getState().devices.Add(device);
                return new SeizeOperation(words[2], model);
            }

            return null;
        }

        public override void processing()
        {
            //поиск устройства по имени
            for (int n = 0; n < model.getState().devices.Count; n++)
            {
                if (model.getState().devices.ElementAt(n).Get_name() == parameters[0])
                {
                    //проверка на занятость
                    if (model.getState().devices.ElementAt(n).device_empty == true)//если свободно
                    {
                        model.getState().devices.ElementAt(n).device_empty = false;
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).my_place++;//передвинул по программе дальше
                    }
                    else
                    {//иначе заносим в очередь, внутри устройства и блокируем перемещение
                        model.getState().devices.ElementAt(n).id_tranzaktions_in_device_queue.Add(
                            model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).get_my_id());
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).blocked = true;
                    }
                    break;
                }
            }
        }
    }
}