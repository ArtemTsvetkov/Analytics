using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analytics.Modeling.GroupByTypes;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.Modeling.Config;
using Analytics.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.CommandsStore.Commands.Modeling;
using Analytics.CommonComponents.ExceptionHandler;

namespace Analytics.Modeling
{
    class ModelingController : ModelingControllerInterface
    {
        private BasicStatisticsModel<ModelingReport, ModelingConfig> model;
        CommandsStoreInterface commandsStore = new ConcreteCommandStore();
        //При откате модели до предыдущего состояния, элементы вью тоже меняются,
        //но так как они прослушиваются на изменения вью, то это влечет за собой 
        //изменение модели и добавление еще одной команды, а она не нужна, так как мы
        //только что забрали предыдущую
        private bool activateChangeListeners = true;

        public ModelingController(ModelingModel model)
        {
            this.model = model;
        }

        public void flagUseCovarChange(bool flag)
        {
            if (activateChangeListeners)
            {
                ModelingConfig config = model.getConfig();
                config.setWithKovar(flag);
                commandsStore.executeCommand(
                    new UpdateConfigCommand<ModelingReport, ModelingConfig>(model, config));
            }
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
            commandsStore.executeCommand(new RunModeling<ModelingConfig>(model));
        }

        public void intervalChange(GropByType interval)
        {
            if (activateChangeListeners)
            {
                ModelingConfig config = model.getConfig();
                config.setInterval(interval);
                commandsStore.executeCommand(
                    new UpdateConfigCommand<ModelingReport, ModelingConfig>(model, config));
            }
        }

        public void numberOfModelingStartsChange(int number)
        {
            if (activateChangeListeners)
            {
                try
                {
                    ModelingConfig config = model.getConfig();
                    config.setNumberOfStartsModeling(number);
                    commandsStore.executeCommand(
                        new UpdateConfigCommand<ModelingReport, ModelingConfig>(model, config));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.getInstance().processing(ex);
                }
            }
        }
    }
}
