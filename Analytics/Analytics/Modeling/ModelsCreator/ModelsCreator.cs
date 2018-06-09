using Analytics.CommonComponents.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelsCreator
{
    class ModelsCreator : DataWorker<List<LicenceInfo>, List<string>>
    {
        private List<LicenceInfo> fields;
        private List<string> result;


        public bool connect()
        {
            return true;
        }

        public void execute()
        {
            //На каждой итерации создается отдельная, малая модель для конкретной лицензии
            for (int i=0; i < fields.Count(); i++)
            {
                LicenceInfo info = fields.ElementAt(i);
                result.Add(info.getName()+"storage" + " STORAGE " + info.getCount());
                result.Add("GENERATE "+info.getAvgRequestedTime()+","+
                    info.getAvgSquereRequestedTime()+","+info.getNumberOfTranzactsOnStart());
                result.Add("QUEUE "+info.getName()+"queue");
                result.Add("ENTER "+ info.getName() + "storage");
                result.Add("DEPART "+ info.getName() + "queue");
                result.Add("ADVANCE "+info.getAvgDelayTimeInTheProcessing()+","+
                    info.getAvgSquereDelayTimeInTheProcessing());
                result.Add("LEAVE "+ info.getName() + "storage");
                result.Add("TERMINATE 1");
            }
        }

        public List<string> getResult()
        {
            return result;
        }

        public void setConfig(List<LicenceInfo> fields)
        {
            result = new List<string>();
            this.fields = fields;
        }
    }
}
