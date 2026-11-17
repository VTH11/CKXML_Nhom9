using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmPhim
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader = null!;
        private Label lblTieuDe = null!;

        private GroupBox grpThongTin = null!;
        private Label lblMaPhim = null!;
        private TextBox txtMaPhim = null!;
        private Label lblTenPhim = null!;
        private TextBox txtTenPhim = null!;
        private Label lblTheLoai = null!;
        private TextBox txtTheLoai = null!;
        private Label lblThoiLuong = null!;
        private TextBox txtThoiLuong = null!;
        private Label lblDaoDien = null!;
        private TextBox txtDaoDien = null!;
        private Label lblApPhich = null!;
        private TextBox txtApPhich = null!;
        private Button btnChonAnh = null!;
        private Panel pnlKhungAnh = null!;
        private PictureBox picApPhich = null!;
        private Label lblKhongCoAnh = null!;

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

        private DataGridView dgvPhim = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && picApPhich?.Image != null)
            {
                picApPhich.Image.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblTieuDe = new Label();

            this.grpThongTin = new GroupBox();
            this.lblMaPhim = new Label();
            this.txtMaPhim = new TextBox();
            this.lblTenPhim = new Label();
            this.txtTenPhim = new TextBox();
            this.lblTheLoai = new Label();
            this.txtTheLoai = new TextBox();
            this.lblThoiLuong = new Label();
            this.txtThoiLuong = new TextBox();
            this.lblDaoDien = new Label();
            this.txtDaoDien = new TextBox();
            this.lblApPhich = new Label();
            this.txtApPhich = new TextBox();
            this.btnChonAnh = new Button();
            this.pnlKhungAnh = new Panel();
            this.picApPhich = new PictureBox();
            this.lblKhongCoAnh = new Label();

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

            this.dgvPhim = new DataGridView();

            this.pnlHeader.SuspendLayout();
            this.grpThongTin.SuspendLayout();
            this.pnlKhungAnh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picApPhich)).BeginInit();
            this.pnlTacVu.SuspendLayout();
            this.grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).BeginInit();
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
            this.lblTieuDe.Text = "QUẢN LÝ DANH MỤC PHIM";
            this.lblTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // grpThongTin
            this.grpThongTin.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpThongTin.Location = new Point(16, 58);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new Size(908, 175);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin bộ phim";

            // Hàng 1: Mã phim, Tên phim
            this.lblMaPhim.AutoSize = true;
            this.lblMaPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblMaPhim.Location = new Point(18, 34);
            this.lblMaPhim.Name = "lblMaPhim";
            this.lblMaPhim.Text = "Mã phim:";

            this.txtMaPhim.BackColor = Color.FromArgb(241, 245, 249);
            this.txtMaPhim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.txtMaPhim.Location = new Point(95, 30);
            this.txtMaPhim.Name = "txtMaPhim";
            this.txtMaPhim.ReadOnly = true;
            this.txtMaPhim.Size = new Size(115, 25);
            this.txtMaPhim.TabIndex = 0;
            this.txtMaPhim.TabStop = false;

            this.lblTenPhim.AutoSize = true;
            this.lblTenPhim.Font = new Font("Segoe UI", 9.5F);
            this.lblTenPhim.Location = new Point(230, 34);
            this.lblTenPhim.Name = "lblTenPhim";
            this.lblTenPhim.Text = "Tên phim:";

            this.txtTenPhim.Font = new Font("Segoe UI", 10F);
            this.txtTenPhim.Location = new Point(305, 30);
            this.txtTenPhim.Name = "txtTenPhim";
            this.txtTenPhim.Size = new Size(440, 25);
            this.txtTenPhim.TabIndex = 1;

            // Hàng 2: Thể loại, Thời lượng
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Font = new Font("Segoe UI", 9.5F);
            this.lblTheLoai.Location = new Point(18, 78);
            this.lblTheLoai.Name = "lblTheLoai";
            this.lblTheLoai.Text = "Thể loại:";

            this.txtTheLoai.Font = new Font("Segoe UI", 10F);
            this.txtTheLoai.Location = new Point(95, 74);
            this.txtTheLoai.Name = "txtTheLoai";
            this.txtTheLoai.Size = new Size(375, 25);
            this.txtTheLoai.TabIndex = 2;

            this.lblThoiLuong.AutoSize = true;
            this.lblThoiLuong.Font = new Font("Segoe UI", 9.5F);
            this.lblThoiLuong.Location = new Point(490, 78);
            this.lblThoiLuong.Name = "lblThoiLuong";
            this.lblThoiLuong.Text = "Thời lượng (phút):";

            this.txtThoiLuong.Font = new Font("Segoe UI", 10F);
            this.txtThoiLuong.Location = new Point(615, 74);
            this.txtThoiLuong.Name = "txtThoiLuong";
            this.txtThoiLuong.Size = new Size(130, 25);
            this.txtThoiLuong.TabIndex = 3;

            // Hàng 3: Đạo diễn, Áp phích
            this.lblDaoDien.AutoSize = true;
            this.lblDaoDien.Font = new Font("Segoe UI", 9.5F);
            this.lblDaoDien.Location = new Point(18, 122);
            this.lblDaoDien.Name = "lblDaoDien";
            this.lblDaoDien.Text = "Đạo diễn:";

            this.txtDaoDien.Font = new Font("Segoe UI", 10F);
            this.txtDaoDien.Location = new Point(95, 118);
            this.txtDaoDien.Name = "txtDaoDien";
            this.txtDaoDien.Size = new Size(235, 25);
            this.txtDaoDien.TabIndex = 4;

            this.lblApPhich.AutoSize = true;
            this.lblApPhich.Font = new Font("Segoe UI", 9.5F);
            this.lblApPhich.Location = new Point(345, 122);
            this.lblApPhich.Name = "lblApPhich";
            this.lblApPhich.Text = "Áp phích:";

            this.txtApPhich.Font = new Font("Segoe UI", 10F);
            this.txtApPhich.Location = new Point(415, 118);
            this.txtApPhich.Name = "txtApPhich";
            this.txtApPhich.Size = new Size(232, 25);
            this.txtApPhich.TabIndex = 5;
            this.txtApPhich.Leave += new System.EventHandler(this.txtApPhich_Leave);

            this.btnChonAnh.BackColor = Color.FromArgb(226, 232, 240);
            this.btnChonAnh.FlatAppearance.BorderSize = 0;
            this.btnChonAnh.FlatStyle = FlatStyle.Flat;
            this.btnChonAnh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnChonAnh.ForeColor = Color.FromArgb(51, 65, 85);
            this.btnChonAnh.Location = new Point(653, 117);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new Size(92, 27);
            this.btnChonAnh.TabIndex = 6;
            this.btnChonAnh.Text = "Chọn ảnh...";
            this.btnChonAnh.UseVisualStyleBackColor = false;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);

            // Khung hiển thị Áp phích bên phải
            this.pnlKhungAnh.BackColor = Color.FromArgb(241, 245, 249);
            this.pnlKhungAnh.BorderStyle = BorderStyle.FixedSingle;
            this.pnlKhungAnh.Location = new Point(765, 22);
            this.pnlKhungAnh.Name = "pnlKhungAnh";
            this.pnlKhungAnh.Size = new Size(125, 140);

            this.picApPhich.Dock = DockStyle.Fill;
            this.picApPhich.Name = "picApPhich";
            this.picApPhich.SizeMode = PictureBoxSizeMode.Zoom;
            this.picApPhich.Visible = false;

            this.lblKhongCoAnh.Dock = DockStyle.Fill;
            this.lblKhongCoAnh.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            this.lblKhongCoAnh.ForeColor = Color.FromArgb(100, 116, 139);
            this.lblKhongCoAnh.Name = "lblKhongCoAnh";
            this.lblKhongCoAnh.Text = "Chưa có ảnh\náp phích";
            this.lblKhongCoAnh.TextAlign = ContentAlignment.MiddleCenter;

            this.pnlKhungAnh.Controls.Add(this.picApPhich);
            this.pnlKhungAnh.Controls.Add(this.lblKhongCoAnh);

            this.grpThongTin.Controls.AddRange(new Control[]
            {
                this.lblMaPhim, this.txtMaPhim,
                this.lblTenPhim, this.txtTenPhim,
                this.lblTheLoai, this.txtTheLoai,
                this.lblThoiLuong, this.txtThoiLuong,
                this.lblDaoDien, this.txtDaoDien,
                this.lblApPhich, this.txtApPhich, this.btnChonAnh,
                this.pnlKhungAnh
            });

            // pnlTacVu
            this.pnlTacVu.Location = new Point(16, 240);
            this.pnlTacVu.Name = "pnlTacVu";
            this.pnlTacVu.Size = new Size(908, 42);
            this.pnlTacVu.TabIndex = 1;

            CauHinhNut(this.btnThem, "Thêm mới", Color.FromArgb(22, 163, 74), new Point(0, 3), 0, this.btnThem_Click);
            CauHinhNut(this.btnSua, "Cập nhật (Sửa)", Color.FromArgb(37, 99, 235), new Point(145, 3), 1, this.btnSua_Click);
            CauHinhNut(this.btnXoa, "Xóa phim", Color.FromArgb(220, 38, 38), new Point(290, 3), 2, this.btnXoa_Click);
            CauHinhNut(this.btnLamMoi, "Làm mới", Color.FromArgb(71, 85, 105), new Point(435, 3), 3, this.btnLamMoi_Click);
            CauHinhNut(this.btnDong, "Đóng", Color.FromArgb(100, 116, 139), new Point(778, 3), 4, this.btnDong_Click);

            this.pnlTacVu.Controls.AddRange(new Control[]
            {
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi, this.btnDong
            });

            // grpTimKiem
            this.grpTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.grpTimKiem.Location = new Point(16, 288);
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
                "Tất cả", "Mã phim", "Tên phim", "Thể loại", "Đạo diễn"
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

            // dgvPhim
            this.dgvPhim.AllowUserToAddRows = false;
            this.dgvPhim.AllowUserToDeleteRows = false;
            this.dgvPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhim.BackgroundColor = Color.White;
            this.dgvPhim.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhim.Location = new Point(16, 360);
            this.dgvPhim.MultiSelect = false;
            this.dgvPhim.Name = "dgvPhim";
            this.dgvPhim.ReadOnly = true;
            this.dgvPhim.RowHeadersVisible = false;
            this.dgvPhim.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhim.Size = new Size(908, 245);
            this.dgvPhim.TabIndex = 3;
            this.dgvPhim.CellClick += new DataGridViewCellEventHandler(this.dgvPhim_CellClick);

            // frmPhim
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(940, 620);
            this.Controls.Add(this.dgvPhim);
            this.Controls.Add(this.grpTimKiem);
            this.Controls.Add(this.pnlTacVu);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmPhim";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Quản lý danh mục phim";
            this.Load += new System.EventHandler(this.frmPhim_Load);

            this.pnlHeader.ResumeLayout(false);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.pnlKhungAnh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picApPhich)).EndInit();
            this.pnlTacVu.ResumeLayout(false);
            this.grpTimKiem.ResumeLayout(false);
            this.grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhim)).EndInit();
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
