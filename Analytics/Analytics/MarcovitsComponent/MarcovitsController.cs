using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analytics.Modeling.GroupByTypes;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.MarcovitsComponent.Config;
using Analytics.CommonComponents.CommandsStore.Commands.Modeling;

namespace Analytics.MarcovitsComponent
{
    class MarcovitsController : MarcovitsControllerInterface
    {
        private BasicStatisticsModel<MarcovitsModelState, MarcovitsConfig> model;
        CommandsStoreInterface commandsStore = new ConcreteCommandStore();
        //При откате модели до предыдущего состояния, элементы вью тоже меняются,
        //но так как они прослушиваются на изменения вью, то это влечет за собой 
        //изменение модели и добавление еще одной команды, а она не нужна, так как мы
        //только что забрали предыдущую
        private bool activateChangeListeners = true;

        public MarcovitsController(MarcovitsModel model)
        {
            this.model = model;
        }

        public void getNextState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            commandsStore.rollbackRecoveryModel();
            activateChangeListeners = true;
        }

        public void getPreviousState()
        {
            //Вначале отключение прослушивания управляющих елементов вью
            activateChangeListeners = false;
            commandsStore.recoveryModel();
            activateChangeListeners = true;
        }

        public void getStatistics()
        {
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand<MarcovitsConfig>(model));
        }

        public void intervalChange(GropByType interval)
        {
            if (activateChangeListeners)
            {
                MarcovitsConfig config = new MarcovitsConfig(
                    "D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb",
                    interval);
                commandsStore.executeCommand(
                    new UpdateConfigCommand<MarcovitsModelState, MarcovitsConfig>(model, config));
            }
        }
    }
}
