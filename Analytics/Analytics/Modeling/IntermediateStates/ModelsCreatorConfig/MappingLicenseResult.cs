using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.IntermediateStates.ModelsCreatorConfig
{
    class MappingLicenseResult
    {
        public string licenseName;
        public double[] characteristic;

        public MappingLicenseResult(string licenseName, int count)
        {
            this.licenseName = licenseName;
            characteristic = new double[count];
        }
    }
}
