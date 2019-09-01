using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.HandModifiedDataPanel.Halpers
{
    class TablesStructureCreater
    {
        public void create(Form1 form)
        {
            form.DataGridView4Elem.RowHeadersVisible = false;
            form.DataGridView6Elem.RowHeadersVisible = false;


            DataGridViewTextBoxColumn col1;
            col1 = new DataGridViewTextBoxColumn();
            col1.Name = "Название лицензии";
            col1.HeaderText = "Название лицензии";
            col1.Width = 176;
            DataGridViewTextBoxColumn col2;
            col2 = new DataGridViewTextBoxColumn();
            col2.Name = "Наличие (шт)";
            col2.HeaderText = "Наличие (шт)";
            col2.Width = 150;
            form.DataGridView4Elem.Columns.Add(col1);
            form.DataGridView4Elem.Columns.Add(col2);


            DataGridViewTextBoxColumn col10;
            col10 = new DataGridViewTextBoxColumn();
            col10.Name = "Название лицензии";
            col10.HeaderText = "Название лицензии";
            col10.Width = 176;
            DataGridViewTextBoxColumn col11;
            col11 = new DataGridViewTextBoxColumn();
            col11.Name = "Стоимость от общего бюджета (%)";
            col11.HeaderText = "Стоимость от общего бюджета (%)";
            col11.Width = 150;
            form.DataGridView6Elem.Columns.Add(col10);
            form.DataGridView6Elem.Columns.Add(col11);

            form.DataGridView4Elem.Rows.Clear();
            form.DataGridView6Elem.Rows.Clear();
        }
    }
}
