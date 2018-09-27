using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.Interfaces.AdwancedModelsInterfaces;
using Analytics.HandModifiedDataPanel.Interfaces;
using Analytics.HandModifiedDataPanel.ModelConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataModel :
        BasicModel<HandModifiedDataState, ModelConfiguratorInterface<HandModifiedDataState>>, 
        RecoveredModel, 
        HandModifiedDataModelInterface
    {
        private HandModifiedDataState state;

        public HandModifiedDataModel()
        {
            state = new HandModifiedDataState();
        }

        public ModelsState copySelf()
        {
            return state.copy();
        }

        public override HandModifiedDataState getResult()
        {
            return state.copy();
        }

        public override void loadStore()
        {
            throw new NotImplementedException();
            //Загрузка данных для таблиц
            notifyObservers();
        }

        public void recoverySelf(ModelsState state)
        {
            HandModifiedDataState copy = (HandModifiedDataState)state;
            this.state = copy.copy();
            notifyObservers();
        }

        public void saveNewData()
        {
            throw new NotImplementedException();
        }

        public override void setConfig(ModelConfiguratorInterface<HandModifiedDataState> configData)
        {
            state = configData.configureMe(state);
        }
    }
}
