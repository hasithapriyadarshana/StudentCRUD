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

namespace StudentCRUD
{
    public partial class Form1 : Form
    {
        string conStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\BICT\Semester3\ITC 2303 Visual Application Programming\StudentCRUD\StudentDB.mdf"";Integrated Security=True";
        int selectedId = 0;
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Students", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "INSERT INTO Students (Name, Age, Email) VALUES (@Name, @Age, @Email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added");
                LoadData();
                ClearFields();

            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedId == 0) return;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "UPDATE Students SET Name=@Name, Age=@Age, Email=@Email WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", selectedId);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
                LoadData();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0) return;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "DELETE FROM Students WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", selectedId);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted");
                LoadData();
                ClearFields();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells[0].Value);
                txtName.Text = row.Cells[1].Value.ToString();
                txtAge.Text = row.Cells[2].Value.ToString();
                txtEmail.Text = row.Cells[3].Value.ToString();
            }
        }
        private void ClearFields()
        {
            txtName.Clear();
            txtAge.Clear();
            txtEmail.Clear();
            selectedId = 0;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblStudentRegistrationSystem_Click(object sender, EventArgs e)
        {

        }
    }
}
