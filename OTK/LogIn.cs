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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OTK
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.Table_Fill("Сотрудник", $"select * from worker where login = '{textBox1.Text}' and password = '{textBox2.Text}'");

            try
            {
                if (Main.ds.Tables["Сотрудник"].Rows[0]["login"].ToString() == textBox1.Text && Main.ds.Tables["Сотрудник"].Rows[0]["password"].ToString() == textBox2.Text)
                {
                    Main.Table_Fill("Заказ", "select * from dogovor");

                    BD bd = new BD();
                    Main.tabControl1.TabPages.RemoveAt(0);
                    Main.tabControl1.TabPages.RemoveAt(0);
                    Main.tabControl1.Controls.Add(bd.tabControl1.TabPages[0]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.tabControl1.Controls.Remove(Main.tabControl1.SelectedTab);
        }
        private void button3_click(object sender, EventArgs e)
        {
            string sql = "Insert into client (name, address, phone) values('" + textBox5.Text + "', '" + textBox4.Text + "', '" + maskedTextBox1.Text + "')";
            if (!Main.Modification_Execute(sql))
                return;
        }
    }
}
