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
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Initial Catalog=Cake; Integrated Security=true");
        SqlCommand com = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt;

        int id;

        void ShowData()
        {
            con.Open();
            da = new SqlDataAdapter("select * from selectOrder('" + Constant.id + "')", con);
            dt = new DataTable();
            da.Fill(dt);
            //dt.DefaultView.Sort="dec"
            dataGridView1.DataSource = dt;
            con.Close();
            dataGridView1.ClearSelection();
        }
        void DesData()
        {
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Width = 215;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 58;

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Cake Name";
            dataGridView1.Columns[2].HeaderText = "Cake Type";
            dataGridView1.Columns[3].HeaderText = "Cake Price";
            dataGridView1.Columns[4].HeaderText = "Status";
            dataGridView1.Columns[5].HeaderText = "Cus ID";
            dataGridView1.RowTemplate.Height = 500;
        }
        private void Order_Load(object sender, EventArgs e)
        {
            if (Lancall.language == "khmer")
            {
                ShowData();
                lblCOL.Text = "តារាងនំដែលបានកម្មង់";
                dataGridView1.Columns[0].Width = 0;
                dataGridView1.Columns[1].Width = 215;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 58;

                dataGridView1.Columns[0].HeaderText = "លេខរៀង";
                dataGridView1.Columns[1].HeaderText = "ឈ្មោះនំ";
                dataGridView1.Columns[2].HeaderText = "ប្រភេទនំ";
                dataGridView1.Columns[3].HeaderText = "តំលៃនំ";
                dataGridView1.Columns[4].HeaderText = "សំគាល់";
                dataGridView1.Columns[5].HeaderText = "លេខសំគាល់ភ្ញៀវ";
            }
            else
            {
                ShowData();
                DesData();
            }
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //deleteCake();
            //ShowData();
            //DesData();
            if (Lancall.button == "true")
            {
                deleteCake();
                this.Close();
                new Order().ShowDialog();
            }
            MessageBox.Show(id.ToString());
        }
        void deleteCake()
        {
            id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            con.Open();
            com = new SqlCommand("deleteOrder", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ID", id);
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
