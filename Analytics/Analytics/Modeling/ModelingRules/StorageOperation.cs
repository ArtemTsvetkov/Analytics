using Analytics.Modeling.ModelingInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    class StorageOperation : BasicOperation
    {
        public StorageOperation(ModelingModel model) : base(model)
        {

        }

        public override Operation check(string rule)
        {
            string[] words = rule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1 && words[1] == "STORAGE")
            {
                Storage storage = new Storage(words[0], int.Parse(words[2]));
                model.getState().storages.Add(storage);
                return new StorageOperation(model);
            }
            //Для случая наличия метки
            if (words.Length > 2 && words[2] == "STORAGE")
            {
                Lable lable = new Lable(model.getState().newRules.Count, words[0]);//создание метки
                model.getState().lables.Add(lable);
                Storage storage = new Storage(words[1], int.Parse(words[3]));
                model.getState().storages.Add(storage);
                return new StorageOperation(model);
            }

            return null;
        }

        public override void processing()
        {
            //передвинул по программе дальше
            model.getState().tranzakts.ElementAt(model.getState().idProcessingTranzact).
                my_place += 1;
        }
    }
}
