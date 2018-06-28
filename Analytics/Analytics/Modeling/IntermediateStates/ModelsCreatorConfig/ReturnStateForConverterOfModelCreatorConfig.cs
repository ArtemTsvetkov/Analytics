using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.IntermediateStates.ModelsCreatorConfig
{
    class ReturnStateForConverterOfModelCreatorConfig
    {
        //Время получение лицензий
        public List<MappingLicenseResult> bufOftimeBetweenQueryToGetLicenses = 
            new List<MappingLicenseResult>();
        //Время между получением и возвращением лицензий
        public List<MappingLicenseResult> bufOfTimesOfInBetweenOutLicenses = 
            new List<MappingLicenseResult>();
        //Кол-во закупленных лицензий
        public int[] numberBuyLicenses;
        //Количество полученных лицензий за определенный промежуток времени
        public List<MappingLicenseResult> numberOfGetingLicensesPerTime =
            new List<MappingLicenseResult>();
        //Среднее количество запросов на получение лицензии в течении 
        //заданного промежутка времени
        public int[] avgLicensePerTime;
    }
}
