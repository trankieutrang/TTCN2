
namespace QuanLyKho
{
    partial class frmHangTonKho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mskNgayTH = new System.Windows.Forms.MaskedTextBox();
            this.mskNgayXuat = new System.Windows.Forms.MaskedTextBox();
            this.mskNgaynhap = new System.Windows.Forms.MaskedTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTKtieuhuy = new System.Windows.Forms.TextBox();
            this.txtTong = new System.Windows.Forms.Label();
            this.txtTKtong = new System.Windows.Forms.TextBox();
            this.txtHSD = new System.Windows.Forms.Label();
            this.txtHSDTK = new System.Windows.Forms.TextBox();
            this.txtSoTH = new System.Windows.Forms.Label();
            this.txtSoXuat = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSoNhap = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMaHang = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_TKketqua = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnĐong = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLoc = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtTenHangLoc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboMaHang = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_TKketqua)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(480, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 341);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mskNgayTH);
            this.groupBox1.Controls.Add(this.mskNgayXuat);
            this.groupBox1.Controls.Add(this.mskNgaynhap);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtTKtieuhuy);
            this.groupBox1.Controls.Add(this.txtTong);
            this.groupBox1.Controls.Add(this.txtTKtong);
            this.groupBox1.Controls.Add(this.txtHSD);
            this.groupBox1.Controls.Add(this.txtHSDTK);
            this.groupBox1.Controls.Add(this.txtSoTH);
            this.groupBox1.Controls.Add(this.txtSoXuat);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtSoNhap);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtMaHang);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 336);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chi tiết";
            // 
            // mskNgayTH
            // 
            this.mskNgayTH.Location = new System.Drawing.Point(85, 132);
            this.mskNgayTH.Mask = "00/00/0000";
            this.mskNgayTH.Name = "mskNgayTH";
            this.mskNgayTH.Size = new System.Drawing.Size(100, 20);
            this.mskNgayTH.TabIndex = 36;
            this.mskNgayTH.ValidatingType = typeof(System.DateTime);
            // 
            // mskNgayXuat
            // 
            this.mskNgayXuat.Location = new System.Drawing.Point(85, 94);
            this.mskNgayXuat.Mask = "00/00/0000";
            this.mskNgayXuat.Name = "mskNgayXuat";
            this.mskNgayXuat.Size = new System.Drawing.Size(100, 20);
            this.mskNgayXuat.TabIndex = 35;
            this.mskNgayXuat.ValidatingType = typeof(System.DateTime);
            // 
            // mskNgaynhap
            // 
            this.mskNgaynhap.Location = new System.Drawing.Point(85, 57);
            this.mskNgaynhap.Mask = "00/00/0000";
            this.mskNgaynhap.Name = "mskNgaynhap";
            this.mskNgaynhap.Size = new System.Drawing.Size(100, 20);
            this.mskNgaynhap.TabIndex = 34;
            this.mskNgaynhap.ValidatingType = typeof(System.DateTime);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Ngày CN TH:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Ngày CN xuất:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Ngày CN nhập:";
            // 
            // txtTKtieuhuy
            // 
            this.txtTKtieuhuy.Location = new System.Drawing.Point(288, 132);
            this.txtTKtieuhuy.Name = "txtTKtieuhuy";
            this.txtTKtieuhuy.Size = new System.Drawing.Size(100, 20);
            this.txtTKtieuhuy.TabIndex = 0;
            // 
            // txtTong
            // 
            this.txtTong.AutoSize = true;
            this.txtTong.Location = new System.Drawing.Point(194, 175);
            this.txtTong.Name = "txtTong";
            this.txtTong.Size = new System.Drawing.Size(80, 13);
            this.txtTong.TabIndex = 27;
            this.txtTong.Text = "Tổng hàng tồn:";
            // 
            // txtTKtong
            // 
            this.txtTKtong.Location = new System.Drawing.Point(288, 168);
            this.txtTKtong.Name = "txtTKtong";
            this.txtTKtong.Size = new System.Drawing.Size(100, 20);
            this.txtTKtong.TabIndex = 0;
            // 
            // txtHSD
            // 
            this.txtHSD.AutoSize = true;
            this.txtHSD.Location = new System.Drawing.Point(194, 26);
            this.txtHSD.Name = "txtHSD";
            this.txtHSD.Size = new System.Drawing.Size(67, 13);
            this.txtHSD.TabIndex = 25;
            this.txtHSD.Text = "HSD còn lại:";
            // 
            // txtHSDTK
            // 
            this.txtHSDTK.Location = new System.Drawing.Point(286, 19);
            this.txtHSDTK.Name = "txtHSDTK";
            this.txtHSDTK.Size = new System.Drawing.Size(100, 20);
            this.txtHSDTK.TabIndex = 0;
            // 
            // txtSoTH
            // 
            this.txtSoTH.AutoSize = true;
            this.txtSoTH.Location = new System.Drawing.Point(194, 139);
            this.txtSoTH.Name = "txtSoTH";
            this.txtSoTH.Size = new System.Drawing.Size(90, 13);
            this.txtSoTH.TabIndex = 19;
            this.txtSoTH.Text = "Số hàng tiêu hủy:";
            // 
            // txtSoXuat
            // 
            this.txtSoXuat.Location = new System.Drawing.Point(288, 94);
            this.txtSoXuat.Name = "txtSoXuat";
            this.txtSoXuat.Size = new System.Drawing.Size(100, 20);
            this.txtSoXuat.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(194, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Số lượng xuất:";
            // 
            // txtSoNhap
            // 
            this.txtSoNhap.Location = new System.Drawing.Point(288, 57);
            this.txtSoNhap.Name = "txtSoNhap";
            this.txtSoNhap.Size = new System.Drawing.Size(100, 20);
            this.txtSoNhap.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(194, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Số lượng nhập:";
            // 
            // txtMaHang
            // 
            this.txtMaHang.Location = new System.Drawing.Point(85, 19);
            this.txtMaHang.Name = "txtMaHang";
            this.txtMaHang.Size = new System.Drawing.Size(100, 20);
            this.txtMaHang.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mã Hàng:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Location = new System.Drawing.Point(9, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(465, 273);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_TKketqua);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 266);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kết quả";
            // 
            // dataGridView_TKketqua
            // 
            this.dataGridView_TKketqua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_TKketqua.Location = new System.Drawing.Point(7, 20);
            this.dataGridView_TKketqua.Name = "dataGridView_TKketqua";
            this.dataGridView_TKketqua.Size = new System.Drawing.Size(445, 240);
            this.dataGridView_TKketqua.TabIndex = 0;
            this.dataGridView_TKketqua.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_TKketqua_CellClick);
            this.dataGridView_TKketqua.Click += new System.EventHandler(this.dataGridView_TKketqua_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCapNhat);
            this.panel3.Controls.Add(this.btnĐong);
            this.panel3.Controls.Add(this.btnHuy);
            this.panel3.Controls.Add(this.btnIn);
            this.panel3.Location = new System.Drawing.Point(9, 406);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(871, 40);
            this.panel3.TabIndex = 2;
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.Location = new System.Drawing.Point(112, 10);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(75, 23);
            this.btnCapNhat.TabIndex = 4;
            this.btnCapNhat.Text = "Cập Nhật";
            this.btnCapNhat.UseVisualStyleBackColor = true;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnĐong
            // 
            this.btnĐong.Location = new System.Drawing.Point(672, 10);
            this.btnĐong.Name = "btnĐong";
            this.btnĐong.Size = new System.Drawing.Size(75, 23);
            this.btnĐong.TabIndex = 7;
            this.btnĐong.Text = "Đóng";
            this.btnĐong.UseVisualStyleBackColor = true;
            this.btnĐong.Click += new System.EventHandler(this.btnĐong_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(290, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(494, 10);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(75, 23);
            this.btnIn.TabIndex = 6;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(9, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(871, 41);
            this.panel4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(329, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "HÀNG TỒN KHO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Mã Hàng:";
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(377, 16);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(75, 25);
            this.btnLoc.TabIndex = 3;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Location = new System.Drawing.Point(9, 59);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(465, 65);
            this.panel5.TabIndex = 18;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTenHangLoc);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cboMaHang);
            this.groupBox3.Controls.Add(this.btnLoc);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(456, 59);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lọc";
            // 
            // txtTenHangLoc
            // 
            this.txtTenHangLoc.Location = new System.Drawing.Point(253, 19);
            this.txtTenHangLoc.Name = "txtTenHangLoc";
            this.txtTenHangLoc.Size = new System.Drawing.Size(100, 20);
            this.txtTenHangLoc.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(185, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Tên Hàng:";
            // 
            // cboMaHang
            // 
            this.cboMaHang.FormattingEnabled = true;
            this.cboMaHang.Location = new System.Drawing.Point(69, 19);
            this.cboMaHang.Name = "cboMaHang";
            this.cboMaHang.Size = new System.Drawing.Size(100, 21);
            this.cboMaHang.TabIndex = 17;
            this.cboMaHang.TextChanged += new System.EventHandler(this.cboMaHang_TextChanged);
            // 
            // frmHangTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 453);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmHangTonKho";
            this.Text = "Hàng Tồn Kho";
            this.Load += new System.EventHandler(this.frmHangTonKho_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_TKketqua)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnĐong;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_TKketqua;
        private System.Windows.Forms.TextBox txtMaHang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtTong;
        private System.Windows.Forms.TextBox txtTKtong;
        private System.Windows.Forms.Label txtHSD;
        private System.Windows.Forms.TextBox txtHSDTK;
        private System.Windows.Forms.Label txtSoTH;
        private System.Windows.Forms.TextBox txtSoXuat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSoNhap;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTKtieuhuy;
        private System.Windows.Forms.MaskedTextBox mskNgayTH;
        private System.Windows.Forms.MaskedTextBox mskNgayXuat;
        private System.Windows.Forms.MaskedTextBox mskNgaynhap;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTenHangLoc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboMaHang;
    }
}