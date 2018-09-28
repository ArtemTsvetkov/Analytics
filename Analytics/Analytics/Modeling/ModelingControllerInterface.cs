using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling
{
    interface ModelingControllerInterface
    {
        void getStatistics();
        void intervalChange(GropByType interval);
        void numberOfModelingStartsChange(int number);
        void flagUseCovarChange(bool flag);
        void getPreviousState();
        void getNextState();
    }
}
