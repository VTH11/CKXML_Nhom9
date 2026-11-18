using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmSuatChieu
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private GroupBox grpThongTin = null!;
        private Label lblMaSuat = null!;
        private TextBox txtMaSuat = null!;
        private Label lblPhim = null!;
        private ComboBox cbbPhim = null!;
        private Label lblPhong = null!;
        private ComboBox cbbPhong = null!;
        private Label lblNgayChieu = null!;
        private DateTimePicker dtpNgayChieu = null!;
        private Label lblGioBatDau = null!;
        private DateTimePicker dtpGioBatDau = null!;
        private Label lblGioKetThuc = null!;
        private TextBox txtGioKetThucDuKien = null!;
        private Label lblGiaVe = null!;
        private TextBox txtGiaVe = null!;

        private Panel pnlTacVu = null!;
        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnLamMoi = null!;
        private Button btnKiemThuThuatToan = null!;
        private Button btnDong = null!;

        private GroupBox grpLoc = null!;
        private Label lblLocPhim = null!;
        private ComboBox cbbLocPhim = null!;
        private CheckBox chkLocNgay = null!;
        private DateTimePicker dtpLocNgay = null!;
        private Button btnLoc = null!;
        private Button btnHienTatCa = null!;
        private Button btnXuatHtml = null!;

        private DataGridView dgvSuatChieu = null!;

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

            this.grpThongTin = new GroupBox();
            this.lblMaSuat = new Label();
            this.txtMaSuat = new TextBox();
            this.lblPhim = new Label();
            this.cbbPhim = new ComboBox();
            this.lblPhong = new Label();
            this.cbbPhong = new ComboBox();
            this.lblNgayChieu = new Label();
            this.dtpNgayChieu = new DateTimePicker();
            this.lblGioBatDau = new Label();
            this.dtpGioBatDau = new DateTimePicker();
            this.lblGioKetThuc = new Label();
            this.txtGioKetThucDuKien = new TextBox();
            this.lblGiaVe = new Label();
            this.txtGiaVe = new TextBox();

            this.pnlTacVu = new Panel();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.btnKiemThuThuatToan = new Button();
            this.btnDong = new Button();

            this.grpLoc = new GroupBox();
            this.lblLocPhim = new Label();
            this.cbbLocPhim = new ComboBox();
            this.chkLocNgay = new CheckBox();
            this.dtpLocNgay = new DateTimePicker();
            this.btnLoc = new Button();
            this.btnHienTatCa = new Button();
            this.btnXuatHtml = new Button();

            this.dgvSuatChieu = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlTacVu.SuspendLayout();
            this.grpLoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuatChieu)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(960, 50);

            // lblTieuDe
            this.lblTieuDe.Dock = DockStyle.Fill;
            this.lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new Size(960, 50);
            this.lblTieuDe.Text = "QUẢN LÝ SUẤT CHIẾU PHIM (KIỂM TRA XUNG ĐỘT LỊCH CHIẾU)";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // grpThongTin
            this.grpThongTin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTin.Location = new Point(16, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new Size(928, 130);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin suất chiếu";

            // Hàng 1: Mã suất, Phim chiếu, Phòng chiếu
            this.lblMaSuat.AutoSize = true;
            this.lblMaSuat.Font = new Font("Segoe UI", 9.5F);
            this.lblMaSuat.Location = new Point(18, 34);
            this.lblMaSuat.Name = "lblMaSuat";
            this.lblMaSuat.Text = "Mã suất:";

            this.txtMaSuat.BackColor = Color.FromArgb(241, 245, 249);
            this.txtMaSuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtMaSuat.Location = new Point(88, 30);
            this.txtMaSuat.Name = "txtMaSuat";
            this.txtMaSuat.ReadOnly = true;
            this.txtMaSuat.Size = new Size(100, 25);
            this.txtMaSuat.TabIndex = 0;
            this.txtMaSuat.TabStop = false;

            this.lblPhim.AutoSize = true;
            this.lblPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblPhim.Location = new Point(205, 34);
            this.lblPhim.Name = "lblPhim";
            this.lblPhim.Text = "Bộ phim:";

            this.cbbPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbPhim.Font = new Font("Segoe UI", 10F);
            this.cbbPhim.Location = new Point(275, 30);
            this.cbbPhim.Name = "cbbPhim";
            this.cbbPhim.Size = new Size(340, 25);
            this.cbbPhim.TabIndex = 1;
            this.cbbPhim.SelectedIndexChanged += new System.EventHandler(this.ThayDoiPhimHoacGio_Changed);

            this.lblPhong.AutoSize = true;
            this.lblPhong.Font = new Font("Segoe UI", 9.5F);
            this.lblPhong.Location = new Point(630, 34);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Text = "Phòng:";

            this.cbbPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbPhong.Font = new Font("Segoe UI", 10F);
            this.cbbPhong.Location = new Point(688, 30);
            this.cbbPhong.Name = "cbbPhong";
            this.cbbPhong.Size = new Size(222, 25);
            this.cbbPhong.TabIndex = 2;

            // Hàng 2: Ngày chiếu, Giờ bắt đầu, Giờ kết thúc dự kiến (+15p dọn rạp), Giá vé
            this.lblNgayChieu.AutoSize = true;
            this.lblNgayChieu.Font = new Font("Segoe UI", 9.5F);
            this.lblNgayChieu.Location = new Point(18, 82);
            this.lblNgayChieu.Name = "lblNgayChieu";
            this.lblNgayChieu.Text = "Ngày chiếu:";

            this.dtpNgayChieu.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayChieu.Font = new Font("Segoe UI", 10F);
            this.dtpNgayChieu.Format = DateTimePickerFormat.Custom;
            this.dtpNgayChieu.Location = new Point(98, 78);
            this.dtpNgayChieu.Name = "dtpNgayChieu";
            this.dtpNgayChieu.Size = new Size(115, 25);
            this.dtpNgayChieu.TabIndex = 3;

            this.lblGioBatDau.AutoSize = true;
            this.lblGioBatDau.Font = new Font("Segoe UI", 9.5F);
            this.lblGioBatDau.Location = new Point(225, 82);
            this.lblGioBatDau.Name = "lblGioBatDau";
            this.lblGioBatDau.Text = "Giờ BĐ:";

            this.dtpGioBatDau.CustomFormat = "HH:mm:ss";
            this.dtpGioBatDau.Font = new Font("Segoe UI", 10F);
            this.dtpGioBatDau.Format = DateTimePickerFormat.Custom;
            this.dtpGioBatDau.Location = new Point(282, 78);
            this.dtpGioBatDau.Name = "dtpGioBatDau";
            this.dtpGioBatDau.ShowUpDown = true;
            this.dtpGioBatDau.Size = new Size(95, 25);
            this.dtpGioBatDau.TabIndex = 4;
            this.dtpGioBatDau.ValueChanged += new System.EventHandler(this.ThayDoiPhimHoacGio_Changed);

            this.lblGioKetThuc.AutoSize = true;
            this.lblGioKetThuc.Font = new Font("Segoe UI", 9.5F);
            this.lblGioKetThuc.Location = new Point(388, 82);
            this.lblGioKetThuc.Name = "lblGioKetThuc";
            this.lblGioKetThuc.Text = "Giờ KT (+15p):";

            this.txtGioKetThucDuKien.BackColor = Color.FromArgb(241, 245, 249);
            this.txtGioKetThucDuKien.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.txtGioKetThucDuKien.ForeColor = Color.FromArgb(14, 116, 144);
            this.txtGioKetThucDuKien.Location = new Point(490, 78);
            this.txtGioKetThucDuKien.Name = "txtGioKetThucDuKien";
            this.txtGioKetThucDuKien.ReadOnly = true;
            this.txtGioKetThucDuKien.Size = new Size(195, 25);
            this.txtGioKetThucDuKien.TabIndex = 5;
            this.txtGioKetThucDuKien.TabStop = false;

            this.lblGiaVe.AutoSize = true;
            this.lblGiaVe.Font = new Font("Segoe UI", 9.5F);
            this.lblGiaVe.Location = new Point(698, 82);
            this.lblGiaVe.Name = "lblGiaVe";
            this.lblGiaVe.Text = "Giá vé (đ):";

            this.txtGiaVe.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtGiaVe.Location = new Point(772, 78);
            this.txtGiaVe.Name = "txtGiaVe";
            this.txtGiaVe.Size = new Size(138, 25);
            this.txtGiaVe.TabIndex = 6;

            this.grpThongTin.Controls.AddRange(new Control[]
            {
                this.lblMaSuat, this.txtMaSuat,
                this.lblPhim, this.cbbPhim,
                this.lblPhong, this.cbbPhong,
                this.lblNgayChieu, this.dtpNgayChieu,
                this.lblGioBatDau, this.dtpGioBatDau,
                this.lblGioKetThuc, this.txtGioKetThucDuKien,
                this.lblGiaVe, this.txtGiaVe
            });

            // pnlTacVu
            this.pnlTacVu.Location = new Point(16, 195);
            this.pnlTacVu.Name = "pnlTacVu";
            this.pnlTacVu.Size = new Size(928, 42);
            this.pnlTacVu.TabIndex = 1;

            CauHinhNut(this.btnThem, "Thêm suất chiếu", Color.FromArgb(22, 163, 74), new Point(0, 3), 0, this.btnThem_Click, 132, 35);
            CauHinhNut(this.btnSua, "Cập nhật (Sửa)", Color.FromArgb(37, 99, 235), new Point(140, 3), 1, this.btnSua_Click, 132, 35);
            CauHinhNut(this.btnXoa, "Xóa suất chiếu", Color.FromArgb(220, 38, 38), new Point(280, 3), 2, this.btnXoa_Click, 132, 35);
            CauHinhNut(this.btnLamMoi, "Làm mới", Color.FromArgb(71, 85, 105), new Point(420, 3), 3, this.btnLamMoi_Click, 105, 35);
            CauHinhNut(this.btnKiemThuThuatToan, "Chạy kiểm thử thuật toán (6 ca)", Color.FromArgb(217, 119, 6), new Point(535, 3), 4, this.btnKiemThuThuatToan_Click, 265, 35);
            CauHinhNut(this.btnDong, "Đóng", Color.FromArgb(100, 116, 139), new Point(810, 3), 5, this.btnDong_Click, 118, 35);

            this.pnlTacVu.Controls.AddRange(new Control[]
            {
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi, this.btnKiemThuThuatToan, this.btnDong
            });

            // grpLoc
            this.grpLoc.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpLoc.Location = new Point(16, 243);
            this.grpLoc.Name = "grpLoc";
            this.grpLoc.Size = new Size(928, 62);
            this.grpLoc.TabIndex = 2;
            this.grpLoc.TabStop = false;
            this.grpLoc.Text = "Lọc danh sách suất chiếu theo Phim và theo Ngày (XPath)";

            this.lblLocPhim.AutoSize = true;
            this.lblLocPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblLocPhim.Location = new Point(18, 28);
            this.lblLocPhim.Name = "lblLocPhim";
            this.lblLocPhim.Text = "Lọc theo phim:";

            this.cbbLocPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbLocPhim.Font = new Font("Segoe UI", 9.5F);
            this.cbbLocPhim.Location = new Point(115, 24);
            this.cbbLocPhim.Name = "cbbLocPhim";
            this.cbbLocPhim.Size = new Size(245, 25);
            this.cbbLocPhim.TabIndex = 0;

            this.chkLocNgay.AutoSize = true;
            this.chkLocNgay.Font = new Font("Segoe UI", 9.5F);
            this.chkLocNgay.Location = new Point(372, 27);
            this.chkLocNgay.Name = "chkLocNgay";
            this.chkLocNgay.TabIndex = 1;
            this.chkLocNgay.Text = "Lọc theo ngày:";
            this.chkLocNgay.UseVisualStyleBackColor = true;
            this.chkLocNgay.CheckedChanged += new System.EventHandler(this.chkLocNgay_CheckedChanged);

            this.dtpLocNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpLocNgay.Enabled = false;
            this.dtpLocNgay.Font = new Font("Segoe UI", 9.5F);
            this.dtpLocNgay.Format = DateTimePickerFormat.Custom;
            this.dtpLocNgay.Location = new Point(485, 24);
            this.dtpLocNgay.Name = "dtpLocNgay";
            this.dtpLocNgay.Size = new Size(105, 25);
            this.dtpLocNgay.TabIndex = 2;

            CauHinhNut(this.btnLoc, "Lọc dữ liệu", Color.FromArgb(14, 116, 144), new Point(600, 19), 3, this.btnLoc_Click, 98, 32);
            CauHinhNut(this.btnHienTatCa, "Hiện tất cả", Color.FromArgb(100, 116, 139), new Point(704, 19), 4, this.btnHienTatCa_Click, 98, 32);
            CauHinhNut(this.btnXuatHtml, "Xuất HTML", Color.FromArgb(22, 163, 74), new Point(808, 19), 5, this.btnXuatHtml_Click, 106, 32);

            this.grpLoc.Controls.AddRange(new Control[]
            {
                this.lblLocPhim, this.cbbLocPhim,
                this.chkLocNgay, this.dtpLocNgay,
                this.btnLoc, this.btnHienTatCa, this.btnXuatHtml
            });

            // dgvSuatChieu
            this.dgvSuatChieu.AllowUserToAddRows = false;
            this.dgvSuatChieu.AllowUserToDeleteRows = false;
            this.dgvSuatChieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuatChieu.BackgroundColor = Color.White;
            this.dgvSuatChieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuatChieu.Location = new Point(16, 315);
            this.dgvSuatChieu.MultiSelect = false;
            this.dgvSuatChieu.Name = "dgvSuatChieu";
            this.dgvSuatChieu.ReadOnly = true;
            this.dgvSuatChieu.RowHeadersVisible = false;
            this.dgvSuatChieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuatChieu.Size = new Size(928, 285);
            this.dgvSuatChieu.TabIndex = 3;
            this.dgvSuatChieu.CellClick += new DataGridViewCellEventHandler(this.dgvSuatChieu_CellClick);

            // frmSuatChieu
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(960, 615);
            this.Controls.Add(this.dgvSuatChieu);
            this.Controls.Add(this.grpLoc);
            this.Controls.Add(this.pnlTacVu);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSuatChieu";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý suất chiếu phim";
            this.Load += new System.EventHandler(this.frmSuatChieu_Load);

            this.pnlHeader.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlTacVu.ResumeLayout(false);
            this.grpLoc.ResumeLayout(false);
            this.grpLoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuatChieu)).EndInit();
            this.ResumeLayout(false);
        }

        private void CauHinhNut(Button btn, string text, Color backColor, Point location, int tabIndex, System.EventHandler onClick, int width = 130, int height = 35)
        {
            btn.BackColor = backColor;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Location = location;
            btn.Size = new Size(width, height);
            btn.TabIndex = tabIndex;
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
            btn.Cursor = Cursors.Hand;
            btn.Click += onClick;
        }
    }
}

