using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OTK;

namespace OTK
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public static MySqlConnection connection = new MySqlConnection("Server=localhost; Port=3306; Database=otk; Uid=root; Pwd=159357;");
        public static DataSet ds = new DataSet();
        public static TabControl tabControl1 = new TabControl();

        public static void Table_Fill(string name, string sql)
        {
            if (ds.Tables[name] != null)
                ds.Tables[name].Clear();
            MySqlDataAdapter dat;
            dat = new MySqlDataAdapter(sql, connection);
            dat.Fill(ds, name);
            connection.Close();
        }


        public static bool Modification_Execute(string sql)
        {
            MySqlCommand com;
            com = new MySqlCommand(sql, connection);
            connection.Open();
            try
            {
                com.ExecuteNonQuery();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Обновление базы данных не было выполнено либо из-за некорректно указанных" + " обновляемых данных  либо отсутствующих, но при этом обязательных!!!", "Ошибка");
                connection.Close(); return false;
            }
            connection.Close();
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.Size = new Size(1184, 647);
            this.Controls.Add(tabControl1);
            Main.Table_Fill("Клиент", "Select * from client order by id");
            Main.Table_Fill("Услуги", "Select * from type order by id");
            Main.Table_Fill("Сотрудники", "Select * from worker order by id");

            LogIn login = new LogIn();
            tabControl1.Controls.Add(login.tabControl1.TabPages[0]);
            tabControl1.Controls.Add(login.tabControl1.TabPages[0]);
        }
    }
}