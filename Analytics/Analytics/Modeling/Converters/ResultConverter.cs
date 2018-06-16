using Analytics.Modeling.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.Converters
{
    class ResultConverter : DataConverter<ModelingState, ModelingReport>
    {
        public ModelingReport convert(ModelingState data)
        {
            ModelingReport avgReport = data.report.copyReport(data);
            //Вычисление средних значений
            for (int i = 0; i < avgReport.getMaxTranzactsInQueue().Count(); i++)
            {
                avgReport.getMaxTranzactsInQueue().ElementAt(i).value =
                    avgReport.getMaxTranzactsInQueue().ElementAt(i).value / 
                    (double)data.numberOfStartsModel;
            }

            for (int i = 0; i < avgReport.getAvgTranzactsInQueue().Count(); i++)
            {
                avgReport.getAvgTranzactsInQueue().ElementAt(i).value =
                    avgReport.getAvgTranzactsInQueue().ElementAt(i).value /
                    (double)data.numberOfStartsModel;
            }

            for (int i = 0; i < avgReport.getNumberRunTranzactsOnLable().Count(); i++)
            {
                avgReport.getNumberRunTranzactsOnLable().ElementAt(i).value =
                    avgReport.getNumberRunTranzactsOnLable().ElementAt(i).value /
                    (double)data.numberOfStartsModel;
            }

            for (int i = 0; i < avgReport.getVariablesValue().Count(); i++)
            {
                avgReport.getVariablesValue().ElementAt(i).value =
                    avgReport.getVariablesValue().ElementAt(i).value /
                    (double)data.numberOfStartsModel;
            }

            avgReport.interval = data.interval;
            return avgReport;
        }
    }
}
