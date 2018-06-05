using Analytics.Modeling.ModelingExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class TransferOperation : BasicOperation
    {
        public TransferOperation(ModelingModel model) : base(model)//Пустой для функции check
        {

        }

        public TransferOperation(string a, string b, string c, string d, ModelingModel model) 
            : base(model)
        {
            parameters = new string[4];
            parameters[0] = a;
            parameters[1] = b;
            parameters[2] = c;
            parameters[3] = d;
        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "TRANSFER")
            {
                //только обработчик режима безуслвоной передачи, при необходимости дополнить
                string[] param = words[1].Split(new char[] { ',' });
                string A = "";
                string B = "";
                string C = "";
                string D = "";

                switch (param.Length)
                {
                    case 1:
                        A = param[0];
                        break;
                    case 2:
                        A = param[0];
                        B = param[1];
                        break;
                    case 3:
                        A = param[0];
                        B = param[1];
                        C = param[2];
                        break;
                    case 4:
                        A = param[0];
                        B = param[1];
                        C = param[2];
                        D = param[3];
                        break;
                    default:
                        throw new IncorrectFormatOperation();
                }

                return new TransferOperation(A, B, C, D, model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "TRANSFER")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                //только обработчик режима безуслвоной передачи, при необходимости дополнить
                string[] param = words[2].Split(new char[] { ',' });
                string A = "";
                string B = "";
                string C = "";
                string D = "";

                switch (param.Length)
                {
                    case 1:
                        A = param[0];
                        break;
                    case 2:
                        A = param[0];
                        B = param[1];
                        break;
                    case 3:
                        A = param[0];
                        B = param[1];
                        C = param[2];
                        break;
                    case 4:
                        A = param[0];
                        B = param[1];
                        C = param[2];
                        D = param[3];
                        break;
                    default:
                        throw new IncorrectFormatOperation();
                }

                return new TransferOperation(A, B, C, D, model);
            }

            return null;
        }

        public override Operation clone()
        {
            return new TransferOperation(parameters[0], parameters[1], parameters[2], 
                parameters[3], model);
        }

        public override void processing()
        {
            //только обработчик режима безуслвоной передачи, при необходимости дополнить
            //проверка на режим безусловной передачи
            if (parameters[0] == "" & parameters[1] != "" & parameters[2] == "" 
                & parameters[3] == "")
            {
                //переход транзакта по метке
                //ищем нужную метку
                for (int n = 0; n < model.getState().lables.Count; n++)
                {
                    if (model.getState().lables.ElementAt(n).get_name() == parameters[1])
                    {
                        model.getState().tranzakts.ElementAt(model.getState().
                            idProcessingTranzact).my_place = 
                            model.getState().lables.ElementAt(n).get_my_plase();
                        break;
                    }
                }
            }
        }
    }
}