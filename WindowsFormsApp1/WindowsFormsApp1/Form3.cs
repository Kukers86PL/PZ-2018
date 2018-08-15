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
    public partial class Form3 : Form
    {
        public Form form = null;
        public SqlConnection connection = null;

        public Form3(Form form, SqlConnection connection)
        {
            InitializeComponent();
            this.form = form;
            this.connection = connection;
            this.form.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sqlQuery = "insert into zabiegi (nazwa, cena) values (\'" + textBox1.Text + "\', " + numericUpDown1.Value.ToString() + ")";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            form.Enabled = true;
            form.Show();
            this.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            form.Enabled = true;
        }
    }
}
