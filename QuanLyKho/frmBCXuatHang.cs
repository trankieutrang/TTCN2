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
    public partial class frmBCXuatHang : Form
    {
        public frmBCXuatHang()
        {
            InitializeComponent();
        }

        private void frmBCXuatHang_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();
            txtMaNH.ReadOnly = true;
            txtSoLuong.ReadOnly = true;
            txtTenHang.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            mskNgayLap.ReadOnly = true;
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
            dataGridView_BCXuat.DataSource = null;
        }
        DataTable tableHT;
        private void loadDataToGridView()
        {
            string sql;
            sql = "	select a.MaNhomHang,b.TenHang,d.NgayLap,c.SoLuong from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK";
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_BCXuat.DataSource = tableHT;
            dataGridView_BCXuat.Columns[0].HeaderText = "Mã NH";
            dataGridView_BCXuat.Columns[1].HeaderText = "Tên Hàng";
            dataGridView_BCXuat.Columns[2].HeaderText = "Ngày Lập";
            dataGridView_BCXuat.Columns[3].HeaderText = "Số Lượng";
            // Không cho phép thêm mới dữ liệu trực tiếp trên lưới
            dataGridView_BCXuat.AllowUserToAddRows = false;
            // Không cho phép sửa dữ liệu trực tiếp trên lưới
            dataGridView_BCXuat.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView_BCXuat_Click(object sender, EventArgs e)
        {
            txtMaNH.Text = dataGridView_BCXuat.CurrentRow.Cells["MaNhomHang"].Value.ToString();
            txtTenHang.Text = dataGridView_BCXuat.CurrentRow.Cells["TenHang"].Value.ToString();
            mskNgayLap.Text = dataGridView_BCXuat.CurrentRow.Cells["NgayLap"].Value.ToString();
            txtSoLuong.Text = dataGridView_BCXuat.CurrentRow.Cells["SoLuong"].Value.ToString();
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
            sql = "Select a.MaNhomHang,b.TenHang,d.NgayLap,c.SoLuong from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK WHERE 1=1";
            if (cboNhomHang.Text != "")
                sql = sql + " AND a.TenNhomHang Like '%" + cboNhomHang.Text + "%' ";
            if (cboThang.Text != "")
                sql = sql + " AND MONTH(d.NgayLap) Like '%" + cboThang.Text + "%' ";
            if (cboQuy.Text != "")
                sql = sql + " AND DATEPART(quarter, d.NgayLap) Like '%" + cboQuy.Text + "%'";
            if (txtNam.Text != "")
                sql = sql + "AND Year(d.NgayLap) Like '%" + txtNam.Text + "%'";


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
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where a.TenNhomHang='" + cboNhomHang.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboThang.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where a.TenNhomHang='" + cboNhomHang.Text + "'and MONTH(d.NgayLap)='" + cboThang.Text + "' "));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboQuy.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK  where TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK  where TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboThang.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong)from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where DATEPART(mm,(d.NgayLap)) = '" + cboThang.Text + "'" +
                    "and TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
                if ((cboNhomHang.Text != "") && (cboQuy.Text != "") && (txtNam.Text != ""))
                {
                    tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where DATEPART(qq,(d.NgayLap)) = '" + cboQuy.Text + "'" +
                    "and TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                    txtTongTien.Text = tong.ToString();
                }
            }
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_BCXuat.DataSource = tableHT;
            tableHT = Class.Functions.GetDataToTable(sql);
            dataGridView_BCXuat.DataSource = tableHT;
            dataGridView_BCXuat.Columns[0].HeaderText = "Mã NH";
            dataGridView_BCXuat.Columns[1].HeaderText = "Tên Hàng";
            dataGridView_BCXuat.Columns[2].HeaderText = "Ngày Lập";
            dataGridView_BCXuat.Columns[3].HeaderText = "Số Lượng";
            Class.Functions.RunSql(sql);
        }
        private void ResetValues()
        {
            cboNhomHang.Text = "";
            txtNam.Text = "";
            cboThang.Text = "";
            cboQuy.Text = "";
            txtTongTien.Text = "0";
            cboNhomHang.Focus();
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dataGridView_BCXuat.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMain f1 = new frmMain();
            f1.Show();
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
            exRange.Range["D2:F2"].Value = "BÁO CÁO XUẤT HÀNG";
            string sql = "Select a.MaNhomHang,b.TenHang,d.NgayLap,c.SoLuong from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK WHERE 1=1";
            if (cboNhomHang.Text != "")
                sql = sql + " AND a.TenNhomHang Like '%" + cboNhomHang.Text + "%' ";
            if (cboThang.Text != "")
                sql = sql + " AND MONTH(d.NgayLap) Like '%" + cboThang.Text + "%' ";
            if (cboQuy.Text != "")
                sql = sql + " AND DATEPART(quarter, d.NgayLap) Like '%" + cboQuy.Text + "%'";
            if (txtNam.Text != "")
                sql = sql + "AND Year(d.NgayLap) Like '%" + txtNam.Text + "%'";

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
            exRange.Range["B6:B6"].Value = "Nhóm Hàng";
            exRange.Range["C6:C6"].Value = "Tên Hàng";
            exRange.Range["D6:D6"].Value = "Ngày lập";
            exRange.Range["E6:E6"].Value = "Số lượng";
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
            exRange = exSheet.Cells[cot][hang + 10];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng SL Xuất:";
            exRange = exSheet.Cells[cot + 1][hang + 10];
            exRange.Font.Bold = true;
            double tong;
            if ((cboNhomHang.Text != "") && (cboThang.Text != ""))
            {
                tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                    "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where a.TenNhomHang='" + cboNhomHang.Text + "'and MONTH(d.NgayLap)='" + cboThang.Text + "' "));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][hang + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.Functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboNhomHang.Text != "") && (cboQuy.Text != ""))
            {
                tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                    "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK  where TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(qq,(d.NgayLap))='" + cboQuy.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][hang + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.Functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboNhomHang.Text != "") && (txtNam.Text != ""))
            {
                tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                    "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK  where TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][hang + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.Functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboNhomHang.Text != "") && (cboThang.Text != "") && (txtNam.Text != ""))
            {
                tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong)from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                    "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where DATEPART(mm,(d.NgayLap)) = '" + cboThang.Text + "'" +
                "and TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][hang + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.Functions.ChuyenSoSangChu(tong.ToString());
            }
            if ((cboNhomHang.Text != "") && (cboQuy.Text != "") && (txtNam.Text != ""))
            {
                tong = Convert.ToDouble(Class.Functions.GetFieldValues("select sum(c.SoLuong) from NhomHang a join HangHoa b on a.MaNhomHang=b.MaNhomHang join ChiTietPhieuXK c on b.MaHang=c.MaHang " +
                        "join PhieuXK d on d.MaPhieuXK=c.MaPhieuXK where DATEPART(qq,(d.NgayLap)) = '" + cboQuy.Text + "'" +
                    "and TenNhomHang='" + cboNhomHang.Text + "'and DATEPART(yyyy,(d.NgayLap))='" + txtNam.Text + "'"));
                exRange.Value2 = tong.ToString();
                exRange = exSheet.Cells[1][hang + 11]; //Ô A1 
                exRange.Range["A1:F1"].MergeCells = true;
                exRange.Range["A1:F1"].Font.Bold = true;
                exRange.Range["A1:F1"].Font.Italic = true;
                exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
                exRange.Range["A1:F1"].Value = "Bằng chữ: " + Class.Functions.ChuyenSoSangChu(tong.ToString());
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
