using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.IntermediateStates.ModelsCreatorConfig
{
    class ReturnStateForConverterOfModelCreatorConfig
    {
        public List<MappingLicenseResult> bufOftimeBetweenQueryToGetLicenses = 
            new List<MappingLicenseResult>();
        public List<MappingLicenseResult> bufOfTimesOfInBetweenOutLicenses = 
            new List<MappingLicenseResult>();
        //Кол-во закупленных лицензий
        public int[] numberBuyLicenses;
    }
}
