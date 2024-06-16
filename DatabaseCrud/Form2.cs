using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseCrud
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        OleDbDataAdapter dadapter;
        DataSet dset;
        string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;" + "data source=" + Application.StartupPath +"/Database.accdb";

        private void Form2_Load(object sender, EventArgs e)
        {
            loadData();
          
        }

        private void loadData()
        {
            dadapter = new OleDbDataAdapter("select * from students", connstring);
            dset = new System.Data.DataSet();
            dadapter.Fill(dset);
            dataGridView1.DataSource = dset.Tables[0].DefaultView;
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "LastName";
            dataGridView1.Columns[2].HeaderText = "FirstName";
            dataGridView1.Columns[3].HeaderText = "Course";
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + "data source=" + Application.StartupPath + "/Database.accdb";

            string dbcommand = "INSERT into students (lname, fname, course) " + " VALUES ('" +textBox1.Text + "','" +textBox2.Text + "','" + textBox3.Text  +"')";

            OleDbDataAdapter DataAdapterTest = new OleDbDataAdapter(dbcommand, dbconnection);
            DataSet StudentDataSet = new DataSet();
            if (!textBox1.Text.Equals("") && !textBox2.Text.Equals("") && !textBox3.Text.Equals(""))
            {
                DataAdapterTest.Fill(StudentDataSet);
                loadData();
                MessageBox.Show("Record Submitted", "Congrats");
            }
            else
                MessageBox.Show("Please complete entries", "Error");
        }

        private OleDbConnection GetConnection()
        {
            OleDbConnection con = new OleDbConnection(connstring);
            
            return con;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = GetConnection();
            conn.Open();
            String MyString = "DELETE FROM students WHERE Id = @1" ;
            OleDbCommand command = new OleDbCommand(MyString, conn);
            command.Parameters.AddWithValue("@1",textBox4.Text);
            DialogResult ans = MessageBox.Show("Delete record", "Confirm", MessageBoxButtons.YesNo);
            if (ans == DialogResult.Yes)
            {
                command.ExecuteNonQuery();
                conn.Close();
                loadData();
                MessageBox.Show("Record Deleted", "Success");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = GetConnection();
            conn.Open();
            String MyString = "UPDATE students set lname=@1,fname=@2,course=@3 WHERE Id = @4";
            OleDbCommand command = new OleDbCommand(MyString, conn);
            command.Parameters.AddWithValue("@1", textBox1.Text);
            command.Parameters.AddWithValue("@2", textBox2.Text);
            command.Parameters.AddWithValue("@3", textBox3.Text);
            command.Parameters.AddWithValue("@4", textBox4.Text);
            if (!textBox1.Text.Equals("") && !textBox2.Text.Equals("") && !textBox3.Text.Equals("") && !textBox4.Text.Equals(""))
            {
                command.ExecuteNonQuery();
                conn.Close();
                loadData();
                MessageBox.Show("Record Updated", "Success");
            }
            else
                MessageBox.Show("Please select a record and complete entries", "Error");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void Form2_Deactivate(object sender, EventArgs e)
        {
          
        }
    }
}
