
namespace QuanLyKho
{
    partial class frmDangNhap
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
            this.lblSaiPass = new System.Windows.Forms.Label();
            this.checkShowPass = new System.Windows.Forms.CheckBox();
            this.btThoat = new System.Windows.Forms.Button();
            this.btDangNhap = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTenDN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSaiPass);
            this.panel1.Controls.Add(this.checkShowPass);
            this.panel1.Controls.Add(this.btThoat);
            this.panel1.Controls.Add(this.btDangNhap);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 195);
            this.panel1.TabIndex = 0;
            // 
            // lblSaiPass
            // 
            this.lblSaiPass.AutoSize = true;
            this.lblSaiPass.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaiPass.ForeColor = System.Drawing.Color.Red;
            this.lblSaiPass.Location = new System.Drawing.Point(19, 146);
            this.lblSaiPass.Name = "lblSaiPass";
            this.lblSaiPass.Size = new System.Drawing.Size(0, 16);
            this.lblSaiPass.TabIndex = 6;
            // 
            // checkShowPass
            // 
            this.checkShowPass.AutoSize = true;
            this.checkShowPass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkShowPass.Location = new System.Drawing.Point(19, 120);
            this.checkShowPass.Name = "checkShowPass";
            this.checkShowPass.Size = new System.Drawing.Size(122, 19);
            this.checkShowPass.TabIndex = 5;
            this.checkShowPass.Text = "Hiện thị mật khẩu";
            this.checkShowPass.UseVisualStyleBackColor = true;
            this.checkShowPass.CheckedChanged += new System.EventHandler(this.checkShowPass_CheckedChanged);
            // 
            // btThoat
            // 
            this.btThoat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThoat.Location = new System.Drawing.Point(287, 169);
            this.btThoat.Name = "btThoat";
            this.btThoat.Size = new System.Drawing.Size(83, 23);
            this.btThoat.TabIndex = 4;
            this.btThoat.Text = "Thoát";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // btDangNhap
            // 
            this.btDangNhap.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btDangNhap.Location = new System.Drawing.Point(172, 169);
            this.btDangNhap.Name = "btDangNhap";
            this.btDangNhap.Size = new System.Drawing.Size(83, 23);
            this.btDangNhap.TabIndex = 3;
            this.btDangNhap.Text = "Đăng Nhập";
            this.btDangNhap.UseVisualStyleBackColor = true;
            this.btDangNhap.Click += new System.EventHandler(this.btDangNhap_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtPass);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(4, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(379, 52);
            this.panel3.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(144, 16);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(222, 20);
            this.txtPass.TabIndex = 2;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mật Khẩu:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTenDN);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 54);
            this.panel2.TabIndex = 0;
            // 
            // txtTenDN
            // 
            this.txtTenDN.Location = new System.Drawing.Point(144, 16);
            this.txtTenDN.Name = "txtTenDN";
            this.txtTenDN.Size = new System.Drawing.Size(222, 20);
            this.txtTenDN.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên Đăng Nhập:";
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 202);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDangNhap_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtTenDN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btThoat;
        private System.Windows.Forms.Button btDangNhap;
        private System.Windows.Forms.CheckBox checkShowPass;
        private System.Windows.Forms.Label lblSaiPass;
    }
}

