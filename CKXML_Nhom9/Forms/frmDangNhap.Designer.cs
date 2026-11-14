using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;
        private Label lblPhuDe = null!;
        private Label lblTenDangNhap = null!;
        private TextBox txtTenDangNhap = null!;
        private Label lblMatKhau = null!;
        private TextBox txtMatKhau = null!;
        private CheckBox chkHienMatKhau = null!;
        private Button btnDangNhap = null!;
        private Button btnThoat = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblTieuDe = new Label();
            this.lblPhuDe = new Label();
            this.lblTenDangNhap = new Label();
            this.txtTenDangNhap = new TextBox();
            this.lblMatKhau = new Label();
            this.txtMatKhau = new TextBox();
            this.chkHienMatKhau = new CheckBox();
            this.btnDangNhap = new Button();
            this.btnThoat = new Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Controls.Add(this.lblPhuDe);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(440, 80);
            this.pnlHeader.TabIndex = 0;

            // lblTieuDe
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 14);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new Size(440, 28);
            this.lblTieuDe.Text = "HỆ THỐNG RẠP CHIẾU PHIM";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // lblPhuDe
            this.lblPhuDe.AutoSize = false;
            this.lblPhuDe.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblPhuDe.ForeColor = Color.FromArgb(226, 232, 240);
            this.lblPhuDe.Location = new Point(0, 44);
            this.lblPhuDe.Name = "lblPhuDe";
            this.lblPhuDe.Size = new Size(440, 22);
            this.lblPhuDe.Text = "Đăng nhập hệ thống quản lý và bán vé";
            this.lblPhuDe.TextAlign = ContentAlignment.MiddleCenter;

            // lblTenDangNhap
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTenDangNhap.Location = new Point(45, 110);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new Size(110, 19);
            this.lblTenDangNhap.TabIndex = 1;
            this.lblTenDangNhap.Text = "Tên đăng nhập:";

            // txtTenDangNhap
            this.txtTenDangNhap.Font = new Font("Segoe UI", 11F);
            this.txtTenDangNhap.Location = new Point(48, 134);
            this.txtTenDangNhap.MaxLength = 20;
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new Size(344, 27);
            this.txtTenDangNhap.TabIndex = 0;

            // lblMatKhau
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblMatKhau.Location = new Point(45, 175);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new Size(78, 19);
            this.lblMatKhau.TabIndex = 2;
            this.lblMatKhau.Text = "Mật khẩu:";

            // txtMatKhau
            this.txtMatKhau.Font = new Font("Segoe UI", 11F);
            this.txtMatKhau.Location = new Point(48, 199);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new Size(344, 27);
            this.txtMatKhau.TabIndex = 1;
            this.txtMatKhau.UseSystemPasswordChar = true;

            // chkHienMatKhau
            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Checked = false;
            this.chkHienMatKhau.Font = new Font("Segoe UI", 9.5F);
            this.chkHienMatKhau.Location = new Point(48, 234);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new Size(121, 21);
            this.chkHienMatKhau.TabIndex = 2;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);

            // btnDangNhap
            this.btnDangNhap.BackColor = Color.FromArgb(37, 99, 235);
            this.btnDangNhap.FlatAppearance.BorderSize = 0;
            this.btnDangNhap.FlatStyle = FlatStyle.Flat;
            this.btnDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDangNhap.ForeColor = Color.White;
            this.btnDangNhap.Location = new Point(48, 272);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new Size(165, 38);
            this.btnDangNhap.TabIndex = 3;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = false;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);

            // btnThoat
            this.btnThoat.BackColor = Color.FromArgb(226, 232, 240);
            this.btnThoat.DialogResult = DialogResult.Cancel;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = FlatStyle.Flat;
            this.btnThoat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnThoat.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnThoat.Location = new Point(227, 272);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new Size(165, 38);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);

            // frmDangNhap
            this.AcceptButton = this.btnDangNhap;
            this.CancelButton = this.btnThoat;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(440, 335);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.chkHienMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblTenDangNhap);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống";
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
