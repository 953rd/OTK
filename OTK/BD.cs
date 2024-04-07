using OTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTK
{
    public partial class BD : Form
    {
        public BD()
        {
            InitializeComponent();
        }

        private void счётToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dogovor project = new dogovor();
            Main.tabControl1.Controls.Add(project.tabControl1.TabPages[0]);
            Main.tabControl1.SelectedIndex = Main.tabControl1.TabCount - 1;
        }

        private void вкладыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dogovor project = new dogovor();
            Main.tabControl1.Controls.Add(project.tabControl1.TabPages[0]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (Main.tabControl1.TabCount > 0)
                Main.tabControl1.TabPages.RemoveAt(Main.tabControl1.TabCount - 1);
            LogIn login = new LogIn();
            Main.tabControl1.Controls.Add(login.tabControl1.TabPages[0]);
        }
    }
}
