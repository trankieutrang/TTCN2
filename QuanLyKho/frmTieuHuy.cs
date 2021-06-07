using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyKho.Class;
using System.Data.SqlClient;
using System.Data.Sql;


namespace QuanLyKho
{
    public partial class frmTieuHuy : Form
    {
        public frmTieuHuy()
        {
            InitializeComponent();
        }
        private void frmTieuHuy_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
            Class.Functions.FillCombo("SELECT MaNV, HoTenNV FROM NhanVien", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            Class.Functions.FillCombo("SELECT MaHang FROM HangHoa", cboMaHang, "MaHang", "MaHang");
            cboMaHang.SelectedIndex = -1;
            txtTong.Enabled = false;
            txtTenNV.Enabled = false;
            txtTenHang.Enabled = false;
            txtMaDon.Enabled = false;
            cboMaHang.Enabled = false;
        }
        DataTable tableHTH;
        private void loadDataToGridView()
        {
            string sql;
            sql = "select a.MaDon,b.MaHang,a.MaNV,a.NgayLap,b.LyDo,b.DonGia,b.SoLuong," +
                " (b.DonGia*b.SoLuong) as'Tong'" +
                "from ThuHoi_TieuHuy a join ChiTietThuHoi_TieuHuy b on a.MaDon = b.MaDon";
            tableHTH = Class.Functions.GetDataToTable(sql);
            dataGridView_HTHtieuhuy.DataSource = tableHTH;
            dataGridView_HTHtieuhuy.Columns[0].HeaderText = "Mã Đơn";
            dataGridView_HTHtieuhuy.Columns[1].HeaderText = "Mã Hàng";
            dataGridView_HTHtieuhuy.Columns[2].HeaderText = "Mã NV";
            dataGridView_HTHtieuhuy.Columns[3].HeaderText = "Ngày Lập";
            dataGridView_HTHtieuhuy.Columns[4].HeaderText = "Lý do";
            dataGridView_HTHtieuhuy.Columns[5].HeaderText = "Đơn giá";
            dataGridView_HTHtieuhuy.Columns[6].HeaderText = "Số lượng";
            dataGridView_HTHtieuhuy.Columns[7].HeaderText = "Tổng";
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_HTHtieuhuy.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_HTHtieuhuy.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void dataGridView_HTHtieuhuy_Click(object sender, EventArgs e)
        {
            txtMaDon.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["MaDon"].Value.ToString();
            cboMaHang.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["MaHang"].Value.ToString();
            cboMaNV.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["MaNV"].Value.ToString();
            mskNgayLap.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["NgayLap"].Value.ToString();
            txtLyDo.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["LyDo"].Value.ToString();
            txtDonGia.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["DonGia"].Value.ToString();
            txtSoLuong.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtTong.Text = dataGridView_HTHtieuhuy.CurrentRow.Cells["Tong"].Value.ToString();

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHangTonKho f2 = new frmHangTonKho();
            f2.Show();
        }

        private void ResetValues()
        {
            txtMaDon.Text = "";
            cboMaHang.Text = "";
            cboMaNV.Text = "";
            txtLyDo.Text = "";
            mskNgayLap.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
            txtTong.Text = "";
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = false;
            btnSua.Enabled = true;
            btnDong.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMaDon.Enabled = true;
            txtMaDon.Focus();
            cboMaHang.Enabled = true;
            ResetValues();
            loadDataToGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaDon.Text.Trim().Length ==0)
            {
                MessageBox.Show(" Bạn cần nhập mã đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDon.Focus();
                return;
            }
            if (cboMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaHang.Focus();
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần chọn mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (txtDonGia.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }
            if (mskNgayLap.Text == "  /  /")
            {
                MessageBox.Show(" Bạn cần nhập ngày lập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayLap.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            string sql = "SELECT MaDon FROM ThuHoi_TieuHuy WHERE MaDon=N'" + txtMaDon.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã đơn này đã tồn tại, bạn phải chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDon.Focus();
                return;
            }
            if (!Class.Functions.IsDate(mskNgayLap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày theo dạng dd/mm/yyyy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayLap.Text = "";
                mskNgayLap.Focus();
                return;
            }
            int a, b;
            double tong;
            a = int.Parse(txtDonGia.Text);
            b = int.Parse(txtSoLuong.Text);
            tong = a * b;
            txtTong.Text= tong.ToString();
            string sql1 = "insert into ThuHoi_TieuHuy(MaDon,TenDon, NgayLap, MaNV) values('" + txtMaDon.Text.Trim() + "','','" + Class.Functions.ConvertDateTime(mskNgayLap.Text) + "', '" + cboMaNV.Text + "')";
            string sql2 = "insert into ChiTietThuHoi_TieuHuy(MaDon,MaHang , SoLuong, DonGia,TongCong,LyDo,GhiChu)" +
                " values('" + txtMaDon.Text.Trim() + "','" + cboMaHang.Text + "', '" + txtSoLuong.Text.Trim() + "', '" + txtDonGia.Text.Trim() + "', " +
                "'" + txtTong.Text.Trim() + "', '" + txtLyDo.Text.Trim() + "','')";
            Class.Functions.RunSql(sql1);
            Class.Functions.RunSql(sql2);
            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tableHTH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaDon.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn muốn xoá bản ghi này?", "Thông báo",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE ChiTietThuHoi_TieuHuy WHERE MaDon='" + txtMaDon.Text + "'and MaHang='" + cboMaHang.Text + "'";
                Class.Functions.RunSqlDel(sql);
                string sql1 = "DELETE ThuHoi_TieuHuy WHERE MaDon='" + txtMaDon.Text + "'";
                Class.Functions.RunSqlDel(sql1);
                loadDataToGridView();
                ResetValues();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnHuy.Enabled = false;
                btnLuu.Enabled = false;
                btnDong.Enabled = true;
                txtMaDon.Enabled = false;
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            
            if (tableHTH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaDon.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaHang.Text == "")
            {
                MessageBox.Show("Bạn phải chọn mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaHang.Focus();
                return;
            }
            if (mskNgayLap.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày lập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayLap.Focus();
                return;
            }
            if (!Class.Functions.IsDate(mskNgayLap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày theo dạng dd/mm/yyyy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayLap.Text = "";
                mskNgayLap.Focus();
                return;
            }
            if (cboMaNV.Text == "")
            {
                MessageBox.Show(" Bạn cần chọn mã NV", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("Bạn phải nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaHang.Focus();
                return;
            }
            int a, b;
            double tong;
            a = int.Parse(txtDonGia.Text);
            b = int.Parse(txtSoLuong.Text);
            tong = a * b;
            txtTong.Text = tong.ToString();
            string sql = "UPDATE ThuHoi_TieuHuy SET NgayLap='" + Class.Functions.ConvertDateTime(mskNgayLap.Text) + "', MaNV='" + cboMaNV.Text + "' WHERE MaDon='"+txtMaDon.Text+"'";
            Class.Functions.RunSql(sql);
            string sql1 = "UPDATE ChiTietThuHoi_TieuHuy SET SoLuong='" + txtSoLuong.Text+ "',DonGia='" + txtDonGia.Text + "',LyDo='"+txtLyDo.Text+ "', TongCong='"+txtTong.Text+"'where MaDon='" + txtMaDon.Text + "' and MaHang='"+cboMaHang.Text+"' ";
            Class.Functions.RunSql(sql1);
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;
            btnDong.Enabled = true;
        }

        private void cboMaHang_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenHang from HangHoa where MaHang = N'" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Functions.GetFieldValues(str);
        }

        private void cboMaNV_TextChanged(object sender, EventArgs e)
        {
            string str2;
            if (cboMaNV.Text == "")
            {
                txtTenNV.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str2 = "Select HoTenNV from NhanVien where MaNV = N'" + cboMaNV.SelectedValue + "'";
            txtTenNV.Text = Functions.GetFieldValues(str2);
        }
    }
}
