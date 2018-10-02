using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Analytics.Modeling.GroupByTypes;
using Analytics.CommonComponents.BasicObjects.Statistics;
using Analytics.MarcovitsComponent.Config;
using Analytics.CommonComponents.CommandsStore.Commands.Modeling;
using Analytics.HandModifiedDataPanel;

namespace Analytics.MarcovitsComponent
{
    class MarcovitsController : MarcovitsControllerInterface, Observer
    {
        private BasicStatisticsModel<MarcovitsModelState, MarcovitsConfig> model;
        private HandModifiedDataModel handModifiedDataModel;
        CommandsStoreInterface commandsStore;

        public MarcovitsController(MarcovitsModel model, 
            HandModifiedDataModel handModifiedDataModel, CommandsStoreInterface commandsStore)
        {
            this.model = model;
            this.handModifiedDataModel = handModifiedDataModel;
            this.handModifiedDataModel.subscribe(this);
            this.commandsStore = commandsStore;
        }

        public void getStatistics()
        {
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand<MarcovitsConfig>(model));
        }

        public void intervalChange(GropByType interval)
        {
            MarcovitsConfig config = model.getConfig();
            config.setInterval(interval);
            commandsStore.executeCommand(
                new UpdateConfigCommand<MarcovitsModelState, MarcovitsConfig>(model, config));
        }

        //Подписка на модель с данными о количестве и процентном соотношении лицензий
        public void notify()
        {
            HandModifiedDataState result = handModifiedDataModel.getResult();
            MarcovitsConfig config = model.getConfig();
            config.UnicSoftwareNames = result.unicSoftwareNames;
            config.NumberOfPurcharedLicenses = result.numberOfPurcharedLicenses;
            config.Percents = result.percents;
            config.NotifyObservers = false;
            UpdateConfigCommand<MarcovitsModelState, MarcovitsConfig> command =
                new UpdateConfigCommand<MarcovitsModelState, MarcovitsConfig>(model, config);
            command.execute();
            //commandsStore.executeCommand(
                    //new UpdateConfigCommand<MarcovitsModelState, MarcovitsConfig>(model, config));
        }
    }
}
