namespace CKXML_Nhom9.Helpers
{
    /// <summary>
    /// Lưu thông tin phiên đăng nhập hiện tại của người dùng.
    /// </summary>
    public static class Session
    {
        public static string TenDangNhap { get; set; } = string.Empty;
        public static string MaNV { get; set; } = string.Empty;
        public static int QuyenHan { get; set; } = 0; // 1 = Quản lý, 2 = Nhân viên

        /// <summary>
        /// Kiểm tra người dùng hiện tại có phải Quản lý (quyền 1) hay không.
        /// </summary>
        public static bool LaQuanLy => QuyenHan == 1;

        /// <summary>
        /// Xóa thông tin phiên khi đăng xuất.
        /// </summary>
        public static void DangXuat()
        {
            TenDangNhap = string.Empty;
            MaNV = string.Empty;
            QuyenHan = 0;
        }
    }
}

