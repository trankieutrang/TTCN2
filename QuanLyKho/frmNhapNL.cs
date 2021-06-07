using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKho.Class;
using System.Data.SqlClient;
using System.Data.Sql;

namespace QuanLyKho
{
    public partial class frmNhapNL : Form
    {
        public frmNhapNL()
        {
            InitializeComponent();
        }

        private void frmNhapNL_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
            Class.Functions.FillCombo("SELECT MaNV, HoTenNV FROM NhanVien", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            Class.Functions.FillCombo("SELECT MaHang FROM HangHoa a join NhomHang b on a.MaNhomHang=b.MaNhomHang where b.MaNhomHang='NH01'", cboMaHang, "MaHang", "MaHang");
            cboMaHang.SelectedIndex = -1;
            Class.Functions.FillCombo("SELECT MaNCC FROM NhaCungCap", cboMaNCC, "MaNCC", "MaNCC");
            cboMaNCC.SelectedIndex = -1;
            Class.Functions.FillCombo("SELECT MaDonDatHang FROM DonDatHang", cboMaDon, "MaDonDatHang", "MaDonDatHang");
            cboMaDon.SelectedIndex = -1;
            txtTong.Enabled = false;
            txtThanhTien.Enabled = false;
            txtTenNV.Enabled = false;
            txtTenHang.Enabled = false;
            txtCK.Enabled = false;
            txtMaDon.Enabled = false;
            txtTenNCC.Enabled = false;
            txtTongtien.Enabled = false;
        }
        DataTable tableHT;
        private void loadDataToGridView()
        {
            string sql;
            sql = "select a.MaPhieuNK,a.MaNCC,a.MaNV,a.MaDonDatHang,b.MaHang," +
                "a.NgayLap,b.SoLuong,b.DonGia,(b.SoLuong*b.DonGia) as Tong,b.TyLeChietKhau,(b.TyleChietKhau*b.SoLuong*b.DonGia) as ChietKhau,((b.SoLuong*b.DonGia)-(b.TyleChietKhau*b.SoLuong*b.DonGia)) as ThanhTien,a.GhiChu" +
                " from PhieuNK a join ChiTietPhieuNK b on a.MaPhieuNK = b.MaPhieuNK join HangHoa on b.MaHang=HangHoa.MaHang join NhomHang on HangHoa.MaNhomHang = NhomHang.MaNhomHang where NhomHang.MaNhomHang='NH01'";
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_NhapNL.DataSource = tableHT;
            dataGridView_NhapNL.Columns[0].HeaderText = "Mã Phiếu";
            dataGridView_NhapNL.Columns[1].HeaderText = "Mã NCC";
            dataGridView_NhapNL.Columns[2].HeaderText = "Mã NV";
            dataGridView_NhapNL.Columns[3].HeaderText = "Mã ĐơnĐH";
            dataGridView_NhapNL.Columns[4].HeaderText = "Mã Hàng";
            dataGridView_NhapNL.Columns[5].HeaderText = "Ngày lập";
            dataGridView_NhapNL.Columns[6].HeaderText = "Số lượng";
            dataGridView_NhapNL.Columns[7].HeaderText = "Đơn Giá";
            dataGridView_NhapNL.Columns[8].HeaderText = "Tổng";
            dataGridView_NhapNL.Columns[9].HeaderText = "Tỉ lệ CK";
            dataGridView_NhapNL.Columns[10].HeaderText = "Chiết khấu";
            dataGridView_NhapNL.Columns[11].HeaderText = "Thành tiền";
            dataGridView_NhapNL.Columns[12].HeaderText = "Ghi chú";
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_NhapNL.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_NhapNL.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView_NhapNL_Click(object sender, EventArgs e)
        {
            double tong;
            txtMaDon.Text = dataGridView_NhapNL.CurrentRow.Cells["MaPhieuNK"].Value.ToString();
            cboMaHang.Text = dataGridView_NhapNL.CurrentRow.Cells["MaHang"].Value.ToString();
            cboMaNV.Text = dataGridView_NhapNL.CurrentRow.Cells["MaNV"].Value.ToString();
            mskNgayLap.Text = dataGridView_NhapNL.CurrentRow.Cells["NgayLap"].Value.ToString();
            txtGhiChu.Text = dataGridView_NhapNL.CurrentRow.Cells["GhiChu"].Value.ToString();
            txtDonGia.Text = dataGridView_NhapNL.CurrentRow.Cells["DonGia"].Value.ToString();
            txtSoLuong.Text = dataGridView_NhapNL.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtTong.Text = dataGridView_NhapNL.CurrentRow.Cells["Tong"].Value.ToString();
            cboMaNCC.Text = dataGridView_NhapNL.CurrentRow.Cells["MaNCC"].Value.ToString();
            cboMaDon.Text = dataGridView_NhapNL.CurrentRow.Cells["MaDonDatHang"].Value.ToString();
            txtTiLe.Text = dataGridView_NhapNL.CurrentRow.Cells["TyLeChietKhau"].Value.ToString();
            txtCK.Text = dataGridView_NhapNL.CurrentRow.Cells["ChietKhau"].Value.ToString();
            txtSoLuong.Text = dataGridView_NhapNL.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtThanhTien.Text = dataGridView_NhapNL.CurrentRow.Cells["ThanhTien"].Value.ToString();
            txtTongtien.Text = dataGridView_NhapNL.CurrentRow.Cells["ThanhTien"].Value.ToString();
            tong = Convert.ToDouble(Class.Functions.GetFieldValues("select ((b.SoLuong*b.DonGia)-(b.TyleChietKhau*b.SoLuong*b.DonGia)) as ThanhTien" +
                " from PhieuNK a join ChiTietPhieuNK b on a.MaPhieuNK = b.MaPhieuNK where a.MaPhieuNK='"+txtMaDon.Text+"' and b.MaHang='"+cboMaHang.Text+"'"));
            txtTongtien.Text = tong.ToString();
            lblBangChu.Text = Class.Functions.ChuyenSoSangChu(tong.ToString());
        }

        private void cboMaNCC_TextChanged(object sender, EventArgs e)
        {
            string str1;
            if (cboMaNCC.Text == "")
            {
                txtTenNCC.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str1 = "Select TenNCC from NhaCungCap where MaNCC = N'" + cboMaNCC.SelectedValue + "'";
            txtTenNCC.Text = Functions.GetFieldValues(str1);
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

        private void cboMaHang_TextChanged(object sender, EventArgs e)
        {
            string str3;
            if (cboMaHang.Text == "")
            {
                txtTenHang.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str3 = "Select TenHang from HangHoa where MaHang = N'" + cboMaHang.SelectedValue + "'";
            txtTenHang.Text = Functions.GetFieldValues(str3);
        }
        private void ResetValues()
        {
            txtMaDon.Text = "";
            cboMaHang.Text = "";
            cboMaNCC.Text = "";
            cboMaNV.Text = "";
            txtGhiChu.Text = "";
            mskNgayLap.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
            txtTong.Text = "";
            txtCK.Text = "";
            txtThanhTien.Text = "";
            txtTiLe.Text = "";
            txtTongtien.Text = "";
            cboMaDon.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            txtMaDon.Enabled = false;
            cboMaHang.Enabled = true;
            ResetValues();
            loadDataToGridView();
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

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain f2 = new frmMain();
            f2.Show();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int count = 0;
            count = dataGridView_NhapNL.Rows.Count;
            string chuoi = "";
            int chuoi1 = 0;
            chuoi = Convert.ToString(dataGridView_NhapNL.Rows[count - 1].Cells[0].Value);
            chuoi1 = Convert.ToInt32((chuoi.Remove(0, 3)));
            if (chuoi1 + 1 < 10)
            {
                txtMaDon.Text = "PNK0" + (chuoi1 + 1).ToString();
            }
            else
            {
                txtMaDon.Text = "PNK" + (chuoi1 + 1).ToString();
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
            if (cboMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập mã NCC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }
            if (cboMaDon.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần chọn mã đơn đặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaDon.Focus();
                return;
            }
            if (txtTiLe.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập tỉ lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTiLe.Focus();
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
            string sql = "SELECT MaPhieuNK FROM PhieuNK WHERE MaPhieuNK=N'" + txtMaDon.Text.Trim() + "'";
            /*if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã phiếu này đã tồn tại, bạn phải chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDon.Focus();
                return;
            }*/
            if (!Class.Functions.IsDate(mskNgayLap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày theo dạng dd/mm/yyyy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayLap.Text = "";
                mskNgayLap.Focus();
                return;
            }
            int s, d, c;
            double tong,chietkhau,ttien;
            d = int.Parse(txtDonGia.Text);
            s = int.Parse(txtSoLuong.Text);
            tong = s * d;
            txtTong.Text = tong.ToString();
            c = int.Parse(txtTiLe.Text);
            chietkhau = tong * c*0.01;
            txtCK.Text = chietkhau.ToString();
            ttien = tong - chietkhau;
            txtThanhTien.Text = ttien.ToString();
            string sql1 = "insert into PhieuNK(MaPhieuNK, NgayLap, MaNCC, MaNV, MaDonDatHang, GhiChu) values ('"+txtMaDon.Text.Trim()+ "','" + Class.Functions.ConvertDateTime(mskNgayLap.Text) + "','"+cboMaNCC.Text+"','"+cboMaNV.Text+"','"+cboMaDon.Text+"','')";
            string sql2 = "insert into ChiTietPhieuNK(MaPhieuNK, MaHang , SoLuong, DonGia,TongCong,TyLeChietKhau,ChietKhau,ThanhTien) " +
                "values('" + txtMaDon.Text.Trim() + "', '" + cboMaHang.Text + "', '" + txtSoLuong.Text+ "'," +
                " '" + txtDonGia.Text + "', '" + txtTong.Text + "','" + txtTiLe.Text+ "','" + txtCK.Text+ "','" + txtThanhTien.Text+ "')";
            Class.Functions.RunSql(sql1);
            Class.Functions.RunSql(sql2);
            loadDataToGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (tableHT.Rows.Count == 0)
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
                string sql = "DELETE ChiTietPhieuNK WHERE MaPhieuNK='" + txtMaDon.Text + "'and MaHang='" + cboMaHang.Text + "'";
                Class.Functions.RunSqlDel(sql);
                string sql1 = "DELETE PhieuNK WHERE MaPhieuNK='" + txtMaDon.Text + "'";
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
            if (tableHT.Rows.Count == 0)
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
            if (cboMaDon.Text == "")
            {
                MessageBox.Show("Bạn phải chọn mã Đơn ĐH", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaDon.Focus();
                return;
            }
            if (cboMaNCC.Text == "")
            {
                MessageBox.Show("Bạn phải chọn mã NCC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
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
            if (txtTiLe.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập tỉ lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTiLe.Focus();
                return;
            }
            int s, d, c;
            double tong, chietkhau, ttien;
            d = int.Parse(txtDonGia.Text);
            s = int.Parse(txtSoLuong.Text);
            tong = s * d;
            txtTong.Text = tong.ToString();
            c = int.Parse(txtTiLe.Text);
            chietkhau = tong * c * 0.01;
            txtCK.Text = chietkhau.ToString();
            ttien = tong - chietkhau;
            txtThanhTien.Text = ttien.ToString();
            string sql = "UPDATE PhieuNK SET NgayLap='" + Class.Functions.ConvertDateTime(mskNgayLap.Text) + "', MaNV='" + cboMaNV.Text + "', MaNCC='" + cboMaNCC.Text + "', MaDonDatHang='" + cboMaDon.Text + "', GhiChu='" + txtGhiChu.Text + "' WHERE MaPhieuNK='" + txtMaDon.Text + "'";
            Class.Functions.RunSql(sql);
            string sql1 = "UPDATE ChiTietPhieuNK SET SoLuong='" + txtSoLuong.Text + "',DonGia='" + txtDonGia.Text + "',TyLeChietKhau='" + txtTiLe.Text + "', TongCong='" + txtTong.Text + "', ChietKhau='" + txtCK.Text + "', ThanhTien='" + txtThanhTien.Text + "'where MaPhieuNK='" + txtMaDon.Text + "' and MaHang='" + cboMaHang.Text + "' ";
            Class.Functions.RunSql(sql1);
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadDataToGridView();
            ResetValues();
            btnHuy.Enabled = false;
            btnDong.Enabled = true;
        }
    }
}
