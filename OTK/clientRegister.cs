using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OTK
{
    public partial class clientRegister : Form
    {
        public clientRegister()
        {
            InitializeComponent();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            string sql = "Select id, name, address, phone from client";
            Main.Table_Fill("Клиент", sql);
            Main.ds.Tables["Клиент"].DefaultView.Sort = "id";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.tabControl1.Controls.Remove(Main.tabControl1.SelectedTab);
        }

        private void button2(object sender, EventArgs e)
        {
            string sql = "Insert into client (name, address, phone) values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "')";
            if (!Main.Modification_Execute(sql))
                return;
        }
    }

}
