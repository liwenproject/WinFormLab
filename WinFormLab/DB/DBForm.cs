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

namespace WinFormLab.DB
{
    public partial class DBForm : Form
    {
        string msg = "";

        public DBForm()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string conn = @"server=XW10CH170827A\SQLEXPRESS;database=Northwind;Connect Timeout=30;user Id=sa;Password=1234;Min Pool Size=5;Max Pool Size=60;";
            string SQL = "SELECT * FROM Orders WHERE EmployeeID = @EmployeeID ";
            var dbParam = new List<SqlParameter>();
            dbParam.Add(new SqlParameter("EmployeeID", "1"));

            DBMSSQL svc = new DBMSSQL();
            DataTable dt = svc.DBQuery(conn, SQL, dbParam, ref msg);

            grid1.DataSource = dt;
        }

    }
}
