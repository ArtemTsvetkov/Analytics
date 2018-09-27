using Analytics.HandModifiedDataPanel.DataConverters;
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
        private HandModifiedDataModel model;
        private DataConverter<HandModifiedDataState, FormsElementsValues>
            converter;

        public HandModifiedDataView(Form1 form, HandModifiedDataModel model)
        {
            this.form = form;
            this.model = model;
            model.subscribe(this);
            converter = new ResultConverter();
            model.loadStore();
        }

        public string getName()
        {
            return "HandModifiedDataView";
        }

        public void notify()
        {
            setValuesToFormsElements(converter.convert(model.getResult()));
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(2);
        }

        private void setValuesToFormsElements(FormsElementsValues values)
        {

        }
    }
}
