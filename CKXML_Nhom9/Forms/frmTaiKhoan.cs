using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmTaiKhoan : Form
    {
        private const string TEN_FILE_XML = "TaiKhoan.xml";
        private const string TEN_FILE_XSD = "TaiKhoan.xsd";
        private const string TEN_THE = "TaiKhoan";

        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object? sender, EventArgs e)
        {
            if (!Session.LaQuanLy)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng Quản lý tài khoản (chỉ dành cho Quản lý)!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cbbTieuChiTim.SelectedIndex = 0;
            TaiDanhSachNhanVienLenCombo();
            TaiDanhSachTaiKhoan();
            LamMoiO();
        }

        private void chkHienMatKhau_CheckedChanged(object? sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        /// <summary>
        /// Nạp danh sách nhân viên từ NhanVien.xml vào ComboBox.
        /// </summary>
        private void TaiDanhSachNhanVienLenCombo()
        {
            DataTable dtNV = XmlHelper.LoadDataTable("NhanVien.xml");
            if (!dtNV.Columns.Contains("hienThi"))
            {
                dtNV.Columns.Add("hienThi", typeof(string));
            }

            foreach (DataRow row in dtNV.Rows)
            {
                row["hienThi"] = $"{row["maNV"]} - {row["hoTen"]}";
            }

            cbbNhanVien.DataSource = dtNV;
            cbbNhanVien.DisplayMember = "hienThi";
            cbbNhanVien.ValueMember = "maNV";
        }

        /// <summary>
        /// Tải danh sách tài khoản từ TaiKhoan.xml lên DataGridView kèm tên nhân viên và tên quyền.
        /// </summary>
        private void TaiDanhSachTaiKhoan()
        {
            try
            {
                DataTable dt = XmlHelper.LoadDataTable(TEN_FILE_XML);
                BoSungCotHienThi(dt);
                GanDataTableLenLuoi(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài khoản:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BoSungCotHienThi(DataTable dt)
        {
            if (!dt.Columns.Contains("tenQuyen"))
                dt.Columns.Add("tenQuyen", typeof(string));
            if (!dt.Columns.Contains("hoTenNV"))
                dt.Columns.Add("hoTenNV", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                string q = row["quyenHan"]?.ToString() ?? "2";
                row["tenQuyen"] = q == "1" ? "1 - Quản lý" : "2 - Nhân viên";

                string maNV = row["maNV"]?.ToString() ?? string.Empty;
                string hoTen = XmlHelper.LayGiaTriXPath("NhanVien.xml", $"//NhanVien[maNV={XmlHelper.EscapeXPathLiteral(maNV)}]/hoTen");
                row["hoTenNV"] = hoTen;
            }
        }

        private void GanDataTableLenLuoi(DataTable dt)
        {
            dgvTaiKhoan.DataSource = dt;

            if (dgvTaiKhoan.Columns.Contains("tenDangNhap"))
            {
                dgvTaiKhoan.Columns["tenDangNhap"]!.HeaderText = "Tên đăng nhập";
                dgvTaiKhoan.Columns["tenDangNhap"]!.FillWeight = 95;
            }
            if (dgvTaiKhoan.Columns.Contains("maNV"))
            {
                dgvTaiKhoan.Columns["maNV"]!.HeaderText = "Mã NV";
                dgvTaiKhoan.Columns["maNV"]!.FillWeight = 65;
            }
            if (dgvTaiKhoan.Columns.Contains("hoTenNV"))
            {
                dgvTaiKhoan.Columns["hoTenNV"]!.HeaderText = "Họ tên nhân viên";
                dgvTaiKhoan.Columns["hoTenNV"]!.FillWeight = 130;
            }
            if (dgvTaiKhoan.Columns.Contains("quyenHan"))
            {
                dgvTaiKhoan.Columns["quyenHan"]!.Visible = false;
            }
            if (dgvTaiKhoan.Columns.Contains("tenQuyen"))
            {
                dgvTaiKhoan.Columns["tenQuyen"]!.HeaderText = "Quyền hạn";
                dgvTaiKhoan.Columns["tenQuyen"]!.FillWeight = 90;
            }
            if (dgvTaiKhoan.Columns.Contains("matKhau"))
            {
                dgvTaiKhoan.Columns["matKhau"]!.HeaderText = "Mật khẩu băm (SHA-256)";
                dgvTaiKhoan.Columns["matKhau"]!.FillWeight = 180;
            }
        }

        private void LamMoiO()
        {
            txtTenDangNhap.Clear();
            txtTenDangNhap.ReadOnly = false;
            txtMatKhau.Clear();
            chkHienMatKhau.Checked = false;
            cbbQuyenHan.SelectedIndex = 1; // Mặc định: 2 - Nhân viên
            if (cbbNhanVien.Items.Count > 0)
                cbbNhanVien.SelectedIndex = 0;

            dgvTaiKhoan.ClearSelection();
            txtTenDangNhap.Focus();
        }

        private void dgvTaiKhoan_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTaiKhoan.Rows.Count)
                return;

            DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
            txtTenDangNhap.Text = row.Cells["tenDangNhap"]?.Value?.ToString() ?? string.Empty;
            txtTenDangNhap.ReadOnly = true; // Không sửa khóa chính tên đăng nhập của bản ghi đã có

            string maNV = row.Cells["maNV"]?.Value?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(maNV))
                cbbNhanVien.SelectedValue = maNV;

            string quyen = row.Cells["quyenHan"]?.Value?.ToString() ?? "2";
            cbbQuyenHan.SelectedIndex = quyen == "1" ? 0 : 1;

            txtMatKhau.Clear();
        }

        /// <summary>
        /// Đếm tổng số tài khoản có quyền Quản lý (quyenHan = 1) hiện có trong TaiKhoan.xml.
        /// </summary>
        private int DemSoTaiKhoanAdmin()
        {
            XmlDocument doc = XmlHelper.LoadXmlDocument(TEN_FILE_XML);
            XmlNodeList? nodes = doc.SelectNodes("//TaiKhoan[quyenHan='1']");
            return nodes?.Count ?? 0;
        }

        /// <summary>
        /// Thêm mới một tài khoản vào TaiKhoan.xml.
        /// </summary>
        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tenDN = txtTenDangNhap.Text.Trim();
                if (string.IsNullOrEmpty(tenDN))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (tenDN.Length > 20)
                {
                    MessageBox.Show("Tên đăng nhập không được vượt quá 20 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN))
                {
                    MessageBox.Show($"Tên đăng nhập '{tenDN}' đã tồn tại! Vui lòng nhấn 'Làm mới' và nhập tên đăng nhập khác.", "Trùng tên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtMatKhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu khởi tạo cho tài khoản mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                string maNV = cbbNhanVien.SelectedValue?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên sở hữu tài khoản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string quyenHan = cbbQuyenHan.SelectedIndex == 0 ? "1" : "2";
                string matKhauBam = XmlHelper.BamMatKhauSHA256(txtMatKhau.Text);

                Dictionary<string, string> duLieu = new Dictionary<string, string>
                {
                    { "tenDangNhap", tenDN },
                    { "matKhau", matKhauBam },
                    { "quyenHan", quyenHan },
                    { "maNV", maNV }
                };

                XmlHelper.Them(TEN_FILE_XML, TEN_THE, duLieu);

                List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                if (loiXsd.Count > 0)
                {
                    MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show($"Đã tạo tài khoản '{tenDN}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachTaiKhoan();
                LamMoiO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tài khoản:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Đổi quyền hạn hoặc nhân viên gắn với tài khoản; không cho hạ quyền tài khoản Quản lý (admin) cuối cùng.
        /// </summary>
        private void btnDoiQuyen_Click(object? sender, EventArgs e)
        {
            try
            {
                string tenDN = txtTenDangNhap.Text.Trim();
                if (string.IsNullOrEmpty(tenDN) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN))
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản trên bảng danh sách để đổi quyền / cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string quyenCu = XmlHelper.LayGiaTriXPath(TEN_FILE_XML, $"//TaiKhoan[tenDangNhap={XmlHelper.EscapeXPathLiteral(tenDN)}]/quyenHan");
                string quyenMoi = cbbQuyenHan.SelectedIndex == 0 ? "1" : "2";
                string maNVMoi = cbbNhanVien.SelectedValue?.ToString() ?? string.Empty;

                // Kiểm tra không cho hạ quyền tài khoản admin cuối cùng
                if (quyenCu == "1" && quyenMoi == "2")
                {
                    int soAdmin = DemSoTaiKhoanAdmin();
                    if (soAdmin <= 1)
                    {
                        MessageBox.Show(
                            $"Không thể hạ quyền tài khoản '{tenDN}' xuống Nhân viên vì đây là tài khoản Quản lý (admin) duy nhất còn lại trong hệ thống!",
                            "Từ chối hạ quyền admin cuối cùng",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        cbbQuyenHan.SelectedIndex = 0;
                        return;
                    }
                }

                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "quyenHan", quyenMoi },
                    { "maNV", maNVMoi }
                };

                bool ketQua = XmlHelper.Sua(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN, duLieuMoi);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // Nếu cập nhật chính tài khoản đang đăng nhập thì đồng bộ lại Session
                    if (string.Equals(tenDN, Session.TenDangNhap, StringComparison.OrdinalIgnoreCase))
                    {
                        Session.MaNV = maNVMoi;
                        Session.QuyenHan = int.Parse(quyenMoi);
                    }

                    MessageBox.Show($"Đã cập nhật quyền hạn / nhân viên cho tài khoản '{tenDN}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachTaiKhoan();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tài khoản:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Đặt lại mật khẩu mới (băm SHA-256) cho tài khoản đang chọn.
        /// </summary>
        private void btnDatLaiMatKhau_Click(object? sender, EventArgs e)
        {
            try
            {
                string tenDN = txtTenDangNhap.Text.Trim();
                if (string.IsNullOrEmpty(tenDN) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN))
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản trên bảng danh sách để đặt lại mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtMatKhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới cần đặt lại vào ô 'Mật khẩu'!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn đặt lại mật khẩu mới cho tài khoản '{tenDN}' không?",
                    "Xác nhận đặt lại mật khẩu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                    return;

                string matKhauBam = XmlHelper.BamMatKhauSHA256(txtMatKhau.Text);
                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "matKhau", matKhauBam }
                };

                bool ketQua = XmlHelper.Sua(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN, duLieuMoi);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã đặt lại mật khẩu cho tài khoản '{tenDN}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachTaiKhoan();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt lại mật khẩu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa tài khoản; không cho tự xóa tài khoản đang đăng nhập và không cho xóa admin cuối cùng.
        /// </summary>
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                string tenDN = txtTenDangNhap.Text.Trim();
                if (string.IsNullOrEmpty(tenDN) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN))
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản trên bảng danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Không cho tự xóa chính mình
                if (string.Equals(tenDN, Session.TenDangNhap, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        $"Bạn đang đăng nhập bằng tài khoản '{tenDN}'. Không thể tự xóa tài khoản của chính mình!",
                        "Từ chối xóa tài khoản đang đăng nhập",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // 2. Không cho xóa tài khoản Quản lý (admin) cuối cùng
                string quyen = XmlHelper.LayGiaTriXPath(TEN_FILE_XML, $"//TaiKhoan[tenDangNhap={XmlHelper.EscapeXPathLiteral(tenDN)}]/quyenHan");
                if (quyen == "1" && DemSoTaiKhoanAdmin() <= 1)
                {
                    MessageBox.Show(
                        $"Không thể xóa tài khoản '{tenDN}' vì đây là tài khoản Quản lý (admin) cuối cùng của hệ thống!",
                        "Từ chối xóa admin cuối cùng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa tài khoản '{tenDN}' không?",
                    "Xác nhận xóa tài khoản",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                    return;

                bool ketQua = XmlHelper.Xoa(TEN_FILE_XML, TEN_THE, "tenDangNhap", tenDN);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã xóa tài khoản '{tenDN}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachTaiKhoan();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tài khoản:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoiO();
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    TaiDanhSachTaiKhoan();
                    return;
                }

                string tieuChi = cbbTieuChiTim.SelectedItem?.ToString() ?? "Tất cả";
                string xpath;

                switch (tieuChi)
                {
                    case "Tên đăng nhập":
                        xpath = $"//TaiKhoan[{XmlHelper.BieuThucChuaXPath("tenDangNhap", tuKhoa)}]";
                        break;
                    case "Mã nhân viên":
                        xpath = $"//TaiKhoan[{XmlHelper.BieuThucChuaXPath("maNV", tuKhoa)}]";
                        break;
                    case "Quyền hạn (1 hoặc 2)":
                        xpath = $"//TaiKhoan[{XmlHelper.BieuThucChuaXPath("quyenHan", tuKhoa)}]";
                        break;
                    default:
                        xpath = $"//TaiKhoan[{XmlHelper.BieuThucChuaXPath("tenDangNhap", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("maNV", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("quyenHan", tuKhoa)}]";
                        break;
                }

                DataTable dtKetQua = XmlHelper.TimKiemXPath(TEN_FILE_XML, xpath);
                BoSungCotHienThi(dtKetQua);
                GanDataTableLenLuoi(dtKetQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm tài khoản bằng XPath:\n" + ex.Message, "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem_Click(sender, e);
            }
        }

        private void btnHienTatCa_Click(object? sender, EventArgs e)
        {
            txtTimKiem.Clear();
            cbbTieuChiTim.SelectedIndex = 0;
            TaiDanhSachTaiKhoan();
        }
    }
}

