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
    public partial class frmDatHang : Form
    {
        public frmDatHang()
        {
            InitializeComponent();
        }

        private void frmDatHang_Load(object sender, EventArgs e)
        {
            loadDataToGridView();
            Class.Functions.FillCombo("SELECT MaNV FROM NhanVien", cboMaNV, "MaNV", "MaNV");
            cboMaNV.SelectedIndex = -1;
            Class.Functions.FillCombo("SELECT MaHang FROM HangHoa", cboMaHang, "MaHang", "MaHang");
            cboMaHang.SelectedIndex = -1;
            Class.Functions.FillCombo("SELECT MaNCC FROM NhaCungCap", cboMaNCC, "MaNCC", "MaNCC");
            cboMaNCC.SelectedIndex = -1;
            txtMaDon.Enabled = false;
            txtTenHang.Enabled = false;
            txtTenNCC.Enabled = false;
            txtTenNV.Enabled = false;
            cboMaHang.Enabled = false;
        }
        DataTable tableHTH;
        private void loadDataToGridView()
        {
            string sql;
            sql = "Select a.MaDonDatHang,a.MaNCC,b.MaHang,a.MaNV,a.NgayDat,b.SoLuong,b.GhiChu " +
                "from DonDatHang a join ChiTietDonDatHang b on a.MaDonDatHang=b.MaDonDatHang";
            tableHTH = Class.Functions.GetDataToTable(sql);
            dataGridView_DatHang.DataSource = tableHTH;
            dataGridView_DatHang.Columns[0].HeaderText = "Mã Đơn";
            dataGridView_DatHang.Columns[1].HeaderText = "Mã NCC";
            dataGridView_DatHang.Columns[2].HeaderText = "Mã Hàng";
            dataGridView_DatHang.Columns[3].HeaderText = "Mã NV";
            dataGridView_DatHang.Columns[4].HeaderText = "Ngày lập";
            dataGridView_DatHang.Columns[5].HeaderText = "Số lượng";
            dataGridView_DatHang.Columns[6].HeaderText = "Ghi chú";
            // Không cho thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_DatHang.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_DatHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView_DatHang_Click(object sender, EventArgs e)
        {
            txtMaDon.Text = dataGridView_DatHang.CurrentRow.Cells["MaDonDatHang"].Value.ToString();
            cboMaNCC.Text = dataGridView_DatHang.CurrentRow.Cells["MaNCC"].Value.ToString();
            cboMaHang.Text = dataGridView_DatHang.CurrentRow.Cells["MaHang"].Value.ToString();
            cboMaNV.Text = dataGridView_DatHang.CurrentRow.Cells["MaNV"].Value.ToString();
            mskNgayLap.Text = dataGridView_DatHang.CurrentRow.Cells["NgayDat"].Value.ToString();
            txtSoLuong.Text = dataGridView_DatHang.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtGhiChu.Text = dataGridView_DatHang.CurrentRow.Cells["GhiChu"].Value.ToString();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain f = new frmMain();
            f.Show();
        }
        private void ResetValues()
        {
            txtMaDon.Text = "";
            cboMaHang.Text = "";
            cboMaNV.Text = "";
            cboMaNCC.Text = "";
            mskNgayLap.Text = "";
            txtGhiChu.Text = "";
            txtSoLuong.Text = "";
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
            if (txtMaDon.Text.Trim().Length == 0)
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
            if (cboMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần chọn nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }
            if (mskNgayLap.Text == "  /  /")
            {
                MessageBox.Show(" Bạn cần nhập ngày đặt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayLap.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show(" Bạn cần nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            string sql = "SELECT MaDonDatHang FROM DonDatHang WHERE MaDonDatHang=N'" + txtMaDon.Text.Trim() + "'";
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
            string sql1 = "insert into DonDatHang(MaDonDatHang, MaNV, MaNCC, NgayDat) values('" + txtMaDon.Text.Trim() + "', '" + cboMaNV.Text + "','" + cboMaNCC.Text+"','" + Class.Functions.ConvertDateTime(mskNgayLap.Text) + "')";
            string sql2 = "insert into ChiTietDonDatHang(MaDonDatHang ,MaHang ,SoLuong, GhiChu) values ('"+txtMaDon.Text.Trim() + "','"+cboMaHang.Text+"','"+txtSoLuong.Text.Trim() + "','"+txtGhiChu.Text.Trim() + "')";
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
                string sql = "DELETE ChiTietDonDatHang WHERE MaDonDatHang='" + txtMaDon.Text + "'and MaHang='" + cboMaHang.Text + "'";
                Class.Functions.RunSqlDel(sql);
                string sql1 = "DELETE DonDatHang WHERE MaDon='" + txtMaDon.Text + "'";
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
            if (mskNgayLap.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày đặt", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (cboMaNCC.Text == "")
            {
                MessageBox.Show("Bạn phải chọn mã NCC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            string sql = "UPDATE DonDatHang SET NgayDat='" + Class.Functions.ConvertDateTime(mskNgayLap.Text) + "', MaNV='" + cboMaNV.Text + "',MaNCC='"+cboMaNCC.Text+"' WHERE MaDonDatHang='"+txtMaDon.Text+"'";
            Class.Functions.RunSql(sql);
            string sql1 = "UPDATE ChiTietDonDatHang SET SoLuong='" + txtSoLuong.Text+ "',GhiChu='" + txtGhiChu.Text + "' where MaDonDatHang='" + txtMaDon.Text + "' and MaHang='"+cboMaHang.Text+"' ";
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
    }
    }
