using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
        {
            bool anMatKhau = !chkHienMatKhau.Checked;
            txtMatKhauCu.UseSystemPasswordChar = anMatKhau;
            txtMatKhauMoi.UseSystemPasswordChar = anMatKhau;
            txtXacNhanMatKhau.UseSystemPasswordChar = anMatKhau;
        }

        private void btnLuu_Click(object? sender, EventArgs e)
        {
            string matKhauCu = txtMatKhauCu.Text;
            string matKhauMoi = txtMatKhauMoi.Text;
            string xacNhan = txtXacNhanMatKhau.Text;

            if (string.IsNullOrEmpty(matKhauCu))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauCu.Focus();
                return;
            }

            if (string.IsNullOrEmpty(matKhauMoi))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (matKhauMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp với mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXacNhanMatKhau.Focus();
                return;
            }

            try
            {
                string bamCu = XmlHelper.BamMatKhauSHA256(matKhauCu);
                XmlDocument doc = XmlHelper.LoadXmlDocument("TaiKhoan.xml");
                string xpath = $"//TaiKhoan[tenDangNhap={XmlHelper.EscapeXPathLiteral(Session.TenDangNhap)} and matKhau={XmlHelper.EscapeXPathLiteral(bamCu)}]";
                XmlNode? tkNode = doc.SelectSingleNode(xpath);

                if (tkNode == null)
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.SelectAll();
                    txtMatKhauCu.Focus();
                    return;
                }

                string bamMoi = XmlHelper.BamMatKhauSHA256(matKhauMoi);
                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "matKhau", bamMoi }
                };

                XmlHelper.Sua("TaiKhoan.xml", "TaiKhoan", "tenDangNhap", Session.TenDangNhap, duLieuMoi);

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu:\n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}

