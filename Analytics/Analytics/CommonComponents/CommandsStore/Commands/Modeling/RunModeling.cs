using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.Modeling;
using Analytics.Modeling.Config;
using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore.Commands.Modeling
{
    class RunModeling<TConfigType> : BasicCommand
    {
        new BasicStatisticsModel<ModelingReport, TConfigType> model;
        public RunModeling(BasicStatisticsModel<ModelingReport, TConfigType>
            model) : base(model)
        {
            this.model = model;
        }

        public override void execute()
        {   
            modelsState = model.copySelf();
            model.loadStore();
            model.calculationStatistics();
        }
    }
}
