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
using COMExcel = Microsoft.Office.Interop.Excel;
namespace QuanLyKho
{
    public partial class frmHangTonKho : Form
    {
        public frmHangTonKho()
        {
            InitializeComponent();
        }


        private void frmHangTonKho_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            txtTenHangLoc.ReadOnly = true;
            mskNgayXuat.ReadOnly = true;
            mskNgayTH.ReadOnly = true;
            mskNgaynhap.ReadOnly = true;
            txtMaHang.ReadOnly = true;
            txtSoNhap.ReadOnly = true;
            txtSoXuat.ReadOnly = true;
            txtHSDTK.ReadOnly = true;
            txtTKtieuhuy.ReadOnly = true;
            txtTKtong.ReadOnly = true;
            loadDataToGridView();
            dataGridView_TKketqua.DataSource = null;
            Class.Functions.FillCombo("SELECT MaHang FROM HangHoa", cboMaHang, "MaHang", "MaHang");
            cboMaHang.SelectedIndex = -1;
        }

        private void btnĐong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain f = new frmMain();
            f.Show();
        }

        DataTable tableTKketqua;
        private void loadDataToGridView()
        {
            string sql;
            sql = "select MaHang,(select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "') as 'SLNhap', " +
                "(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "') as 'SLXuat'," +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "') as 'SLTHuy'," +
                "(SELECT MAX(a.ngaylap) FROM PhieuNK a join ChiTietPhieuNK b on a.MaPhieuNK = b.MaPhieuNK WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayNhap'," +
                "(SELECT MAX(a.ngaylap) FROM PhieuXK a join ChiTietPhieuXK b on a.MaPhieuXK = b.MaPhieuXK WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayXuat'," +
                "(SELECT MAX(a.ngaylap) FROM ThuHoi_TieuHuy a join ChiTietThuHoi_TieuHuy b on a.MaDon = b.MaDon WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayTieuHuy'," +
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
            dataGridView_TKketqua.Columns[1].HeaderText = "SL Nhập";
            dataGridView_TKketqua.Columns[2].HeaderText = "SL Xuất";
            dataGridView_TKketqua.Columns[3].HeaderText = "SL Tiêu hủy";
            dataGridView_TKketqua.Columns[4].HeaderText = "HSD còn lại";
            dataGridView_TKketqua.Columns[5].HeaderText = "Ngày nhập";
            dataGridView_TKketqua.Columns[6].HeaderText = "Ngày xuất";
            dataGridView_TKketqua.Columns[7].HeaderText = "Ngày tiêu hủy";
            dataGridView_TKketqua.Columns[8].HeaderText = "Tổng tồn kho";
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_TKketqua.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_TKketqua.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void dataGridView_TKketqua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHang.Text = dataGridView_TKketqua.CurrentRow.Cells["MaHang"].Value.ToString();
            txtSoNhap.Text = dataGridView_TKketqua.CurrentRow.Cells["SLNhap"].Value.ToString();
            txtSoXuat.Text = dataGridView_TKketqua.CurrentRow.Cells["SLXuat"].Value.ToString();
            txtTKtieuhuy.Text = dataGridView_TKketqua.CurrentRow.Cells["SLTHuy"].Value.ToString();
            mskNgaynhap.Text = dataGridView_TKketqua.CurrentRow.Cells["NgayNhap"].Value.ToString();
            mskNgayXuat.Text = dataGridView_TKketqua.CurrentRow.Cells["NgayXuat"].Value.ToString();
            mskNgayTH.Text = dataGridView_TKketqua.CurrentRow.Cells["NgayTieuHuy"].Value.ToString();
            txtHSDTK.Text = dataGridView_TKketqua.CurrentRow.Cells["HSDcon"].Value.ToString();
            txtTKtong.Text = dataGridView_TKketqua.CurrentRow.Cells["Tong"].Value.ToString();
        }

        private void dataGridView_TKketqua_Click(object sender, EventArgs e)
        {
            if (tableTKketqua.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }
        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtSoNhap.Text = "";
            txtSoXuat.Text = "";
            txtTKtieuhuy.Text = "";
            txtHSDTK.Text = "";
            txtTKtong.Text = "";
            mskNgayTH.Text = " / /";
            mskNgaynhap.Text = " / /";
            mskNgayXuat.Text = " / /";
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnHuy.Enabled = false;
            btnCapNhat.Enabled = true;
            btnIn.Enabled = true;
            btnLoc.Enabled = true;
            btnĐong.Enabled = false;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTieuHuy f1 = new frmTieuHuy();
            f1.Show();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "select MaHang,(select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "') as 'SLNhap', " +
                "(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "') as 'SLXuat'," +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "') as 'SLTHuy'," +
                "(SELECT MAX(a.ngaylap) FROM PhieuNK a join ChiTietPhieuNK b on a.MaPhieuNK = b.MaPhieuNK WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayNhap'," +
                "(SELECT MAX(a.ngaylap) FROM PhieuXK a join ChiTietPhieuXK b on a.MaPhieuXK = b.MaPhieuXK WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayXuat'," +
                "(SELECT MAX(a.ngaylap) FROM ThuHoi_TieuHuy a join ChiTietThuHoi_TieuHuy b on a.MaDon = b.MaDon WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayTieuHuy'," +
               "case when (HanSuDung-DATEDIFF(day,NgaySanXuat,GETDATE()))<0 then 'Het han su dung' else convert(nvarchar, (HanSuDung - DATEDIFF(day, NgaySanXuat, GETDATE()))) end as 'HSDcon',"+
                "((case when((select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "')" +
                "-(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "')-" +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "'))< 0 then '0'" +
                "when((select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "')" +
                "-(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "')-" +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "'))> 0 " +
                "then((select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "')" +
                "-(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "')-" +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "')) end)) as'Tong'" +
                "from HangHoa where 1=1";
            if (cboMaHang.Text != "")
                sql = sql + " AND MaHang='" + cboMaHang.Text + "'";
            tableTKketqua = Class.Functions.GetDataToTable(sql);
            if (tableTKketqua.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tableTKketqua.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            tableTKketqua = Class.Functions.GetDataToTable(sql);
            dataGridView_TKketqua.DataSource = tableTKketqua;
            Class.Functions.RunSql(sql);
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
            exRange.Range["D2:H2"].Value = "DANH SÁCH HÀNG TỒN KHO";
            string sql;
            sql = "select MaHang,(select isnull(sum(SoLuong),0) from ChiTietPhieuNK Where MaHang='" + cboMaHang.Text + "') as 'SLNhap', " +
                "(select isnull(sum(SoLuong),0) from ChiTietPhieuXK Where MaHang = '" + cboMaHang.Text + "') as 'SLXuat'," +
                "(select isnull(sum(SoLuong),0) from ChiTietThuHoi_TieuHuy Where MaHang = '" + cboMaHang.Text + "') as 'SLTHuy'," +
                "(SELECT MAX(a.ngaylap) FROM PhieuNK a join ChiTietPhieuNK b on a.MaPhieuNK = b.MaPhieuNK WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayNhap'," +
                "(SELECT MAX(a.ngaylap) FROM PhieuXK a join ChiTietPhieuXK b on a.MaPhieuXK = b.MaPhieuXK WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayXuat'," +
                "(SELECT MAX(a.ngaylap) FROM ThuHoi_TieuHuy a join ChiTietThuHoi_TieuHuy b on a.MaDon = b.MaDon WHERE b.MaHang = '" + cboMaHang.Text + "') as 'NgayTieuHuy'," +
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
                "from HangHoa where 1=1";
            if (cboMaHang.Text != "")
                sql = sql + " AND MaHang='" + cboMaHang.Text + "'";
            tableTKketqua = Class.Functions.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A6:O6"].Font.Bold = true;
            exRange.Range["A6:O6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:E6"].ColumnWidth = 13;
            exRange.Range["E6:O6"].ColumnWidth = 18;
            exRange.Range["A6:O6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A7:O7"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A8:O8"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A9:O9"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:A6"].Value = "STT";
            exRange.Range["B6:B6"].Value = "Mã Hàng";
            exRange.Range["C6:C6"].Value = "SL Nhập";
            exRange.Range["D6:D6"].Value = "SL Xuất";
            exRange.Range["E6:E6"].Value = "SL Tiêu Hủy";
            exRange.Range["F6:F6"].Value = "Ngày CN Nhập";
            exRange.Range["G6:G6"].Value = "Ngày CN Xuất";
            exRange.Range["H6:H6"].Value = "Ngày CN Tiêu Hủy";
            exRange.Range["I6:I6"].Value = "HSD còn lại";
            exRange.Range["J6:J6"].Value = "Tổng tồn kho";
            for (hang = 0; hang < tableTKketqua.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 7] = hang + 1;
                for (cot = 0; cot < tableTKketqua.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 7
                {
                    exSheet.Cells[cot + 2][hang + 7] = tableTKketqua.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 7] = tableTKketqua.Rows[hang][cot].ToString();
                }
            }
            exRange = exSheet.Cells[4][hang + 13]; //Ô A1 
            exRange.Range["C1:E1"].MergeCells = true;
            exRange.Range["C1:E1"].Font.Italic = true;
            exRange.Range["C1:E1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(DateTime.Now);
            exRange.Range["C1:E1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].Font.Italic = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "Ký Tên";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Font.Italic = true;
            exRange.Range["C6:E6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exApp.Visible = true;
        }

        private void cboMaHang_TextChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaHang.Text == "")
            {
                txtTenHangLoc.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenHang from HangHoa where MaHang = N'" + cboMaHang.Text + "'";
            txtTenHangLoc.Text = Functions.GetFieldValues(str);
        }
    }
}
