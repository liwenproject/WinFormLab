using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormLab.Grid
{
    public partial class GridHideBtnForm : Form
    {
        private DataTable dt;
        private DataView dv;

        public GridHideBtnForm()
        {
            InitializeComponent();

            createOrderDataTable();             //建立委託回報的DataTable
            dv = dt.DefaultView;  //建立委託回報的DataView
            grid.DataSource = dv;    //Grid Bind
        }
        private void GridForm1_Load(object sender, EventArgs e)
        {
            DataGridViewLib.ColumnAdd("colCol1", "時間", "col1", 0, true, grid);

            DataGridViewDisableCheckBoxColumn column0 = new DataGridViewDisableCheckBoxColumn("勾選", "CheckBoxes", "");
            grid.Columns.Insert(0, column0);
            DataGridViewDisableButtonColumn column1 = new DataGridViewDisableButtonColumn("按鈕", "按", "Buttons");
            column1.DefaultCellStyle.BackColor = Color.Yellow;
            grid.Columns.Insert(1, column1);
        }

        private void createOrderDataTable()
        {
            dt = new DataTable("OrderReport");
            dt.Columns.Add("check", typeof(bool));
            dt.Columns.Add("col1", typeof(string));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = dt.NewRow();
            row["col1"] = DateTime.Now.ToString("HH:mm:ss");
            dt.Rows.InsertAt(row, 0);
        }

        DataGridViewDisableButtonCell buttonCell;
        DataGridViewDisableCheckBoxCell checkboxCell;
        private void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grid.Rows[e.RowIndex].Cells["colCol1"].Value == null)
                return;

            if ((grid.Rows[e.RowIndex].Cells["colCol1"].Value.ToString().EndsWith("1")) ||
                (grid.Rows[e.RowIndex].Cells["colCol1"].Value.ToString().EndsWith("2")) ||
                (grid.Rows[e.RowIndex].Cells["colCol1"].Value.ToString().EndsWith("5")) ||
                (grid.Rows[e.RowIndex].Cells["colCol1"].Value.ToString().EndsWith("6")) ||
                (grid.Rows[e.RowIndex].Cells["colCol1"].Value.ToString().EndsWith("9")))
            {
                buttonCell = (DataGridViewDisableButtonCell)grid.Rows[e.RowIndex].Cells["Buttons"];
                buttonCell.Hide = true;
                buttonCell.Style.BackColor = Color.Red;

                checkboxCell = (DataGridViewDisableCheckBoxCell)grid.Rows[e.RowIndex].Cells["CheckBoxes"];
                checkboxCell.Hide = true;
            }
            else
            {
                buttonCell = (DataGridViewDisableButtonCell)grid.Rows[e.RowIndex].Cells["Buttons"];
                buttonCell.Hide = false;
            }
        }

    }
}

