using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuplicateRows
{
    public partial class Form1 : Form
    {
        private DataTable _table;

        public Form1()
        {
            InitializeComponent();
            _table = new DataTable();
            InitTable();
            InitGrid();
        }

        private void InitGrid()
        {
            dataGridView1.CellMouseClick += OpenDataGridContextMenu;
            DuplicateStripMenuItem.Click += DuplicateStripMenuItem_Click;
        }

        private void DuplicateStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataRow = ((DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row;
            var index = _table.Rows.IndexOf(dataRow);
            for (int n = 0; n < 3; n++)
            {
                var row = dataRow.Table.NewRow();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        row[i] = dataRow[i].ToString() + n;
                    }
                    else
                    {
                        row[i] = dataRow[i];
                    }
                }
                dataRow.Table.Rows.InsertAt(row, index + n + 1);
            }
        }

        private void OpenDataGridContextMenu(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
                GridContextMenuStrip.Show(Cursor.Position);
            }
        }


        private void InitTable()
        {
            _table.Columns.Add("col1");
            _table.Columns.Add("col2");
            _table.Columns.Add("col3");

            dataGridView1.DataSource = _table;
        }
    }
}
