using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics
{
    public partial class Form1 : Form, Observer
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void notify(object data)
        {
            if (data.GetType() == typeof(List<ResultTableRows>))
            {
                List<ResultTableRows> rw = (List<ResultTableRows>)data;


                int m = rw.Count();
                int n = 3;
                dataGridView1.ColumnCount = n;
                dataGridView1.RowCount = m;
                for (int i = 0; i < m; i++)
                {
                    dataGridView1.Rows[i].Cells[0].Value = rw.ElementAt(i).user;
                    dataGridView1.Rows[i].Cells[1].Value = rw.ElementAt(i).host;
                    dataGridView1.Rows[i].Cells[2].Value = rw.ElementAt(i).po;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model model = new ConcreteModel("D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb", "Information");
            model.subscribe(this);
            CommandsStore commandsStore = new ConcreteCommandStore();
            commandsStore.executeCommand(new LoadDataCommand(model));
        }
    }
}
