using Analytics.CommonComponents.BasicObjects;
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
    class RunModeling<TConfigType> : BasicCommand<ModelingReport, TConfigType>
    {
        public RunModeling(BasicModel<ModelingReport, TConfigType>
            model) : base(model)
        {

        }

        public override void execute()
        {   
            modelsState = model.copySelf();
            model.loadStore();
            model.calculationStatistics();
        }
    }
}
