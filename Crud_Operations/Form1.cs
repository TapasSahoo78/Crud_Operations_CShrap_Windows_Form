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

namespace Crud_Operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-1JPM8GN\MSSQLSERVER01;Initial Catalog=STUDENTB;Integrated Security=True");
        public int StudentID;
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }


        public void GetStudentRecord()
        {
           
            SqlCommand cmd = new SqlCommand("select * from StudentTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            studentRecordDataGridView.DataSource = dt;

        }


        private void Insert_button_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentTb Values(@name,@FatherName,@Roll,@Address,@Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student is successfully saves in the Database", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormControls();

            }
        }


        private bool IsValid()
        {
            if(txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student name is required", "Failed" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        private void Reset_button_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            StudentID = 0 ; 
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtRollNumber.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

            txtStudentName.Focus();
        }

        private void studentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(studentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = studentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = studentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNumber.Text = studentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = studentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = studentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void Update_button_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentTb SET Name = @name,FatherName = @FatherName,RollNumber = @Roll,Address = @Address,Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please Select an student to update his information ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is Deleted Successfully", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please Select an student to delete his information ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
