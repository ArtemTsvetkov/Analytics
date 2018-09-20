using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataView : Observer, NavigatorsView
    {
        private Form1 form;
        private CommandsStore<HandModifiedDataState, HandModifiedDataConfig> commandsStore =
                new ConcreteCommandStore<HandModifiedDataState, HandModifiedDataConfig>();
        private HandModifiedDataModel model;

        public HandModifiedDataView(Form1 form, HandModifiedDataModel model)
        {
            this.form = form;
            this.model = model;
            model.subscribe(this);
        }

        public string getName()
        {
            return "HandModifiedDataView";
        }

        public void notify()
        {
            throw new NotImplementedException();
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(2);
        }
    }
}
