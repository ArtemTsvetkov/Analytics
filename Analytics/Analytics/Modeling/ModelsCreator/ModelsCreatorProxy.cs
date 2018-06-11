using Analytics.CommonComponents.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelsCreator
{
    class ModelsCreatorProxy : DataWorker<ModelsCreatorConfigState, List<string>>
    {
        private DataWorker<ModelsCreatorConfigState, List<string>> creator = new ModelsCreator();

        public void setConfig(ModelsCreatorConfigState fields)
        {
            creator.setConfig(fields);
        }

        public void execute()
        {
            creator.execute();
        }

        public bool connect()
        {
            return creator.connect();
        }

        public List<string> getResult()
        {
            return creator.getResult();
        }    
    }
}
/*
 * Прокси создан на случай, если понадобится как-то контролировать доступ
 */