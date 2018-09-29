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
using Analytics.HandModifiedDataPanel;

namespace Analytics.Modeling
{
    class ModelingController : ModelingControllerInterface, Observer
    {
        private BasicStatisticsModel<ModelingReport, ModelingConfig> model;
        private HandModifiedDataModel handModifiedDataModel;
        CommandsStoreInterface commandsStore;

        public ModelingController(ModelingModel model, 
            HandModifiedDataModel handModifiedDataModel, CommandsStoreInterface commandsStore)
        {
            this.model = model;
            this.handModifiedDataModel = handModifiedDataModel;
            this.handModifiedDataModel.subscribe(this);
            this.commandsStore = commandsStore;
        }

        public void flagUseCovarChange(bool flag)
        {
            ModelingConfig config = model.getConfig();
            config.setWithKovar(flag);
            commandsStore.executeCommand(
                new UpdateConfigCommand<ModelingReport, ModelingConfig>(model, config));
        }

        public void getStatistics()
        {
            commandsStore.executeCommand(new RunModeling<ModelingConfig>(model));
        }

        public void intervalChange(GropByType interval)
        {
            ModelingConfig config = model.getConfig();
            config.setInterval(interval);
            commandsStore.executeCommand(
                new UpdateConfigCommand<ModelingReport, ModelingConfig>(model, config));
        }

        public void numberOfModelingStartsChange(int number)
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

        //Подписка на модель с данными о количестве и процентном соотношении лицензий
        public void notify()
        {
            HandModifiedDataState result = handModifiedDataModel.getResult();
            ModelingConfig config = model.getConfig();
            config.UnicSoftwareNames = result.unicSoftwareNames;
            config.NumberOfPurcharedLicenses = result.numberOfPurcharedLicenses;
            config.Percents = result.percents;
            config.NotifyObservers = false;
            commandsStore.executeCommand(
                    new UpdateConfigCommand<ModelingReport, ModelingConfig>(model, config));
        }
    }
}
