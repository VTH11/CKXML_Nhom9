using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmBaoCaoDoanhThu
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private GroupBox grpChonKy = null!;
        private Label lblTuNgay = null!;
        private DateTimePicker dtpTuNgay = null!;
        private Label lblDenNgay = null!;
        private DateTimePicker dtpDenNgay = null!;
        private Button btnXemBaoCao = null!;
        private Button btnDong = null!;

        private Panel pnlTomTat = null!;
        private Panel pnlCardSoVe = null!;
        private Label lblTitleSoVe = null!;
        private Label lblTongSoVe = null!;
        private Panel pnlCardDoanhThu = null!;
        private Label lblTitleDoanhThu = null!;
        private Label lblTongDoanhThu = null!;

        private DataGridView dgvChiTietVe = null!;

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

            this.grpChonKy = new GroupBox();
            this.lblTuNgay = new Label();
            this.dtpTuNgay = new DateTimePicker();
            this.lblDenNgay = new Label();
            this.dtpDenNgay = new DateTimePicker();
            this.btnXemBaoCao = new Button();
            this.btnDong = new Button();

            this.pnlTomTat = new Panel();
            this.pnlCardSoVe = new Panel();
            this.lblTitleSoVe = new Label();
            this.lblTongSoVe = new Label();
            this.pnlCardDoanhThu = new Panel();
            this.lblTitleDoanhThu = new Label();
            this.lblTongDoanhThu = new Label();

            this.dgvChiTietVe = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.grpChonKy.SuspendLayout();
            this.pnlTomTat.SuspendLayout();
            this.pnlCardSoVe.SuspendLayout();
            this.pnlCardDoanhThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietVe)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(920, 50);

            // lblTieuDe
            this.lblTieuDe.Dock = DockStyle.Fill;
            this.lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new Size(920, 50);
            this.lblTieuDe.Text = "BÁO CÁO DOANH THU BÁN VÉ (XSLT -> HTML)";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // grpChonKy
            this.grpChonKy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpChonKy.Location = new Point(16, 60);
            this.grpChonKy.Name = "grpChonKy";
            this.grpChonKy.Size = new Size(888, 72);
            this.grpChonKy.TabIndex = 0;
            this.grpChonKy.TabStop = false;
            this.grpChonKy.Text = "Chọn khoảng thời gian báo cáo (Mặc định: Tháng hiện tại)";

            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new Font("Segoe UI", 9.5F);
            this.lblTuNgay.Location = new Point(20, 32);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Text = "Từ ngày:";

            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Font = new Font("Segoe UI", 10F);
            this.dtpTuNgay.Format = DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new Point(88, 28);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new Size(140, 25);
            this.dtpTuNgay.TabIndex = 0;
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.KhoangNgay_ValueChanged);

            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new Font("Segoe UI", 9.5F);
            this.lblDenNgay.Location = new Point(255, 32);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Text = "Đến ngày:";

            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Font = new Font("Segoe UI", 10F);
            this.dtpDenNgay.Format = DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new Point(332, 28);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new Size(140, 25);
            this.dtpDenNgay.TabIndex = 1;
            this.dtpDenNgay.ValueChanged += new System.EventHandler(this.KhoangNgay_ValueChanged);

            this.btnXemBaoCao.BackColor = Color.FromArgb(217, 119, 6);
            this.btnXemBaoCao.FlatAppearance.BorderSize = 0;
            this.btnXemBaoCao.FlatStyle = FlatStyle.Flat;
            this.btnXemBaoCao.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnXemBaoCao.ForeColor = Color.White;
            this.btnXemBaoCao.Location = new Point(515, 22);
            this.btnXemBaoCao.Name = "btnXemBaoCao";
            this.btnXemBaoCao.Size = new Size(235, 36);
            this.btnXemBaoCao.TabIndex = 2;
            this.btnXemBaoCao.Text = "Xem báo cáo HTML (XSLT)";
            this.btnXemBaoCao.UseVisualStyleBackColor = false;
            this.btnXemBaoCao.Cursor = Cursors.Hand;
            this.btnXemBaoCao.Click += new System.EventHandler(this.btnXemBaoCao_Click);

            this.btnDong.BackColor = Color.FromArgb(100, 116, 139);
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = FlatStyle.Flat;
            this.btnDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnDong.ForeColor = Color.White;
            this.btnDong.Location = new Point(762, 22);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new Size(110, 36);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Cursor = Cursors.Hand;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.grpChonKy.Controls.AddRange(new Control[]
            {
                this.lblTuNgay, this.dtpTuNgay,
                this.lblDenNgay, this.dtpDenNgay,
                this.btnXemBaoCao, this.btnDong
            });

            // pnlTomTat (Hiển thị nhanh Tổng số vé và Tổng doanh thu ngay trên form)
            this.pnlTomTat.Location = new Point(16, 142);
            this.pnlTomTat.Name = "pnlTomTat";
            this.pnlTomTat.Size = new Size(888, 82);

            this.pnlCardSoVe.BackColor = Color.White;
            this.pnlCardSoVe.BorderStyle = BorderStyle.FixedSingle;
            this.pnlCardSoVe.Location = new Point(0, 0);
            this.pnlCardSoVe.Size = new Size(432, 78);

            this.lblTitleSoVe.AutoSize = true;
            this.lblTitleSoVe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTitleSoVe.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTitleSoVe.Location = new Point(16, 12);
            this.lblTitleSoVe.Text = "TỔNG SỐ VÉ BÁN TRONG KỲ:";

            this.lblTongSoVe.AutoSize = true;
            this.lblTongSoVe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTongSoVe.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblTongSoVe.Location = new Point(16, 36);
            this.lblTongSoVe.Text = "0 vé";

            this.pnlCardSoVe.Controls.Add(this.lblTitleSoVe);
            this.pnlCardSoVe.Controls.Add(this.lblTongSoVe);

            this.pnlCardDoanhThu.BackColor = Color.White;
            this.pnlCardDoanhThu.BorderStyle = BorderStyle.FixedSingle;
            this.pnlCardDoanhThu.Location = new Point(456, 0);
            this.pnlCardDoanhThu.Size = new Size(432, 78);

            this.lblTitleDoanhThu.AutoSize = true;
            this.lblTitleDoanhThu.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblTitleDoanhThu.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblTitleDoanhThu.Location = new Point(16, 12);
            this.lblTitleDoanhThu.Text = "TỔNG DOANH THU TRONG KỲ:";

            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblTongDoanhThu.Location = new Point(16, 36);
            this.lblTongDoanhThu.Text = "0đ";

            this.pnlCardDoanhThu.Controls.Add(this.lblTitleDoanhThu);
            this.pnlCardDoanhThu.Controls.Add(this.lblTongDoanhThu);

            this.pnlTomTat.Controls.Add(this.pnlCardSoVe);
            this.pnlTomTat.Controls.Add(this.pnlCardDoanhThu);

            // dgvChiTietVe
            this.dgvChiTietVe.AllowUserToAddRows = false;
            this.dgvChiTietVe.AllowUserToDeleteRows = false;
            this.dgvChiTietVe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietVe.BackgroundColor = Color.White;
            this.dgvChiTietVe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietVe.Location = new Point(16, 232);
            this.dgvChiTietVe.MultiSelect = false;
            this.dgvChiTietVe.Name = "dgvChiTietVe";
            this.dgvChiTietVe.ReadOnly = true;
            this.dgvChiTietVe.RowHeadersVisible = false;
            this.dgvChiTietVe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietVe.Size = new Size(888, 325);
            this.dgvChiTietVe.TabIndex = 1;

            // frmBaoCaoDoanhThu
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(920, 575);
            this.Controls.Add(this.dgvChiTietVe);
            this.Controls.Add(this.pnlTomTat);
            this.Controls.Add(this.grpChonKy);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmBaoCaoDoanhThu";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Báo cáo doanh thu bán vé";
            this.Load += new System.EventHandler(this.frmBaoCaoDoanhThu_Load);

            this.pnlHeader.ResumeLayout(false);
            this.grpChonKy.ResumeLayout(false);
            this.grpChonKy.PerformLayout();
            this.pnlTomTat.ResumeLayout(false);
            this.pnlCardSoVe.ResumeLayout(false);
            this.pnlCardSoVe.PerformLayout();
            this.pnlCardDoanhThu.ResumeLayout(false);
            this.pnlCardDoanhThu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietVe)).EndInit();
            this.ResumeLayout(false);
        }
    }
}

