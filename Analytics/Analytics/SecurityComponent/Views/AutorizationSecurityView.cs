using Analytics.CommonComponents.BasicObjects;
using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.SecurityComponent
{
    class AutorizationSecurityView : Observer, NavigatorsView
    {
        private Form1 form;
        private BasicModel<SecurityUserInterface, SecurityUserInterface> model;

        public AutorizationSecurityView(Form1 form,
            BasicModel<SecurityUserInterface, SecurityUserInterface> model)
        {
            this.form = form;
            this.model = model;
            this.model.subscribe(this);
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
            return "AutorizationSecurityView";
        }
    }
}
