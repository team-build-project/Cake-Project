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
    public partial class Caketype : Form
    {
        public Caketype()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Initial Catalog=Cake; Integrated Security=true");
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        
        private void Caketype_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblName.Text = Constant.name;
            lblPhone.Text = Constant.phone;
            //btnBack.Text = Lancall.language == "khmer" ? btnBack.Text = "ត្រឡប់" : btnBack.Text = "Back";
            if (Lancall.language=="khmer")
            {
                btnBack.Text = "ត្រឡប់";
                lblCaketype.Text = "ប្រភេទនំ";
                lblIcecreamcake.Text = "នំការរ៉េម";
                lblBirthdaycake.Text = "នំខួបកំណើត";
                lblSpecialcake.Text = "នំពិសេស";
                lblBread.Text = "នំប៉ាំង";
            }
            else
            {
                btnBack.Text = "Back";
                lblCaketype.Text = "Cake type";
                lblIcecreamcake.Text = "Ice Cream Cake";
                lblBirthdaycake.Text = "Birthday Cake";
                lblSpecialcake.Text = "Special Cake";
                lblBread.Text = "Bread";
            }

            con.Open();
            com = new SqlCommand("select userid from tblUser where name = '" + Constant.name + "' and phone = '" + Constant.phone + "'", con);
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                Constant.id = dr.GetInt32(0);
            }
            //MessageBox.Show(Constant.id.ToString());
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Guest().ShowDialog();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (btnBack.Text== "ត្រឡប់")
            {
                Lancall.language = "khmer";
            }
            this.Hide();
            new IceCreamCake().ShowDialog();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new BirthdayCake().ShowDialog();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SpecialCake().ShowDialog();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Bread().ShowDialog();
            this.Close();
        }
    }
}
