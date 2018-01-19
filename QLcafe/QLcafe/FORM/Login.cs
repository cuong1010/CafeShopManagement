using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLcafe.Model;
using QLcafe.DATA;
using System.IO;

namespace QLcafe.FORM
{
    public partial class Login : Form
    {
        CafeEntities db = new CafeEntities();
        public static string MaNguoiDung = null;
        public static int Level;
        
        public Login()
        {
            InitializeComponent();
        }

        void LoginEvent()
        {
            bool flag = false;
            try
            {
                var a = (List<NguoiDung>)new Database().Laytatcathongtin("NguoiDungs", "TaiKhoan", "=", txtAcc.Text);
                foreach (var i in a)
                    if (i.MatKhau.Equals(txtPass.Text))
                        if (i.QuyenHan == -1) MessageBox.Show("TK bi khoa");
                        else
                        {
                            MaNguoiDung = i.MaNguoiDung;
                            Level = i.QuyenHan; flag = true;
                            break;                         
                        }
                if (flag) Dispose(); else MessageBox.Show("Sai thong tin dang nhap");
            }
            catch
            {
                MessageBox.Show("Sai thong tin dang nhap");
            }
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            MaNguoiDung = null;
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Keys.Enter.GetHashCode()) LoginEvent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginEvent();
        }
    }
}
