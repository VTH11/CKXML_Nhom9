using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmPhongChieu
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private GroupBox grpThongTin = null!;
        private Label lblMaPhong = null!;
        private TextBox txtMaPhong = null!;
        private Label lblTenPhong = null!;
        private TextBox txtTenPhong = null!;
        private Label lblSoHang = null!;
        private TextBox txtSoHang = null!;
        private Label lblSoCot = null!;
        private TextBox txtSoCot = null!;
        private Label lblTongSoGhe = null!;
        private TextBox txtTongSoGhe = null!;
        private Label lblGhiChu = null!;

        private Panel pnlTacVu = null!;
        private Button btnThem = null!;
        private Button btnSua = null!;
        private Button btnXoa = null!;
        private Button btnLamMoi = null!;
        private Button btnDong = null!;

        private GroupBox grpTimKiem = null!;
        private Label lblTimTheo = null!;
        private ComboBox cbbTieuChiTim = null!;
        private Label lblTuKhoa = null!;
        private TextBox txtTimKiem = null!;
        private Button btnTimKiem = null!;
        private Button btnHienTatCa = null!;

        private DataGridView dgvPhongChieu = null!;

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
            this.lblMaPhong = new Label();
            this.txtMaPhong = new TextBox();
            this.lblTenPhong = new Label();
            this.txtTenPhong = new TextBox();
            this.lblSoHang = new Label();
            this.txtSoHang = new TextBox();
            this.lblSoCot = new Label();
            this.txtSoCot = new TextBox();
            this.lblTongSoGhe = new Label();
            this.txtTongSoGhe = new TextBox();
            this.lblGhiChu = new Label();

            this.pnlTacVu = new Panel();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.btnDong = new Button();

            this.grpTimKiem = new GroupBox();
            this.lblTimTheo = new Label();
            this.cbbTieuChiTim = new ComboBox();
            this.lblTuKhoa = new Label();
            this.txtTimKiem = new TextBox();
            this.btnTimKiem = new Button();
            this.btnHienTatCa = new Button();

            this.dgvPhongChieu = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlTacVu.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongChieu)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlHeader.Controls.Add(this.lblTieuDe);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(940, 50);

            // lblTieuDe
            this.lblTieuDe.Dock = DockStyle.Fill;
            this.lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new Size(940, 50);
            this.lblTieuDe.Text = "QUẢN LÝ DANH SÁCH PHÒNG CHIẾU";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // grpThongTin
            this.grpThongTin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTin.Location = new Point(16, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new Size(908, 125);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin phòng chiếu";

            // Hàng 1: Mã phòng, Tên phòng
            this.lblMaPhong.AutoSize = true;
            this.lblMaPhong.Font = new Font("Segoe UI", 9.5F);
            this.lblMaPhong.Location = new Point(18, 34);
            this.lblMaPhong.Name = "lblMaPhong";
            this.lblMaPhong.Text = "Mã phòng:";

            this.txtMaPhong.BackColor = Color.FromArgb(241, 245, 249);
            this.txtMaPhong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtMaPhong.Location = new Point(105, 30);
            this.txtMaPhong.Name = "txtMaPhong";
            this.txtMaPhong.ReadOnly = true;
            this.txtMaPhong.Size = new Size(125, 25);
            this.txtMaPhong.TabIndex = 0;
            this.txtMaPhong.TabStop = false;

            this.lblTenPhong.AutoSize = true;
            this.lblTenPhong.Font = new Font("Segoe UI", 9.5F);
            this.lblTenPhong.Location = new Point(265, 34);
            this.lblTenPhong.Name = "lblTenPhong";
            this.lblTenPhong.Text = "Tên phòng chiếu:";

            this.txtTenPhong.Font = new Font("Segoe UI", 10F);
            this.txtTenPhong.Location = new Point(385, 30);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new Size(500, 25);
            this.txtTenPhong.TabIndex = 1;

            // Hàng 2: Số hàng (1-26), Số cột (1-30), Tổng số ghế (tự tính)
            this.lblSoHang.AutoSize = true;
            this.lblSoHang.Font = new Font("Segoe UI", 9.5F);
            this.lblSoHang.Location = new Point(18, 78);
            this.lblSoHang.Name = "lblSoHang";
            this.lblSoHang.Text = "Số hàng (1-26):";

            this.txtSoHang.Font = new Font("Segoe UI", 10F);
            this.txtSoHang.Location = new Point(125, 74);
            this.txtSoHang.Name = "txtSoHang";
            this.txtSoHang.Size = new Size(105, 25);
            this.txtSoHang.TabIndex = 2;
            this.txtSoHang.TextChanged += new System.EventHandler(this.txtSoHangSoCot_TextChanged);

            this.lblSoCot.AutoSize = true;
            this.lblSoCot.Font = new Font("Segoe UI", 9.5F);
            this.lblSoCot.Location = new Point(265, 78);
            this.lblSoCot.Name = "lblSoCot";
            this.lblSoCot.Text = "Số cột (1-30):";

            this.txtSoCot.Font = new Font("Segoe UI", 10F);
            this.txtSoCot.Location = new Point(365, 74);
            this.txtSoCot.Name = "txtSoCot";
            this.txtSoCot.Size = new Size(105, 25);
            this.txtSoCot.TabIndex = 3;
            this.txtSoCot.TextChanged += new System.EventHandler(this.txtSoHangSoCot_TextChanged);

            this.lblTongSoGhe.AutoSize = true;
            this.lblTongSoGhe.Font = new Font("Segoe UI", 9.5F);
            this.lblTongSoGhe.Location = new Point(500, 78);
            this.lblTongSoGhe.Name = "lblTongSoGhe";
            this.lblTongSoGhe.Text = "Tổng số ghế (tự tính):";

            this.txtTongSoGhe.BackColor = Color.FromArgb(241, 245, 249);
            this.txtTongSoGhe.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtTongSoGhe.ForeColor = Color.FromArgb(22, 163, 74);
            this.txtTongSoGhe.Location = new Point(648, 74);
            this.txtTongSoGhe.Name = "txtTongSoGhe";
            this.txtTongSoGhe.ReadOnly = true;
            this.txtTongSoGhe.Size = new Size(105, 25);
            this.txtTongSoGhe.TabIndex = 4;
            this.txtTongSoGhe.TabStop = false;

            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            this.lblGhiChu.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblGhiChu.Location = new Point(762, 78);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Text = "(= Hàng x Cột)";

            this.grpThongTin.Controls.AddRange(new Control[]
            {
                this.lblMaPhong, this.txtMaPhong,
                this.lblTenPhong, this.txtTenPhong,
                this.lblSoHang, this.txtSoHang,
                this.lblSoCot, this.txtSoCot,
                this.lblTongSoGhe, this.txtTongSoGhe,
                this.lblGhiChu
            });

            // pnlTacVu
            this.pnlTacVu.Location = new Point(16, 192);
            this.pnlTacVu.Name = "pnlTacVu";
            this.pnlTacVu.Size = new Size(908, 42);
            this.pnlTacVu.TabIndex = 1;

            CauHinhNut(this.btnThem, "Thêm mới", Color.FromArgb(22, 163, 74), new Point(0, 3), 0, this.btnThem_Click);
            CauHinhNut(this.btnSua, "Cập nhật (Sửa)", Color.FromArgb(37, 99, 235), new Point(145, 3), 1, this.btnSua_Click);
            CauHinhNut(this.btnXoa, "Xóa phòng", Color.FromArgb(220, 38, 38), new Point(290, 3), 2, this.btnXoa_Click);
            CauHinhNut(this.btnLamMoi, "Làm mới", Color.FromArgb(71, 85, 105), new Point(435, 3), 3, this.btnLamMoi_Click);
            CauHinhNut(this.btnDong, "Đóng", Color.FromArgb(100, 116, 139), new Point(778, 3), 4, this.btnDong_Click);

            this.pnlTacVu.Controls.AddRange(new Control[]
            {
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi, this.btnDong
            });

            // grpTimKiem
            this.grpTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpTimKiem.Location = new Point(16, 240);
            this.grpTimKiem.Name = "grpTimKiem";
            this.grpTimKiem.Size = new Size(908, 62);
            this.grpTimKiem.TabIndex = 2;
            this.grpTimKiem.TabStop = false;
            this.grpTimKiem.Text = "Tìm kiếm bằng XPath";

            this.lblTimTheo.AutoSize = true;
            this.lblTimTheo.Font = new Font("Segoe UI", 9.5F);
            this.lblTimTheo.Location = new Point(18, 28);
            this.lblTimTheo.Name = "lblTimTheo";
            this.lblTimTheo.Text = "Tìm theo:";

            this.cbbTieuChiTim.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbTieuChiTim.Font = new Font("Segoe UI", 9.5F);
            this.cbbTieuChiTim.Items.AddRange(new object[]
            {
                "Tất cả", "Mã phòng", "Tên phòng chiếu"
            });
            this.cbbTieuChiTim.Location = new Point(88, 24);
            this.cbbTieuChiTim.Name = "cbbTieuChiTim";
            this.cbbTieuChiTim.Size = new Size(145, 25);
            this.cbbTieuChiTim.TabIndex = 0;

            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new Font("Segoe UI", 9.5F);
            this.lblTuKhoa.Location = new Point(250, 28);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Text = "Từ khóa:";

            this.txtTimKiem.Font = new Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new Point(315, 24);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new Size(325, 25);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.KeyDown += new KeyEventHandler(this.txtTimKiem_KeyDown);

            CauHinhNut(this.btnTimKiem, "Tìm kiếm", Color.FromArgb(14, 116, 144), new Point(655, 19), 2, this.btnTimKiem_Click, 115, 32);
            CauHinhNut(this.btnHienTatCa, "Hiện tất cả", Color.FromArgb(100, 116, 139), new Point(778, 19), 3, this.btnHienTatCa_Click, 115, 32);

            this.grpTimKiem.Controls.AddRange(new Control[]
            {
                this.lblTimTheo, this.cbbTieuChiTim,
                this.lblTuKhoa, this.txtTimKiem,
                this.btnTimKiem, this.btnHienTatCa
            });

            // dgvPhongChieu
            this.dgvPhongChieu.AllowUserToAddRows = false;
            this.dgvPhongChieu.AllowUserToDeleteRows = false;
            this.dgvPhongChieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhongChieu.BackgroundColor = Color.White;
            this.dgvPhongChieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhongChieu.Location = new Point(16, 312);
            this.dgvPhongChieu.MultiSelect = false;
            this.dgvPhongChieu.Name = "dgvPhongChieu";
            this.dgvPhongChieu.ReadOnly = true;
            this.dgvPhongChieu.RowHeadersVisible = false;
            this.dgvPhongChieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongChieu.Size = new Size(908, 285);
            this.dgvPhongChieu.TabIndex = 3;
            this.dgvPhongChieu.CellClick += new DataGridViewCellEventHandler(this.dgvPhongChieu_CellClick);

            // frmPhongChieu
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(940, 615);
            this.Controls.Add(this.dgvPhongChieu);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.pnlTacVu);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPhongChieu";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý danh sách phòng chiếu";
            this.Load += new System.EventHandler(this.frmPhongChieu_Load);

            this.pnlHeader.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlTacVu.ResumeLayout(false);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongChieu)).EndInit();
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

