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
    public partial class AdminOrderList : Form
    {
        public AdminOrderList()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Initial Catalog=Cake; Integrated Security=true");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter da;
        SqlDataReader dr;
        DataTable dt;

        int id = 0;
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void ShowData()
        {
            con.Open();
            da = new SqlDataAdapter("select * from selectAdminOrder()", con);
            dt = new DataTable();
            da.Fill(dt);
            dt.DefaultView.Sort = "name asc";
            dataGridView1.DataSource = dt;
            con.Close();
            dataGridView1.ClearSelection();
            DesData();
        }
        void DesData()
        {
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].Width = 155;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 75;
            dataGridView1.Columns[5].Width = 178;

            dataGridView1.Columns[1].HeaderText = "Cake Name";
            dataGridView1.Columns[2].HeaderText = "Cake Type";
            dataGridView1.Columns[3].HeaderText = "Cake Price";
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[5].HeaderText = "Name";
            dataGridView1.Columns[6].HeaderText = "Phone";

        }
        private void AdminOrderList_Load(object sender, EventArgs e)
        {
            if (Lancall.language == "khmer")
            {
                lblCOL.Text = "តារាងកុម៉ង់របស់ភ្ញៀវ";
                btnDone.Text = "បានទិញហើយ";
            }
            ShowData();
        }
        void deleteRow()
        {
            //string delete="delete form tblOrder inner join tblUser on "
            //con.Open();
            //com=new SqlCommand("delete from tblOrder where ")
            //con.Close();

            //delete tblOrder from tblOrder
            //inner join tblUser
            //on tblOrder.userid = tblUser.userid
        }
        private void btnDone_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                //dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                con.Open();
                com = new SqlCommand("updateOrderStatus", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ID", id);
                com.ExecuteNonQuery();
                con.Close();
                ShowData();
                MessageBox.Show("Done");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                dataGridView1.ClearSelection();
                return;
            }
            btnDone.Enabled = true;
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
        }
    }
}
