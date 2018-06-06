using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore.Commands.Modeling
{
    class RunModeling : BasicCommand<ModelingState, ModelingState>
    {

        public RunModeling(BasicModel<ModelingState, ModelingState>
            model) : base(model)
        {
            
        }

        public override void execute()
        {
            modelsState = model.copySelf();
            model.calculationStatistics();
            model.recoverySelf(modelsState);
        }
    }
}
