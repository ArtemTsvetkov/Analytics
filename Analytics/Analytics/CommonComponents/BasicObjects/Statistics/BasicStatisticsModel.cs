using Analytics.CommonComponents.Interfaces.AdwancedModelsInterfaces;
using Analytics.CommonComponents.Interfaces.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.BasicObjects.Statistics
{
    abstract class BasicStatisticsModel<TTypeOfResult, TConfigType> : 
        BasicModel<TTypeOfResult, TConfigType>, RecoveredModel, StatisticsModelInterface
    {
        abstract public void calculationStatistics();
        abstract public ModelsState copySelf();
        abstract public void recoverySelf(ModelsState state);
        abstract public TConfigType getConfig();
    }
}
