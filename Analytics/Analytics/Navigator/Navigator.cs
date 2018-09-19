using Analytics.CommonComponents.Exceptions.Navigator;
using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Navigator
{
    class Navigator : NavigatorInterface
    {
        private List<NavigatorsView> views;
        private List<string> viewsBeforeThisTime;
        private List<string> viewsAfterThisTime;
        private string currentViewsName;
        private static Navigator navigator;

        private Navigator()
        {
            
        }

        public static NavigatorInterface getInstance()
        {
            if(navigator == null)
            {
                navigator = new Navigator();
                navigator.views = new List<NavigatorsView>();
                navigator.viewsBeforeThisTime = new List<string>();
            }

            return navigator;
        }

        public void addView(NavigatorsView view)
        {
            bool itsViewNotAddedYet = true;
            for(int i=0; i<navigator.views.Count; i++)
            {
                if(view.getName().Equals(navigator.views.ElementAt(i).getName()))
                {
                    itsViewNotAddedYet = false;
                }
            }
            if(itsViewNotAddedYet)
            {
                navigator.views.Add(view);
            }
            else
            {
                throw new ViewAlreadyAddedException();
            }
        }

        public void navigateTo(string viewsName)
        {
            for(int i=0; i<navigator.views.Count; i++)
            {
                if(navigator.views.ElementAt(i).getName().Equals(viewsName))
                {
                    if (navigator.viewsAfterThisTime != null)
                    {
                        navigator.viewsAfterThisTime.Clear();
                    }

                    if (navigator.viewsBeforeThisTime == null)
                    {
                        navigator.viewsBeforeThisTime = new List<string>();
                    }
                    if (currentViewsName != null && !currentViewsName.Equals(""))
                    {
                        navigator.viewsBeforeThisTime.Add(navigator.currentViewsName);
                    }
                    navigator.currentViewsName = viewsName;

                    navigator.views.ElementAt(i).show();
                    return;
                }
            }
            throw new NotFoundView();
        }

        public void navigateToNextView()
        {
            if (navigator.viewsAfterThisTime != null)
            {
                navigator.viewsBeforeThisTime.Add(navigator.currentViewsName);

                string nextView = navigator.viewsAfterThisTime.Last();
                navigator.viewsAfterThisTime.RemoveAt(navigator.viewsAfterThisTime.Count - 1);

                for (int i = 0; i < navigator.views.Count; i++)
                {
                    if (navigator.views.ElementAt(i).getName().Equals(nextView))
                    {
                        navigator.currentViewsName = nextView;
                        navigator.views.ElementAt(i).show();
                        return;
                    }
                }
                throw new NotFoundView();
            }
            else
            {
                throw new ViewsHistoryIsEmtptyException();
            }
        }

        public void navigateToPreviousView()
        {
            if (navigator.viewsBeforeThisTime != null)
            {
                if (navigator.viewsAfterThisTime == null)
                {
                    navigator.viewsAfterThisTime = new List<string>();
                }

                navigator.viewsAfterThisTime.Add(navigator.currentViewsName);

                string previousView = navigator.viewsBeforeThisTime.Last();
                navigator.viewsBeforeThisTime.RemoveAt(navigator.viewsBeforeThisTime.Count - 1);

                for(int i=0; i<navigator.views.Count; i++)
                {
                    if (navigator.views.ElementAt(i).getName().Equals(previousView))
                    {
                        navigator.currentViewsName = previousView;
                        navigator.views.ElementAt(i).show();
                        return;
                    }
                }
                throw new NotFoundView();
            }
            else
            {
                throw new ViewsHistoryIsEmtptyException();
            }
        }

        public string getCurrentViewsName()
        {
            return currentViewsName;
        }
    }
}
