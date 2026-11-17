using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmTaiKhoan
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private GroupBox grpThongTin = null!;
        private Label lblTenDangNhap = null!;
        private TextBox txtTenDangNhap = null!;
        private Label lblNhanVien = null!;
        private ComboBox cbbNhanVien = null!;
        private Label lblQuyenHan = null!;
        private ComboBox cbbQuyenHan = null!;
        private Label lblMatKhau = null!;
        private TextBox txtMatKhau = null!;
        private CheckBox chkHienMatKhau = null!;

        private Panel pnlTacVu = null!;
        private Button btnThem = null!;
        private Button btnDoiQuyen = null!;
        private Button btnDatLaiMatKhau = null!;
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

        private DataGridView dgvTaiKhoan = null!;

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
            this.lblTenDangNhap = new Label();
            this.txtTenDangNhap = new TextBox();
            this.lblNhanVien = new Label();
            this.cbbNhanVien = new ComboBox();
            this.lblQuyenHan = new Label();
            this.cbbQuyenHan = new ComboBox();
            this.lblMatKhau = new Label();
            this.txtMatKhau = new TextBox();
            this.chkHienMatKhau = new CheckBox();

            this.pnlTacVu = new Panel();
            this.btnThem = new Button();
            this.btnDoiQuyen = new Button();
            this.btnDatLaiMatKhau = new Button();
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

            this.dgvTaiKhoan = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlTacVu.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
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
            this.lblTieuDe.Text = "QUẢN LÝ TÀI KHOẢN HỆ THỐNG";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // grpThongTin
            this.grpThongTin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTin.Location = new Point(16, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new Size(908, 125);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin tài khoản";

            // Hàng 1: Tên đăng nhập, Nhân viên sở hữu
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new Font("Segoe UI", 9.5F);
            this.lblTenDangNhap.Location = new Point(18, 34);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Text = "Tên đăng nhập:";

            this.txtTenDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtTenDangNhap.Location = new Point(125, 30);
            this.txtTenDangNhap.MaxLength = 20;
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new Size(235, 25);
            this.txtTenDangNhap.TabIndex = 0;

            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new Font("Segoe UI", 9.5F);
            this.lblNhanVien.Location = new Point(390, 34);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Text = "Nhân viên:";

            this.cbbNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbNhanVien.Font = new Font("Segoe UI", 10F);
            this.cbbNhanVien.Location = new Point(475, 30);
            this.cbbNhanVien.Name = "cbbNhanVien";
            this.cbbNhanVien.Size = new Size(410, 25);
            this.cbbNhanVien.TabIndex = 1;

            // Hàng 2: Quyền hạn, Mật khẩu mới / ban đầu
            this.lblQuyenHan.AutoSize = true;
            this.lblQuyenHan.Font = new Font("Segoe UI", 9.5F);
            this.lblQuyenHan.Location = new Point(18, 78);
            this.lblQuyenHan.Name = "lblQuyenHan";
            this.lblQuyenHan.Text = "Quyền hạn:";

            this.cbbQuyenHan.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbbQuyenHan.Font = new Font("Segoe UI", 10F);
            this.cbbQuyenHan.Items.AddRange(new object[]
            {
                "1 - Quản lý",
                "2 - Nhân viên"
            });
            this.cbbQuyenHan.Location = new Point(125, 74);
            this.cbbQuyenHan.Name = "cbbQuyenHan";
            this.cbbQuyenHan.Size = new Size(235, 25);
            this.cbbQuyenHan.TabIndex = 2;

            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new Font("Segoe UI", 9.5F);
            this.lblMatKhau.Location = new Point(390, 78);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Text = "Mật khẩu:";

            this.txtMatKhau.Font = new Font("Segoe UI", 10F);
            this.txtMatKhau.Location = new Point(475, 74);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new Size(275, 25);
            this.txtMatKhau.TabIndex = 3;
            this.txtMatKhau.UseSystemPasswordChar = true;

            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Checked = false;
            this.chkHienMatKhau.Font = new Font("Segoe UI", 9F);
            this.chkHienMatKhau.Location = new Point(762, 77);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.TabIndex = 4;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);

            this.grpThongTin.Controls.AddRange(new Control[]
            {
                this.lblTenDangNhap, this.txtTenDangNhap,
                this.lblNhanVien, this.cbbNhanVien,
                this.lblQuyenHan, this.cbbQuyenHan,
                this.lblMatKhau, this.txtMatKhau, this.chkHienMatKhau
            });

            // pnlTacVu
            this.pnlTacVu.Location = new Point(16, 192);
            this.pnlTacVu.Name = "pnlTacVu";
            this.pnlTacVu.Size = new Size(908, 42);
            this.pnlTacVu.TabIndex = 1;

            CauHinhNut(this.btnThem, "Thêm tài khoản", Color.FromArgb(22, 163, 74), new Point(0, 3), 0, this.btnThem_Click, 138, 35);
            CauHinhNut(this.btnDoiQuyen, "Đổi quyền / NV", Color.FromArgb(37, 99, 235), new Point(146, 3), 1, this.btnDoiQuyen_Click, 138, 35);
            CauHinhNut(this.btnDatLaiMatKhau, "Đặt lại mật khẩu", Color.FromArgb(217, 119, 6), new Point(292, 3), 2, this.btnDatLaiMatKhau_Click, 145, 35);
            CauHinhNut(this.btnXoa, "Xóa tài khoản", Color.FromArgb(220, 38, 38), new Point(445, 3), 3, this.btnXoa_Click, 130, 35);
            CauHinhNut(this.btnLamMoi, "Làm mới", Color.FromArgb(71, 85, 105), new Point(583, 3), 4, this.btnLamMoi_Click, 115, 35);
            CauHinhNut(this.btnDong, "Đóng", Color.FromArgb(100, 116, 139), new Point(788, 3), 5, this.btnDong_Click, 120, 35);

            this.pnlTacVu.Controls.AddRange(new Control[]
            {
                this.btnThem, this.btnDoiQuyen, this.btnDatLaiMatKhau, this.btnXoa, this.btnLamMoi, this.btnDong
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
                "Tất cả", "Tên đăng nhập", "Mã nhân viên", "Quyền hạn (1 hoặc 2)"
            });
            this.cbbTieuChiTim.Location = new Point(88, 24);
            this.cbbTieuChiTim.Name = "cbbTieuChiTim";
            this.cbbTieuChiTim.Size = new Size(165, 25);
            this.cbbTieuChiTim.TabIndex = 0;

            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Font = new Font("Segoe UI", 9.5F);
            this.lblTuKhoa.Location = new Point(270, 28);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Text = "Từ khóa:";

            this.txtTimKiem.Font = new Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new Point(335, 24);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new Size(305, 25);
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

            // dgvTaiKhoan
            this.dgvTaiKhoan.AllowUserToAddRows = false;
            this.dgvTaiKhoan.AllowUserToDeleteRows = false;
            this.dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiKhoan.BackgroundColor = Color.White;
            this.dgvTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaiKhoan.Location = new Point(16, 312);
            this.dgvTaiKhoan.MultiSelect = false;
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.ReadOnly = true;
            this.dgvTaiKhoan.RowHeadersVisible = false;
            this.dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaiKhoan.Size = new Size(908, 285);
            this.dgvTaiKhoan.TabIndex = 3;
            this.dgvTaiKhoan.CellClick += new DataGridViewCellEventHandler(this.dgvTaiKhoan_CellClick);

            // frmTaiKhoan
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(940, 615);
            this.Controls.Add(this.dgvTaiKhoan);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.pnlTacVu);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmTaiKhoan";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý tài khoản hệ thống";
            this.Load += new System.EventHandler(this.frmTaiKhoan_Load);

            this.pnlHeader.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlTacVu.ResumeLayout(false);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
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

