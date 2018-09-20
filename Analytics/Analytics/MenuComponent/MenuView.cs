using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.MenuComponent
{
    class MenuView : NavigatorsView
    {
        private Form1 form;

        public MenuView(Form1 form)
        {
            this.form = form;
        }

        public string getName()
        {
            return "MenuView";
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(5);
        }
    }
}
