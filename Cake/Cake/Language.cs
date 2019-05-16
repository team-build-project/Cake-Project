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
    public partial class Language : Form
    {
        public Language()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (btnContinue.Text == "Continue")
            {
                btnContinue.Text = "បន្ត";
                Lancall.language = "khmer";
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (btnContinue.Text == "បន្ត")
            {
                btnContinue.Text = "Continue";
                Lancall.language = "usa";
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Login().ShowDialog();
            this.Close();
        }

        private void Language_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (Lancall.language == "khmer")
            {
                btnContinue.Text = "បន្ត";
                
            }
            else
            {
                btnContinue.Text = "Continue";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
