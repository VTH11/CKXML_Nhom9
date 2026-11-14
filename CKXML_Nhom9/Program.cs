using CKXML_Nhom9.Forms;

namespace CKXML_Nhom9
{
    internal static class Program
    {
        /// <summary>
        ///  Điểm khởi đầu của ứng dụng, chạy form Đăng nhập.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new frmDangNhap());
        }
    }
}