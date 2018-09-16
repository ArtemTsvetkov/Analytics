using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Navigator.Basic
{
    interface NavigatorInterface
    {
        void navigateTo(string viewsName);
        void addView(NavigatorsView view);
        void navigateToPreviousView();
        void navigateToNextView();
    }
}
