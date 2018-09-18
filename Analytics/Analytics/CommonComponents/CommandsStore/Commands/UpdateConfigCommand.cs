using Analytics.CommandsStore;
using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.CommandsStore.Commands.Modeling
{
    class UpdateConfigCommand<TTypeOfResult, TConfigType> : BasicCommand
    {
        private TConfigType config;
        new BasicStatisticsModel<TTypeOfResult, TConfigType> model;

        public UpdateConfigCommand(BasicStatisticsModel<TTypeOfResult, TConfigType>
            model, TConfigType config) : base(model)
        {
            this.config = config;
            this.model = model;
        }

        public override void execute()
        {
            modelsState = model.copySelf();
            model.setConfig(config);
        }
    }
}
