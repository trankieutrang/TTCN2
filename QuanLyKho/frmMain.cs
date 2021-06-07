using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKho
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mstripDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?","Thông báo",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Hide();
                frmDangNhap f = new frmDangNhap();
                f.Show();
            }
        }

        private void mstripHangTonKho_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHangTonKho f1 = new frmHangTonKho();
            f1.Show();
        }

        private void mstripDatHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDatHang f2 = new frmDatHang();
            f2.Show();
        }

        private void mstripNguyenLieu_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNhapNL f2 = new frmNhapNL();
            f2.Show();
        }

        private void mstripThanhPham_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmNhapTP f2 = new frmNhapTP();
            f2.Show();
        }
        private void mstripBaoCaoNXT_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBC f2 = new frmBC();
            f2.Show();
        }
    }
}
