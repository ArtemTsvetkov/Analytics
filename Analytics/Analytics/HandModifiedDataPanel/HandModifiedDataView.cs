using Analytics.HandModifiedDataPanel.Halpers;
using Analytics.Navigator.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.HandModifiedDataPanel
{
    class HandModifiedDataView : Observer, NavigatorsView
    {
        private Form1 form;
        private HandModifiedDataModel model;

        public HandModifiedDataView(Form1 form, HandModifiedDataModel model)
        {
            this.form = form;
            this.model = model;
            model.subscribe(this);
            TablesStructureCreater creater = new TablesStructureCreater();
            creater.create(form);
        }

        public string getName()
        {
            return "HandModifiedDataView";
        }

        public void notify()
        {
            setValuesToFormsElements(model.getResult());
        }

        public void show()
        {
            form.tabControl1Elem.SelectTab(2);
        }

        private void setValuesToFormsElements(HandModifiedDataState state)
        {
            if (state.unicSoftwareNames.Count() > 0)
            {
                if (form.DataGridView4Elem.Rows.Count == 1)
                {
                    form.DataGridView4Elem.Rows.Add(state.unicSoftwareNames.Count() - 1);
                    form.DataGridView6Elem.Rows.Add(state.unicSoftwareNames.Count() - 1);
                }

                for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
                {
                    form.DataGridView4Elem.Rows[i].Cells[0].Value = state.unicSoftwareNames[i];
                    form.DataGridView6Elem.Rows[i].Cells[0].Value = state.unicSoftwareNames[i];
                }

                for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
                {
                    form.DataGridView4Elem.Rows[i].Cells[1].Value = state.numberOfPurcharedLicenses[i];
                }

                for (int i = 0; i < state.unicSoftwareNames.Count(); i++)
                {
                    form.DataGridView6Elem.Rows[i].Cells[1].Value = state.percents[i];
                }

                form.label7Elem.Text = "Сумма долей: " + state.sumOfParts.ToString() + "/1.0";
            }
        }
    }
}
