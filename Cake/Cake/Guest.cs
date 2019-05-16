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

namespace Cake
{
    public partial class Guest : Form
    {
        public Guest()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Initial Catalog=Cake; Integrated Security=true");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter da;

        int uid = 0;
        int getId = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Guest_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (Lancall.language == "khmer")
            {
                lblName.Text = "ឈ្មោះ";
                lblPhone.Text = "ទូរស័ព្ទ";
                btnEnter.Text = "ចូល";
                btnBack.Text = "ត្រឡប់";
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
            this.Close();
        }
        void Check()
        {
            con.Open();
            com = new SqlCommand("select * from tblUser where name='" + txtName.Text + "'and phone='" + txtPhone.Text + "'", con);
            SqlDataReader reader = com.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = count + 1;
            }

            if (count == 0)
            {
                reader.Close();

                com = new SqlCommand("insertUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", uid);
                com.Parameters.AddWithValue("@N", txtName.Text);
                com.Parameters.AddWithValue("@P", txtPhone.Text);
                com.ExecuteNonQuery();
                con.Close();
            }
            Constant.name = txtName.Text;
            Constant.phone = txtPhone.Text;
            Constant.id = uid;

            this.Hide();
            new Caketype().ShowDialog();
            this.Close();
            con.Close();
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if(txtName.Text=="" || txtName.Text == null)
            {
                MessageBox.Show("Name is Null");
            }else if(txtPhone.Text==""|| txtPhone.Text == null)
            {
                MessageBox.Show("Phone is Null");
            }
            else
            {
                Check();
            }

        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.Hide();
            //    new Caketype().ShowDialog();
            //    this.Close();
            //}
        }
    }
}
