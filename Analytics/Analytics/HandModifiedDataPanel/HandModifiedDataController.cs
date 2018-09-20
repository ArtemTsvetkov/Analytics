using Analytics.HandModifiedDataPanel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataController : HandModifiedDataControllerInterface
    {
        private HandModifiedDataModel handModifiedDataModel;

        public HandModifiedDataController(HandModifiedDataModel handModifiedDataModel)
        {
            this.handModifiedDataModel = handModifiedDataModel;
        }
    }
}
