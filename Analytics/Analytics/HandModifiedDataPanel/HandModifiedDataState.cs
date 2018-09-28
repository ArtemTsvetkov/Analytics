using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataState : ModelsState
    {
        public string[] unicSoftwareNames;
        public double[] numberOfPurcharedLicenses;
        public double[] percents;
        public double sumOfParts;

        public void calculateSumOfParts()
        {
            if(percents != null)
            {
                sumOfParts = 0;
                for (int i=0; i<percents.Count(); i++)
                {
                    sumOfParts += percents[i];
                }
            }
        }

        public HandModifiedDataState copy()
        {
            HandModifiedDataState copy = new HandModifiedDataState();
            copy.numberOfPurcharedLicenses = (double[])numberOfPurcharedLicenses.Clone();
            copy.unicSoftwareNames = (string[])unicSoftwareNames.Clone();
            copy.percents = (double[])percents.Clone();
            copy.sumOfParts = sumOfParts;

            return copy;
        }
    }
}
