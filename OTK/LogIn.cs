using MySql.Data.MySqlClient;
using OTK;
using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
using System.Data.OleDb;
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
            string workerFIO;
            try
                {
                    if (Main.ds.Tables["Сотрудник"].Rows[0]["login"].ToString() == textBox1.Text && Main.ds.Tables["Сотрудник"].Rows[0]["password"].ToString() == textBox2.Text)
                    {
                        Main.Table_Fill("Заказ", "select * from dogovor");              
                    if (Main.ds.Tables["Сотрудник"].Rows[0]["login"].ToString() == textBox1.Text)
                        {
                            var sql1 = "select FIO from worker where login = @login";
                            var command = new MySqlCommand(sql1, Main.connection);
                            command.Parameters.AddWithValue("@login", textBox1.Text);
                            var adapter = new MySqlDataAdapter(command);
                            var dataset = new DataSet();
                            adapter.Fill(dataset, "worker");
                            workerFIO = dataset.Tables["worker"].Rows[0]["FIO"].ToString();
                        BD bd = new BD(workerFIO);
                        Main.tabControl1.TabPages.RemoveAt(0);
                        Main.tabControl1.TabPages.RemoveAt(0);
                        Main.tabControl1.Controls.Add(bd.tabControl1.TabPages[0]);
                    }
                    
                        
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
