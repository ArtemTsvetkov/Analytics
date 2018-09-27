using Analytics.HandModifiedDataPanel.ModelConfigurator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel.Interfaces
{
    interface HandModifiedDataControllerInterface : Observer//Observer для Security model
    {
        void saveNewData();
        void updateModelsConfig(ModelConfiguratorInterface<HandModifiedDataState> config);
        void getPreviousState();
        void getNextState();
    }
}
