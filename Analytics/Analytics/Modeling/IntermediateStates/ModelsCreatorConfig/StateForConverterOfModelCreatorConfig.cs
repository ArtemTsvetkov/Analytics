using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.IntermediateStates
{
    class StateForConverterOfModelCreatorConfig
    {
        public List<DataSet> bufOftimeBetweenQueryToGetLicenses = new List<DataSet>();
        public List<DataSet> bufOfTimesOfInBetweenOutLicenses = new List<DataSet>();
        public string[] unicNames = new string[0];
        public DataSet numberBuyLicenses = new DataSet();
        public List<DataSet> numberOfGetingLicensesPerTime =
            new List<DataSet>();
        public List<DataSet> avgLicensePerTime = new List<DataSet>();
    }
}
