using System.Drawing;
using System.Windows.Forms;

namespace CKXML_Nhom9.Forms
{
    partial class frmTrangChu
    {
        private System.ComponentModel.IContainer components = null;

        private MenuStrip mnuChinh = null!;
        private ToolStripMenuItem mnuHeThong = null!;
        private ToolStripMenuItem mnuDoiMatKhau = null!;
        private ToolStripMenuItem mnuDangXuat = null!;
        private ToolStripSeparator mnuSep1 = null!;
        private ToolStripMenuItem mnuThoat = null!;

        private ToolStripMenuItem mnuQuanLy = null!;
        private ToolStripMenuItem mnuQuanLyPhim = null!;
        private ToolStripMenuItem mnuQuanLyPhongChieu = null!;
        private ToolStripMenuItem mnuQuanLySuatChieu = null!;
        private ToolStripMenuItem mnuQuanLyNhanVien = null!;
        private ToolStripMenuItem mnuQuanLyTaiKhoan = null!;

        private ToolStripMenuItem mnuNghiepVu = null!;
        private ToolStripMenuItem mnuBanVe = null!;
        private ToolStripMenuItem mnuXemLichChieu = null!;

        private ToolStripMenuItem mnuBaoCao = null!;
        private ToolStripMenuItem mnuBaoCaoDoanhThu = null!;

        private Panel pnlBanner = null!;
        private Label lblTieuDe = null!;
        private Label lblThongTinNguoiDung = null!;

        private FlowLayoutPanel flpChucNang = null!;
        private Button btnBanVe = null!;
        private Button btnXemLichChieu = null!;
        private Button btnQuanLyPhim = null!;
        private Button btnQuanLyPhongChieu = null!;
        private Button btnQuanLySuatChieu = null!;
        private Button btnQuanLyNhanVien = null!;
        private Button btnQuanLyTaiKhoan = null!;
        private Button btnBaoCaoDoanhThu = null!;
        private Button btnDoiMatKhau = null!;
        private Button btnDangXuat = null!;

        private StatusStrip stsTrangThai = null!;
        private ToolStripStatusLabel lblTrangThaiPhien = null!;

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
            this.mnuChinh = new MenuStrip();
            this.mnuHeThong = new ToolStripMenuItem();
            this.mnuDoiMatKhau = new ToolStripMenuItem();
            this.mnuDangXuat = new ToolStripMenuItem();
            this.mnuSep1 = new ToolStripSeparator();
            this.mnuThoat = new ToolStripMenuItem();

            this.mnuNghiepVu = new ToolStripMenuItem();
            this.mnuBanVe = new ToolStripMenuItem();
            this.mnuXemLichChieu = new ToolStripMenuItem();

            this.mnuQuanLy = new ToolStripMenuItem();
            this.mnuQuanLyPhim = new ToolStripMenuItem();
            this.mnuQuanLyPhongChieu = new ToolStripMenuItem();
            this.mnuQuanLySuatChieu = new ToolStripMenuItem();
            this.mnuQuanLyNhanVien = new ToolStripMenuItem();
            this.mnuQuanLyTaiKhoan = new ToolStripMenuItem();

            this.mnuBaoCao = new ToolStripMenuItem();
            this.mnuBaoCaoDoanhThu = new ToolStripMenuItem();

            this.pnlBanner = new Panel();
            this.lblTieuDe = new Label();
            this.lblThongTinNguoiDung = new Label();

            this.flpChucNang = new FlowLayoutPanel();
            this.btnBanVe = new Button();
            this.btnXemLichChieu = new Button();
            this.btnQuanLyPhim = new Button();
            this.btnQuanLyPhongChieu = new Button();
            this.btnQuanLySuatChieu = new Button();
            this.btnQuanLyNhanVien = new Button();
            this.btnQuanLyTaiKhoan = new Button();
            this.btnBaoCaoDoanhThu = new Button();
            this.btnDoiMatKhau = new Button();
            this.btnDangXuat = new Button();

            this.stsTrangThai = new StatusStrip();
            this.lblTrangThaiPhien = new ToolStripStatusLabel();

            this.mnuChinh.SuspendLayout();
            this.pnlBanner.SuspendLayout();
            this.flpChucNang.SuspendLayout();
            this.stsTrangThai.SuspendLayout();
            this.SuspendLayout();

            // mnuChinh
            this.mnuChinh.Font = new Font("Segoe UI", 10F);
            this.mnuChinh.Items.AddRange(new ToolStripItem[]
            {
                this.mnuHeThong,
                this.mnuNghiepVu,
                this.mnuQuanLy,
                this.mnuBaoCao
            });
            this.mnuChinh.Location = new Point(0, 0);
            this.mnuChinh.Name = "mnuChinh";
            this.mnuChinh.Size = new Size(960, 28);
            this.mnuChinh.TabIndex = 0;

