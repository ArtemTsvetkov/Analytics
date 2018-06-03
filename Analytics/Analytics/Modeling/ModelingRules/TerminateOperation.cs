using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie.Modeling.ModelingRules
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
                Lable lable = new Lable(model.state.newRules.Count, words[0]);//создание метки
                model.state.lables.Add(lable);
                return new TerminateOperation(model);
            }

            return null;
        }

        public override void processing()
        {
            //удаление транзакта
            model.state.tranzakts.RemoveAt(model.state.idProcessingTranzact);
        }
    }
}