using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmPhim : Form
    {
        private const string TEN_FILE_XML = "Phim.xml";
        private const string TEN_FILE_XSD = "Phim.xsd";
        private const string TEN_THE = "Phim";

        public frmPhim()
        {
            InitializeComponent();
        }

        private void frmPhim_Load(object? sender, EventArgs e)
        {
            cbbTieuChiTim.SelectedIndex = 0;
            TaiDanhSachPhim();
            LamMoiO();
        }

        /// <summary>
        /// Tải toàn bộ danh sách phim từ Phim.xml lên DataGridView.
        /// </summary>
        private void TaiDanhSachPhim()
        {
            try
            {
                DataTable dt = XmlHelper.LoadDataTable(TEN_FILE_XML);
                GanDataTableLenLuoi(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phim:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gán DataTable vào DataGridView và đặt tiêu đề cột tiếng Việt.
        /// </summary>
        private void GanDataTableLenLuoi(DataTable dt)
        {
            dgvPhim.DataSource = dt;

            if (dgvPhim.Columns.Contains("maPhim"))
            {
                dgvPhim.Columns["maPhim"]!.HeaderText = "Mã phim";
                dgvPhim.Columns["maPhim"]!.FillWeight = 65;
            }
            if (dgvPhim.Columns.Contains("tenPhim"))
            {
                dgvPhim.Columns["tenPhim"]!.HeaderText = "Tên bộ phim";
                dgvPhim.Columns["tenPhim"]!.FillWeight = 160;
            }
            if (dgvPhim.Columns.Contains("theLoai"))
            {
                dgvPhim.Columns["theLoai"]!.HeaderText = "Thể loại";
                dgvPhim.Columns["theLoai"]!.FillWeight = 120;
            }
            if (dgvPhim.Columns.Contains("thoiLuong"))
            {
                dgvPhim.Columns["thoiLuong"]!.HeaderText = "Thời lượng (phút)";
                dgvPhim.Columns["thoiLuong"]!.FillWeight = 85;
            }
            if (dgvPhim.Columns.Contains("daoDien"))
            {
                dgvPhim.Columns["daoDien"]!.HeaderText = "Đạo diễn";
                dgvPhim.Columns["daoDien"]!.FillWeight = 110;
            }
            if (dgvPhim.Columns.Contains("apPhich"))
            {
                dgvPhim.Columns["apPhich"]!.HeaderText = "Đường dẫn áp phích";
                dgvPhim.Columns["apPhich"]!.FillWeight = 110;
            }
        }

        /// <summary>
        /// Xóa trắng các ô nhập và tự động sinh mã phim kế tiếp.
        /// </summary>
        private void LamMoiO()
        {
            try
            {
                txtMaPhim.Text = XmlHelper.SinhMaTuDong("P", TEN_FILE_XML, "maPhim");
            }
            catch
            {
                txtMaPhim.Text = "P001";
            }

            txtTenPhim.Clear();
            txtTheLoai.Clear();
            txtThoiLuong.Clear();
            txtDaoDien.Clear();
            txtApPhich.Clear();
            HienThiAnhApPhich(string.Empty);
            dgvPhim.ClearSelection();
            txtTenPhim.Focus();
        }

        /// <summary>
        /// Tìm đường dẫn tuyệt đối của ảnh áp phích và hiển thị lên PictureBox, xử lý an toàn khi không tìm thấy file ảnh.
        /// </summary>
        private void HienThiAnhApPhich(string duongDanAnh)
        {
            if (picApPhich.Image != null)
            {
                Image oldImg = picApPhich.Image;
                picApPhich.Image = null;
                oldImg.Dispose();
            }

            duongDanAnh = (duongDanAnh ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(duongDanAnh))
            {
                picApPhich.Visible = false;
                lblKhongCoAnh.Visible = true;
                lblKhongCoAnh.ForeColor = Color.FromArgb(100, 116, 139);
                lblKhongCoAnh.Text = "Chưa có ảnh\náp phích";
                return;
            }

            // Kiểm tra đường dẫn tuyệt đối hoặc tương đối trong StartupPath / StartupPath\Data
            string[] cacDuongDanThu =
            {
                duongDanAnh,
                Path.Combine(Application.StartupPath, duongDanAnh),
                Path.Combine(Application.StartupPath, "Data", duongDanAnh)
            };

            string? fileTonTai = null;
            foreach (string path in cacDuongDanThu)
            {
                if (File.Exists(path))
                {
                    fileTonTai = path;
                    break;
                }
            }

            if (fileTonTai == null)
            {
                picApPhich.Visible = false;
                lblKhongCoAnh.Visible = true;
                lblKhongCoAnh.ForeColor = Color.FromArgb(185, 28, 28);
                lblKhongCoAnh.Text = "Không tìm thấy\nfile ảnh:\n" + Path.GetFileName(duongDanAnh);
                return;
            }

            try
            {
                // Đọc qua MemoryStream để không khóa file trên đĩa
                byte[] bytes = File.ReadAllBytes(fileTonTai);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    picApPhich.Image = new Bitmap(ms);
                }
                picApPhich.Visible = true;
                lblKhongCoAnh.Visible = false;
            }
            catch
            {
                picApPhich.Visible = false;
                lblKhongCoAnh.Visible = true;
                lblKhongCoAnh.ForeColor = Color.FromArgb(185, 28, 28);
                lblKhongCoAnh.Text = "File ảnh không\nđúng định dạng";
            }
        }

        private void txtApPhich_Leave(object? sender, EventArgs e)
        {
            HienThiAnhApPhich(txtApPhich.Text);
        }

        /// <summary>
        /// Chọn file ảnh từ máy tính và tự động copy vào thư mục Data/posters.
        /// </summary>
        private void btnChonAnh_Click(object? sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh áp phích phim";
                ofd.Filter = "Tệp hình ảnh (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tất cả các tệp (*.*)|*.*";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string thuMucPosters = Path.Combine(Application.StartupPath, "Data", "posters");
                        Directory.CreateDirectory(thuMucPosters);

                        string tenFile = Path.GetFileName(ofd.FileName);
                        string duongDanDich = Path.Combine(thuMucPosters, tenFile);

                        if (!string.Equals(Path.GetFullPath(ofd.FileName), Path.GetFullPath(duongDanDich), StringComparison.OrdinalIgnoreCase))
                        {
                            File.Copy(ofd.FileName, duongDanDich, true);
                        }

                        txtApPhich.Text = "posters/" + tenFile;
                        HienThiAnhApPhich(txtApPhich.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể sao chép file ảnh:\n" + ex.Message, "Lỗi chọn ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Khi chọn một dòng trên DataGridView, đổ dữ liệu lên các ô nhập và hiển thị ảnh áp phích.
        /// </summary>
        private void dgvPhim_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPhim.Rows.Count)
                return;

            DataGridViewRow row = dgvPhim.Rows[e.RowIndex];
            txtMaPhim.Text = row.Cells["maPhim"]?.Value?.ToString() ?? string.Empty;
            txtTenPhim.Text = row.Cells["tenPhim"]?.Value?.ToString() ?? string.Empty;
            txtTheLoai.Text = row.Cells["theLoai"]?.Value?.ToString() ?? string.Empty;
            txtThoiLuong.Text = row.Cells["thoiLuong"]?.Value?.ToString() ?? string.Empty;
            txtDaoDien.Text = row.Cells["daoDien"]?.Value?.ToString() ?? string.Empty;
            txtApPhich.Text = row.Cells["apPhich"]?.Value?.ToString() ?? string.Empty;

            HienThiAnhApPhich(txtApPhich.Text);
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của các trường nhập liệu phim.
        /// </summary>
        private bool KiemTraHopLe(out int thoiLuong)
        {
            thoiLuong = 0;

            if (string.IsNullOrWhiteSpace(txtTenPhim.Text))
            {
                MessageBox.Show("Vui lòng nhập tên bộ phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhim.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTheLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập thể loại phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTheLoai.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtThoiLuong.Text) || !int.TryParse(txtThoiLuong.Text.Trim(), out thoiLuong) || thoiLuong <= 0)
            {
                MessageBox.Show("Thời lượng phim phải là số nguyên lớn hơn 0 (đơn vị: phút)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThoiLuong.SelectAll();
                txtThoiLuong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDaoDien.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đạo diễn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDaoDien.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Thêm mới một bộ phim vào Phim.xml và kiểm tra lại với Phim.xsd.
        /// </summary>
        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhim = txtMaPhim.Text.Trim();
                if (string.IsNullOrEmpty(maPhim))
                {
                    maPhim = XmlHelper.SinhMaTuDong("P", TEN_FILE_XML, "maPhim");
                    txtMaPhim.Text = maPhim;
                }

                // Kiểm tra trùng mã phim (nếu người dùng đang chọn một dòng cũ trên lưới rồi nhấn Thêm)
                if (XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maPhim", maPhim))
                {
                    string maMoi = XmlHelper.SinhMaTuDong("P", TEN_FILE_XML, "maPhim");
                    DialogResult dr = MessageBox.Show(
                        $"Mã phim '{maPhim}' đã tồn tại trong danh sách!\nBạn có muốn tự động chuyển sang mã mới '{maMoi}' để thêm bộ phim này không?",
                        "Trùng mã phim",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maPhim = maMoi;
                        txtMaPhim.Text = maPhim;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!KiemTraHopLe(out int thoiLuong))
                    return;

                Dictionary<string, string> duLieu = new Dictionary<string, string>
                {
                    { "maPhim", maPhim },
                    { "tenPhim", txtTenPhim.Text.Trim() },
                    { "theLoai", txtTheLoai.Text.Trim() },
                    { "thoiLuong", thoiLuong.ToString() },
                    { "daoDien", txtDaoDien.Text.Trim() },
                    { "apPhich", txtApPhich.Text.Trim() }
                };

                XmlHelper.Them(TEN_FILE_XML, TEN_THE, duLieu);

                // Validate lại file trên đĩa theo XSD
                List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                if (loiXsd.Count > 0)
                {
                    MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show($"Đã thêm thành công bộ phim '{txtTenPhim.Text.Trim()}' (Mã: {maPhim})!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachPhim();
                LamMoiO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phim mới:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thông tin bộ phim đang chọn và kiểm tra lại với Phim.xsd.
        /// </summary>
        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhim = txtMaPhim.Text.Trim();
                if (string.IsNullOrEmpty(maPhim) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maPhim", maPhim))
                {
                    MessageBox.Show("Vui lòng chọn một bộ phim có sẵn trên bảng danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!KiemTraHopLe(out int thoiLuong))
                    return;

                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "tenPhim", txtTenPhim.Text.Trim() },
                    { "theLoai", txtTheLoai.Text.Trim() },
                    { "thoiLuong", thoiLuong.ToString() },
                    { "daoDien", txtDaoDien.Text.Trim() },
                    { "apPhich", txtApPhich.Text.Trim() }
                };

                bool ketQua = XmlHelper.Sua(TEN_FILE_XML, TEN_THE, "maPhim", maPhim, duLieuMoi);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã cập nhật thông tin bộ phim '{maPhim}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachPhim();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật bộ phim:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa bộ phim đang chọn; từ chối xóa nếu phim đang có suất chiếu trong SuatChieu.xml.
        /// </summary>
        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhim = txtMaPhim.Text.Trim();
                if (string.IsNullOrEmpty(maPhim) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maPhim", maPhim))
                {
                    MessageBox.Show("Vui lòng chọn một bộ phim trên bảng danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra ràng buộc khóa ngoại: không cho xóa Phim đang được tham chiếu trong SuatChieu.xml
                string xpathSuatChieu = $"//SuatChieu[maPhim={XmlHelper.EscapeXPathLiteral(maPhim)}]";
                if (XmlHelper.TonTaiXPath("SuatChieu.xml", xpathSuatChieu))
                {
                    MessageBox.Show(
                        $"Không thể xóa bộ phim '{txtTenPhim.Text.Trim()}' (Mã: {maPhim}) vì đang có suất chiếu tham chiếu tới phim này trong SuatChieu.xml!",
                        "Từ chối xóa do ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa bộ phim '{txtTenPhim.Text.Trim()}' (Mã: {maPhim}) không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                    return;

                bool ketQua = XmlHelper.Xoa(TEN_FILE_XML, TEN_THE, "maPhim", maPhim);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã xóa bộ phim '{maPhim}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachPhim();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa bộ phim:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// Tìm kiếm phim bằng biểu thức XPath theo tiêu chí được chọn.
        /// </summary>
        private void btnTimKiem_Click(object? sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    TaiDanhSachPhim();
                    return;
                }

                string tieuChi = cbbTieuChiTim.SelectedItem?.ToString() ?? "Tất cả";
                string xpath;

                switch (tieuChi)
                {
                    case "Mã phim":
                        xpath = $"//Phim[{XmlHelper.BieuThucChuaXPath("maPhim", tuKhoa)}]";
                        break;
                    case "Tên phim":
                        xpath = $"//Phim[{XmlHelper.BieuThucChuaXPath("tenPhim", tuKhoa)}]";
                        break;
                    case "Thể loại":
                        xpath = $"//Phim[{XmlHelper.BieuThucChuaXPath("theLoai", tuKhoa)}]";
                        break;
                    case "Đạo diễn":
                        xpath = $"//Phim[{XmlHelper.BieuThucChuaXPath("daoDien", tuKhoa)}]";
                        break;
                    default:
                        xpath = $"//Phim[{XmlHelper.BieuThucChuaXPath("maPhim", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("tenPhim", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("theLoai", tuKhoa)} or " +
                                $"{XmlHelper.BieuThucChuaXPath("daoDien", tuKhoa)}]";
                        break;
                }

                DataTable dtKetQua = XmlHelper.TimKiemXPath(TEN_FILE_XML, xpath);
                GanDataTableLenLuoi(dtKetQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phim bằng XPath:\n" + ex.Message, "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TaiDanhSachPhim();
        }
    }
}

