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
    public partial class frmBC : Form
    {
        public frmBC()
        {
            InitializeComponent();
        }

        private void frmBC_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            txtMaHang.ReadOnly = true;
            txtTenHangLoc.ReadOnly = true;
            txtSoNhap.ReadOnly = true;
            txtHSDTK.ReadOnly = true;
            txtSoXuat.ReadOnly = true;
            txtTKtong.ReadOnly = true;
            txtTKtieuhuy.ReadOnly = true;
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
            Class.Functions.FillCombo("SELECT MaHang FROM HangHoa", cboMaHang, "MaHang", "MaHang");
            cboMaHang.SelectedIndex = -1;
            loadDataToGridView();
            dataGridView_TKketqua.DataSource = null;
        }
        DataTable tableTKketqua = new DataTable();
        private void loadDataToGridView()
        {
            string sql;
            sql = "select MaHang,TenHang,(select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "') as 'SLNhap', " +
                "(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "') as 'SLXuat'," +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "') as 'SLTHuy'," +
               "case when (HanSuDung-DATEDIFF(day,NgaySanXuat,GETDATE()))<0 then 'Het han su dung' else convert(nvarchar, (HanSuDung - DATEDIFF(day, NgaySanXuat, GETDATE()))) end as 'HSDcon'," +
                "((case when((select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "')" +
                "-(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "')-" +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "'))< 0 then '0'" +
                "when((select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "')" +
                "-(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "')-" +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "'))> 0 " +
                "then((select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "')" +
                "-(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "')-" +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "')) end)) as'Tong'" +
                "from HangHoa where MaHang = '" + cboMaHang.Text + "'";
            tableTKketqua = Class.Functions.GetDataToTable(sql);
            dataGridView_TKketqua.DataSource = tableTKketqua;
            dataGridView_TKketqua.Columns[0].HeaderText = "Mã Hàng";
            dataGridView_TKketqua.Columns[1].HeaderText = "Tên Hàng";
            dataGridView_TKketqua.Columns[2].HeaderText = "SL Nhập";
            dataGridView_TKketqua.Columns[3].HeaderText = "SL Xuất";
            dataGridView_TKketqua.Columns[4].HeaderText = "SL Tiêu hủy";
            dataGridView_TKketqua.Columns[5].HeaderText = "HSD còn lại";
            dataGridView_TKketqua.Columns[6].HeaderText = "Tổng tồn kho";
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_TKketqua.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_TKketqua.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain f = new frmMain();
            f.Show();
        }
        private void dataGridView_TKketqua_Click(object sender, EventArgs e)
        {
            txtMaHang.Text = dataGridView_TKketqua.CurrentRow.Cells["MaHang"].Value.ToString();
            txtTenHangLoc.Text = dataGridView_TKketqua.CurrentRow.Cells["TenHang"].Value.ToString();
            txtSoNhap.Text = dataGridView_TKketqua.CurrentRow.Cells["SLNhap"].Value.ToString();
            txtSoXuat.Text = dataGridView_TKketqua.CurrentRow.Cells["SLXuat"].Value.ToString();
            txtTKtieuhuy.Text = dataGridView_TKketqua.CurrentRow.Cells["SLTHuy"].Value.ToString();
            txtHSDTK.Text = dataGridView_TKketqua.CurrentRow.Cells["HSDcon"].Value.ToString();
            txtTKtong.Text = dataGridView_TKketqua.CurrentRow.Cells["Tong"].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cboMaHang.Text == "") && (cboThang.Text == "") && (cboQuy.Text == "") &&
               (txtNam.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select a.MaHang, a.TenHang,isnull((d.SoLuong),0) as 'SLNhap',isnull((e.SoLuong),0) as 'SLXuat'," +
                "isnull((f.SoLuong),0) as 'SLTHuy',case when(HanSuDung - DATEDIFF(day, NgaySanXuat, GETDATE())) < 0 then 'Het han su dung' " +
                "else convert(nvarchar, (HanSuDung - DATEDIFF(day, NgaySanXuat, GETDATE()))) end as 'HSDcon',case when(isnull(d.SoLuong, 0) - isnull(e.SoLuong, 0) + isnull(f.SoLuong, 0)) < 0 then '0'" +
                "else (isnull(d.SoLuong, 0) - isnull(e.SoLuong, 0) + isnull(f.SoLuong, 0)) end as'Tong' from HangHoa a join ChiTietPhieuNK d on a.MaHang = d.MaHang " +
                "join ChiTietPhieuXK e on a.MaHang = e.MaHang join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK " +
                "join ThuHoi_TieuHuy g on f.MaDon = g.MaDon  where 1=1";
            if (cboMaHang.Text != "")
                sql = sql + " AND a.MaHang Like '%" + cboMaHang.Text + "%' ";
            if (cboThang.Text != "")
                sql = sql + " AND MONTH(b.NgayLap) Like '%" + cboThang.Text + "%'  AND MONTH(c.NgayLap) Like '%" + cboThang.Text + "%'  AND MONTH(g.NgayLap) Like '%" + cboThang.Text + "%'";
            if (cboQuy.Text != "")
                sql = sql + " AND DATEPART(quarter, b.NgayLap) Like '%" + cboQuy.Text + "%' AND DATEPART(quarter, c.NgayLap) Like '%" + cboQuy.Text + "%' AND DATEPART(quarter, g.NgayLap) Like '%" + cboQuy.Text + "%'";
            if (txtNam.Text != "")
                sql = sql + "AND Year(b.NgayLap) Like '%" + txtNam.Text + "%'AND Year(c.NgayLap) Like '%" + txtNam.Text + "%'AND Year(g.NgayLap) Like '%" + txtNam.Text + "%'";
            tableTKketqua = Class.Functions.GetDataToTable(sql);
            if (tableTKketqua.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tableTKketqua.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tableTKketqua = Class.Functions.GetDataToTable(sql);
            dataGridView_TKketqua.DataSource = tableTKketqua;
            dataGridView_TKketqua.Columns[0].HeaderText = "Mã Hàng";
            dataGridView_TKketqua.Columns[1].HeaderText = "Tên Hàng";
            dataGridView_TKketqua.Columns[2].HeaderText = "SL Nhập";
            dataGridView_TKketqua.Columns[3].HeaderText = "SL Xuất";
            dataGridView_TKketqua.Columns[4].HeaderText = "SL Tiêu hủy";
            dataGridView_TKketqua.Columns[5].HeaderText = "HSD còn lại";
            dataGridView_TKketqua.Columns[6].HeaderText = "Tổng tồn kho";
            Class.Functions.RunSql(sql);
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            cboMaHang.Text = "";
            txtNam.Text = "";
            cboThang.Text = "";
            cboQuy.Text = "";
            cboMaHang.Focus();
            dataGridView_TKketqua.DataSource = null;
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            int hang = 0, cot = 0;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:C4"].Font.Size = 10;
            exRange.Range["A1:C4"].Font.Bold = true;
            exRange.Range["A1:C4"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["C1:C1"].ColumnWidth = 15;
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:C1"].Value = "Công ty TNHH Savor Việt Nam";
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Email: support@savor.vn";
            exRange.Range["A3:C3"].MergeCells = true;
            exRange.Range["A3:C3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:C3"].Value = "Hotline khiếu nại: 0392 770 512";
            exRange.Range["A4:C4"].MergeCells = true;
            exRange.Range["A4:C4"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A4:C4"].Value = "Liên hệ hợp tác: 093 4664 262";
            exRange.Range["D2:H2"].Font.Size = 16;
            exRange.Range["D2:H2"].Font.Bold = true;
            exRange.Range["D2:H2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["D2:H2"].MergeCells = true;
            exRange.Range["D2:H2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["D2:F2"].Value = "BÁO CÁO NHẬP XUẤT TỒN";
            string sql = "select a.MaHang, a.TenHang,isnull((d.SoLuong),0) as 'SLNhap',isnull((e.SoLuong),0) as 'SLXuat'," +
                "isnull((f.SoLuong),0) as 'SLTHuy',case when(HanSuDung - DATEDIFF(day, NgaySanXuat, GETDATE())) < 0 then 'Het han su dung' " +
                "else convert(nvarchar, (HanSuDung - DATEDIFF(day, NgaySanXuat, GETDATE()))) end as 'HSDcon',case when(isnull(d.SoLuong, 0) - isnull(e.SoLuong, 0) + isnull(f.SoLuong, 0)) < 0 then '0'" +
                "else (isnull(d.SoLuong, 0) - isnull(e.SoLuong, 0) + isnull(f.SoLuong, 0)) end as'Tong' from HangHoa a join ChiTietPhieuNK d on a.MaHang = d.MaHang " +
                "join ChiTietPhieuXK e on a.MaHang = e.MaHang join ChiTietThuHoi_TieuHuy f on a.MaHang = f.MaHang join PhieuNK b on b.MaPhieuNK = d.MaPhieuNK join PhieuXK c on c.MaPhieuXK = e.MaPhieuXK " +
                "join ThuHoi_TieuHuy g on f.MaDon = g.MaDon  where 1=1";
            if (cboMaHang.Text != "")
                sql = sql + " AND a.MaHang Like '%" + cboMaHang.Text + "%' ";
            if (cboThang.Text != "")
                sql = sql + " AND MONTH(b.NgayLap) Like '%" + cboThang.Text + "%'  AND MONTH(c.NgayLap) Like '%" + cboThang.Text + "%'  AND MONTH(g.NgayLap) Like '%" + cboThang.Text + "%'";
            if (cboQuy.Text != "")
                sql = sql + " AND DATEPART(quarter, b.NgayLap) Like '%" + cboQuy.Text + "%' AND DATEPART(quarter, c.NgayLap) Like '%" + cboQuy.Text + "%' AND DATEPART(quarter, g.NgayLap) Like '%" + cboQuy.Text + "%'";
            if (txtNam.Text != "")
                sql = sql + "AND Year(b.NgayLap) Like '%" + txtNam.Text + "%'AND Year(c.NgayLap) Like '%" + txtNam.Text + "%'AND Year(g.NgayLap) Like '%" + txtNam.Text + "%'";
            DataTable tableHT = Class.Functions.GetDataToTable(sql);

            //Tạo dòng tiêu đề bảng
            exRange.Range["A6:O6"].Font.Bold = true;
            exRange.Range["A6:O6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C6:C6"].ColumnWidth = 22;
            exRange.Range["A6:B6"].ColumnWidth = 15;
            exRange.Range["D6:O6"].ColumnWidth = 15;
            exRange.Range["A6:O6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A7:O7"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A8:O8"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A9:O9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A10:O10"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A11:O11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A12:O12"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A13:O13"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:A6"].Value = "STT";
            exRange.Range["B6:B6"].Value = "Mã Hàng";
            exRange.Range["C6:C6"].Value = "Tên Hàng";
            exRange.Range["D6:D6"].Value = "SL Nhập";
            exRange.Range["E6:E6"].Value = "SL Xuất";
            exRange.Range["F6:F6"].Value = "SL Tiêu Hủy";
            exRange.Range["G6:G6"].Value = "HSD còn lại";
            exRange.Range["H6:H6"].Value = "Tổng tồn kho";
            for (hang = 0; hang < tableHT.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 7] = hang + 1;
                for (cot = 0; cot < tableHT.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 7
                {
                    exSheet.Cells[cot + 2][hang + 7] = tableHT.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 7] = tableHT.Rows[hang][cot].ToString();
                }
            }
            exRange = exSheet.Cells[4][hang + 13]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(DateTime.Now);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Ký Tên";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exApp.Visible = true;
        }
    }
}
