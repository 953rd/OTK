using OTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZstdSharp.Unsafe;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace OTK
{
    public partial class dogovor : Form
    {
        public dogovor()
        {
            InitializeComponent();
        }
        public static int n = 0;
        private void FieldForm_Clear()
        {
            textBox1.Text = "0";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox5.Text = "";
            checkBox1.Checked = false;
            textBox1.Enabled = true; textBox1.Focus();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            Load();
        }

        private void Load()
        {
            Main.Table_Fill("Заказ", "Select dogovor.id, date, client.naming, type.name, worker.FIO, skidka, type.price, status from dogovor inner join type on dogovor.id_type = type.id inner join client on dogovor.id_client = client.id inner join worker on dogovor.id_worker = worker.id order by id");
            dataGridView1.DataSource = Main.ds.Tables["Заказ"].DefaultView;
            comboBox1.DataSource = Main.ds.Tables["Услуги"];
            comboBox1.DisplayMember = "name";

            comboBox2.DataSource = Main.ds.Tables["Сотрудники"];
            comboBox2.DisplayMember = "FIO";

            comboBox3.DataSource = Main.ds.Tables["Клиент"];
            comboBox3.DisplayMember = "naming";

            dataGridView1.Columns["id_client"].Visible = false;
            dataGridView1.Columns["id_worker"].Visible = false;
            dataGridView1.Columns["id_type"].Visible = false;
            dataGridView1.Columns["id"].HeaderText = "Код";
            dataGridView1.Columns["date"].HeaderText = "Дата";
            dataGridView1.Columns["naming"].HeaderText = "Клиент";
            dataGridView1.Columns["name"].HeaderText = "Услуга";
            dataGridView1.Columns["FIO"].HeaderText = "Код сотрудника";
            dataGridView1.Columns["skidka"].HeaderText = "Скидка %";
            dataGridView1.Columns["price"].HeaderText = "Цена";
            dataGridView1.Columns["status"].HeaderText = "Статус";
            dataGridView1.Columns["id"].DisplayIndex = 0;
            dataGridView1.Columns["date"].DisplayIndex = 1;
            dataGridView1.Columns["naming"].DisplayIndex = 2;
            dataGridView1.Columns["name"].DisplayIndex = 3;
            dataGridView1.Columns["FIO"].DisplayIndex = 4;
            dataGridView1.Columns["skidka"].DisplayIndex = 5;
            dataGridView1.Columns["price"].DisplayIndex = 6;
            dataGridView1.Columns["status"].DisplayIndex = 7;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoResizeColumns();
            dataGridView1.CurrentCell = null;
            if (Main.ds.Tables["Заказ"].Rows.Count > n)
            {
                FieldForm_Fill();
            }
        }
        private void FieldForm_Fill()
        {
            textBox1.Text = Main.ds.Tables["Заказ"].Rows[n]["id"].ToString();
            dateTimePicker1.Text = Main.ds.Tables["Заказ"].Rows[n]["date"].ToString();
            comboBox1.Text = Main.ds.Tables["Заказ"].Rows[n]["name"].ToString();
            comboBox2.Text = Main.ds.Tables["Заказ"].Rows[n]["FIO"].ToString();
            comboBox3.Text = Main.ds.Tables["Заказ"].Rows[n]["naming"].ToString();
            textBox5.Text = Main.ds.Tables["Заказ"].Rows[n]["skidka"].ToString();
           
            checkBox1.Checked = Convert.ToInt32(Main.ds.Tables["Заказ"].Rows[n]["status"]) == 1;

            textBox1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (n < Main.ds.Tables["Заказ"].Rows.Count) n++;
            if (Main.ds.Tables["Заказ"].Rows.Count > n)
            {
                FieldForm_Fill();
            }
            else
            {
                FieldForm_Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FieldForm_Clear();
            n = Main.ds.Tables["Заказ"].Rows.Count;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (n > 0)
            {
                n--; FieldForm_Fill();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Main.ds.Tables["Заказ"].Rows.Count > 0)
            {
                n = 0; FieldForm_Fill();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string sql;
            string Id3 = Main.ds.Tables["Клиент"].DefaultView[comboBox3.SelectedIndex]["id"].ToString();
            string Id1 = Main.ds.Tables["Услуги"].DefaultView[comboBox1.SelectedIndex]["id"].ToString();
            string Id2 = Main.ds.Tables["Сотрудники"].DefaultView[comboBox2.SelectedIndex]["id"].ToString();
            if (n < Main.ds.Tables["Заказ"].Rows.Count)
            {
                sql = "Update dogovor set date = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', id_client =" + Id3 + ", id_type =" + Id1 + ", id_worker =" + Id2 + ", skidka =" + textBox5.Text + ", status =" + checkBox1.Checked + " where id = " + textBox1.Text;
                if (!Main.Modification_Execute(sql))
                    return;
            }
            else
            {
                sql = "Insert into dogovor (id, date, id_client, id_type, id_worker, skidka, status) values(" + textBox1.Text + ", '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " + Id3 + ", " + Id1 + ", " + Id2 + ", " + textBox5.Text + ", " + checkBox1.Checked + ")";
                if (!Main.Modification_Execute(sql))
                    return;
                textBox1.Enabled = false;
            }
            Load();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string message = "Вы точно хотите удалить Заказ с кодом " + textBox1.Text + "?";
            string caption = "Удаление Заказа";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            string sql1 = "Delete from dogovor where id =" + textBox1.Text;
            Main.Modification_Execute(sql1);

            Main.ds.Tables["Заказ"].Rows.RemoveAt(n);

            if (Main.ds.Tables["Заказ"].Rows.Count > n)
                FieldForm_Fill();
            else
                FieldForm_Clear();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Main.tabControl1.Controls.Remove(Main.tabControl1.SelectedTab);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int test;
            string on = Main.ds.Tables["Услуги"].DefaultView[comboBox1.SelectedIndex]["id"].ToString();
            int i = int.Parse(on);
            string l = Main.ds.Tables["Услуги"].Rows[i - 1]["price"].ToString();
            int n = int.Parse(l);
            if (textBox5.Text != "")
            {
                try
                {
                    test = Convert.ToInt32(textBox5.Text);
                    if (test > 100 | test < 0)
                    { test = 0; }
                }
                catch (Exception)
                {
                    test = 0;
                }
            }
            else
            { test = 0; }
            int h = n - (n * test / 100);
            textBox7.Text = h.ToString();
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string on = Main.ds.Tables["Услуги"].DefaultView[comboBox1.SelectedIndex]["id"].ToString();
            int i;
            if (comboBox1.Text == "")
            {
                textBox7.Text = "0";
                return;
            }
            else { i = int.Parse(on); }
            string l = Main.ds.Tables["Услуги"].Rows[i - 1]["price"].ToString();
            int n = int.Parse(l);
            int test;
            try
            {
                test = Convert.ToInt32(textBox5.Text);
                if (test > 100 | test < 0)
                { test = 0; }
            }
            catch (Exception)
            {
                test = 0;
            }

            int h = n - (n * test / 100);
            textBox7.Text = h.ToString();
        }
    }
}
