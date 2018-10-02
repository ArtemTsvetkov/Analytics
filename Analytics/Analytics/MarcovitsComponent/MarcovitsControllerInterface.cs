using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.MarcovitsComponent
{
    interface MarcovitsControllerInterface
    {
        void getStatistics();
        void intervalChange(GropByType interval);
    }
}
