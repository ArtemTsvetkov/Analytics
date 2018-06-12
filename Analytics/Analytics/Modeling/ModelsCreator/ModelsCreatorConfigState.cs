using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelsCreator
{
    class ModelsCreatorConfigState
    {
        //Расчет с учeтом ковариации
        public bool withKovar = false;
        //Информация о лицензиях
        public List<LicenceInfo> licenceInfo;
        //Корреляция между запросами на лицензии
        public double[,] korellation;
    }
}
