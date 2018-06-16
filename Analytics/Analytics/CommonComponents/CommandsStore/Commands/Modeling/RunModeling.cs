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
        private TConfigType configWithNoResetFlag;

        public RunModeling(BasicModel<ModelingReport, TConfigType>
            model, TConfigType configWithNoResetFlag) : base(model)
        {
            this.configWithNoResetFlag = configWithNoResetFlag;
        }

        public override void execute()
        {
            modelsState = model.copySelf();
            model.setConfig(configWithNoResetFlag);
            model.loadStore();
            model.calculationStatistics();
            model.recoverySelf(modelsState);
        }
    }
}
