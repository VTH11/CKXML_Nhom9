using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmBanVe
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private Panel pnlTrai = null!;
        private GroupBox grpChonSuat = null!;
        private Label lblPhim = null!;
        private ComboBox cbbPhim = null!;
        private Label lblNgayChieu = null!;
        private ComboBox cbbNgayChieu = null!;
        private Label lblSuatChieu = null!;
        private ComboBox cbbSuatChieu = null!;
        private Label lblThongTinPhong = null!;

        private GroupBox grpThongTinVe = null!;
        private Label lblNhanVien = null!;
        private Label lblGiaVeTitle = null!;
        private Label lblGiaVe = null!;
        private Label lblGheTitle = null!;
        private TextBox txtGheDangChon = null!;
        private Label lblTongTienTitle = null!;
        private Label lblTongTien = null!;
        private Button btnBoChon = null!;
        private Button btnXacNhanBanVe = null!;
        private Button btnDong = null!;

        private Panel pnlPhai = null!;
        private Panel pnlManHinhVaChuThich = null!;
        private Label lblManHinh = null!;
        private Panel pnlChuThich = null!;
        private Panel pnlMauTrong = null!;
        private Label lblChuThichTrong = null!;
        private Panel pnlMauDangChon = null!;
        private Label lblChuThichDangChon = null!;
        private Panel pnlMauDaBan = null!;
        private Label lblChuThichDaBan = null!;
        private Panel pnlVungChuaGhe = null!;

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

            this.pnlTrai = new Panel();
            this.grpChonSuat = new GroupBox();
            this.lblPhim = new Label();
            this.cbbPhim = new ComboBox();
            this.lblNgayChieu = new Label();
            this.cbbNgayChieu = new ComboBox();
            this.lblSuatChieu = new Label();
            this.cbbSuatChieu = new ComboBox();
            this.lblThongTinPhong = new Label();

            this.grpThongTinVe = new GroupBox();
            this.lblNhanVien = new Label();
            this.lblGiaVeTitle = new Label();
            this.lblGiaVe = new Label();
            this.lblGheTitle = new Label();
            this.txtGheDangChon = new TextBox();
            this.lblTongTienTitle = new Label();
            this.lblTongTien = new Label();
            this.btnBoChon = new Button();
            this.btnXacNhanBanVe = new Button();
            this.btnDong = new Button();

            this.pnlPhai = new Panel();
            this.pnlManHinhVaChuThich = new Panel();
            this.lblManHinh = new Label();
            this.pnlChuThich = new Panel();
            this.pnlMauTrong = new Panel();
            this.lblChuThichTrong = new Label();
            this.pnlMauDangChon = new Panel();
            this.lblChuThichDangChon = new Label();
            this.pnlMauDaBan = new Panel();
            this.lblChuThichDaBan = new Label();
            this.pnlVungChuaGhe = new Panel();

            this.pnlHeader.SuspendLayout();
            this.pnlTrai.SuspendLayout();
            this.grpChonSuat.SuspendLayout();
            this.grpThongTinVe.SuspendLayout();
            this.pnlPhai.SuspendLayout();
            this.pnlManHinhVaChuThich.SuspendLayout();
            this.pnlChuThich.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(1080, 50);

            // lblTieuDe
            this.lblTieuDe.Dock = DockStyle.Fill;
            this.lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new Size(1080, 50);
            this.lblTieuDe.Text = "BÁN VÉ XEM PHIM - SƠ ĐỒ GHẾ ĐỘNG";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // pnlTrai
            this.pnlTrai.Controls.Add(this.grpThongTinVe);
            this.pnlTrai.Controls.Add(this.grpChonSuat);
            this.pnlTrai.Dock = DockStyle.Left;
            this.pnlTrai.Location = new Point(0, 50);
            this.pnlTrai.Name = "pnlTrai";
            this.pnlTrai.Padding = new Padding(12);
            this.pnlTrai.Size = new Size(340, 610);

            // grpChonSuat
            this.grpChonSuat.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpChonSuat.Location = new Point(12, 10);
            this.grpChonSuat.Name = "grpChonSuat";
            this.grpChonSuat.Size = new Size(316, 252);
            this.grpChonSuat.TabIndex = 0;
            this.grpChonSuat.TabStop = false;
            this.grpChonSuat.Text = "1. Chọn suất chiếu (Lọc XPath)";

            this.lblPhim.AutoSize = true;
            this.lblPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblPhim.Location = new Point(14, 28);
            this.lblPhim.Name = "lblPhim";
            this.lblPhim.Text = "Chọn bộ phim:";

            this.cbbPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbPhim.Font = new Font("Segoe UI", 9.5F);
            this.cbbPhim.Location = new Point(16, 50);
            this.cbbPhim.Name = "cbbPhim";
            this.cbbPhim.Size = new Size(284, 25);
            this.cbbPhim.TabIndex = 0;
            this.cbbPhim.SelectedIndexChanged += new System.EventHandler(this.cbbPhim_SelectedIndexChanged);

            this.lblNgayChieu.AutoSize = true;
            this.lblNgayChieu.Font = new Font("Segoe UI", 9.5F);
            this.lblNgayChieu.Location = new Point(14, 86);
            this.lblNgayChieu.Name = "lblNgayChieu";
            this.lblNgayChieu.Text = "Chọn ngày chiếu:";

            this.cbbNgayChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbNgayChieu.Font = new Font("Segoe UI", 9.5F);
            this.cbbNgayChieu.Location = new Point(16, 108);
            this.cbbNgayChieu.Name = "cbbNgayChieu";
            this.cbbNgayChieu.Size = new Size(284, 25);
            this.cbbNgayChieu.TabIndex = 1;
            this.cbbNgayChieu.SelectedIndexChanged += new System.EventHandler(this.cbbNgayChieu_SelectedIndexChanged);

            this.lblSuatChieu.AutoSize = true;
            this.lblSuatChieu.Font = new Font("Segoe UI", 9.5F);
            this.lblSuatChieu.Location = new Point(14, 144);
            this.lblSuatChieu.Name = "lblSuatChieu";
            this.lblSuatChieu.Text = "Chọn suất chiếu (Giờ - Phòng):";

            this.cbbSuatChieu.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbSuatChieu.Font = new Font("Segoe UI", 9.5F);
            this.cbbSuatChieu.Location = new Point(16, 166);
            this.cbbSuatChieu.Name = "cbbSuatChieu";
            this.cbbSuatChieu.Size = new Size(284, 25);
            this.cbbSuatChieu.TabIndex = 2;
            this.cbbSuatChieu.SelectedIndexChanged += new System.EventHandler(this.cbbSuatChieu_SelectedIndexChanged);

            this.lblThongTinPhong.BackColor = Color.FromArgb(241, 245, 249);
            this.lblThongTinPhong.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblThongTinPhong.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblThongTinPhong.Location = new Point(16, 200);
            this.lblThongTinPhong.Name = "lblThongTinPhong";
            this.lblThongTinPhong.Padding = new Padding(6, 4, 6, 4);
            this.lblThongTinPhong.Size = new Size(284, 42);
            this.lblThongTinPhong.Text = "Phòng: --\nTrạng thái ghế: --";

            this.grpChonSuat.Controls.AddRange(new Control[]
            {
                this.lblPhim, this.cbbPhim,
                this.lblNgayChieu, this.cbbNgayChieu,
                this.lblSuatChieu, this.cbbSuatChieu,
                this.lblThongTinPhong
            });

            // grpThongTinVe
            this.grpThongTinVe.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTinVe.Location = new Point(12, 270);
            this.grpThongTinVe.Name = "grpThongTinVe";
            this.grpThongTinVe.Size = new Size(316, 328);
            this.grpThongTinVe.TabIndex = 1;
            this.grpThongTinVe.TabStop = false;
            this.grpThongTinVe.Text = "2. Thông tin ghế chọn & Thanh toán";

            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblNhanVien.ForeColor = Color.FromArgb(71, 85, 105);
            this.lblNhanVien.Location = new Point(14, 28);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Text = "NV bán vé: --";

            this.lblGiaVeTitle.AutoSize = true;
            this.lblGiaVeTitle.Font = new Font("Segoe UI", 9.5F);
            this.lblGiaVeTitle.Location = new Point(14, 54);
            this.lblGiaVeTitle.Name = "lblGiaVeTitle";
            this.lblGiaVeTitle.Text = "Đơn giá vé:";

            this.lblGiaVe.AutoSize = true;
            this.lblGiaVe.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblGiaVe.ForeColor = Color.FromArgb(37, 99, 235);
            this.lblGiaVe.Location = new Point(110, 52);
            this.lblGiaVe.Name = "lblGiaVe";
            this.lblGiaVe.Text = "0đ";

            this.lblGheTitle.AutoSize = true;
            this.lblGheTitle.Font = new Font("Segoe UI", 9.5F);
            this.lblGheTitle.Location = new Point(14, 82);
            this.lblGheTitle.Name = "lblGheTitle";
            this.lblGheTitle.Text = "Danh sách ghế đang chọn:";

            this.txtGheDangChon.BackColor = Color.FromArgb(248, 250, 252);
            this.txtGheDangChon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtGheDangChon.ForeColor = Color.FromArgb(22, 163, 74);
            this.txtGheDangChon.Location = new Point(16, 104);
            this.txtGheDangChon.Multiline = true;
            this.txtGheDangChon.Name = "txtGheDangChon";
            this.txtGheDangChon.ReadOnly = true;
            this.txtGheDangChon.ScrollBars = ScrollBars.Vertical;
            this.txtGheDangChon.Size = new Size(284, 58);
            this.txtGheDangChon.TabIndex = 0;
            this.txtGheDangChon.TabStop = false;
            this.txtGheDangChon.Text = "Chưa chọn ghế nào";

            this.lblTongTienTitle.AutoSize = true;
            this.lblTongTienTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTongTienTitle.Location = new Point(14, 174);
            this.lblTongTienTitle.Name = "lblTongTienTitle";
            this.lblTongTienTitle.Text = "TỔNG TIỀN:";

            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTongTien.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblTongTien.Location = new Point(112, 169);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Text = "0đ";

            this.btnXacNhanBanVe.BackColor = Color.FromArgb(22, 163, 74);
            this.btnXacNhanBanVe.Enabled = false;
            this.btnXacNhanBanVe.FlatAppearance.BorderSize = 0;
            this.btnXacNhanBanVe.FlatStyle = FlatStyle.Flat;
            this.btnXacNhanBanVe.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.btnXacNhanBanVe.ForeColor = Color.White;
            this.btnXacNhanBanVe.Location = new Point(16, 212);
            this.btnXacNhanBanVe.Name = "btnXacNhanBanVe";
            this.btnXacNhanBanVe.Size = new Size(284, 44);
            this.btnXacNhanBanVe.TabIndex = 1;
            this.btnXacNhanBanVe.Text = "XÁC NHẬN BÁN VÉ";
            this.btnXacNhanBanVe.UseVisualStyleBackColor = false;
            this.btnXacNhanBanVe.Cursor = Cursors.Hand;
            this.btnXacNhanBanVe.Click += new System.EventHandler(this.btnXacNhanBanVe_Click);

            this.btnBoChon.BackColor = Color.FromArgb(71, 85, 105);
            this.btnBoChon.FlatAppearance.BorderSize = 0;
            this.btnBoChon.FlatStyle = FlatStyle.Flat;
            this.btnBoChon.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnBoChon.ForeColor = Color.White;
            this.btnBoChon.Location = new Point(16, 268);
            this.btnBoChon.Name = "btnBoChon";
            this.btnBoChon.Size = new Size(138, 36);
            this.btnBoChon.TabIndex = 2;
            this.btnBoChon.Text = "Bỏ chọn ghế";
            this.btnBoChon.UseVisualStyleBackColor = false;
            this.btnBoChon.Cursor = Cursors.Hand;
            this.btnBoChon.Click += new System.EventHandler(this.btnBoChon_Click);

            this.btnDong.BackColor = Color.FromArgb(100, 116, 139);
            this.btnDong.FlatAppearance.BorderSize = 0;
            this.btnDong.FlatStyle = FlatStyle.Flat;
            this.btnDong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.btnDong.ForeColor = Color.White;
            this.btnDong.Location = new Point(162, 268);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new Size(138, 36);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Cursor = Cursors.Hand;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.grpThongTinVe.Controls.AddRange(new Control[]
            {
                this.lblNhanVien,
                this.lblGiaVeTitle, this.lblGiaVe,
                this.lblGheTitle, this.txtGheDangChon,
                this.lblTongTienTitle, this.lblTongTien,
                this.btnXacNhanBanVe,
                this.btnBoChon, this.btnDong
            });

            // pnlPhai (Khu vực sơ đồ ghế)
            this.pnlPhai.Controls.Add(this.pnlVungChuaGhe);
            this.pnlPhai.Controls.Add(this.pnlManHinhVaChuThich);
            this.pnlPhai.Dock = DockStyle.Fill;
            this.pnlPhai.Location = new Point(340, 50);
            this.pnlPhai.Name = "pnlPhai";
            this.pnlPhai.Padding = new Padding(10, 10, 14, 14);
            this.pnlPhai.Size = new Size(740, 610);

            // pnlManHinhVaChuThich
            this.pnlManHinhVaChuThich.Controls.Add(this.pnlChuThich);
            this.pnlManHinhVaChuThich.Controls.Add(this.lblManHinh);
            this.pnlManHinhVaChuThich.Dock = DockStyle.Top;
            this.pnlManHinhVaChuThich.Location = new Point(10, 10);
            this.pnlManHinhVaChuThich.Name = "pnlManHinhVaChuThich";
            this.pnlManHinhVaChuThich.Size = new Size(716, 86);

            // lblManHinh
            this.lblManHinh.BackColor = Color.FromArgb(51, 65, 85);
            this.lblManHinh.Dock = DockStyle.Top;
            this.lblManHinh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblManHinh.ForeColor = Color.White;
            this.lblManHinh.Location = new Point(0, 0);
            this.lblManHinh.Name = "lblManHinh";
            this.lblManHinh.Size = new Size(716, 36);
            this.lblManHinh.Text = "MÀN HÌNH CHIẾU (SCREEN)";
            this.lblManHinh.TextAlign = ContentAlignment.MiddleCenter;

            // pnlChuThich
            this.pnlChuThich.BackColor = Color.White;
            this.pnlChuThich.BorderStyle = BorderStyle.FixedSingle;
            this.pnlChuThich.Dock = DockStyle.Bottom;
            this.pnlChuThich.Location = new Point(0, 44);
            this.pnlChuThich.Name = "pnlChuThich";
            this.pnlChuThich.Size = new Size(716, 38);

            // Chú thích 1: Ghế trống
            this.pnlMauTrong.BackColor = Color.FromArgb(226, 232, 240);
            this.pnlMauTrong.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMauTrong.Location = new Point(85, 8);
            this.pnlMauTrong.Size = new Size(26, 20);

            this.lblChuThichTrong.AutoSize = true;
            this.lblChuThichTrong.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblChuThichTrong.ForeColor = Color.FromArgb(51, 65, 85);
            this.lblChuThichTrong.Location = new Point(118, 9);
            this.lblChuThichTrong.Text = "Ghế trống";

            // Chú thích 2: Đang chọn
            this.pnlMauDangChon.BackColor = Color.FromArgb(22, 163, 74);
            this.pnlMauDangChon.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMauDangChon.Location = new Point(275, 8);
            this.pnlMauDangChon.Size = new Size(26, 20);

            this.lblChuThichDangChon.AutoSize = true;
            this.lblChuThichDangChon.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblChuThichDangChon.ForeColor = Color.FromArgb(22, 163, 74);
            this.lblChuThichDangChon.Location = new Point(308, 9);
            this.lblChuThichDangChon.Text = "Ghế đang chọn";

            // Chú thích 3: Đã bán
            this.pnlMauDaBan.BackColor = Color.FromArgb(220, 38, 38);
            this.pnlMauDaBan.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMauDaBan.Location = new Point(495, 8);
            this.pnlMauDaBan.Size = new Size(26, 20);

            this.lblChuThichDaBan.AutoSize = true;
            this.lblChuThichDaBan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblChuThichDaBan.ForeColor = Color.FromArgb(220, 38, 38);
            this.lblChuThichDaBan.Location = new Point(528, 9);
            this.lblChuThichDaBan.Text = "Ghế đã bán";

            this.pnlChuThich.Controls.AddRange(new Control[]
            {
                this.pnlMauTrong, this.lblChuThichTrong,
                this.pnlMauDangChon, this.lblChuThichDangChon,
                this.pnlMauDaBan, this.lblChuThichDaBan
            });

            // pnlVungChuaGhe
            this.pnlVungChuaGhe.AutoScroll = true;
            this.pnlVungChuaGhe.BackColor = Color.White;
            this.pnlVungChuaGhe.BorderStyle = BorderStyle.FixedSingle;
            this.pnlVungChuaGhe.Dock = DockStyle.Fill;
            this.pnlVungChuaGhe.Location = new Point(10, 96);
            this.pnlVungChuaGhe.Name = "pnlVungChuaGhe";
            this.pnlVungChuaGhe.Padding = new Padding(12);
            this.pnlVungChuaGhe.Size = new Size(716, 500);
            this.pnlVungChuaGhe.Resize += new System.EventHandler(this.pnlVungChuaGhe_Resize);

            // frmBanVe
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(1080, 660);
            this.Controls.Add(this.pnlPhai);
            this.Controls.Add(this.pnlTrai);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new Size(980, 640);
            this.Name = "frmBanVe";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Bán vé xem phim - Sơ đồ ghế động";
            this.Load += new System.EventHandler(this.frmBanVe_Load);

            this.pnlHeader.ResumeLayout(false);
            this.pnlTrai.ResumeLayout(false);
            this.grpChonSuat.ResumeLayout(false);
            this.grpChonSuat.PerformLayout();
            this.grpThongTinVe.ResumeLayout(false);
            this.grpThongTinVe.PerformLayout();
            this.pnlPhai.ResumeLayout(false);
            this.pnlManHinhVaChuThich.ResumeLayout(false);
            this.pnlChuThich.ResumeLayout(false);
            this.pnlChuThich.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

