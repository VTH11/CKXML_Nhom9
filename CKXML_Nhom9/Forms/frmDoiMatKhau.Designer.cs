using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmDoiMatKhau
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTieuDe = null!;
        private Label lblMatKhauCu = null!;
        private TextBox txtMatKhauCu = null!;
        private Label lblMatKhauMoi = null!;
        private TextBox txtMatKhauMoi = null!;
        private Label lblXacNhanMatKhau = null!;
        private TextBox txtXacNhanMatKhau = null!;
        private CheckBox chkHienMatKhau = null!;
        private Button btnLuu = null!;
        private Button btnHuy = null!;

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
            this.lblTieuDe = new Label();
            this.lblMatKhauCu = new Label();
            this.txtMatKhauCu = new TextBox();
            this.lblMatKhauMoi = new Label();
            this.txtMatKhauMoi = new TextBox();
            this.lblXacNhanMatKhau = new Label();
            this.txtXacNhanMatKhau = new TextBox();
            this.chkHienMatKhau = new CheckBox();
            this.btnLuu = new Button();
            this.btnHuy = new Button();
            this.SuspendLayout();

            // lblTieuDe
            this.lblTieuDe.BackColor = Color.FromArgb(30, 41, 59);
            this.lblTieuDe.Dock = DockStyle.Top;
            this.lblTieuDe.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new Size(410, 52);
            this.lblTieuDe.Text = "ĐỔI MẬT KHẨU TÀI KHOẢN";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // lblMatKhauCu
            this.lblMatKhauCu.AutoSize = true;
            this.lblMatKhauCu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblMatKhauCu.Location = new Point(35, 70);
            this.lblMatKhauCu.Name = "lblMatKhauCu";
            this.lblMatKhauCu.Size = new Size(125, 17);
            this.lblMatKhauCu.Text = "Mật khẩu hiện tại:";

            // txtMatKhauCu
            this.txtMatKhauCu.Font = new Font("Segoe UI", 10.5F);
            this.txtMatKhauCu.Location = new Point(38, 92);
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.Size = new Size(334, 26);
            this.txtMatKhauCu.TabIndex = 0;
            this.txtMatKhauCu.UseSystemPasswordChar = true;

            // lblMatKhauMoi
            this.lblMatKhauMoi.AutoSize = true;
            this.lblMatKhauMoi.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblMatKhauMoi.Location = new Point(35, 130);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new Size(100, 17);
            this.lblMatKhauMoi.Text = "Mật khẩu mới:";

            // txtMatKhauMoi
            this.txtMatKhauMoi.Font = new Font("Segoe UI", 10.5F);
            this.txtMatKhauMoi.Location = new Point(38, 152);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Size = new Size(334, 26);
            this.txtMatKhauMoi.TabIndex = 1;
            this.txtMatKhauMoi.UseSystemPasswordChar = true;

            // lblXacNhanMatKhau
            this.lblXacNhanMatKhau.AutoSize = true;
            this.lblXacNhanMatKhau.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblXacNhanMatKhau.Location = new Point(35, 190);
            this.lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            this.lblXacNhanMatKhau.Size = new Size(158, 17);
            this.lblXacNhanMatKhau.Text = "Xác nhận mật khẩu mới:";

            // txtXacNhanMatKhau
            this.txtXacNhanMatKhau.Font = new Font("Segoe UI", 10.5F);
            this.txtXacNhanMatKhau.Location = new Point(38, 212);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.Size = new Size(334, 26);
            this.txtXacNhanMatKhau.TabIndex = 2;
            this.txtXacNhanMatKhau.UseSystemPasswordChar = true;

            // chkHienMatKhau
            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Font = new Font("Segoe UI", 9.5F);
            this.chkHienMatKhau.Location = new Point(38, 246);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new Size(118, 21);
            this.chkHienMatKhau.TabIndex = 3;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);

            // btnLuu
            this.btnLuu.BackColor = Color.FromArgb(22, 163, 74);
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = FlatStyle.Flat;
            this.btnLuu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLuu.ForeColor = Color.White;
            this.btnLuu.Location = new Point(38, 280);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new Size(160, 36);
            this.btnLuu.TabIndex = 4;
            this.btnLuu.Text = "Lưu mật khẩu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuy
            this.btnHuy.BackColor = Color.FromArgb(226, 232, 240);
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.FlatStyle = FlatStyle.Flat;
            this.btnHuy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnHuy.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnHuy.Location = new Point(212, 280);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new Size(160, 36);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // frmDoiMatKhau
            this.AcceptButton = this.btnLuu;
            this.CancelButton = this.btnHuy;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(410, 336);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.chkHienMatKhau);
            this.Controls.Add(this.txtXacNhanMatKhau);
            this.Controls.Add(this.lblXacNhanMatKhau);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.lblMatKhauMoi);
            this.Controls.Add(this.txtMatKhauCu);
            this.Controls.Add(this.lblMatKhauCu);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Đổi mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
