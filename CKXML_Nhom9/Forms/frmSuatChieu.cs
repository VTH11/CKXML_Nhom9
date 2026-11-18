using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmSuatChieu : Form
    {
        private const string TEN_FILE_XML = "SuatChieu.xml";
        private const string TEN_FILE_XSD = "SuatChieu.xsd";
        private const string TEN_THE = "SuatChieu";
        private const int PHUT_DON_RAP = 15;

        private readonly bool _cheDoChiXem;

        public frmSuatChieu(bool cheDoChiXem = false)
        {
            _cheDoChiXem = cheDoChiXem;
            InitializeComponent();
        }

        private void frmSuatChieu_Load(object? sender, EventArgs e)
        {
            if (!_cheDoChiXem && !Session.LaQuanLy)
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng Quản lý suất chiếu (chỉ dành cho Quản lý)!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            dtpLocNgay.Value = new DateTime(2026, 11, 12);
            NapDanhSachPhimVaPhong();
            TaiDanhSachSuatChieu();
            LamMoiO();

            if (_cheDoChiXem || !Session.LaQuanLy)
            {
                lblTieuDe.Text = "XEM LỊCH CHIẾU PHIM";
                this.Text = "Xem lịch chiếu phim";
                btnThem.Visible = false;
                btnSua.Visible = false;
                btnXoa.Visible = false;
                btnKiemThuThuatToan.Visible = false;
            }
        }

        /// <summary>
        /// Nạp danh sách Phim và Phòng chiếu từ XML vào các ComboBox.
        /// </summary>
        private void NapDanhSachPhimVaPhong()
        {
            // 1. ComboBox chọn phim
            DataTable dtPhim = XmlHelper.LoadDataTable("Phim.xml");
            if (!dtPhim.Columns.Contains("hienThi"))
                dtPhim.Columns.Add("hienThi", typeof(string));

            foreach (DataRow r in dtPhim.Rows)
            {
                r["hienThi"] = $"{r["maPhim"]} - {r["tenPhim"]} ({r["thoiLuong"]} phút)";
            }

            cbbPhim.DataSource = dtPhim;
            cbbPhim.DisplayMember = "hienThi";
            cbbPhim.ValueMember = "maPhim";

            // 2. ComboBox lọc theo phim
            DataTable dtLocPhim = dtPhim.Copy();
            DataRow allRow = dtLocPhim.NewRow();
            allRow["maPhim"] = "";
            allRow["hienThi"] = "-- Tất cả các phim --";
            dtLocPhim.Rows.InsertAt(allRow, 0);

            cbbLocPhim.DataSource = dtLocPhim;
            cbbLocPhim.DisplayMember = "hienThi";
            cbbLocPhim.ValueMember = "maPhim";
            cbbLocPhim.SelectedIndex = 0;

            // 3. ComboBox chọn phòng chiếu
            DataTable dtPhong = XmlHelper.LoadDataTable("PhongChieu.xml");
            if (!dtPhong.Columns.Contains("hienThi"))
                dtPhong.Columns.Add("hienThi", typeof(string));

            foreach (DataRow r in dtPhong.Rows)
            {
                r["hienThi"] = $"{r["maPhong"]} - {r["tenPhong"]}";
            }

            cbbPhong.DataSource = dtPhong;
            cbbPhong.DisplayMember = "hienThi";
            cbbPhong.ValueMember = "maPhong";
        }

        /// <summary>
        /// Lấy thời lượng (phút) của một mã phim từ Phim.xml.
        /// </summary>
        public static int LayThoiLuongPhim(string maPhim, out string tenPhim)
        {
            tenPhim = XmlHelper.LayGiaTriXPath("Phim.xml", $"//Phim[maPhim={XmlHelper.EscapeXPathLiteral(maPhim)}]/tenPhim");
            string tlStr = XmlHelper.LayGiaTriXPath("Phim.xml", $"//Phim[maPhim={XmlHelper.EscapeXPathLiteral(maPhim)}]/thoiLuong");
            return int.TryParse(tlStr, out int tl) ? tl : 0;
        }

        /// <summary>
        /// Cập nhật ngay ô Giờ kết thúc dự kiến (= Giờ BĐ + Thời lượng phim + 15 phút dọn rạp) khi chọn phim hoặc đổi giờ.
        /// </summary>
        private void ThayDoiPhimHoacGio_Changed(object? sender, EventArgs e)
        {
            CapNhatGioKetThucDuKien();
        }

        private void CapNhatGioKetThucDuKien()
        {
            string maPhim = cbbPhim.SelectedValue?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(maPhim))
            {
                txtGioKetThucDuKien.Text = "--:--:--";
                return;
            }

            int thoiLuong = LayThoiLuongPhim(maPhim, out _);
            TimeSpan gioBD = new TimeSpan(dtpGioBatDau.Value.Hour, dtpGioBatDau.Value.Minute, 0);
            TimeSpan tongThoiGian = TimeSpan.FromMinutes(thoiLuong + PHUT_DON_RAP);
            TimeSpan gioKT = gioBD.Add(tongThoiGian);

            if (gioKT > TimeSpan.FromHours(24))
            {
                TimeSpan gioQuaNgay = gioKT.Subtract(TimeSpan.FromHours(24));
                txtGioKetThucDuKien.ForeColor = Color.FromArgb(220, 38, 38);
                txtGioKetThucDuKien.Text = $"{gioQuaNgay:hh\\:mm\\:ss} (Vắt qua 24:00!)";
            }
            else
            {
                txtGioKetThucDuKien.ForeColor = Color.FromArgb(14, 116, 144);
                string hienThiKT = gioKT == TimeSpan.FromHours(24) ? "24:00:00" : gioKT.ToString(@"hh\:mm\:ss");
                txtGioKetThucDuKien.Text = $"{hienThiKT} ({thoiLuong}p + {PHUT_DON_RAP}p)";
            }
        }

        /// <summary>
        /// THUẬT TOÁN 1: Kiểm tra xung đột lịch chiếu cùng phòng, cùng ngày.
        /// Công thức:
        /// - ketThuc = batDau + thoiLuong + 15 phút dọn rạp
        /// - Hai suất chiếu xung đột khi: (batDauMoi < ketThucCu) && (ketThucMoi > batDauCu)
        /// - Khi sửa suất chiếu, truyền maSuatBoQua để loại chính nó khỏi phép so sánh.
        /// Trả về true nếu BỊ TRÙNG (xung đột), false nếu HỢP LỆ.
        /// </summary>
        public static bool KiemTraXungDot(
            string maPhong,
            string ngay,
            TimeSpan gioBD,
            int thoiLuong,
            string? maSuatBoQua,
            out string thongBaoXungDot,
            XmlDocument? docSuatGiaLap = null)
        {
            thongBaoXungDot = string.Empty;

            TimeSpan batDauMoi = new TimeSpan(gioBD.Hours, gioBD.Minutes, gioBD.Seconds);
            TimeSpan ketThucMoi = batDauMoi.Add(TimeSpan.FromMinutes(thoiLuong + PHUT_DON_RAP));

            // 1. Chặn suất chiếu vắt qua nửa đêm (kết thúc sau 24:00)
            if (ketThucMoi > TimeSpan.FromHours(24))
            {
                TimeSpan gioLe = ketThucMoi.Subtract(TimeSpan.FromHours(24));
                thongBaoXungDot =
                    $"Suất chiếu bắt đầu lúc {batDauMoi:hh\\:mm} (thời lượng {thoiLuong} phút + {PHUT_DON_RAP} phút dọn rạp = {thoiLuong + PHUT_DON_RAP} phút) " +
                    $"sẽ kết thúc lúc {gioLe:hh\\:mm} ngày hôm sau (vượt quá 24:00).\n" +
                    "Hệ thống không cho phép xếp suất chiếu vắt qua nửa đêm!";
                return true;
            }

            // 2. Truy vấn XPath lấy tất cả suất chiếu cùng phòng và cùng ngày (dùng dữ liệu giả lập nếu có, hoặc đọc từ SuatChieu.xml)
            XmlDocument docSuat = docSuatGiaLap ?? XmlHelper.LoadXmlDocument(TEN_FILE_XML);
            string xpath = $"//SuatChieu[maPhong={XmlHelper.EscapeXPathLiteral(maPhong)} and ngayChieu={XmlHelper.EscapeXPathLiteral(ngay)}]";
            XmlNodeList? dsSuatCungPhongNgay = docSuat.SelectNodes(xpath);

            if (dsSuatCungPhongNgay == null || dsSuatCungPhongNgay.Count == 0)
                return false;

            foreach (XmlNode node in dsSuatCungPhongNgay)
            {
                string maSuatCu = node.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;

                // Loại chính suất đang sửa khỏi phép so sánh
                if (!string.IsNullOrEmpty(maSuatBoQua) &&
                    string.Equals(maSuatCu, maSuatBoQua, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                string maPhimCu = node.SelectSingleNode("maPhim")?.InnerText.Trim() ?? string.Empty;
                string gioBDCuStr = node.SelectSingleNode("gioBatDau")?.InnerText.Trim() ?? "00:00:00";

                if (!TimeSpan.TryParse(gioBDCuStr, out TimeSpan batDauCu))
                    continue;

                int thoiLuongCu = LayThoiLuongPhim(maPhimCu, out string tenPhimCu);
                if (thoiLuongCu <= 0 && string.Equals(maPhimCu, "P001", StringComparison.OrdinalIgnoreCase))
                {
                    thoiLuongCu = 131;
                    tenPhimCu = "Mai";
                }
                TimeSpan ketThucCu = batDauCu.Add(TimeSpan.FromMinutes(thoiLuongCu + PHUT_DON_RAP));

                // Điều kiện giao nhau giữa 2 khoảng thời gian [batDauMoi, ketThucMoi) và [batDauCu, ketThucCu)
                if (batDauMoi < ketThucCu && ketThucMoi > batDauCu)
                {
                    string ktMoiStr = ketThucMoi == TimeSpan.FromHours(24) ? "24:00" : ketThucMoi.ToString(@"hh\:mm");
                    string ktCuStr = ketThucCu == TimeSpan.FromHours(24) ? "24:00" : ketThucCu.ToString(@"hh\:mm");

                    thongBaoXungDot =
                        $"PHÁT HIỆN XUNG ĐỘT LỊCH CHIẾU TẠI PHÒNG '{maPhong}' NGÀY {ngay}!\n\n" +
                        $"- Suất chiếu bị trùng : {maSuatCu}\n" +
                        $"- Phim đang chiếu     : {tenPhimCu} ({maPhimCu} - {thoiLuongCu} phút)\n" +
                        $"- Khung giờ suất cũ   : {batDauCu:hh\\:mm} -> {ktCuStr} (đã tính {PHUT_DON_RAP} phút dọn rạp)\n" +
                        $"- Khung giờ bạn chọn  : {batDauMoi:hh\\:mm} -> {ktMoiStr} ({thoiLuong} phút + {PHUT_DON_RAP} phút dọn rạp)";
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Đếm số lượng vé đã bán của một suất chiếu trong Ve.xml bằng XPath.
        /// </summary>
        private int DemSoVeDaBan(string maSuat)
        {
            XmlDocument docVe = XmlHelper.LoadXmlDocument("Ve.xml");
            XmlNodeList? nodes = docVe.SelectNodes($"//Ve[maSuat={XmlHelper.EscapeXPathLiteral(maSuat)}]");
            return nodes?.Count ?? 0;
        }

        private void TaiDanhSachSuatChieu()
        {
            try
            {
                DataTable dt = XmlHelper.LoadDataTable(TEN_FILE_XML);
                BoSungThongTinMoRong(dt);
                GanDataTableLenLuoi(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách suất chiếu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Bổ sung cột Tên phim, Tên phòng, Giờ kết thúc (+15p) và Số vé đã bán để hiển thị trực quan trên lưới.
        /// </summary>
        private void BoSungThongTinMoRong(DataTable dt)
        {
            if (!dt.Columns.Contains("tenPhim"))
                dt.Columns.Add("tenPhim", typeof(string));
            if (!dt.Columns.Contains("tenPhong"))
                dt.Columns.Add("tenPhong", typeof(string));
            if (!dt.Columns.Contains("gioKetThuc"))
                dt.Columns.Add("gioKetThuc", typeof(string));
            if (!dt.Columns.Contains("soVeDaBan"))
                dt.Columns.Add("soVeDaBan", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                string maSuat = row["maSuat"]?.ToString() ?? string.Empty;
                string maPhim = row["maPhim"]?.ToString() ?? string.Empty;
                string maPhong = row["maPhong"]?.ToString() ?? string.Empty;
                string gioBDStr = row["gioBatDau"]?.ToString() ?? "00:00:00";

                int thoiLuong = LayThoiLuongPhim(maPhim, out string tenPhim);
                string tenPhong = XmlHelper.LayGiaTriXPath("PhongChieu.xml", $"//PhongChieu[maPhong={XmlHelper.EscapeXPathLiteral(maPhong)}]/tenPhong");

                row["tenPhim"] = tenPhim;
                row["tenPhong"] = tenPhong;

                if (TimeSpan.TryParse(gioBDStr, out TimeSpan bd))
                {
                    TimeSpan kt = bd.Add(TimeSpan.FromMinutes(thoiLuong + PHUT_DON_RAP));
                    row["gioKetThuc"] = kt == TimeSpan.FromHours(24) ? "24:00:00" : kt.ToString(@"hh\:mm\:ss");
                }

                row["soVeDaBan"] = DemSoVeDaBan(maSuat).ToString();
            }
        }

        private void GanDataTableLenLuoi(DataTable dt)
        {
            dgvSuatChieu.DataSource = dt;

            if (dgvSuatChieu.Columns.Contains("maSuat"))
            {
                dgvSuatChieu.Columns["maSuat"]!.HeaderText = "Mã suất";
                dgvSuatChieu.Columns["maSuat"]!.FillWeight = 65;
            }
            if (dgvSuatChieu.Columns.Contains("maPhim"))
            {
                dgvSuatChieu.Columns["maPhim"]!.HeaderText = "Mã phim";
                dgvSuatChieu.Columns["maPhim"]!.FillWeight = 60;
            }
            if (dgvSuatChieu.Columns.Contains("tenPhim"))
            {
                dgvSuatChieu.Columns["tenPhim"]!.HeaderText = "Tên bộ phim";
                dgvSuatChieu.Columns["tenPhim"]!.FillWeight = 145;
            }
            if (dgvSuatChieu.Columns.Contains("maPhong"))
            {
                dgvSuatChieu.Columns["maPhong"]!.HeaderText = "Mã phòng";
                dgvSuatChieu.Columns["maPhong"]!.FillWeight = 65;
            }
            if (dgvSuatChieu.Columns.Contains("tenPhong"))
            {
                dgvSuatChieu.Columns["tenPhong"]!.HeaderText = "Tên phòng";
                dgvSuatChieu.Columns["tenPhong"]!.FillWeight = 115;
            }
            if (dgvSuatChieu.Columns.Contains("ngayChieu"))
            {
                dgvSuatChieu.Columns["ngayChieu"]!.HeaderText = "Ngày chiếu";
                dgvSuatChieu.Columns["ngayChieu"]!.FillWeight = 80;
            }
            if (dgvSuatChieu.Columns.Contains("gioBatDau"))
            {
                dgvSuatChieu.Columns["gioBatDau"]!.HeaderText = "Giờ BĐ";
                dgvSuatChieu.Columns["gioBatDau"]!.FillWeight = 70;
            }
            if (dgvSuatChieu.Columns.Contains("gioKetThuc"))
            {
                dgvSuatChieu.Columns["gioKetThuc"]!.HeaderText = "Giờ KT (+15p)";
                dgvSuatChieu.Columns["gioKetThuc"]!.FillWeight = 80;
            }
            if (dgvSuatChieu.Columns.Contains("giaVe"))
            {
                dgvSuatChieu.Columns["giaVe"]!.HeaderText = "Giá vé (đ)";
                dgvSuatChieu.Columns["giaVe"]!.FillWeight = 80;
            }
            if (dgvSuatChieu.Columns.Contains("soVeDaBan"))
            {
                dgvSuatChieu.Columns["soVeDaBan"]!.HeaderText = "Vé đã bán";
                dgvSuatChieu.Columns["soVeDaBan"]!.FillWeight = 65;
            }
        }

        private void LamMoiO()
        {
            try
            {
                txtMaSuat.Text = XmlHelper.SinhMaTuDong("SC", TEN_FILE_XML, "maSuat");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sinh mã suất chiếu tự động:\n" + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSuat.Text = "SC001";
            }

            if (cbbPhim.Items.Count > 0)
                cbbPhim.SelectedIndex = 0;
            if (cbbPhong.Items.Count > 0)
                cbbPhong.SelectedIndex = 0;

            dtpNgayChieu.Value = DateTime.Today;
            dtpGioBatDau.Value = DateTime.Today.AddHours(18);
            txtGiaVe.Text = "90000";
            CapNhatGioKetThucDuKien();
            dgvSuatChieu.ClearSelection();
        }

        private void dgvSuatChieu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSuatChieu.Rows.Count)
                return;

            DataGridViewRow row = dgvSuatChieu.Rows[e.RowIndex];
            txtMaSuat.Text = row.Cells["maSuat"]?.Value?.ToString() ?? string.Empty;

            string maPhim = row.Cells["maPhim"]?.Value?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(maPhim))
                cbbPhim.SelectedValue = maPhim;

            string maPhong = row.Cells["maPhong"]?.Value?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(maPhong))
                cbbPhong.SelectedValue = maPhong;

            string ngayStr = row.Cells["ngayChieu"]?.Value?.ToString() ?? string.Empty;
            if (DateTime.TryParseExact(ngayStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime nc) ||
                DateTime.TryParse(ngayStr, out nc))
            {
                dtpNgayChieu.Value = nc;
            }

            string gioStr = row.Cells["gioBatDau"]?.Value?.ToString() ?? "09:00:00";
            if (TimeSpan.TryParse(gioStr, out TimeSpan ts))
            {
                dtpGioBatDau.Value = DateTime.Today.Add(ts);
            }

            txtGiaVe.Text = row.Cells["giaVe"]?.Value?.ToString() ?? "0";
            CapNhatGioKetThucDuKien();
        }

        private bool KiemTraHopLe(out string maPhim, out string maPhong, out string ngayXml, out TimeSpan gioBD, out decimal giaVe, out int thoiLuong)
        {
            maPhim = cbbPhim.SelectedValue?.ToString() ?? string.Empty;
            maPhong = cbbPhong.SelectedValue?.ToString() ?? string.Empty;
            ngayXml = dtpNgayChieu.Value.ToString("yyyy-MM-dd");
            gioBD = new TimeSpan(dtpGioBatDau.Value.Hour, dtpGioBatDau.Value.Minute, 0);
            giaVe = 0;
            thoiLuong = 0;

            if (string.IsNullOrEmpty(maPhim))
            {
                MessageBox.Show("Vui lòng chọn bộ phim!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbPhim.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng chọn phòng chiếu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbPhong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGiaVe.Text) ||
                !decimal.TryParse(txtGiaVe.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out giaVe) ||
                giaVe < 0)
            {
                MessageBox.Show("Giá vé phải là số không âm (>= 0)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaVe.SelectAll();
                txtGiaVe.Focus();
                return false;
            }

            thoiLuong = LayThoiLuongPhim(maPhim, out _);
            return true;
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                string maSuat = txtMaSuat.Text.Trim();
                if (string.IsNullOrEmpty(maSuat))
                {
                    maSuat = XmlHelper.SinhMaTuDong("SC", TEN_FILE_XML, "maSuat");
                    txtMaSuat.Text = maSuat;
                }

                if (XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maSuat", maSuat))
                {
                    string maMoi = XmlHelper.SinhMaTuDong("SC", TEN_FILE_XML, "maSuat");
                    DialogResult dr = MessageBox.Show(
                        $"Mã suất chiếu '{maSuat}' đã tồn tại!\nBạn có muốn tự động chuyển sang mã mới '{maMoi}' để thêm suất chiếu mới không?",
                        "Trùng mã suất chiếu",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        maSuat = maMoi;
                        txtMaSuat.Text = maSuat;
                    }
                    else
                    {
                        return;
                    }
                }

                if (!KiemTraHopLe(out string maPhim, out string maPhong, out string ngayXml, out TimeSpan gioBD, out decimal giaVe, out int thoiLuong))
                    return;

                // Gọi Thuật toán 1 kiểm tra xung đột lịch chiếu (khi thêm mới: maSuatBoQua = null)
                if (KiemTraXungDot(maPhong, ngayXml, gioBD, thoiLuong, null, out string thongBaoXungDot))
                {
                    MessageBox.Show(thongBaoXungDot, "Cảnh báo xung đột lịch chiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, string> duLieu = new Dictionary<string, string>
                {
                    { "maSuat", maSuat },
                    { "maPhim", maPhim },
                    { "maPhong", maPhong },
                    { "ngayChieu", ngayXml },
                    { "gioBatDau", gioBD.ToString(@"hh\:mm\:ss") },
                    { "giaVe", giaVe.ToString(CultureInfo.InvariantCulture) }
                };

                XmlHelper.Them(TEN_FILE_XML, TEN_THE, duLieu);

                List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                if (loiXsd.Count > 0)
                {
                    MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show($"Đã thêm suất chiếu '{maSuat}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachSuatChieu();
                LamMoiO();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm suất chiếu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                string maSuat = txtMaSuat.Text.Trim();
                if (string.IsNullOrEmpty(maSuat) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maSuat", maSuat))
                {
                    MessageBox.Show("Vui lòng chọn một suất chiếu có sẵn trên bảng danh sách để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Không cho sửa suất chiếu đã có vé bán ra
                int soVeDaBan = DemSoVeDaBan(maSuat);
                if (soVeDaBan > 0)
                {
                    MessageBox.Show(
                        $"Không thể sửa suất chiếu '{maSuat}' vì suất chiếu này đã có {soVeDaBan} vé được bán ra trong Ve.xml!",
                        "Từ chối sửa suất chiếu đã có vé",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (!KiemTraHopLe(out string maPhim, out string maPhong, out string ngayXml, out TimeSpan gioBD, out decimal giaVe, out int thoiLuong))
                    return;

                // Gọi Thuật toán 1 kiểm tra xung đột (loại bỏ chính maSuat đang sửa)
                if (KiemTraXungDot(maPhong, ngayXml, gioBD, thoiLuong, maSuat, out string thongBaoXungDot))
                {
                    MessageBox.Show(thongBaoXungDot, "Cảnh báo xung đột lịch chiếu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dictionary<string, string> duLieuMoi = new Dictionary<string, string>
                {
                    { "maPhim", maPhim },
                    { "maPhong", maPhong },
                    { "ngayChieu", ngayXml },
                    { "gioBatDau", gioBD.ToString(@"hh\:mm\:ss") },
                    { "giaVe", giaVe.ToString(CultureInfo.InvariantCulture) }
                };

                bool ketQua = XmlHelper.Sua(TEN_FILE_XML, TEN_THE, "maSuat", maSuat, duLieuMoi);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã cập nhật suất chiếu '{maSuat}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachSuatChieu();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật suất chiếu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                string maSuat = txtMaSuat.Text.Trim();
                if (string.IsNullOrEmpty(maSuat) || !XmlHelper.KiemTraTrung(TEN_FILE_XML, TEN_THE, "maSuat", maSuat))
                {
                    MessageBox.Show("Vui lòng chọn một suất chiếu trên bảng danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Không cho xóa suất chiếu đã có vé bán ra
                int soVeDaBan = DemSoVeDaBan(maSuat);
                if (soVeDaBan > 0)
                {
                    MessageBox.Show(
                        $"Không thể xóa suất chiếu '{maSuat}' vì suất chiếu này đã có {soVeDaBan} vé được bán ra trong Ve.xml!",
                        "Từ chối xóa suất chiếu đã có vé",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa suất chiếu '{maSuat}' không?",
                    "Xác nhận xóa suất chiếu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr != DialogResult.Yes)
                    return;

                bool ketQua = XmlHelper.Xoa(TEN_FILE_XML, TEN_THE, "maSuat", maSuat);
                if (ketQua)
                {
                    List<string> loiXsd = XmlHelper.ValidateXSD(TEN_FILE_XML, TEN_FILE_XSD);
                    if (loiXsd.Count > 0)
                    {
                        MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiXsd), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    MessageBox.Show($"Đã xóa suất chiếu '{maSuat}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachSuatChieu();
                    LamMoiO();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa suất chiếu:\n" + ex.Message, "Lỗi ghi file XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void chkLocNgay_CheckedChanged(object? sender, EventArgs e)
        {
            dtpLocNgay.Enabled = chkLocNgay.Checked;
        }

        /// <summary>
        /// Lọc danh sách suất chiếu theo Phim và/hoặc theo Ngày bằng biểu thức XPath.
        /// </summary>
        private void btnLoc_Click(object? sender, EventArgs e)
        {
            try
            {
                string maPhimLoc = cbbLocPhim.SelectedValue?.ToString() ?? string.Empty;
                bool locNgay = chkLocNgay.Checked;
                string ngayLoc = dtpLocNgay.Value.ToString("yyyy-MM-dd");

                List<string> dieuKien = new List<string>();
                if (!string.IsNullOrEmpty(maPhimLoc))
                {
                    dieuKien.Add($"maPhim={XmlHelper.EscapeXPathLiteral(maPhimLoc)}");
                }
                if (locNgay)
                {
                    dieuKien.Add($"ngayChieu={XmlHelper.EscapeXPathLiteral(ngayLoc)}");
                }

                if (dieuKien.Count == 0)
                {
                    TaiDanhSachSuatChieu();
                    return;
                }

                string xpath = "//SuatChieu[" + string.Join(" and ", dieuKien) + "]";
                DataTable dtKetQua = XmlHelper.TimKiemXPath(TEN_FILE_XML, xpath);
                BoSungThongTinMoRong(dtKetQua);
                GanDataTableLenLuoi(dtKetQua);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc suất chiếu bằng XPath:\n" + ex.Message, "Lỗi lọc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHienTatCa_Click(object? sender, EventArgs e)
        {
            cbbLocPhim.SelectedIndex = 0;
            chkLocNgay.Checked = false;
            TaiDanhSachSuatChieu();
        }

        /// <summary>
        /// Xuất bảng lịch chiếu ra HTML bằng DanhSachSuatChieu.xslt và mở trên trình duyệt.
        /// Nếu tích chọn "Lọc theo ngày" thì xuất theo ngày đang chọn, ngược lại xuất tất cả các ngày.
        /// </summary>
        private void btnXuatHtml_Click(object? sender, EventArgs e)
        {
            try
            {
                string? ngayXml = chkLocNgay.Checked ? dtpLocNgay.Value.ToString("yyyy-MM-dd") : "TATCA";
                BaoCaoHelper.XuatVaMoDanhSachSuatChieu(ngayXml);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất lịch chiếu HTML bằng XSLT:\n" + ex.Message, "Lỗi XSLT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Chạy tự động 6 ca kiểm thử chuẩn của Thuật toán 1 (KiemTraXungDot) bằng dữ liệu giả lập trong bộ nhớ (không ghi vào dữ liệu thật).
        /// </summary>
        private void btnKiemThuThuatToan_Click(object? sender, EventArgs e)
        {
            if (!Session.LaQuanLy)
                return;

            // Tạo dữ liệu giả lập trong bộ nhớ (KHÔNG đụng vào file SuatChieu.xml thật)
            string ngayMau = "2026-11-12";
            XmlDocument docGiaLap = new XmlDocument();
            docGiaLap.LoadXml(
                "<DanhSachSuatChieu>" +
                "  <SuatChieu>" +
                "    <maSuat>SC001</maSuat>" +
                "    <maPhim>P001</maPhim>" +
                "    <maPhong>PC01</maPhong>" +
                $"    <ngayChieu>{ngayMau}</ngayChieu>" +
                "    <gioBatDau>09:00:00</gioBatDau>" +
                "    <giaVe>90000</giaVe>" +
                "  </SuatChieu>" +
                "</DanhSachSuatChieu>");

            DataTable dtTest = new DataTable();
            dtTest.Columns.Add("TenCa", typeof(string));
            dtTest.Columns.Add("DuLieuNhap", typeof(string));
            dtTest.Columns.Add("KetQuaMongDoi", typeof(string));
            dtTest.Columns.Add("KetQuaThucTe", typeof(string));
            dtTest.Columns.Add("DatKhongDat", typeof(string));
            dtTest.Columns.Add("ChiTietThuatToan", typeof(string));

            var danhSachCa = new[]
            {
                new
                {
                    Ten = "Ca 1: Trùng hoàn toàn",
                    Phong = "PC01",
                    Ngay = ngayMau,
                    GioBD = new TimeSpan(9, 0, 0),
                    ThoiLuong = 131,
                    KyVongXungDot = true,
                    KyVongText = "TỪ CHỐI (Trùng lịch)"
                },
                new
                {
                    Ten = "Ca 2: Chồng phần đầu",
                    Phong = "PC01",
                    Ngay = ngayMau,
                    GioBD = new TimeSpan(8, 0, 0),
                    ThoiLuong = 94, // 08:00 + 94p + 15p = 09:49 > 09:00
                    KyVongXungDot = true,
                    KyVongText = "TỪ CHỐI (Trùng lịch)"
                },
                new
                {
                    Ten = "Ca 3: Chồng phần cuối",
                    Phong = "PC01",
                    Ngay = ngayMau,
                    GioBD = new TimeSpan(11, 15, 0),
                    ThoiLuong = 60, // 11:15 < 11:26 (SC001 kết thúc dọn rạp lúc 11:26)
                    KyVongXungDot = true,
                    KyVongText = "TỪ CHỐI (Trùng lịch)"
                },
                new
                {
                    Ten = "Ca 4: Nằm gọn bên trong",
                    Phong = "PC01",
                    Ngay = ngayMau,
                    GioBD = new TimeSpan(9, 30, 0),
                    ThoiLuong = 90, // 09:30 -> 11:15 nằm trọn trong [09:00, 11:26]
                    KyVongXungDot = true,
                    KyVongText = "TỪ CHỐI (Trùng lịch)"
                },
                new
                {
                    Ten = "Ca 5: Sát nhau đúng 15 phút dọn rạp",
                    Phong = "PC01",
                    Ngay = ngayMau,
                    GioBD = new TimeSpan(11, 26, 0),
                    ThoiLuong = 75, // 11:26 + 75p + 15p = 12:56 (tiếp giáp đúng lúc SC001 xong 11:26)
                    KyVongXungDot = false,
                    KyVongText = "CHO PHÉP (Hợp lệ)"
                },
                new
                {
                    Ten = "Ca 6: Khác phòng cùng giờ",
                    Phong = "PC03",
                    Ngay = ngayMau,
                    GioBD = new TimeSpan(9, 0, 0),
                    ThoiLuong = 131, // Cùng giờ 09:00 -> 11:26 nhưng ở phòng PC03
                    KyVongXungDot = false,
                    KyVongText = "CHO PHÉP (Hợp lệ)"
                }
            };

            foreach (var ca in danhSachCa)
            {
                TimeSpan gioKT = ca.GioBD.Add(TimeSpan.FromMinutes(ca.ThoiLuong + PHUT_DON_RAP));
                bool biTrung = KiemTraXungDot(ca.Phong, ca.Ngay, ca.GioBD, ca.ThoiLuong, null, out string thongBao, docGiaLap);

                bool datYeuCau = (biTrung == ca.KyVongXungDot);
                string thucTeStr = biTrung ? "TỪ CHỐI (Trùng lịch)" : "CHO PHÉP (Hợp lệ)";
                string datStr = datYeuCau ? "Đạt" : "Không đạt";
                string duLieuNhap = $"Phòng {ca.Phong}, Ngày {ca.Ngay}, BĐ {ca.GioBD:hh\\:mm}, TL {ca.ThoiLuong}p (+15p -> KT {gioKT:hh\\:mm})";
                string chiTiet = biTrung
                    ? thongBao.Replace("\n", " | ")
                    : $"Không giao với SC001 (09:00 -> 11:26 tại PC01).";

                dtTest.Rows.Add(
                    ca.Ten,
                    duLieuNhap,
                    ca.KyVongText,
                    thucTeStr,
                    datStr,
                    chiTiet);
            }

            HienThiHopThoaiKetQuaKiemThu(dtTest);
        }

        /// <summary>
        /// Hiển thị cửa sổ kết quả 6 ca kiểm thử Thuật toán 1 để chụp ảnh đưa vào báo cáo.
        /// </summary>
        private void HienThiHopThoaiKetQuaKiemThu(DataTable dtTest)
        {
            using (Form frmKQ = new Form())
            {
                frmKQ.Text = "Kiểm thử Thuật toán 1 - Phát hiện xung đột lịch chiếu (Dữ liệu giả lập trong bộ nhớ)";
                frmKQ.Size = new Size(1080, 520);
                frmKQ.MinimumSize = new Size(920, 450);
                frmKQ.StartPosition = FormStartPosition.CenterParent;
                frmKQ.BackColor = Color.White;

                Label lblHeader = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 68,
                    BackColor = Color.FromArgb(30, 41, 59),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "BẢNG KẾT QUẢ 6 CA KIỂM THỬ HÀM KiemTraXungDot (CHẠY TRÊN BỘ NHỚ - KHÔNG GHI DỮ LIỆU THẬT)\n" +
                           "Suất gốc giả lập: SC001 | Phòng: PC01 | Ngày: 2026-11-12 | Phim: Mai (131 phút + 15 phút dọn rạp) | Khung giờ bận: 09:00 -> 11:26"
                };

                DataGridView dgvKQ = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = dtTest,
                    AllowUserToAddRows = false,
                    ReadOnly = true,
                    RowHeadersVisible = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    BackgroundColor = Color.White,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    Font = new Font("Segoe UI", 9.5F),
                    RowTemplate = { Height = 46 }
                };

                dgvKQ.DataBindingComplete += (s, e) =>
                {
                    if (dgvKQ.Columns.Contains("TenCa"))
                    {
                        dgvKQ.Columns["TenCa"]!.HeaderText = "Tên ca kiểm thử";
                        dgvKQ.Columns["TenCa"]!.FillWeight = 130;
                    }
                    if (dgvKQ.Columns.Contains("DuLieuNhap"))
                    {
                        dgvKQ.Columns["DuLieuNhap"]!.HeaderText = "Dữ liệu nhập";
                        dgvKQ.Columns["DuLieuNhap"]!.FillWeight = 195;
                    }
                    if (dgvKQ.Columns.Contains("KetQuaMongDoi"))
                    {
                        dgvKQ.Columns["KetQuaMongDoi"]!.HeaderText = "Kết quả mong đợi";
                        dgvKQ.Columns["KetQuaMongDoi"]!.FillWeight = 105;
                    }
                    if (dgvKQ.Columns.Contains("KetQuaThucTe"))
                    {
                        dgvKQ.Columns["KetQuaThucTe"]!.HeaderText = "Kết quả thực tế";
                        dgvKQ.Columns["KetQuaThucTe"]!.FillWeight = 105;
                    }
                    if (dgvKQ.Columns.Contains("DatKhongDat"))
                    {
                        dgvKQ.Columns["DatKhongDat"]!.HeaderText = "Đạt / Không đạt";
                        dgvKQ.Columns["DatKhongDat"]!.FillWeight = 80;
                    }
                    if (dgvKQ.Columns.Contains("ChiTietThuatToan"))
                    {
                        dgvKQ.Columns["ChiTietThuatToan"]!.HeaderText = "Ghi chú chi tiết";
                        dgvKQ.Columns["ChiTietThuatToan"]!.FillWeight = 175;
                    }

                    foreach (DataGridViewRow r in dgvKQ.Rows)
                    {
                        string kq = r.Cells["KetQuaThucTe"]?.Value?.ToString() ?? string.Empty;
                        r.Cells["KetQuaThucTe"].Style.ForeColor = kq.StartsWith("CHO PHÉP")
                            ? Color.FromArgb(22, 163, 74)
                            : Color.FromArgb(220, 38, 38);
                        r.Cells["KetQuaThucTe"].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

                        string dat = r.Cells["DatKhongDat"]?.Value?.ToString() ?? string.Empty;
                        r.Cells["DatKhongDat"].Style.ForeColor = dat == "Đạt"
                            ? Color.FromArgb(22, 163, 74)
                            : Color.FromArgb(220, 38, 38);
                        r.Cells["DatKhongDat"].Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    }
                };

                frmKQ.Controls.Add(dgvKQ);
                frmKQ.Controls.Add(lblHeader);
                frmKQ.ShowDialog(this);
            }
        }
    }
}

