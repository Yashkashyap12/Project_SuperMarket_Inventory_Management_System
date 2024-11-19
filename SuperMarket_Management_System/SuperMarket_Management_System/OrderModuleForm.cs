using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SuperMarket_Management_System
{
    public partial class OrderModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;

        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT cid, cname FROM tblCustomer WHERE CONCAT(cid,cname) LIKE '%"+txtSearchCustomer.Text+"%'", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            conn.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tblProduct WHERE CONCAT(pid, pname, pprice, pdescription, pcategory) LIKE '%" + txtSearchProduct.Text + "%' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt32(UDqty.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UDqty.Value = UDqty.Value - 1;
                return;
            }
            if (Convert.ToInt32(UDqty.Value) > 0)
            {
                int total = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(UDqty.Value);
                txtTotal.Text = total.ToString();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCustName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPname.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomId.Text == "")
                {
                    MessageBox.Show("Please Select Customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPid.Text == "")
                {
                    MessageBox.Show("Please Select Product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure want to Insert this Order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO tblOrder(orderdate,pid,cid,qty,price,total)VALUES(@orderdate,@pid,@cid,@qty,@price,@total)", conn);
                    cmd.Parameters.AddWithValue("@orderdate", dtOrder.Value);
                    cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                    cmd.Parameters.AddWithValue("@cid", Convert.ToInt32(txtCustomId.Text));
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(UDqty.Value));
                    cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Order has been successfully Inserted");
                    

                    cmd = new SqlCommand("UPDATE tblProduct SET pqty = (pqty-@pqty) WHERE pid LIKE '" + txtPid.Text + "' ", conn);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(UDqty.Value));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Clear();
                    LoadProduct();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCustomId.Clear();
            txtCustName.Clear();

            txtPid.Clear();
            txtPname.Clear();   

            txtPrice.Clear();
            UDqty.Value = 0;
            txtTotal.Clear();
            dtOrder.Value=DateTime.Now;
        }

        public void GetQty()
        {
            cmd = new SqlCommand("SELECT pqty FROM tblProduct WHERE pid = '" + txtPid.Text + "' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32( dr[0].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}