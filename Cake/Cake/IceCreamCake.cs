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
    public partial class IceCreamCake : Form
    {
        public IceCreamCake()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Initial Catalog=Cake; Integrated Security=true");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt;

        int orderid = 0;

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (btnBack.Text == "back")
            {
                Lancall.language = "usa";
            }
            this.Hide();
            new Caketype().ShowDialog();
            this.Close();
        }
        void ShowData()
        {
            con.Open();
            da = new SqlDataAdapter("select * from selectIceCreamCake()", con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img = (DataGridViewImageColumn)dataGridView1.Columns["cakepicture"];
            img.ImageLayout = DataGridViewImageCellLayout.Stretch;
            con.Close();
            DesData();
        }
        private void IceCreamCake_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (Lancall.language == "khmer")
            {
                btnBack.Text = "ត្រឡប់";
                lblIcecreamcake.Text = "នំការរ៉េម";
                ShowData();
                dataGridView1.Columns[1].HeaderText = "ឈ្មោះនំ";
                dataGridView1.Columns[2].HeaderText = "ប្រភេទនំ";
                dataGridView1.Columns[3].HeaderText = "តំលៃនំ";
                dataGridView1.Columns[4].HeaderText = "រូបនំ";
            }
            else
            {
                btnBack.Text = "Back";
            }

            ShowData();
        }
        void DesData()
        {
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 400;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Width = 200;
            //dataGridView1.RowTemplate.Height = 500;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (dataGridView1.CurrentRow.Selected == true)
            {
                con.Open();
                com = new SqlCommand("insertOrder", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", orderid);
                com.Parameters.AddWithValue("@CN", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                com.Parameters.AddWithValue("@CT", dataGridView1.CurrentRow.Cells[2].Value.ToString());
                com.Parameters.AddWithValue("@CP", dataGridView1.CurrentRow.Cells[3].Value.ToString());
                com.Parameters.AddWithValue("@S", 0);
                com.Parameters.AddWithValue("@UID", Constant.id);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Row Clicked");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new Order().ShowDialog();
        }
        
    }
}
