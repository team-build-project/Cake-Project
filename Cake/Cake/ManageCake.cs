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
using System.IO;
using System.Drawing.Imaging;
using Microsoft.VisualBasic;

namespace Cake
{
    public partial class ManageCake : Form
    {
        public ManageCake()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Initial Catalog=Cake; Integrated Security=true");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter da;
        DataTable dt;
        OpenFileDialog ofd = new OpenFileDialog();

        int cid = 0;
        MemoryStream ms;
        byte[] photo;
        string fp;

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        void ShowData()
        {
            con.Open();
            da = new SqlDataAdapter("select * from selectCake()", con);
            dt = new DataTable();
            da.Fill(dt);
            dt.DefaultView.Sort = "id desc";
            dataGridView1.DataSource = dt;

            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img = (DataGridViewImageColumn)dataGridView1.Columns["cakepicture"];
            img.ImageLayout = DataGridViewImageCellLayout.Stretch;
            con.Close();
            dataGridView1.ClearSelection();
        }
        void DesData()
        {
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 400;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Width = 200;
            //dataGridView1.RowTemplate.Height = 500;
        }
        private void ManageCake_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (Lancall.language == "khmer")
            {
                lblName.Text = "ឈ្មោះ";
                lblType.Text = "ប្រភេទ";
                lblPrice.Text = "តំប្លៃ";
                lblBrowse.Text = "រករូប";
                lblOrderList.Text = "ចូលទៅតារាងកុម៉ង់";
                btnAdd.Text = "បញ្ចូល";
                btnUpdate.Text = "កែប្រែ";
                btnDelete.Text = "លុប";
                btnBack.Text = "ត្រឡប់";
            }
            ShowData();
            DesData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Admin().ShowDialog();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            new AdminOrderList().ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtName.Text == null)
            {
                MessageBox.Show("Name is Null");
            }else if (cboCakeType.Text == "" || cboCakeType.Text == null)
            {
                MessageBox.Show("CakeType is Null");
            }else if (txtPrice.Text == "" || txtPrice.Text == null)
            {
                MessageBox.Show("Price is Null");
            }
            else if (picCake.Image == null)
            {
                MessageBox.Show("Photo is Null");
            }
            else
            {
                photo = File.ReadAllBytes(fp);
                con.Open();
                com = new SqlCommand("insertCake", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", cid);
                com.Parameters.AddWithValue("@CN", txtName.Text);
                com.Parameters.AddWithValue("@CT", cboCakeType.Text);
                com.Parameters.AddWithValue("@CP", txtPrice.Text);
                com.Parameters.AddWithValue("@CPI", photo);
                com.ExecuteNonQuery();
                con.Close();
                ShowData();
                ClearText();
            }
        }

        private void lblBrowse_Click(object sender, EventArgs e)
        {
            ofd.Filter = "All Files |";
            ofd.Title = "Insert Picture";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fp = ofd.FileName;
                picCake.Image = Image.FromFile(fp);
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) {
                return;
            }
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cboCakeType.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            photo = (Byte[])row.Cells[4].Value;
            ms = new MemoryStream(photo);
            picCake.Image = Image.FromStream(ms);

            cid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (fp != null) { 
                photo = File.ReadAllBytes(fp);
            }

            con.Open();
            com = new SqlCommand("updateCake", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ID", cid);
            com.Parameters.AddWithValue("@CN", txtName.Text);
            com.Parameters.AddWithValue("@CT", cboCakeType.Text);
            com.Parameters.AddWithValue("@CP", txtPrice.Text);
            com.Parameters.AddWithValue("@CPI", photo);
            com.ExecuteNonQuery();
            con.Close();
            ShowData();
            MessageBox.Show("");
        }
        void ClearText()
        {
            txtName.Clear();
            txtPrice.Clear();
            cboCakeType.Text = "";
            //picCake.Image = Image.FromFile(ofd.FileName);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            com = new SqlCommand("deleteCake", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ID", cid);
            com.ExecuteNonQuery();
            con.Close();
            ShowData();
            ClearText();
        }
    }
}
