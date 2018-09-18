using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent.Views
{
    class ChangePasswordView : Observer, NavigatorsView
    {
        private Form1 form;
        private SecurityModel model;

        public ChangePasswordView(Form1 form)
        {
            this.form = form;
            model = new SecurityModel();
            model.subscribe(this);
        }

        public void notify()
        {
            throw new NotImplementedException();
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(3);
            form.tabControl2Elem.SelectTab(1);
        }

        public string getName()
        {
            return "ChangePasswordView";
        }
    }
}
