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
    public partial class CustomerForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=LAPTOP-49L09RTP\SQLEXPRESS;Initial Catalog=SuperMarketInventoryDb;Integrated Security=True;");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tblCustomer", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CustomerModuleForm moduleForm = new CustomerModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadCustomer();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm customerModule = new CustomerModuleForm();
                customerModule.lblCustomerId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.txtCustomerName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModule.txtCustomerPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();

                customerModule.btnSave.Enabled = false;
                customerModule.btnUpdate.Enabled = true;
                customerModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure want to delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM tblCustomer WHERE cid LIKE'" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadCustomer();
        }
    }
}
