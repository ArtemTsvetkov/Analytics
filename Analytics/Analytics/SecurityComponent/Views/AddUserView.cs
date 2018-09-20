using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent.Views
{
    class AddUserView : Observer, NavigatorsView
    {
        private Form1 form;
        private SecurityModel model;

        public AddUserView(Form1 form)
        {
            this.form = form;
            model = new SecurityModel();
            model.subscribe(this);
        }

        public string getName()
        {
            return "AddUserView";
        }

        public void notify()
        {
            throw new NotImplementedException();
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(1);
        }
    }
}
