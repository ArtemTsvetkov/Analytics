using Analytics.HandModifiedDataPanel.DataConverters;
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
        private CommandsStoreInterface commandsStore =
                new ConcreteCommandStore();
        //При откате модели до предыдущего состояния, элементы вью тоже меняются,
        //но так как они прослушиваются на изменения вью, то это влечет за собой 
        //изменение модели и добавление еще одной команды, а она не нужна, так как мы
        //только что забрали предыдущую
        private bool activateChangeListeners = true;

        public HandModifiedDataController(HandModifiedDataModel model, SecurityModel securityModel)
        {
            this.model = model;
            this.securityModel = securityModel;
        }

        public void getPreviousState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        public void getNextState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            commandsStore.rollbackRecoveryModel();
            activateChangeListeners = true;
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
            throw new NotImplementedException();
        }

        public void updateModelsConfig(ModelConfiguratorInterface<HandModifiedDataState> config)
        {
            if (activateChangeListeners)
            {
                commandsStore.executeCommand(new UpdateConfigModel(model, config));
            }
        }
    }
}
