using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormLab.Helpers;    //Add


namespace WinFormLab.Ctrl
{
    public partial class CtrlForm : Form
    {
        public CtrlForm()
        {
            InitializeComponent();
        }

        private void CtrlForm_Load(object sender, EventArgs e)
        {
            cbNearMonth.LoadEnum<MonthNearEnum>();
        }
    }

    public enum MonthNearEnum
    {
        [Description("近月")]
        Near1st,
        [Description("次近月")]
        Near2nd,
        [Description("近二月")]
        Near2Months
    }
}
