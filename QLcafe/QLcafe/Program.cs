using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLcafe.FORM;
using QLcafe.DATA;
using QLcafe.Model;


namespace QLcafe
{   
    static class Program
    {   
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {   
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            do
            { 
                Application.Run(new Login());
                if(Login.MaNguoiDung!=null)
                {
                    if (Login.Level == 0) Application.Run(new Admin());
                    else Application.Run(new NhanVien()); 
                }
                
            } while (Login.MaNguoiDung != null);           
        }
    }
}
