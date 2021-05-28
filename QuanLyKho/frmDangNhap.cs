using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using QuanLyKho.Class;

namespace QuanLyKho
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        public static SqlConnection Conn;
        private void ResetValues()
        {
            txtTenDN.Text = "";
            txtPass.Text = "";
            txtTenDN.Focus();
        }
        private void btDangNhap_Click(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            if ((txtTenDN.Text ==""))
            {
                MessageBox.Show("Hãy nhập tên đăng nhập!!!", "Yêu cầu ...",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((txtPass.Text == ""))
            {
                MessageBox.Show("Hãy nhập mật khẩu!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if((txtTenDN.Text != "") && (txtPass.Text != ""))
            {
                string connString = "Data Source=DESKTOP-ACG6RK9\\SQLEXPRESS01;Initial Catalog=QuanLyKho;Integrated Security=True";
                Conn = new SqlConnection();                 //Cấp phát đối tượng
                Conn.ConnectionString = connString;         //Kết nối
                Conn.Open();
                string sql = "select * from Users where UserName = '"+txtTenDN.Text+"' and Pass_word ='"+txtPass.Text+"'";
                SqlCommand cmd = new SqlCommand(sql,Conn);
                SqlDataReader dta = cmd.ExecuteReader();
                if(dta.Read()==true)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    this.Hide();
                    frmMain f1 = new frmMain();
                    f1.Show();
                }    
                else
                {
                    lblSaiPass.Text = "Thông tin đăng nhập không đúng. Vui lòng nhập lại!";
                    ResetValues();
                }    
            }  
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát khỏi chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if(checkShowPass.Checked)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }
    }
}
