 using System.ComponentModel;
using System.Data;
using System.Drawing;
 using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLcafe.DATA;
using QLcafe.Model;
using QLcafe.BUS;
using System.Data.SqlClient;

namespace QLcafe.FORM
{
    public partial class QuenMK : Form
    {
       BUS_QLTKNV bus = new BUS_QLTKNV();
        Database data=new Database();
        bool kt = false; string ma;

        private void button1_Click(object sender, System.EventArgs e)
        {

        }
    
     /*   public QuenMK()
        {
            InitializeComponent();
            button1.Enabled = false;
        }
        private bool Xacnhan()
        {
            string MK = txtMKMoi.Text;
            string NhaplaiMK = txtNhaplaiMK.Text;
            if (MK.Equals(NhaplaiMK)) return true;
            else
            {
                MessageBox.Show("Mat khau khong trung!");
                return false;
            }
        }
      

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        
        private void txtTK_TextChanged(object sender, EventArgs e)
        {
            ma = data.TimTaiKhoan(txtTK.Text);
            if (ma != null)
            {
                lbTK.ForeColor = Color.Green;
                lbTK.Text = "Có tài khoản này";
                kt = true;
            }
            else
            {
                lbTK.ForeColor = Color.Red;
                lbTK.Text = "Không có tài khoản này";
                kt = false;
            }
            if (kt) button1.Enabled = true; else button1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Xacnhan())
                bus.QuenMK(ma, txtMKMoi.Text, txtMK2.Text);
        }
      * */
    }
}
