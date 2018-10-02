using Analytics.HandModifiedDataPanel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analytics.HandModifiedDataPanel.ModelConfigurator;
using Analytics.SecurityComponent;
using Analytics.CommonComponents.Exceptions.Security;
using Analytics.CommonComponents.CommandsStore.Commands.HandModifiedData;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataController : HandModifiedDataControllerInterface
    {
        private HandModifiedDataModel model;
        private SecurityModel securityModel;
        private bool currentUserIsAdmin;
        private CommandsStoreInterface commandsStore;

        public HandModifiedDataController(HandModifiedDataModel model, 
            SecurityModel securityModel, CommandsStoreInterface commandsStore)
        {
            this.model = model;
            this.securityModel = securityModel;
            this.securityModel.subscribe(this);
            this.commandsStore = commandsStore;
        }

        public void notify()
        {
            currentUserIsAdmin = securityModel.getResult().isAdmin();
        }

        public void saveNewData()
        {
            if(currentUserIsAdmin)
            {
                model.saveNewData();
            }
            else
            {
                throw new InsufficientPermissionsException("This user does not"
                    + "have sufficient rights to perform the specified operation");
            }
        }

        public void updateModelsConfig(ModelConfiguratorInterface<HandModifiedDataState> config)
        {
            commandsStore.executeCommand(new UpdateConfigModel(model, config));
        }

        public void loadStore()
        {
            model.loadStore();
        }
    }
}
