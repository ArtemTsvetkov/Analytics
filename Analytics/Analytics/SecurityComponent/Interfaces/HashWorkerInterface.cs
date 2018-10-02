using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent.Interfaces
{
    interface HashWorkerInterface<TTypeOfConfig>
    {
        void setConfig(TTypeOfConfig config);
        string getHash(string password, string sult);
        string getSult(SecurityUserInterface user);
    }
}