            // mnuHeThong
            this.mnuHeThong.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.mnuDoiMatKhau,
                this.mnuDangXuat,
                this.mnuSep1,
                this.mnuThoat
            });
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Text = "Hệ thống";

            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            this.mnuDoiMatKhau.Click += new System.EventHandler(this.mnuDoiMatKhau_Click);

            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);

            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);

            // mnuNghiepVu
            this.mnuNghiepVu.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.mnuBanVe,
                this.mnuXemLichChieu
            });
            this.mnuNghiepVu.Name = "mnuNghiepVu";
            this.mnuNghiepVu.Text = "Nghiệp vụ";

            this.mnuBanVe.Name = "mnuBanVe";
            this.mnuBanVe.Text = "Bán vé xem phim";
            this.mnuBanVe.Click += new System.EventHandler(this.mnuBanVe_Click);

            this.mnuXemLichChieu.Name = "mnuXemLichChieu";
            this.mnuXemLichChieu.Text = "Xem lịch chiếu";
            this.mnuXemLichChieu.Click += new System.EventHandler(this.mnuXemLichChieu_Click);

            // mnuQuanLy
            this.mnuQuanLy.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.mnuQuanLyPhim,
                this.mnuQuanLyPhongChieu,
                this.mnuQuanLySuatChieu,
                this.mnuQuanLyNhanVien,
                this.mnuQuanLyTaiKhoan
            });
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Text = "Quản lý danh mục";

            this.mnuQuanLyPhim.Name = "mnuQuanLyPhim";
            this.mnuQuanLyPhim.Text = "Quản lý phim";
            this.mnuQuanLyPhim.Click += new System.EventHandler(this.mnuQuanLyPhim_Click);

            this.mnuQuanLyPhongChieu.Name = "mnuQuanLyPhongChieu";
            this.mnuQuanLyPhongChieu.Text = "Quản lý phòng chiếu";
            this.mnuQuanLyPhongChieu.Click += new System.EventHandler(this.mnuQuanLyPhongChieu_Click);

            this.mnuQuanLySuatChieu.Name = "mnuQuanLySuatChieu";
            this.mnuQuanLySuatChieu.Text = "Quản lý suất chiếu";
            this.mnuQuanLySuatChieu.Click += new System.EventHandler(this.mnuQuanLySuatChieu_Click);

            this.mnuQuanLyNhanVien.Name = "mnuQuanLyNhanVien";
            this.mnuQuanLyNhanVien.Text = "Quản lý nhân viên";
            this.mnuQuanLyNhanVien.Click += new System.EventHandler(this.mnuQuanLyNhanVien_Click);

            this.mnuQuanLyTaiKhoan.Name = "mnuQuanLyTaiKhoan";
            this.mnuQuanLyTaiKhoan.Text = "Quản lý tài khoản";
            this.mnuQuanLyTaiKhoan.Click += new System.EventHandler(this.mnuQuanLyTaiKhoan_Click);

            // mnuBaoCao
            this.mnuBaoCao.DropDownItems.AddRange(new ToolStripItem[]
            {
                this.mnuBaoCaoDoanhThu
            });
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Text = "Báo cáo";

            this.mnuBaoCaoDoanhThu.Name = "mnuBaoCaoDoanhThu";
            this.mnuBaoCaoDoanhThu.Text = "Báo cáo doanh thu";
            this.mnuBaoCaoDoanhThu.Click += new System.EventHandler(this.mnuBaoCaoDoanhThu_Click);

            // pnlBanner
            this.pnlBanner.BackColor = Color.FromArgb(30, 41, 59);
            this.pnlBanner.Controls.Add(this.lblThongTinNguoiDung);
            this.pnlBanner.Controls.Add(this.lblTieuDe);
            this.pnlBanner.Dock = DockStyle.Top;
            this.pnlBanner.Location = new Point(0, 28);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new Size(960, 95);

            // lblTieuDe
            this.lblTieuDe.Dock = DockStyle.Top;
            this.lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTieuDe.ForeColor = Color.White;
            this.lblTieuDe.Location = new Point(0, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Padding = new Padding(25, 14, 0, 0);
            this.lblTieuDe.Size = new Size(960, 52);
            this.lblTieuDe.Text = "HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM VÀ BÁN VÉ";

            // lblThongTinNguoiDung
            this.lblThongTinNguoiDung.Dock = DockStyle.Bottom;
            this.lblThongTinNguoiDung.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            this.lblThongTinNguoiDung.ForeColor = Color.FromArgb(241, 245, 249);
            this.lblThongTinNguoiDung.Location = new Point(0, 55);
            this.lblThongTinNguoiDung.Name = "lblThongTinNguoiDung";
            this.lblThongTinNguoiDung.Padding = new Padding(27, 0, 0, 10);
            this.lblThongTinNguoiDung.Size = new Size(960, 40);
            this.lblThongTinNguoiDung.Text = "Xin chào...";

            // flpChucNang
            this.flpChucNang.Dock = DockStyle.Fill;
            this.flpChucNang.Location = new Point(0, 123);
            this.flpChucNang.Name = "flpChucNang";
            this.flpChucNang.Padding = new Padding(30, 25, 30, 25);
            this.flpChucNang.Size = new Size(960, 430);
            this.flpChucNang.AutoScroll = true;

            // Gom màu theo 4 nhóm chức năng:
            // 1. Nhóm Nghiệp vụ: Xanh lá (22, 163, 74)
            Color mauNghiepVu = Color.FromArgb(22, 163, 74);
            // 2. Nhóm Danh mục: Xanh dương (37, 99, 235)
            Color mauDanhMuc = Color.FromArgb(37, 99, 235);
            // 3. Nhóm Báo cáo: Cam hổ phách (217, 119, 6)
            Color mauBaoCao = Color.FromArgb(217, 119, 6);
            // 4. Nhóm Hệ thống: Xám xanh Slate (71, 85, 105)
            Color mauHeThong = Color.FromArgb(71, 85, 105);

            CauHinhNutChucNang(this.btnBanVe, "BÁN VÉ XEM PHIM", mauNghiepVu, this.mnuBanVe_Click, 0);
            CauHinhNutChucNang(this.btnXemLichChieu, "XEM LỊCH CHIẾU", mauNghiepVu, this.mnuXemLichChieu_Click, 1);

            CauHinhNutChucNang(this.btnQuanLyPhim, "QUẢN LÝ PHIM", mauDanhMuc, this.mnuQuanLyPhim_Click, 2);
            CauHinhNutChucNang(this.btnQuanLyPhongChieu, "QUẢN LÝ PHÒNG CHIẾU", mauDanhMuc, this.mnuQuanLyPhongChieu_Click, 3);
            CauHinhNutChucNang(this.btnQuanLySuatChieu, "QUẢN LÝ SUẤT CHIẾU", mauDanhMuc, this.mnuQuanLySuatChieu_Click, 4);
            CauHinhNutChucNang(this.btnQuanLyNhanVien, "QUẢN LÝ NHÂN VIÊN", mauDanhMuc, this.mnuQuanLyNhanVien_Click, 5);

            CauHinhNutChucNang(this.btnBaoCaoDoanhThu, "BÁO CÁO DOANH THU", mauBaoCao, this.mnuBaoCaoDoanhThu_Click, 6);

            CauHinhNutChucNang(this.btnQuanLyTaiKhoan, "QUẢN LÝ TÀI KHOẢN", mauHeThong, this.mnuQuanLyTaiKhoan_Click, 7);
            CauHinhNutChucNang(this.btnDoiMatKhau, "ĐỔI MẬT KHẨU", mauHeThong, this.mnuDoiMatKhau_Click, 8);
            CauHinhNutChucNang(this.btnDangXuat, "ĐĂNG XUẤT", mauHeThong, this.mnuDangXuat_Click, 9);

            this.flpChucNang.Controls.AddRange(new Control[]
            {
                this.btnBanVe,
                this.btnXemLichChieu,
                this.btnQuanLyPhim,
                this.btnQuanLyPhongChieu,
                this.btnQuanLySuatChieu,
                this.btnQuanLyNhanVien,
                this.btnBaoCaoDoanhThu,
                this.btnQuanLyTaiKhoan,
                this.btnDoiMatKhau,
                this.btnDangXuat
            });

            // stsTrangThai
            this.stsTrangThai.Items.AddRange(new ToolStripItem[]
            {
                this.lblTrangThaiPhien
            });
            this.stsTrangThai.Location = new Point(0, 553);
            this.stsTrangThai.Name = "stsTrangThai";
            this.stsTrangThai.Size = new Size(960, 24);

            this.lblTrangThaiPhien.Name = "lblTrangThaiPhien";
            this.lblTrangThaiPhien.Text = "Sẵn sàng";

            // frmTrangChu
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 250, 252);
            this.ClientSize = new Size(960, 577);
            this.Controls.Add(this.flpChucNang);
            this.Controls.Add(this.pnlBanner);
            this.Controls.Add(this.stsTrangThai);
            this.Controls.Add(this.mnuChinh);
            this.MainMenuStrip = this.mnuChinh;
            this.MinimumSize = new Size(880, 560);
            this.Name = "frmTrangChu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Trang chủ - Hệ thống quản lý rạp chiếu phim và bán vé";
            this.Load += new System.EventHandler(this.frmTrangChu_Load);

            this.mnuChinh.ResumeLayout(false);
            this.mnuChinh.PerformLayout();
            this.pnlBanner.ResumeLayout(false);
            this.flpChucNang.ResumeLayout(false);
            this.stsTrangThai.ResumeLayout(false);
            this.stsTrangThai.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CauHinhNutChucNang(Button btn, string text, Color backColor, System.EventHandler onClick, int tabIndex)
        {
            btn.BackColor = backColor;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.Margin = new Padding(12);
            btn.Padding = new Padding(8, 0, 8, 0);
            btn.Size = new Size(200, 95);
            btn.TabIndex = tabIndex;
            btn.Text = text;
            btn.UseVisualStyleBackColor = false;
            btn.Cursor = Cursors.Hand;
            btn.Click += onClick;
        }
    }
}
