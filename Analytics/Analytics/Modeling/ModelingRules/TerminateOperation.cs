using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class TerminateOperation : BasicOperation
    {
        public TerminateOperation(ModelingModel model) : base(model)
        {

        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0 && words[0] == "TERMINATE")
            {
                return new TerminateOperation(model);
            }
            //Для случая наличия метки
            if (words.Length > 1 && words[1] == "TERMINATE")//Для случая существования метки
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                return new TerminateOperation(model);
            }

            return null;
        }

        public override void processing()
        {
            //удаление транзакта
            model.getState().tranzakts.RemoveAt(model.getState().idProcessingTranzact);
        }
    }
}