using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmPhongChieu : Form
    {
        private const string TEN_FILE_XML = "PhongChieu.xml";
        private const string TEN_FILE_XSD = "PhongChieu.xsd";
        private const string TEN_THE = "PhongChieu";

        public frmPhongChieu()
        {
            InitializeComponent();
        }

        private void frmPhongChieu_Load(object? sender, EventArgs e)
        {
            cbbTieuChiTim.SelectedIndex = 0;
            TaiDanhSachPhongChieu();
            LamMoiO();
        }

        /// <summary>
        /// Tải toàn bộ danh sách phòng chiếu từ PhongChieu.xml lên DataGridView.
        /// </summary>
        private void TaiDanhSachPhongChieu()
        {
            try
            {
                DataTable dt = XmlHelper.LoadDataTable(TEN_FILE_XML);
                GanDataTableLenLuoi(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng chiếu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gán DataTable vào DataGridView và đặt tiêu đề cột tiếng Việt.
        /// </summary>
        private void GanDataTableLenLuoi(DataTable dt)
        {
            dgvPhongChieu.DataSource = dt;

            if (dgvPhongChieu.Columns.Contains("maPhong"))
            {
                dgvPhongChieu.Columns["maPhong"]!.HeaderText = "Mã phòng";
                dgvPhongChieu.Columns["maPhong"]!.FillWeight = 80;
            }
            if (dgvPhongChieu.Columns.Contains("tenPhong"))
            {
                dgvPhongChieu.Columns["tenPhong"]!.HeaderText = "Tên phòng chiếu";
                dgvPhongChieu.Columns["tenPhong"]!.FillWeight = 200;
            }
            if (dgvPhongChieu.Columns.Contains("soHang"))
            {
                dgvPhongChieu.Columns["soHang"]!.HeaderText = "Số hàng ghế (A-Z)";
                dgvPhongChieu.Columns["soHang"]!.FillWeight = 100;
            }
            if (dgvPhongChieu.Columns.Contains("soCot"))
            {
                dgvPhongChieu.Columns["soCot"]!.HeaderText = "Số cột ghế (1-30)";
                dgvPhongChieu.Columns["soCot"]!.FillWeight = 100;
            }
            if (dgvPhongChieu.Columns.Contains("tongSoGhe"))
            {
                dgvPhongChieu.Columns["tongSoGhe"]!.HeaderText = "Tổng số ghế";
                dgvPhongChieu.Columns["tongSoGhe"]!.FillWeight = 100;
            }
        }

        /// <summary>
        /// Xóa trắng các ô nhập và tự sinh mã phòng kế tiếp (PC04...).
        /// </summary>
        private void LamMoiO()
        {
            try
            {
                txtMaPhong.Text = XmlHelper.SinhMaTuDong("PC", TEN_FILE_XML, "maPhong", 2);
            }
            catch
            {
                txtMaPhong.Text = "PC01";
            }

            txtTenPhong.Clear();
            txtSoHang.Clear();
            txtSoCot.Clear();
            txtTongSoGhe.Text = "0";
            dgvPhongChieu.ClearSelection();
            txtTenPhong.Focus();
        }

        /// <summary>
        /// Tự động tính Tổng số ghế = Số hàng x Số cột khi người dùng gõ.
        /// </summary>
        private void txtSoHangSoCot_TextChanged(object? sender, EventArgs e)
        {
            if (int.TryParse(txtSoHang.Text.Trim(), out int soHang) &&
                int.TryParse(txtSoCot.Text.Trim(), out int soCot) &&
                soHang >= 1 && soHang <= 26 &&
                soCot >= 1 && soCot <= 30)
            {
                txtTongSoGhe.Text = (soHang * soCot).ToString();
            }
            else
            {
                txtTongSoGhe.Text = "0";
            }
        }

        private void dgvPhongChieu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhongChieu.Rows.Count)
                return;

            DataGridViewRow row = dgvPhongChieu.Rows[e.RowIndex];
            txtMaPhong.Text = row.Cells["maPhong"]?.Value?.ToString() ?? string.Empty;
            txtTenPhong.Text = row.Cells["tenPhong"]?.Value?.ToString() ?? string.Empty;
            txtSoHang.Text = row.Cells["soHang"]?.Value?.ToString() ?? string.Empty;
            txtSoCot.Text = row.Cells["soCot"]?.Value?.ToString() ?? string.Empty;
            txtTongSoGhe.Text = row.Cells["tongSoGhe"]?.Value?.ToString() ?? "0";
        }

        /// <summary>
        /// Kiểm tra hợp lệ: tên phòng không trống, số hàng 1-26, số cột 1-30.
        /// </summary>
        private bool KiemTraHopLe(out int soHang, out int soCot, out int tongSoGhe)
        {
            soHang = 0;
            soCot = 0;
            tongSoGhe = 0;

            if (string.IsNullOrWhiteSpace(txtTenPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phòng chiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhong.Focus();
                return false;
            }

            if (!int.TryParse(txtSoHang.Text.Trim(), out soHang) || soHang < 1 || soHang > 26)
            {
                MessageBox.Show("Số hàng ghế phải là số nguyên từ 1 đến 26 (tương ứng hàng A đến Z)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHang.SelectAll();
                txtSoHang.Focus();
                return false;
            }

            if (!int.TryParse(txtSoCot.Text.Trim(), out soCot) || soCot < 1 || soCot > 30)
            {
                MessageBox.Show("Số cột ghế phải là số nguyên từ 1 đến 30!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoCot.SelectAll();
                txtSoCot.Focus();
                return false;
            }

            tongSoGhe = soHang * soCot;
            txtTongSoGhe.Text = tongSoGhe.ToString();
            return true;
        }

        /// <summary>
        /// Kiểm tra phòng chiếu có suất chiếu nào đã bán vé trong Ve.xml hay chưa.
        /// </summary>
        private bool PhongDaCoSuatChieuBanVe(string maPhong, out string maSuatDaBan)
        {
            maSuatDaBan = string.Empty;
            XmlDocument docSuat = XmlHelper.LoadXmlDocument("SuatChieu.xml");
            XmlNodeList? dsSuat = docSuat.SelectNodes($"//SuatChieu[maPhong={XmlHelper.EscapeXPathLiteral(maPhong)}]");
            if (dsSuat == null || dsSuat.Count == 0)
                return false;

            foreach (XmlNode suat in dsSuat)
            {
                string maSuat = suat.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(maSuat))
                {
                    if (XmlHelper.TonTaiXPath("Ve.xml", $"//Ve[maSuat={XmlHelper.EscapeXPathLiteral(maSuat)}]"))
                    {
                        maSuatDaBan = maSuat;
                        return true;
                    }
                }
            }
            return false;
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhong = txtMaPhong.Text.Trim();
                if (string.IsNullOrEmpty(maPhong))
                {
                    maPhong = XmlHelper.SinhMaTuDong("PC", TEN_FILE_XML, "maPhong", 2);
                    txtMaPhong.Text = maPhong;
                }

                if (XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maPhong", maPhong))
                {
                    string maMoi = XmlHelper.SinhMaTuDong("PC", TEN_FILE_XML, "maPhong", 2);
                    DialogResult dr = MessageBox.Show(
                        $"Mã phòng '{maPhong}' đã tồn tại!\nBạn có muốn tự động chuyển sang mã mới '{maMoi}' để thêm phòng mới không?",
                        "Trùng mã phòng",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maPhong = maMoi;
                        txtMaPhong.Text = maPhong;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!KiemTraHopLe(out int soHang, out int soCot, out int tongSoGhe))
                    return;

                Dictionary<string, string> duLieu = new Dictionary<string, string>
                {
                    { "maPhong", maPhong },
                    { "tenPhong", txtTenPhong.Text.Trim() },
                    { "soHang", soHang.ToString() },
                    { "soCot", soCot.ToString() },
                    { "tongSoGhe", tongSoGhe.ToString() }
                };

                XmlHelper.Them(TEN_FILE_XML, TEN_THE, duLieu);

                List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                if (loiXsd.Count > 0)
                {
                    MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show($"Đã thêm phòng chiếu '{txtTenPhong.Text.Trim()}' (Mã: {maPhong}, {tongSoGhe} ghế) thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachPhongChieu();
                LamMoiO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phòng chiếu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhong = txtMaPhong.Text.Trim();
                if (string.IsNullOrEmpty(maPhong) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maPhong", maPhong))
                {
                    MessageBox.Show("Vui lòng chọn một phòng chiếu có sẵn trên bảng danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraHopLe(out int soHangMoi, out int soCotMoi, out int tongSoGheMoi))
                    return;

                // Kiểm tra xem người dùng có thay đổi soHang hoặc soCot so với dữ liệu gốc không
                string xpathPhong = $"//PhongChieu[maPhong={XmlHelper.EscapeXPathLiteral(maPhong)}]";
                int.TryParse(XmlHelper.LayGiaTriXPath(TEN_FILE_XML, xpathPhong + "/soHang"), out int soHangCu);
                int.TryParse(XmlHelper.LayGiaTriXPath(TEN_FILE_XML, xpathPhong + "/soCot"), out int soCotCu);

                if (soHangMoi != soHangCu || soCotMoi != soCotCu)
                {
                    if (PhongDaCoSuatChieuBanVe(maPhong, out string maSuatDaBan))
                    {
                        MessageBox.Show(
                            $"Không thể thay đổi Số hàng / Số cột của phòng '{maPhong}' (từ {soHangCu}x{soCotCu} sang {soHangMoi}x{soCotMoi})!\n" +
                            $"Lý do: Phòng này đang có suất chiếu '{maSuatDaBan}' đã phát sinh vé bán ra trong Ve.xml.",
                            "Từ chối sửa kích thước phòng",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        txtSoHang.Text = soHangCu.ToString();
                        txtSoCot.Text = soCotCu.ToString();
                        return;
                    }
                }

                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "tenPhong", txtTenPhong.Text.Trim() },
                    { "soHang", soHangMoi.ToString() },
                    { "soCot", soCotMoi.ToString() },
                    { "tongSoGhe", tongSoGheMoi.ToString() }
                };

                bool ketQua = XmlHelper.Sua(TEN_FILE_XML, TEN_THE, "maPhong", maPhong, duLieuMoi);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã cập nhật phòng chiếu '{maPhong}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachPhongChieu();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật phòng chiếu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhong = txtMaPhong.Text.Trim();
                if (string.IsNullOrEmpty(maPhong) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maPhong", maPhong))
                {
                    MessageBox.Show("Vui lòng chọn một phòng chiếu trên bảng danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Không cho xóa phòng đang có suất chiếu
                if (XmlHelper.TonTaiXPath("SuatChieu.xml", $"//SuatChieu[maPhong={XmlHelper.EscapeXPathLiteral(maPhong)}]"))
                {
                    MessageBox.Show(
                        $"Không thể xóa phòng chiếu '{txtTenPhong.Text.Trim()}' (Mã: {maPhong}) vì đang có suất chiếu sử dụng phòng này trong SuatChieu.xml!",
                        "Từ chối xóa do ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa phòng chiếu '{txtTenPhong.Text.Trim()}' (Mã: {maPhong}) không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                    return;

                bool ketQua = XmlHelper.Xoa(TEN_FILE_XML, TEN_THE, "maPhong", maPhong);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã xóa phòng chiếu '{maPhong}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachPhongChieu();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa phòng chiếu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    TaiDanhSachPhongChieu();
                    return;
                }

                string tieuChi = cbbTieuChiTim.SelectedItem?.ToString() ?? "Tất cả";
                string xpath;

                switch (tieuChi)
                {
                    case "Mã phòng":
                        xpath = $"//PhongChieu[{XmlHelper.BieuThucChuaXPath("maPhong", tuKhoa)}]";
                        break;
                    case "Tên phòng chiếu":
                        xpath = $"//PhongChieu[{XmlHelper.BieuThucChuaXPath("tenPhong", tuKhoa)}]";
                        break;
                    default:
                        xpath = $"//PhongChieu[{XmlHelper.BieuThucChuaXPath("maPhong", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("tenPhong", tuKhoa)}]";
                        break;
                }

                DataTable dtKetQua = XmlHelper.TimKiemXPath(TEN_FILE_XML, xpath);
                GanDataTableLenLuoi(dtKetQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phòng chiếu bằng XPath:\n" + ex.Message, "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TaiDanhSachPhongChieu();
        }
    }
}

