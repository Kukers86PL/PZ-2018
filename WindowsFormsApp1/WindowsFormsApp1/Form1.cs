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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static String connectionString = "Data Source=KUKI-LAPTOP\\SQLEXPRESS;Initial Catalog=PW_DB;Integrated Security=True";
        public SqlConnection connection = null;
        public String id_pacjenta = "";
        public String id_zabiegu = "";
        public String id_zlecenia = "";

        public void refresh_pacjenci()
        {
            String sqlQuery = "select * from pacjenci";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            listView1.Items.Clear();

            while (reader.Read())
            {
                String[] row = { reader["imie"].ToString(), reader["nazwisko"].ToString() };
                listView1.Items.Add(reader["id"].ToString()).SubItems.AddRange(row);
            }

            reader.Close();
        }

        public void refresh_zabiegi()
        {
            String sqlQuery = "select * from zabiegi";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            listView2.Items.Clear();

            while (reader.Read())
            {
                String[] row = { reader["nazwa"].ToString(), reader["cena"].ToString() };
                listView2.Items.Add(reader["id"].ToString()).SubItems.AddRange(row);
            }

            reader.Close();
        }

        public void refresh_zlecone(String pacjent)
        {
            String sqlQuery = "select zl.id as id, za.nazwa as nazwa from zlecenia as zl, zabiegi as za where za.id=zl.id_zabiegu and zl.id_pacjenta=" + pacjent;
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            listView3.Items.Clear();

            while (reader.Read())
            {
                listView3.Items.Add(reader["id"].ToString()).SubItems.Add(reader["nazwa"].ToString());
            }

            reader.Close();
        }

        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            connection.Open();

            listView1.View = View.Details;
            listView1.Columns.Add(new ColumnHeader());
            listView1.Columns[0].Text = "ID";
            listView1.Columns[0].Width = 30;
            listView1.Columns.Add(new ColumnHeader());
            listView1.Columns[1].Text = "Imie";
            listView1.Columns[1].Width = 100;
            listView1.Columns.Add(new ColumnHeader());
            listView1.Columns[2].Text = "Nazwisko";
            listView1.Columns[2].Width = 100;
            listView1.FullRowSelect = true;

            listView2.View = View.Details;
            listView2.Columns.Add(new ColumnHeader());
            listView2.Columns[0].Text = "ID";
            listView2.Columns[0].Width = 30;
            listView2.Columns.Add(new ColumnHeader());
            listView2.Columns[1].Text = "Nazwa";
            listView2.Columns[1].Width = 100;
            listView2.Columns.Add(new ColumnHeader());
            listView2.Columns[2].Text = "Cena";
            listView2.Columns[2].Width = 100;
            listView2.FullRowSelect = true;

            listView3.View = View.Details;
            listView3.Columns.Add(new ColumnHeader());
            listView3.Columns[0].Text = "ID";
            listView3.Columns[0].Width = 30;
            listView3.Columns.Add(new ColumnHeader());
            listView3.Columns[1].Text = "Nazwa";
            listView3.Columns[1].Width = 100;
            listView3.FullRowSelect = true;
        }

        ~Form1()
        {
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(this, connection);
            form.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_pacjenta = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
            refresh_zlecone(id_pacjenta);
            label1.Text = id_pacjenta;
        }
    
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String id = listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text;
                String sqlQuery = "delete from pacjenci where id=" + id;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                refresh_pacjenci();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            refresh_pacjenci();
            refresh_zabiegi();
        }

        private void Form1_EnabledChanged(object sender, EventArgs e)
        {
            refresh_pacjenci();
            refresh_zabiegi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3(this, connection);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                String id = listView2.Items[listView2.FocusedItem.Index].SubItems[0].Text;
                String sqlQuery = "delete from zabiegi where id=" + id;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                refresh_zabiegi();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                String sqlQuery = "insert into zlecenia (id_pacjenta, id_zabiegu) values ( " + id_pacjenta + ", " + id_zabiegu +")";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                refresh_zlecone(id_pacjenta);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                String sqlQuery = "delete from zlecenia where id=" + id_zlecenia;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                refresh_zlecone(id_pacjenta);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_zabiegu = listView2.Items[listView2.FocusedItem.Index].SubItems[0].Text;
            label2.Text = id_zabiegu;
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_zlecenia = listView3.Items[listView3.FocusedItem.Index].SubItems[0].Text;
            label3.Text = id_zlecenia;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                String sqlQuery = "select sum(za.cena) as suma from zlecenia as zl, zabiegi as za where za.id = zl.id_zabiegu and zl.id_pacjenta = " + id_pacjenta;
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                listView3.Items.Clear();

                while (reader.Read())
                {
                    label4.Text = reader["suma"].ToString();
                }

                reader.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                refresh_zlecone(id_pacjenta);
            }
        }
    }
}
