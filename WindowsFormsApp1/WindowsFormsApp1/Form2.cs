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
    public partial class Form2 : Form
    {
        public Form form = null;
        public SqlConnection connection = null;

        public Form2(Form form, SqlConnection connection)
        {
            InitializeComponent();
            this.form = form;
            form.Enabled = false;
            this.connection = connection;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sqlQuery = "insert into pacjenci(imie, nazwisko) values(\'" + textBox1.Text + "\', \'" + textBox2.Text + "\')";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            form.Enabled = true;
            form.Show();
            this.Close();
        }
    }
}
