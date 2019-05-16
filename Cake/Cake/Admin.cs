using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cake
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (Lancall.language == "khmer")
            {
                lblUsername.Text = "ឈ្មោះអ្នកប្រើ";
                lblPassword.Text = "ពាក្យសំងាត់";
                btnBack.Text = "ត្រឡម់";
                btnEnter.Text = "ចូល";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
            this.Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtUsername.Text == null)
            {
                MessageBox.Show("Username is Null");
            }
            else if (txtPassword.Text == "" || txtPassword.Text == null)
            {
                MessageBox.Show("Password is Null");
            }else if (txtUsername.Text == "admin" && txtPassword.Text == "admin")
            {
                this.Hide();
                new ManageCake().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect");
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Hide();
                new ManageCake().ShowDialog();
                this.Close();
            }
        }
    }
}
