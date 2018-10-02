using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel.ModelConfigurator
{
    class UpdateNumberOfLicensesWithModificator : ModelConfiguratorInterface<HandModifiedDataState>
    {
        double value;

        public UpdateNumberOfLicensesWithModificator(double value)
        {
            this.value = value;
        }

        public HandModifiedDataState configureMe(HandModifiedDataState oldState)
        {
            for(int i=0; i<oldState.unicSoftwareNames.Count(); i++)
            {
                oldState.numberOfPurcharedLicenses[i] =
                    oldState.numberOfPurcharedLicenses[i] * value;
            }

            return oldState;
        }
    }
}
