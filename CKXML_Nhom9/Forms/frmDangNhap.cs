using System;
using System.Windows.Forms;
using System.Xml;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Bật/tắt hiển thị ký tự mật khẩu.
        /// </summary>
        private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        /// <summary>
        /// Xử lý đăng nhập: kiểm tra thông tin trong TaiKhoan.xml bằng XPath và mật khẩu băm SHA-256.
        /// </summary>
        private void btnDangNhap_Click(object? sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            try
            {
                string matKhauBam = XmlHelper.BamMatKhauSHA256(matKhau);
                XmlDocument doc = XmlHelper.LoadXmlDocument("TaiKhoan.xml");

                // Dựng XPath an toàn chống XPath Injection
                string xpath = $"//TaiKhoan[tenDangNhap={XmlHelper.EscapeXPathLiteral(tenDangNhap)} and matKhau={XmlHelper.EscapeXPathLiteral(matKhauBam)}]";
                XmlNode? tkNode = doc.SelectSingleNode(xpath);

                if (tkNode == null)
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.SelectAll();
                    txtMatKhau.Focus();
                    return;
                }

                string maNV = tkNode.SelectSingleNode("maNV")?.InnerText.Trim() ?? string.Empty;
                string quyenStr = tkNode.SelectSingleNode("quyenHan")?.InnerText.Trim() ?? "2";
                int.TryParse(quyenStr, out int quyenHan);

                // Lưu thông tin vào Session
                Session.TenDangNhap = tenDangNhap;
                Session.MaNV = maNV;
                Session.QuyenHan = quyenHan;

                // Mở trang chủ và ẩn form đăng nhập
                this.Hide();
                using (frmTrangChu trangChu = new frmTrangChu())
                {
                    trangChu.ShowDialog();
                    if (trangChu.DaDangXuat)
                    {
                        // Nếu người dùng chọn Đăng xuất thì xóa mật khẩu và quay lại màn hình đăng nhập
                        Session.DangXuat();
                        txtMatKhau.Clear();
                        chkHienMatKhau.Checked = false;
                        txtMatKhau.UseSystemPasswordChar = true;
                        this.Show();
                        txtTenDangNhap.Focus();
                    }
                    else
                    {
                        // Nếu người dùng đóng cửa sổ Trang chủ bằng nút X hoặc Thoát thì thoát hẳn chương trình
                        Session.DangXuat();
                        this.Close();
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc dữ liệu tài khoản:\n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Đóng ứng dụng khi nhấn nút Thoát.
        /// </summary>
        private void btnThoat_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}

