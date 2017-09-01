using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormLab.Extensions;    //Add

namespace WinFormLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cbNearMonth.LoadEnum<MonthNearEnum>();

            Console.WriteLine(MonthNearEnum.Near1st.ToDescription());
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
