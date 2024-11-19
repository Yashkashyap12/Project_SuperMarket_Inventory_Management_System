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

namespace SuperMarket_Management_System
{
    public partial class OrderForm : Form

    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }
        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT orderid,orderdate,O.pid,P.pname,O.cid,C.cname,qty,price,total FROM tblOrder AS O JOIN tblCustomer AS C ON O.cid=C.cid JOIN tblProduct AS P ON O.pid=P.pid WHERE CONCAT(orderid,orderdate,O.pid,P.pname,O.cid,C.cname,qty,price) LIKE'%"+txtSearchbox.Text+"%'", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(),Convert.ToDateTime( dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total+= Convert.ToInt32(dr[8].ToString());
            }
            dr.Close();
            conn.Close();

            lblqty.Text = i.ToString();
            lbltotal.Text = total.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to delete this order?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM tblOrder WHERE orderid LIKE'" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Order has been successfully deleted!");

                    cmd = new SqlCommand("UPDATE tblProduct SET pqty = (pqty+@pqty) WHERE pid LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "' ", conn);
                    cmd.Parameters.AddWithValue("@pqty", Convert.ToInt32(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            LoadOrder();
        }

        private void txtSearchbox_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }
    }
}
