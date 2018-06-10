using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class EnterOperation : BasicOperation
    {
        public EnterOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        private EnterOperation(string name, int numberOfOccupiedPoints, ModelingModel model) : 
            base(model)
        {
            parameters = new string[2];
            parameters[0] = name;
            parameters[1] = numberOfOccupiedPoints.ToString();
        }

        private EnterOperation(string name, ModelingModel model) : base(model)
        {
            parameters = new string[1];
            parameters[0] = name;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "ENTER")
            {
                string[] param = words[1].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                if(param.Count() == 2)
                {
                    return new EnterOperation(param[0], int.Parse(param[1]), model);
                }
                if (param.Count() == 1)
                {
                    return new EnterOperation(words[1], model);
                }
                /* if (param.Count() > 2)
                {
                    //ВЫЗОВ ИСКЛЮЧЕНИЯ-НЕВЕРНЫЙ ФОРМАТ
                }*/
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "ENTER")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                string[] param = words[2].Split(new char[] { ',' }, StringSplitOptions.
                    RemoveEmptyEntries);
                if (param.Count() == 2)
                {
                    return new EnterOperation(param[0], int.Parse(param[1]), model);
                }
                if (param.Count() == 1)
                {
                    return new EnterOperation(words[2], model);
                }
                /* if (param.Count() > 2)
                {
                    //ВЫЗОВ ИСКЛЮЧЕНИЯ-НЕВЕРНЫЙ ФОРМАТ
                }*/
            }

            return null;
        }

        public override void processing()
        {
            //поиск устройства по имени
            for (int n = 0; n < model.getState().storages.Count; n++)
            {
                if (model.getState().storages.ElementAt(n).Get_name() == parameters[0])
                {
                    //проверка на занятость
                    //если свободно
                    if (parameters.Count() == 2 && model.getState().storages.ElementAt(n).
                        checkEmptyPlaces(int.Parse(parameters[1])) == true)
                    {
                        model.getState().storages.ElementAt(n).enterStorage(int.
                            Parse(parameters[1]));
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).my_place++;//передвинул по программе дальше
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).sparePlace = 0;
                        return;
                    }
                    if (parameters.Count() == 1 && model.getState().storages.ElementAt(n).
                       checkEmptyPlaces(1) == true)
                    {
                        model.getState().storages.ElementAt(n).enterStorage(1);
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).my_place++;//передвинул по программе дальше
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).sparePlace = 0;
                        return;
                    }
                    //проверка, возможно, использовался режим both у операции Transfer
                    //и у транзакта есть запасное место перехода, для случая занятости основного
                    //(основной-в данном случае операция enter)
                    if(model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).sparePlace != 0)
                    {
                        //То переместил транзакт на запасное место
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).my_place = 
                            model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).sparePlace;
                        //Обнулил запасное место
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).sparePlace = 0;
                        return;
                    }
                    //иначе заносим в очередь, внутри устройства и блокируем перемещение
                    model.getState().storages.ElementAt(n).id_tranzaktions_in_device_queue.Add(
                        model.getState().tranzakts.ElementAt(model.getState().
                        idProcessingTranzact).get_my_id());
                    model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                        blocked = true;
                    break;
                }
            }
        }

        public override Operation clone()
        {
            if (parameters.Count() == 1)
            {
                return new EnterOperation(parameters[0], model);
            }
            if (parameters.Count() == 2)
            {
                return new EnterOperation(parameters[0], int.Parse(parameters[1]), model);
            }
            //ВЫЗОВ ИСКЛЮЧЕНИЯ-НЕВЕРНЫЙ ФОРМАТ
            throw new Exception();
        }
    }
}
