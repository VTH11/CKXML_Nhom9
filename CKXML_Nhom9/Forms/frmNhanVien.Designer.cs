using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private GroupBox grpThongTin = null!;
        private Label lblMaNV = null!;
        private TextBox txtMaNV = null!;
        private Label lblHoTen = null!;
        private TextBox txtHoTen = null!;
        private Label lblGioiTinh = null!;
        private RadioButton radNam = null!;
        private RadioButton radNu = null!;
        private Label lblNgaySinh = null!;
        private DateTimePicker dtpNgaySinh = null!;
        private Label lblSoDienThoai = null!;
        private TextBox txtSoDienThoai = null!;
        private Label lblChucVu = null!;
        private ComboBox cbbChucVu = null!;
        private Label lblDiaChi = null!;
        private TextBox txtDiaChi = null!;

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

        private DataGridView dgvNhanVien = null!;

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
            this.lblMaNV = new Label();
            this.txtMaNV = new TextBox();
            this.lblHoTen = new Label();
            this.txtHoTen = new TextBox();
            this.lblGioiTinh = new Label();
            this.radNam = new RadioButton();
            this.radNu = new RadioButton();
            this.lblNgaySinh = new Label();
            this.dtpNgaySinh = new DateTimePicker();
            this.lblSoDienThoai = new Label();
            this.txtSoDienThoai = new TextBox();
            this.lblChucVu = new Label();
            this.cbbChucVu = new ComboBox();
            this.lblDiaChi = new Label();
            this.txtDiaChi = new TextBox();

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

            this.dgvNhanVien = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlTacVu.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
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
            this.lblTieuDe.Text = "QUẢN LÝ DANH SÁCH NHÂN VIÊN";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // grpThongTin
            this.grpThongTin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTin.Location = new Point(16, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new Size(908, 155);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin nhân viên";

            // Hàng 1: Mã NV, Họ tên, Giới tính
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new Font("Segoe UI", 9.5F);
            this.lblMaNV.Location = new Point(18, 34);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Text = "Mã NV:";

            this.txtMaNV.BackColor = Color.FromArgb(241, 245, 249);
            this.txtMaNV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtMaNV.Location = new Point(95, 30);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.ReadOnly = true;
            this.txtMaNV.Size = new Size(115, 25);
            this.txtMaNV.TabIndex = 0;
            this.txtMaNV.TabStop = false;

            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new Font("Segoe UI", 9.5F);
            this.lblHoTen.Location = new Point(235, 34);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Text = "Họ và tên:";

            this.txtHoTen.Font = new Font("Segoe UI", 10F);
            this.txtHoTen.Location = new Point(315, 30);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new Size(340, 25);
            this.txtHoTen.TabIndex = 1;

            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new Font("Segoe UI", 9.5F);
            this.lblGioiTinh.Location = new Point(680, 34);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Text = "Giới tính:";

            this.radNam.AutoSize = true;
            this.radNam.Checked = true;
            this.radNam.Font = new Font("Segoe UI", 9.5F);
            this.radNam.Location = new Point(755, 32);
            this.radNam.Name = "radNam";
            this.radNam.TabIndex = 2;
            this.radNam.TabStop = true;
            this.radNam.Text = "Nam";
            this.radNam.UseVisualStyleBackColor = true;

            this.radNu.AutoSize = true;
            this.radNu.Font = new Font("Segoe UI", 9.5F);
            this.radNu.Location = new Point(825, 32);
            this.radNu.Name = "radNu";
            this.radNu.TabIndex = 3;
            this.radNu.Text = "Nữ";
            this.radNu.UseVisualStyleBackColor = true;

            // Hàng 2: Ngày sinh, Số điện thoại, Chức vụ
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new Font("Segoe UI", 9.5F);
            this.lblNgaySinh.Location = new Point(18, 76);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Text = "Ngày sinh:";

            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Font = new Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new Point(95, 72);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new Size(135, 25);
            this.dtpNgaySinh.TabIndex = 4;

            this.lblSoDienThoai.AutoSize = true;
            this.lblSoDienThoai.Font = new Font("Segoe UI", 9.5F);
            this.lblSoDienThoai.Location = new Point(255, 76);
            this.lblSoDienThoai.Name = "lblSoDienThoai";
            this.lblSoDienThoai.Text = "Số ĐT (10 số):";

            this.txtSoDienThoai.Font = new Font("Segoe UI", 10F);
            this.txtSoDienThoai.Location = new Point(355, 72);
            this.txtSoDienThoai.MaxLength = 10;
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.Size = new Size(175, 25);
            this.txtSoDienThoai.TabIndex = 5;

            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Font = new Font("Segoe UI", 9.5F);
            this.lblChucVu.Location = new Point(555, 76);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Text = "Chức vụ:";

            this.cbbChucVu.Font = new Font("Segoe UI", 10F);
            this.cbbChucVu.Items.AddRange(new object[]
            {
                "Nhân viên bán vé",
                "Quản lý rạp",
                "Nhân viên soát vé",
                "Nhân viên kỹ thuật"
            });
            this.cbbChucVu.Location = new Point(625, 72);
            this.cbbChucVu.Name = "cbbChucVu";
            this.cbbChucVu.Size = new Size(265, 25);
            this.cbbChucVu.TabIndex = 6;

            // Hàng 3: Địa chỉ
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new Font("Segoe UI", 9.5F);
            this.lblDiaChi.Location = new Point(18, 118);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Text = "Địa chỉ:";

            this.txtDiaChi.Font = new Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new Point(95, 114);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new Size(795, 25);
            this.txtDiaChi.TabIndex = 7;

            this.grpThongTin.Controls.AddRange(new Control[]
            {
                this.lblMaNV, this.txtMaNV,
                this.lblHoTen, this.txtHoTen,
                this.lblGioiTinh, this.radNam, this.radNu,
                this.lblNgaySinh, this.dtpNgaySinh,
                this.lblSoDienThoai, this.txtSoDienThoai,
                this.lblChucVu, this.cbbChucVu,
                this.lblDiaChi, this.txtDiaChi
            });

            // pnlTacVu
            this.pnlTacVu.Location = new Point(16, 220);
            this.pnlTacVu.Name = "pnlTacVu";
            this.pnlTacVu.Size = new Size(908, 42);
            this.pnlTacVu.TabIndex = 1;

            CauHinhNut(this.btnThem, "Thêm mới", Color.FromArgb(22, 163, 74), new Point(0, 3), 0, this.btnThem_Click);
            CauHinhNut(this.btnSua, "Cập nhật (Sửa)", Color.FromArgb(37, 99, 235), new Point(145, 3), 1, this.btnSua_Click);
            CauHinhNut(this.btnXoa, "Xóa nhân viên", Color.FromArgb(220, 38, 38), new Point(290, 3), 2, this.btnXoa_Click);
            CauHinhNut(this.btnLamMoi, "Làm mới", Color.FromArgb(71, 85, 105), new Point(435, 3), 3, this.btnLamMoi_Click);
            CauHinhNut(this.btnDong, "Đóng", Color.FromArgb(100, 116, 139), new Point(778, 3), 4, this.btnDong_Click);

            this.pnlTacVu.Controls.AddRange(new Control[]
            {
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi, this.btnDong
            });

            // grpTimKiem
            this.grpTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpTimKiem.Location = new Point(16, 268);
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
                "Tất cả", "Mã nhân viên", "Họ tên", "Số điện thoại", "Chức vụ"
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

            // dgvNhanVien
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNhanVien.BackgroundColor = Color.White;
            this.dgvNhanVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNhanVien.Location = new Point(16, 340);
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.RowHeadersVisible = false;
            this.dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.Size = new Size(908, 260);
            this.dgvNhanVien.TabIndex = 3;
            this.dgvNhanVien.CellClick += new DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);

            // frmNhanVien
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(940, 615);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.pnlTacVu);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmNhanVien";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý danh sách nhân viên";
            this.Load += new System.EventHandler(this.frmNhanVien_Load);

            this.pnlHeader.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlTacVu.ResumeLayout(false);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
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

