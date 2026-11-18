using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmNhanVien : Form
    {
        private const string TEN_FILE_XML = "NhanVien.xml";
        private const string TEN_FILE_XSD = "NhanVien.xsd";
        private const string TEN_THE = "NhanVien";

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object? sender, EventArgs e)
        {
            if (!Session.LaQuanLy)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng Quản lý nhân viên (chỉ dành cho Quản lý)!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            cbbTieuChiTim.SelectedIndex = 0;
            TaiDanhSachNhanVien();
            LamMoiO();
        }

        /// <summary>
        /// Tải toàn bộ danh sách nhân viên từ NhanVien.xml lên DataGridView.
        /// </summary>
        private void TaiDanhSachNhanVien()
        {
            try
            {
                DataTable dt = XmlHelper.LoadDataTable(TEN_FILE_XML);
                GanDataTableLenLuoi(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gán DataTable vào DataGridView và đặt tiêu đề cột tiếng Việt.
        /// </summary>
        private void GanDataTableLenLuoi(DataTable dt)
        {
            dgvNhanVien.DataSource = dt;

            if (dgvNhanVien.Columns.Contains("maNV"))
            {
                dgvNhanVien.Columns["maNV"]!.HeaderText = "Mã NV";
                dgvNhanVien.Columns["maNV"]!.FillWeight = 65;
            }
            if (dgvNhanVien.Columns.Contains("hoTen"))
            {
                dgvNhanVien.Columns["hoTen"]!.HeaderText = "Họ và tên";
                dgvNhanVien.Columns["hoTen"]!.FillWeight = 135;
            }
            if (dgvNhanVien.Columns.Contains("gioiTinh"))
            {
                dgvNhanVien.Columns["gioiTinh"]!.HeaderText = "Giới tính";
                dgvNhanVien.Columns["gioiTinh"]!.FillWeight = 65;
            }
            if (dgvNhanVien.Columns.Contains("ngaySinh"))
            {
                dgvNhanVien.Columns["ngaySinh"]!.HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["ngaySinh"]!.FillWeight = 85;
            }
            if (dgvNhanVien.Columns.Contains("diaChi"))
            {
                dgvNhanVien.Columns["diaChi"]!.HeaderText = "Địa chỉ";
                dgvNhanVien.Columns["diaChi"]!.FillWeight = 160;
            }
            if (dgvNhanVien.Columns.Contains("soDienThoai"))
            {
                dgvNhanVien.Columns["soDienThoai"]!.HeaderText = "Số điện thoại";
                dgvNhanVien.Columns["soDienThoai"]!.FillWeight = 90;
            }
            if (dgvNhanVien.Columns.Contains("chucVu"))
            {
                dgvNhanVien.Columns["chucVu"]!.HeaderText = "Chức vụ";
                dgvNhanVien.Columns["chucVu"]!.FillWeight = 110;
            }
        }

        /// <summary>
        /// Xóa trắng các ô nhập và tự sinh mã nhân viên kế tiếp (NV004...).
        /// </summary>
        private void LamMoiO()
        {
            try
            {
                txtMaNV.Text = XmlHelper.SinhMaTuDong("NV", TEN_FILE_XML, "maNV");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sinh mã nhân viên tự động:\n" + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNV.Text = "NV001";
            }

            txtHoTen.Clear();
            radNam.Checked = true;
            radNu.Checked = false;
            dtpNgaySinh.Value = new DateTime(2000, 1, 1);
            txtSoDienThoai.Clear();
            cbbChucVu.Text = "Nhân viên bán vé";
            txtDiaChi.Clear();
            dgvNhanVien.ClearSelection();
            txtHoTen.Focus();
        }

        /// <summary>
        /// Khi chọn dòng trên DataGridView, đổ dữ liệu lên các ô nhập.
        /// </summary>
        private void dgvNhanVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhanVien.Rows.Count)
                return;

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
            txtMaNV.Text = row.Cells["maNV"]?.Value?.ToString() ?? string.Empty;
            txtHoTen.Text = row.Cells["hoTen"]?.Value?.ToString() ?? string.Empty;

            string gioiTinh = row.Cells["gioiTinh"]?.Value?.ToString() ?? "Nam";
            if (string.Equals(gioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase))
                radNu.Checked = true;
            else
                radNam.Checked = true;

            string ngaySinhStr = row.Cells["ngaySinh"]?.Value?.ToString() ?? string.Empty;
            if (DateTime.TryParseExact(ngaySinhStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ns) ||
                DateTime.TryParse(ngaySinhStr, out ns))
            {
                dtpNgaySinh.Value = ns;
            }

            txtDiaChi.Text = row.Cells["diaChi"]?.Value?.ToString() ?? string.Empty;
            txtSoDienThoai.Text = row.Cells["soDienThoai"]?.Value?.ToString() ?? string.Empty;
            cbbChucVu.Text = row.Cells["chucVu"]?.Value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Kiểm tra hợp lệ thông tin nhân viên: không để trống, đủ 16 tuổi trở lên, SĐT đúng 10 chữ số.
        /// </summary>
        private bool KiemTraHopLe()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Kiểm tra ngày sinh hợp lệ: phải đủ 16 tuổi trở lên tính tới ngày hiện tại
            DateTime ngaySinh = dtpNgaySinh.Value.Date;
            DateTime ngayToiThieu16Tuoi = DateTime.Today.AddYears(-16);
            if (ngaySinh > ngayToiThieu16Tuoi)
            {
                MessageBox.Show(
                    $"Nhân viên phải đủ 16 tuổi trở lên (ngày sinh phải từ {ngayToiThieu16Tuoi:dd/MM/yyyy} trở về trước)!",
                    "Ngày sinh không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                dtpNgaySinh.Focus();
                return false;
            }

            string sdt = txtSoDienThoai.Text.Trim();
            if (!Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                MessageBox.Show("Số điện thoại phải gồm đúng 10 chữ số (0-9)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoDienThoai.SelectAll();
                txtSoDienThoai.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbbChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn chức vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbChucVu.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }

            return true;
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                string maNV = txtMaNV.Text.Trim();
                if (string.IsNullOrEmpty(maNV))
                {
                    maNV = XmlHelper.SinhMaTuDong("NV", TEN_FILE_XML, "maNV");
                    txtMaNV.Text = maNV;
                }

                if (XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maNV", maNV))
                {
                    string maMoi = XmlHelper.SinhMaTuDong("NV", TEN_FILE_XML, "maNV");
                    DialogResult dr = MessageBox.Show(
                        $"Mã nhân viên '{maNV}' đã tồn tại!\nBạn có muốn tự động chuyển sang mã mới '{maMoi}' để thêm nhân viên này không?",
                        "Trùng mã nhân viên",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maNV = maMoi;
                        txtMaNV.Text = maNV;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!KiemTraHopLe())
                    return;

                string gioiTinh = radNu.Checked ? "Nữ" : "Nam";
                string ngaySinhXml = dtpNgaySinh.Value.ToString("yyyy-MM-dd");

                Dictionary<string, string> duLieu = new Dictionary<string, string>
                {
                    { "maNV", maNV },
                    { "hoTen", txtHoTen.Text.Trim() },
                    { "gioiTinh", gioiTinh },
                    { "ngaySinh", ngaySinhXml },
                    { "diaChi", txtDiaChi.Text.Trim() },
                    { "soDienThoai", txtSoDienThoai.Text.Trim() },
                    { "chucVu", cbbChucVu.Text.Trim() }
                };

                XmlHelper.Them(TEN_FILE_XML, TEN_THE, duLieu);

                List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                if (loiXsd.Count > 0)
                {
                    MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show($"Đã thêm thành công nhân viên '{txtHoTen.Text.Trim()}' (Mã: {maNV})!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachNhanVien();
                LamMoiO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                string maNV = txtMaNV.Text.Trim();
                if (string.IsNullOrEmpty(maNV) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maNV", maNV))
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên có sẵn trên bảng danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraHopLe())
                    return;

                string gioiTinh = radNu.Checked ? "Nữ" : "Nam";
                string ngaySinhXml = dtpNgaySinh.Value.ToString("yyyy-MM-dd");

                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "hoTen", txtHoTen.Text.Trim() },
                    { "gioiTinh", gioiTinh },
                    { "ngaySinh", ngaySinhXml },
                    { "diaChi", txtDiaChi.Text.Trim() },
                    { "soDienThoai", txtSoDienThoai.Text.Trim() },
                    { "chucVu", cbbChucVu.Text.Trim() }
                };

                bool ketQua = XmlHelper.Sua(TEN_FILE_XML, TEN_THE, "maNV", maNV, duLieuMoi);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã cập nhật thông tin nhân viên '{maNV}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachNhanVien();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhân viên:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa nhân viên; từ chối xóa nếu nhân viên đã có tài khoản (TaiKhoan.xml) hoặc đã bán vé (Ve.xml), báo rõ lý do.
        /// </summary>
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                string maNV = txtMaNV.Text.Trim();
                if (string.IsNullOrEmpty(maNV) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maNV", maNV))
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên trên bảng danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool coTaiKhoan = XmlHelper.TonTaiXPath("TaiKhoan.xml", $"//TaiKhoan[maNV={XmlHelper.EscapeXPathLiteral(maNV)}]");
                bool daBanVe = XmlHelper.TonTaiXPath("Ve.xml", $"//Ve[maNV={XmlHelper.EscapeXPathLiteral(maNV)}]");

                if (coTaiKhoan || daBanVe)
                {
                    List<string> lyDo = new List<string>();
                    if (coTaiKhoan)
                        lyDo.Add("- Nhân viên này đang được cấp tài khoản đăng nhập trong TaiKhoan.xml.");
                    if (daBanVe)
                        lyDo.Add("- Nhân viên này đã có lịch sử bán vé trong Ve.xml.");

                    MessageBox.Show(
                        $"Không thể xóa nhân viên '{txtHoTen.Text.Trim()}' (Mã: {maNV}) vì:\n" + string.Join("\n", lyDo),
                        "Từ chối xóa do ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhân viên '{txtHoTen.Text.Trim()}' (Mã: {maNV}) không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                    return;

                bool ketQua = XmlHelper.Xoa(TEN_FILE_XML, TEN_THE, "maNV", maNV);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã xóa nhân viên '{maNV}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachNhanVien();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    TaiDanhSachNhanVien();
                    return;
                }

                string tieuChi = cbbTieuChiTim.SelectedItem?.ToString() ?? "Tất cả";
                string xpath;

                switch (tieuChi)
                {
                    case "Mã nhân viên":
                        xpath = $"//NhanVien[{XmlHelper.BieuThucChuaXPath("maNV", tuKhoa)}]";
                        break;
                    case "Họ tên":
                        xpath = $"//NhanVien[{XmlHelper.BieuThucChuaXPath("hoTen", tuKhoa)}]";
                        break;
                    case "Số điện thoại":
                        xpath = $"//NhanVien[{XmlHelper.BieuThucChuaXPath("soDienThoai", tuKhoa)}]";
                        break;
                    case "Chức vụ":
                        xpath = $"//NhanVien[{XmlHelper.BieuThucChuaXPath("chucVu", tuKhoa)}]";
                        break;
                    default:
                        xpath = $"//NhanVien[{XmlHelper.BieuThucChuaXPath("maNV", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("hoTen", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("soDienThoai", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("chucVu", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("diaChi", tuKhoa)}]";
                        break;
                }

                DataTable dtKetQua = XmlHelper.TimKiemXPath(TEN_FILE_XML, xpath);
                GanDataTableLenLuoi(dtKetQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm nhân viên bằng XPath:\n" + ex.Message, "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TaiDanhSachNhanVien();
        }
    }
}

