using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormLab
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            dBToolStripMenuItem_Click(dBToolStripMenuItem, null);
            controlToolStripMenuItem_Click(controlToolStripMenuItem, null);
            gridToolStripMenuItem_Click(gridToolStripMenuItem, null);
        }

        private void dBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB.DBForm frm = new DB.DBForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void controlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ctrl.CtrlForm frm = new Ctrl.CtrlForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grid.GridHideBtnForm frm = new Grid.GridHideBtnForm();
            frm.MdiParent = this;
            frm.Show();
        }
    }

}
