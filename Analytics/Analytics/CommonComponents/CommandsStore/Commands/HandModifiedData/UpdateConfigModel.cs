using Analytics.CommandsStore;
using Analytics.HandModifiedDataPanel;
using Analytics.HandModifiedDataPanel.ModelConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.CommandsStore.Commands.HandModifiedData
{
    class UpdateConfigModel : BasicCommand
    {
        private ModelConfiguratorInterface<HandModifiedDataState> config;
        new HandModifiedDataModel model;

        public UpdateConfigModel(HandModifiedDataModel model,
            ModelConfiguratorInterface<HandModifiedDataState> config) : base(model)
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