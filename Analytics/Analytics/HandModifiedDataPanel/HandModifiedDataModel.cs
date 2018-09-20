using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.Interfaces.AdwancedModelsInterfaces;
using Analytics.HandModifiedDataPanel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataModel :
        BasicModel<HandModifiedDataState, HandModifiedDataState>, RecoveredModel, 
        HandModifiedDataModelInterface
    {
        public ModelsState copySelf()
        {
            throw new NotImplementedException();
        }

        public override HandModifiedDataState getResult()
        {
            throw new NotImplementedException();
        }

        public override void loadStore()
        {
            throw new NotImplementedException();
        }

        public void recoverySelf(ModelsState state)
        {
            throw new NotImplementedException();
        }

        public void saveNewData()
        {
            throw new NotImplementedException();
        }

        public override void setConfig(HandModifiedDataState configData)
        {
            throw new NotImplementedException();
        }
    }
}
