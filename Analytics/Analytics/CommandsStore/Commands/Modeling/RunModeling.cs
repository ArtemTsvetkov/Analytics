using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore.Commands.Modeling
{
    class RunModeling<TConfigType> : BasicCommand<ModelingState, ModelingState, TConfigType>
    {

        public RunModeling(BasicModel<ModelingState, ModelingState, TConfigType>
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
