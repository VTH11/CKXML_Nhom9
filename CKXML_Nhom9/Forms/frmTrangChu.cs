using System;
using System.Windows.Forms;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmTrangChu : Form
    {
        /// <summary>
        /// Đánh dấu người dùng chủ động nhấn Đăng xuất để quay về frmDangNhap.
        /// </summary>
        public bool DaDangXuat { get; private set; } = false;

        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void frmTrangChu_Load(object? sender, EventArgs e)
        {
            HienThiThongTinPhien();
            ApDungPhanQuyen();
        }

        /// <summary>
        /// Đọc tên nhân viên từ NhanVien.xml theo Session.MaNV và hiển thị lên giao diện.
        /// </summary>
        private void HienThiThongTinPhien()
        {
            string hoTen = XmlHelper.LayGiaTriXPath(
                "NhanVien.xml",
                $"//NhanVien[maNV={XmlHelper.EscapeXPathLiteral(Session.MaNV)}]/hoTen");

            if (string.IsNullOrEmpty(hoTen))
                hoTen = Session.TenDangNhap;

            string tenQuyen = Session.LaQuanLy ? "Quản lý" : "Nhân viên";

            lblThongTinNguoiDung.Text = $"Xin chào: {hoTen}  |  Mã NV: {Session.MaNV}  |  Tài khoản: {Session.TenDangNhap}  |  Vai trò: {tenQuyen}";
            lblTrangThaiPhien.Text = $"Đang đăng nhập: {Session.TenDangNhap} | {tenQuyen}";
        }

        /// <summary>
        /// Phân quyền hiển thị chức năng:
        /// - Quyền 1 (Quản lý): Thấy toàn bộ chức năng.
        /// - Quyền 2 (Nhân viên): Chỉ thấy Bán vé, Xem lịch chiếu, Đổi mật khẩu, Đăng xuất;
        ///   ẩn Quản lý nhân viên/phim/phòng/suất chiếu/tài khoản/báo cáo doanh thu.
        /// </summary>
        private void ApDungPhanQuyen()
        {
            bool laQuanLy = Session.LaQuanLy;

            // Ẩn/hiện trên MenuStrip
            mnuQuanLy.Visible = laQuanLy;
            mnuQuanLyPhim.Visible = laQuanLy;
            mnuQuanLyPhongChieu.Visible = laQuanLy;
            mnuQuanLySuatChieu.Visible = laQuanLy;
            mnuQuanLyNhanVien.Visible = laQuanLy;
            mnuQuanLyTaiKhoan.Visible = laQuanLy;

            mnuBaoCao.Visible = laQuanLy;
            mnuBaoCaoDoanhThu.Visible = laQuanLy;

            // Ẩn/hiện trên các nút chức năng màn hình chính (FlowLayoutPanel tự dồn các ô còn lại)
            btnQuanLyPhim.Visible = laQuanLy;
            btnQuanLyPhongChieu.Visible = laQuanLy;
            btnQuanLySuatChieu.Visible = laQuanLy;
            btnQuanLyNhanVien.Visible = laQuanLy;
            btnQuanLyTaiKhoan.Visible = laQuanLy;
            btnBaoCaoDoanhThu.Visible = laQuanLy;

            flpChucNang.PerformLayout();
        }

        private void mnuDoiMatKhau_Click(object? sender, EventArgs e)
        {
            using (frmDoiMatKhau frm = new frmDoiMatKhau())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuDangXuat_Click(object? sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất khỏi hệ thống?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                Session.DangXuat();
                DaDangXuat = true;
                this.Close();
            }
        }

        private void mnuThoat_Click(object? sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Bạn có muốn thoát chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                DaDangXuat = false;
                this.Close();
            }
        }

        private void mnuBanVe_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Bán vé (frmBanVe) sẽ được xây dựng ở Giai đoạn 5.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuXemLichChieu_Click(object? sender, EventArgs e)
        {
            using (frmSuatChieu frm = new frmSuatChieu(cheDoChiXem: true))
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuQuanLyPhim_Click(object? sender, EventArgs e)
        {
            using (frmPhim frm = new frmPhim())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuQuanLyPhongChieu_Click(object? sender, EventArgs e)
        {
            using (frmPhongChieu frm = new frmPhongChieu())
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuQuanLySuatChieu_Click(object? sender, EventArgs e)
        {
            using (frmSuatChieu frm = new frmSuatChieu(cheDoChiXem: false))
            {
                frm.ShowDialog(this);
            }
        }

        private void mnuQuanLyNhanVien_Click(object? sender, EventArgs e)
        {
            using (frmNhanVien frm = new frmNhanVien())
            {
                frm.ShowDialog(this);
                HienThiThongTinPhien();
            }
        }

        private void mnuQuanLyTaiKhoan_Click(object? sender, EventArgs e)
        {
            using (frmTaiKhoan frm = new frmTaiKhoan())
            {
                frm.ShowDialog(this);
                HienThiThongTinPhien();
                ApDungPhanQuyen();
            }
        }

        private void mnuBaoCaoDoanhThu_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Báo cáo doanh thu (XSLT) sẽ được xây dựng ở Giai đoạn 6.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

