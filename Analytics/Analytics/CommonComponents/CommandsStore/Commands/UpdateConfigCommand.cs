using Analytics.CommandsStore;
using Analytics.CommonComponents.BasicObjects;
using Analytics.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.CommandsStore.Commands.Modeling
{
    class UpdateConfigCommand<TTypeOfResult, TConfigType> : BasicCommand<TTypeOfResult, TConfigType>
    {
        private TConfigType config;

        public UpdateConfigCommand(BasicModel<TTypeOfResult, TConfigType>
            model, TConfigType config) : base(model)
        {
            this.config = config;
        }

        public override void execute()
        {
            modelsState = model.copySelf();
            model.setConfig(config);
        }
    }
}
