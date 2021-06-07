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
using COMExcel = Microsoft.Office.Interop.Excel;
namespace QuanLyKho
{
    public partial class frmBCTonKho : Form
    {
        public frmBCTonKho()
        {
            InitializeComponent();
        }

        private void frmBCTonKho_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            txtMaNH.ReadOnly = true;
            txtSoLuong.ReadOnly = true;
            txtTenHang.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0";
            cboThang.Items.Add("1");
            cboThang.Items.Add("2");
            cboThang.Items.Add("3");
            cboThang.Items.Add("4");
            cboThang.Items.Add("5");
            cboThang.Items.Add("6");
            cboThang.Items.Add("7");
            cboThang.Items.Add("8");
            cboThang.Items.Add("9");
            cboThang.Items.Add("10");
            cboThang.Items.Add("11");
            cboThang.Items.Add("12");
            cboQuy.Items.Add("1");
            cboQuy.Items.Add("2");
            cboQuy.Items.Add("3");
            cboQuy.Items.Add("4");
            Class.Functions.FillCombo("SELECT MaNhomHang,TenNhomHang FROM NhomHang", cboNhomHang, "MaNhomHang", "TenNhomHang");
            cboNhomHang.SelectedIndex = -1;
            loadDataToGridView();
            dataGridView_BCTon.DataSource = null;
        }
        DataTable tableHT;
        private void loadDataToGridView()
        {
            string sql;
            sql = "	select a.MaHang,a.TenHang,sum(isnull(d.SoLuong,0)-isnull(e.SoLuong,0)-isnull(f.SoLuong,0)) as'TonKho' from HangHoa a left join ChiTietPhieuNK d on a.MaHang = d.MaHang left join ChiTietPhieuXK e on a.MaHang = e.MaHang left join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang group by a.MaHang,a.TenHang having sum(isnull(d.SoLuong, 0) - isnull(e.SoLuong, 0) - isnull(f.SoLuong, 0)) > 0";
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_BCTon.DataSource = tableHT;
            dataGridView_BCTon.Columns[0].HeaderText = "Mã NH";
            dataGridView_BCTon.Columns[1].HeaderText = "Tên Hàng";
            dataGridView_BCTon.Columns[2].HeaderText = "Số Lượng Tồn";
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_BCTon.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_BCTon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView_BCTon_Click(object sender, EventArgs e)
        {
            txtMaNH.Text = dataGridView_BCTon.CurrentRow.Cells["MaNhomHang"].Value.ToString();
            txtTenHang.Text = dataGridView_BCTon.CurrentRow.Cells["TenHang"].Value.ToString();
            txtSoLuong.Text = dataGridView_BCTon.CurrentRow.Cells["SoLuong"].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            double tong;
            if (cboNhomHang.Text == "")
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhomHang.Focus();
                return;
            }
            if ((cboNhomHang.Text == "") && (cboThang.Text == "") && (cboQuy.Text == "") &&
               (txtNam.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select a.MaHang,a.TenHang, isnull((select isnull(SoLuong, 0) from ChiTietPhieuNK b join HangHoa a on a.Mahang = b.MaHang" +
                        "join PhieuNK d on d.MaPhieuNK=b.MaPhieuNK where 1=1 AND MONTH(d.NgayLap) ='" + cboThang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'" +
                        "and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "')-(select isnull(SoLuong, 0) from ChiTietPhieuXK b " +
                        "join HangHoa a on a.Mahang = b.MaHang join PhieuXK d on d.MaPhieuXK=b.MaPhieuNK where 1=1 AND MONTH(d.NgayLap) ='" + cboThang.Text + "'" +
                        "and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'')-" +
                        "(select isnull(SoLuong,0) from ChiTietThuHoi_TieuHuy b join HangHoa a on a.Mahang = b.MaHang join ThuHoi_TieuHuy d on d.MaDon=b.MaDon where 1=1 " +
                        "AND MONTH(d.NgayLap) ='" + cboThang.Text + "'and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'),0) " +
                        "as 'Tong'" +
                        "from HangHoa a left join ChiTietPhieuNK d on a.MaHang = d.MaHang left join ChiTietPhieuXK e on a.MaHang = e.MaHang " +
                "left join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang left join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK " +
                "left join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK left join ThuHoi_TieuHuy g on f.MaDon = g.MaDon where WHERE 1=1";
            if (cboNhomHang.Text != "")
                sql = sql + " AND a.TenNhomHang Like '%" + cboNhomHang.Text + "%'";
            if (cboThang.Text != "")
                sql = sql + " AND MONTH(b.NgayLap) Like '%" + cboThang.Text + "%'  AND MONTH(c.NgayLap) Like '%" + cboThang.Text + "%'  AND MONTH(g.NgayLap) Like '%" + cboThang.Text + "%' ";
            if (cboQuy.Text != "")
                sql = sql + " AND DATEPART(quarter, b.NgayLap) Like '%" + cboQuy.Text + "%' AND DATEPART(quarter, c.NgayLap) Like '%" + cboQuy.Text + "%' AND DATEPART(quarter, g.NgayLap) Like '%" + cboQuy.Text + "%'";
            if (txtNam.Text != "")
                sql = sql + "AND Year(b.NgayLap) Like '%" + txtNam.Text + "%'AND Year(c.NgayLap) Like '%" + txtNam.Text + "%'AND Year(g.NgayLap) Like '%" + txtNam.Text + "%'";
            DataTable tableHT = Class.Functions.GetDataToTable(sql);
            if (tableHT.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTongTien.Text = "0";
            }
            else
            {
                MessageBox.Show("Có " + tableHT.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Cập nhật lại tổng tiền cho báo cáo
                if (cboNhomHang.Text != "")
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select isnull((select isnull(sum(SoLuong), 0) from ChiTietPhieuNK b join HangHoa a on a.Mahang = b.MaHang" +
                        "join PhieuNK d on d.MaPhieuNK=b.MaPhieuNK where MaNhomHang = '"+cboNhomHang.SelectedValue+"')-(select isnull(sum(SoLuong), 0) from ChiTietPhieuXK b " +
                        "join HangHoa a on a.Mahang = b.MaHang join PhieuXK d on d.MaPhieuXK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + "')-(select isnull(sum(SoLuong)," +
                        " 0) from ChiTietThuHoi_TieuHuy b join HangHoa a on a.Mahang = b.MaHang join ThuHoi_TieuHuy d on d.MaDon=b.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'),0) as 'Tong'from HangHoa where MaNhomHang = '" + cboNhomHang.SelectedValue + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboThang.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select isnull((select isnull(sum(SoLuong), 0) from ChiTietPhieuNK b join HangHoa a on a.Mahang = b.MaHang" +
                        "join PhieuNK d on d.MaPhieuNK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + "'AND MONTH(b.NgayLap) ='" + cboThang.Text + "')-(select isnull(sum(SoLuong), 0) from ChiTietPhieuXK b " +
                        "join HangHoa a on a.Mahang = b.MaHang join PhieuXK d on d.MaPhieuXK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + "'AND MONTH(b.NgayLap) ='" + cboThang.Text + "')-(select isnull(sum(SoLuong)," +
                        " 0) from ChiTietThuHoi_TieuHuy b join HangHoa a on a.Mahang = b.MaHang join ThuHoi_TieuHuy d on d.MaDon=b.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'AND MONTH(b.NgayLap) ='" + cboThang.Text + "'),0)" +
                        " as 'Tong'from HangHoa a left join ChiTietPhieuNK d on a.MaHang = d.MaHang left join ChiTietPhieuXK e on a.MaHang = e.MaHang " +
                "left join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang left join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK " +
                "left join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK left join ThuHoi_TieuHuy g on f.MaDon = g.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'and MONTH(b.NgayLap)='" + cboThang.Text + "'" +
                "AND MONTH(c.NgayLap) ='" + cboThang.Text + "'  AND MONTH(g.NgayLap) ='" + cboThang.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboQuy.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select isnull((select isnull(sum(SoLuong), 0) from ChiTietPhieuNK b join HangHoa a on a.Mahang = b.MaHang" +
                        "join PhieuNK d on d.MaPhieuNK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + "'and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "')-(select isnull(sum(SoLuong), 0) from ChiTietPhieuXK b " +
                        "join HangHoa a on a.Mahang = b.MaHang join PhieuXK d on d.MaPhieuXK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + "'and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "')-(select isnull(sum(SoLuong)," +
                        " 0) from ChiTietThuHoi_TieuHuy b join HangHoa a on a.Mahang = b.MaHang join ThuHoi_TieuHuy d on d.MaDon=b.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "'),0) as 'Tong'" +
                        "from HangHoa a left join ChiTietPhieuNK d on a.MaHang = d.MaHang left join ChiTietPhieuXK e on a.MaHang = e.MaHang " +
                "left join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang left join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK " +
                "left join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK left join ThuHoi_TieuHuy g on f.MaDon = g.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "' " +
                "and DATEPART(qq,(b.NgayLap))='" + cboQuy.Text + "' and DATEPART(qq,(c.NgayLap))='" + cboQuy.Text + "'and DATEPART(qq,(g.NgayLap))='" + cboQuy.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select isnull((select isnull(sum(SoLuong), 0) from ChiTietPhieuNK b join HangHoa a on a.Mahang = b.MaHang" +
                        "join PhieuNK d on d.MaPhieuNK=b.MaPhieuNK where MaNhomHang ='" + cboNhomHang.SelectedValue + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "')-(select isnull(sum(SoLuong), 0) from ChiTietPhieuXK b " +
                        "join HangHoa a on a.Mahang = b.MaHang join PhieuXK d on d.MaPhieuXK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "')-(select isnull(sum(SoLuong)," +
                        " 0) from ChiTietThuHoi_TieuHuy b join HangHoa a on a.Mahang = b.MaHang join ThuHoi_TieuHuy d on d.MaDon=b.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'),0) as 'Tong'" +
                        "from HangHoa a left join ChiTietPhieuNK d on a.MaHang = d.MaHang left join ChiTietPhieuXK e on a.MaHang = e.MaHang " +
                "left join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang left join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK " +
                "left join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK left join ThuHoi_TieuHuy g on f.MaDon = g.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'" +
                "and DATEPART(yyyy,(b.NgayLap))='" + txtNam.Text + "' and DATEPART(yyyy,(c.NgayLap))='" + txtNam.Text + "' and DATEPART(yyyy,(g.NgayLap))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboThang.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select isnull((select isnull(sum(SoLuong), 0) from ChiTietPhieuNK b join HangHoa a on a.Mahang = b.MaHang" +
                        "join PhieuNK d on d.MaPhieuNK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + " AND MONTH(d.NgayLap) ='" + cboThang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "')-(select isnull(sum(SoLuong), 0) from ChiTietPhieuXK b " +
                        "join HangHoa a on a.Mahang = b.MaHang join PhieuXK d on d.MaPhieuXK=b.MaPhieuNK where MaNhomHang = '" + cboNhomHang.SelectedValue + " AND MONTH(d.NgayLap) ='" + cboThang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'')-" +
                        "(select isnull(sum(SoLuong), 0) from ChiTietThuHoi_TieuHuy b join HangHoa a on a.Mahang = b.MaHang join ThuHoi_TieuHuy d on d.MaDon=b.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'AND MONTH(d.NgayLap) ='" + cboThang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'),0) as 'Tong'" +
                        "from HangHoa a left join ChiTietPhieuNK d on a.MaHang = d.MaHang left join ChiTietPhieuXK e on a.MaHang = e.MaHang " +
                "left join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang left join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK " +
                "left join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK left join ThuHoi_TieuHuy g on f.MaDon = g.MaDon where MaNhomHang = '" + cboNhomHang.SelectedValue + "'" +
                    "and DATEPART(yyyy,(b.NgayLap))='" + txtNam.Text + "' and DATEPART(yyyy,(c.NgayLap))='" + txtNam.Text + "' and DATEPART(yyyy,(g.NgayLap))='" + txtNam.Text + "'" +
                    "AND MONTH(b.NgayLap) ='" + cboThang.Text + "'AND MONTH(c.NgayLap) ='" + cboThang.Text + "'  AND MONTH(g.NgayLap) ='" + cboThang.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                /*if ((cboNhomHang.Text != "") && (cboQuy.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where DATEPART(qq,(d.NgayLap)) = '" + cboQuy.Text + "'" +
                    "and TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }*/
            }
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_BCTon.DataSource = tableHT;
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_BCTon.DataSource = tableHT;
            dataGridView_BCTon.Columns[0].HeaderText = "Mã NH";
            dataGridView_BCTon.Columns[1].HeaderText = "Tên Hàng";
            dataGridView_BCTon.Columns[2].HeaderText = "Số lượng";
            Class.Functions.RunSql(sql);
        }

    }
}
