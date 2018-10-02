using Analytics.HandModifiedDataPanel.Interfaces;
using Analytics.MarcovitsComponent;
using Analytics.Modeling;
using Analytics.SecurityComponent.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.InitialyzerComponent
{
    class InitComponents
    {
        public ModelingControllerInterface modelingController;
        public SecurityControllerInterface securityController;
        public HandModifiedDataControllerInterface handModifiedDataController;
        public MarcovitsControllerInterface marcovitsController;
        public CommandsStoreInterface commandsStore;
    }
}
