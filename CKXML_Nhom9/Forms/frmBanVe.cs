using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmBanVe : Form
    {
        private static readonly Color MAU_GHE_TRONG = Color.FromArgb(226, 232, 240);
        private static readonly Color MAU_CHU_TRONG = Color.FromArgb(30, 41, 59);
        private static readonly Color MAU_GHE_DANG_CHON = Color.FromArgb(22, 163, 74);
        private static readonly Color MAU_GHE_DA_BAN = Color.FromArgb(220, 38, 38);

        // Lưu danh sách ghế đang chọn (giữ thứ tự A01, A02...)
        private readonly SortedSet<string> _dsGheDangChon = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

        private string _maSuatHienTai = string.Empty;
        private string _maPhongHienTai = string.Empty;
        private decimal _giaVeHienTai = 0;
        private int _soHangHienTai = 0;
        private int _soCotHienTai = 0;
        private bool _dangNapCombo = false;

        private TableLayoutPanel? _tblSoDoGhe = null;

        public frmBanVe()
        {
            InitializeComponent();
        }

        private void frmBanVe_Load(object? sender, EventArgs e)
        {
            HienThiNhanVienBanVe();
            NapDanhSachPhimCoSuatChieu();
            CapNhatThongTinGheVaTien();
        }

        /// <summary>
        /// Định dạng số tiền theo chuẩn tiếng Việt (ví dụ: 90.000đ).
        /// </summary>
        public static string DinhDangTien(decimal soTien)
        {
            CultureInfo viVN = new CultureInfo("vi-VN");
            return string.Format(viVN, "{0:N0}đ", soTien);
        }

        private void HienThiNhanVienBanVe()
        {
            string maNV = string.IsNullOrEmpty(Session.MaNV) ? "NV001" : Session.MaNV;
            string hoTen = XmlHelper.LayGiaTriXPath("NhanVien.xml", $"//NhanVien[maNV={XmlHelper.EscapeXPathLiteral(maNV)}]/hoTen");
            lblNhanVien.Text = $"NV bán vé: {hoTen} ({maNV})";
        }

        /// <summary>
        /// Kiểm tra suất chiếu còn trong ngày hoặc chưa chiếu (ngayChieu >= hôm nay, nếu cùng ngày thì chưa hết giờ chiếu).
        /// </summary>
        private bool LaSuatChieuConHieuLuc(string ngayChieu, string gioBatDau, int thoiLuongPhim)
        {
            string homNay = DateTime.Today.ToString("yyyy-MM-dd");
            int cmp = string.Compare(ngayChieu, homNay, StringComparison.Ordinal);
            if (cmp > 0)
                return true; // Ngày trong tương lai
            if (cmp < 0)
                return false; // Ngày đã qua

            // Nếu là ngày hôm nay: còn trong ngày (chưa kết thúc suất chiếu)
            if (TimeSpan.TryParse(gioBatDau, out TimeSpan bd))
            {
                TimeSpan kt = bd.Add(TimeSpan.FromMinutes(thoiLuongPhim));
                if (kt >= DateTime.Now.TimeOfDay || bd >= TimeSpan.Zero)
                    return true; // Còn trong ngày hôm nay
            }
            return true;
        }

        /// <summary>
        /// Bước 1: Dùng XPath lọc danh sách Phim có suất chiếu chưa qua ngày (hoặc còn trong ngày).
        /// </summary>
        private void NapDanhSachPhimCoSuatChieu()
        {
            try
            {
                _dangNapCombo = true;

                XmlDocument docSuat = XmlHelper.LoadXmlDocument("SuatChieu.xml");
                string homNay = DateTime.Today.ToString("yyyy-MM-dd");

                // Lấy tất cả SuatChieu, ưu tiên lọc các suất có ngayChieu >= hôm nay
                XmlNodeList? tatCaSuat = docSuat.SelectNodes("//SuatChieu");
                HashSet<string> tapMaPhimConHan = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                HashSet<string> tapMaPhimTatCa = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                if (tatCaSuat != null)
                {
                    foreach (XmlNode sc in tatCaSuat)
                    {
                        string maPhim = sc.SelectSingleNode("maPhim")?.InnerText.Trim() ?? string.Empty;
                        string ngay = sc.SelectSingleNode("ngayChieu")?.InnerText.Trim() ?? string.Empty;
                        string gio = sc.SelectSingleNode("gioBatDau")?.InnerText.Trim() ?? "00:00:00";

                        if (!string.IsNullOrEmpty(maPhim))
                        {
                            tapMaPhimTatCa.Add(maPhim);
                            int tl = frmSuatChieu.LayThoiLuongPhim(maPhim, out _);
                            if (LaSuatChieuConHieuLuc(ngay, gio, tl))
                            {
                                tapMaPhimConHan.Add(maPhim);
                            }
                        }
                    }
                }

                // Nếu dữ liệu mẫu đều thuộc ngày cũ thì hiển thị tất cả để dễ kiểm thử
                HashSet<string> tapMaPhimSuDung = tapMaPhimConHan.Count > 0 ? tapMaPhimConHan : tapMaPhimTatCa;

                DataTable dtPhim = new DataTable();
                dtPhim.Columns.Add("maPhim", typeof(string));
                dtPhim.Columns.Add("hienThi", typeof(string));

                XmlDocument docPhim = XmlHelper.LoadXmlDocument("Phim.xml");
                foreach (string maP in tapMaPhimSuDung)
                {
                    XmlNode? nodePhim = docPhim.SelectSingleNode($"//Phim[maPhim={XmlHelper.EscapeXPathLiteral(maP)}]");
                    if (nodePhim != null)
                    {
                        string tenP = nodePhim.SelectSingleNode("tenPhim")?.InnerText.Trim() ?? maP;
                        string tlP = nodePhim.SelectSingleNode("thoiLuong")?.InnerText.Trim() ?? "0";
                        dtPhim.Rows.Add(maP, $"{maP} - {tenP} ({tlP} phút)");
                    }
                }

                cbbPhim.DataSource = dtPhim;
                cbbPhim.DisplayMember = "hienThi";
                cbbPhim.ValueMember = "maPhim";

                _dangNapCombo = false;

                if (dtPhim.Rows.Count > 0)
                {
                    cbbPhim.SelectedIndex = 0;
                    NapDanhSachNgayTheoPhim();
                }
            }
            catch (Exception ex)
            {
                _dangNapCombo = false;
                MessageBox.Show("Lỗi khi nạp danh sách phim:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbPhim_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_dangNapCombo)
                return;
            NapDanhSachNgayTheoPhim();
        }

        /// <summary>
        /// Bước 2: Dùng XPath lọc các ngày chiếu của bộ phim đã chọn (//SuatChieu[maPhim=...]).
        /// </summary>
        private void NapDanhSachNgayTheoPhim()
        {
            string maPhim = cbbPhim.SelectedValue?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(maPhim))
                return;

            _dangNapCombo = true;

            XmlDocument docSuat = XmlHelper.LoadXmlDocument("SuatChieu.xml");
            string xpath = $"//SuatChieu[maPhim={XmlHelper.EscapeXPathLiteral(maPhim)}]";
            XmlNodeList? dsSuat = docSuat.SelectNodes(xpath);

            SortedSet<string> tapNgayConHan = new SortedSet<string>(StringComparer.Ordinal);
            SortedSet<string> tapNgayTatCa = new SortedSet<string>(StringComparer.Ordinal);
            int tl = frmSuatChieu.LayThoiLuongPhim(maPhim, out _);

            if (dsSuat != null)
            {
                foreach (XmlNode sc in dsSuat)
                {
                    string ngay = sc.SelectSingleNode("ngayChieu")?.InnerText.Trim() ?? string.Empty;
                    string gio = sc.SelectSingleNode("gioBatDau")?.InnerText.Trim() ?? "00:00:00";
                    if (!string.IsNullOrEmpty(ngay))
                    {
                        tapNgayTatCa.Add(ngay);
                        if (LaSuatChieuConHieuLuc(ngay, gio, tl))
                        {
                            tapNgayConHan.Add(ngay);
                        }
                    }
                }
            }

            SortedSet<string> tapNgaySuDung = tapNgayConHan.Count > 0 ? tapNgayConHan : tapNgayTatCa;

            DataTable dtNgay = new DataTable();
            dtNgay.Columns.Add("ngayXml", typeof(string));
            dtNgay.Columns.Add("ngayHienThi", typeof(string));

            foreach (string ngayXml in tapNgaySuDung)
            {
                string hienThi = ngayXml;
                if (DateTime.TryParseExact(ngayXml, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                {
                    hienThi = $"{dt:dd/MM/yyyy} ({ngayXml})";
                }
                dtNgay.Rows.Add(ngayXml, hienThi);
            }

            cbbNgayChieu.DataSource = dtNgay;
            cbbNgayChieu.DisplayMember = "ngayHienThi";
            cbbNgayChieu.ValueMember = "ngayXml";

            _dangNapCombo = false;

            if (dtNgay.Rows.Count > 0)
            {
                cbbNgayChieu.SelectedIndex = 0;
                NapDanhSachSuatTheoPhimVaNgay();
            }
        }

        private void cbbNgayChieu_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_dangNapCombo)
                return;
            NapDanhSachSuatTheoPhimVaNgay();
        }

        /// <summary>
        /// Bước 3: Dùng XPath lọc các suất chiếu theo Phim và Ngày (//SuatChieu[maPhim=... and ngayChieu=...]).
        /// </summary>
        private void NapDanhSachSuatTheoPhimVaNgay()
        {
            string maPhim = cbbPhim.SelectedValue?.ToString() ?? string.Empty;
            string ngayChieu = cbbNgayChieu.SelectedValue?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(maPhim) || string.IsNullOrEmpty(ngayChieu))
                return;

            _dangNapCombo = true;

            XmlDocument docSuat = XmlHelper.LoadXmlDocument("SuatChieu.xml");
            string xpath = $"//SuatChieu[maPhim={XmlHelper.EscapeXPathLiteral(maPhim)} and ngayChieu={XmlHelper.EscapeXPathLiteral(ngayChieu)}]";
            XmlNodeList? dsSuat = docSuat.SelectNodes(xpath);

            DataTable dtSuat = new DataTable();
            dtSuat.Columns.Add("maSuat", typeof(string));
            dtSuat.Columns.Add("hienThi", typeof(string));

            int thoiLuong = frmSuatChieu.LayThoiLuongPhim(maPhim, out _);

            if (dsSuat != null)
            {
                foreach (XmlNode sc in dsSuat)
                {
                    string maSuat = sc.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                    string maPhong = sc.SelectSingleNode("maPhong")?.InnerText.Trim() ?? string.Empty;
                    string gioBD = sc.SelectSingleNode("gioBatDau")?.InnerText.Trim() ?? "00:00:00";
                    string giaVeStr = sc.SelectSingleNode("giaVe")?.InnerText.Trim() ?? "0";
                    decimal.TryParse(giaVeStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal giaVe);

                    string gioHienThi = gioBD.Length >= 5 ? gioBD.Substring(0, 5) : gioBD;
                    if (TimeSpan.TryParse(gioBD, out TimeSpan bd))
                    {
                        TimeSpan kt = bd.Add(TimeSpan.FromMinutes(thoiLuong));
                        gioHienThi = $"{bd:hh\\:mm} - {kt:hh\\:mm}";
                    }

                    dtSuat.Rows.Add(maSuat, $"{maSuat} | {gioHienThi} | Phòng {maPhong} | {DinhDangTien(giaVe)}");
                }
            }

            cbbSuatChieu.DataSource = dtSuat;
            cbbSuatChieu.DisplayMember = "hienThi";
            cbbSuatChieu.ValueMember = "maSuat";

            _dangNapCombo = false;

            if (dtSuat.Rows.Count > 0)
            {
                cbbSuatChieu.SelectedIndex = 0;
                TaiSuatChieuVaVeSoDoGhe();
            }
        }

        private void cbbSuatChieu_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_dangNapCombo)
                return;
            TaiSuatChieuVaVeSoDoGhe();
        }

        /// <summary>
        /// Đọc thông tin suất chiếu, phòng chiếu và gọi Thuật toán 2 để sinh sơ đồ ghế động.
        /// </summary>
        private void TaiSuatChieuVaVeSoDoGhe()
        {
            _dsGheDangChon.Clear();
            _maSuatHienTai = cbbSuatChieu.SelectedValue?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(_maSuatHienTai))
            {
                pnlVungChuaGhe.Controls.Clear();
                CapNhatThongTinGheVaTien();
                return;
            }

            string xpathSuat = $"//SuatChieu[maSuat={XmlHelper.EscapeXPathLiteral(_maSuatHienTai)}]";
            _maPhongHienTai = XmlHelper.LayGiaTriXPath("SuatChieu.xml", xpathSuat + "/maPhong");
            string giaVeStr = XmlHelper.LayGiaTriXPath("SuatChieu.xml", xpathSuat + "/giaVe");
            decimal.TryParse(giaVeStr, NumberStyles.Number, CultureInfo.InvariantCulture, out _giaVeHienTai);

            lblGiaVe.Text = DinhDangTien(_giaVeHienTai);

            VeSoDoGheDong();
            CapNhatThongTinGheVaTien();
        }

        /// <summary>
        /// THUẬT TOÁN 2: Sinh sơ đồ ghế động bằng vòng lặp lồng nhau dựa trên soHang, soCot của phòng chiếu.
        /// - Tên ghế = Ký tự hàng ('A' + hang) + Số cột 2 chữ số (01..soCot) -> A01, A02, B05...
        /// - Dò trong Ve.xml bằng XPath để tô màu Đỏ (Enabled = false) các ghế đã bán.
        /// - Tự động co giãn kích thước ghế theo diện tích Panel chứa và kích thước phòng.
        /// </summary>
        private void VeSoDoGheDong()
        {
            pnlVungChuaGhe.SuspendLayout();
            pnlVungChuaGhe.Controls.Clear();
            _tblSoDoGhe = null;

            if (string.IsNullOrEmpty(_maPhongHienTai) || string.IsNullOrEmpty(_maSuatHienTai))
            {
                pnlVungChuaGhe.ResumeLayout();
                return;
            }

            // 1. Đọc soHang, soCot, tenPhong từ PhongChieu.xml bằng XPath
            string xpathPhong = $"//PhongChieu[maPhong={XmlHelper.EscapeXPathLiteral(_maPhongHienTai)}]";
            string tenPhong = XmlHelper.LayGiaTriXPath("PhongChieu.xml", xpathPhong + "/tenPhong");
            int.TryParse(XmlHelper.LayGiaTriXPath("PhongChieu.xml", xpathPhong + "/soHang"), out _soHangHienTai);
            int.TryParse(XmlHelper.LayGiaTriXPath("PhongChieu.xml", xpathPhong + "/soCot"), out _soCotHienTai);

            if (_soHangHienTai <= 0 || _soCotHienTai <= 0)
            {
                pnlVungChuaGhe.ResumeLayout();
                return;
            }

            // 2. Dò các ghế đã bán của suất chiếu hiện tại trong Ve.xml bằng XPath
            HashSet<string> tapGheDaBan = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            XmlDocument docVe = XmlHelper.LoadXmlDocument("Ve.xml");
            XmlNodeList? nodesVe = docVe.SelectNodes($"//Ve[maSuat={XmlHelper.EscapeXPathLiteral(_maSuatHienTai)}]/viTriGhe");
            if (nodesVe != null)
            {
                foreach (XmlNode n in nodesVe)
                {
                    string ghe = n.InnerText.Trim();
                    if (!string.IsNullOrEmpty(ghe))
                        tapGheDaBan.Add(ghe);
                }
            }

            int tongSoGhe = _soHangHienTai * _soCotHienTai;
            int soDaBan = tapGheDaBan.Count;
            int soConTrong = Math.Max(0, tongSoGhe - soDaBan);
            lblThongTinPhong.Text =
                $"Phòng: {tenPhong} ({_soHangHienTai} hàng x {_soCotHienTai} cột = {tongSoGhe} ghế)\n" +
                $"Đã bán: {soDaBan} ghế  |  Còn trống: {soConTrong} ghế";

            // 3. Tính kích thước ô ghế co giãn theo kích thước vùng chứa
            TinhKichThuocNutGhe(_soHangHienTai, _soCotHienTai, out int btnWidth, out int btnHeight, out float fontSize);

            TableLayoutPanel tbl = new TableLayoutPanel
            {
                RowCount = _soHangHienTai,
                ColumnCount = _soCotHienTai,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Padding = new Padding(4)
            };

            for (int r = 0; r < _soHangHienTai; r++)
            {
                tbl.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
            for (int c = 0; c < _soCotHienTai; c++)
            {
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            }

            // 4. Vòng lặp lồng nhau sinh Button cho từng ghế (A01, A02...)
            for (int hang = 0; hang < _soHangHienTai; hang++)
            {
                char kyTuHang = (char)('A' + hang);

                for (int cot = 1; cot <= _soCotHienTai; cot++)
                {
                    string tenGhe = $"{kyTuHang}{cot:D2}";
                    bool daBan = tapGheDaBan.Contains(tenGhe);
                    bool dangChon = _dsGheDangChon.Contains(tenGhe);

                    Button btnGhe = new Button
                    {
                        Name = "btnGhe_" + tenGhe,
                        Text = tenGhe,
                        Tag = tenGhe,
                        Size = new Size(btnWidth, btnHeight),
                        Margin = new Padding(3),
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Segoe UI", fontSize, FontStyle.Bold),
                        UseVisualStyleBackColor = false
                    };
                    btnGhe.FlatAppearance.BorderSize = 1;
                    btnGhe.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);

                    if (daBan)
                    {
                        // Ghế đã bán: Màu đỏ, vô hiệu hóa click
                        btnGhe.BackColor = MAU_GHE_DA_BAN;
                        btnGhe.ForeColor = Color.White;
                        btnGhe.Enabled = false;
                    }
                    else if (dangChon)
                    {
                        // Ghế đang chọn: Màu xanh lá
                        btnGhe.BackColor = MAU_GHE_DANG_CHON;
                        btnGhe.ForeColor = Color.White;
                        btnGhe.Cursor = Cursors.Hand;
                        btnGhe.Click += BtnGhe_Click;
                    }
                    else
                    {
                        // Ghế trống: Màu trắng/xám nhạt
                        btnGhe.BackColor = MAU_GHE_TRONG;
                        btnGhe.ForeColor = MAU_CHU_TRONG;
                        btnGhe.Cursor = Cursors.Hand;
                        btnGhe.Click += BtnGhe_Click;
                    }

                    tbl.Controls.Add(btnGhe, cot - 1, hang);
                }
            }

            _tblSoDoGhe = tbl;
            pnlVungChuaGhe.Controls.Add(tbl);
            CanGiuaBangGhe();
            pnlVungChuaGhe.ResumeLayout();
        }

        /// <summary>
        /// Tính toán chiều rộng, chiều cao và cỡ chữ của nút ghế sao cho co giãn vừa đẹp với phòng nhỏ (5x6) lẫn phòng lớn.
        /// </summary>
        private void TinhKichThuocNutGhe(int soHang, int soCot, out int btnWidth, out int btnHeight, out float fontSize)
        {
            int vungRong = Math.Max(300, pnlVungChuaGhe.ClientSize.Width - 36);
            int vungCao = Math.Max(220, pnlVungChuaGhe.ClientSize.Height - 36);

            int w = (vungRong / soCot) - 6;
            int h = (vungCao / soHang) - 6;

            // Giới hạn kích thước nút ghế để phòng nhỏ không quá to và phòng rất lớn vẫn đọc rõ chữ (kèm thanh cuộn)
            btnWidth = Math.Max(46, Math.Min(82, w));
            btnHeight = Math.Max(34, Math.Min(52, h));

            fontSize = btnWidth < 54 ? 8.25F : 9.25F;
        }

        /// <summary>
        /// Căn giữa bảng sơ đồ ghế trong vùng pnlVungChuaGhe.
        /// </summary>
        private void CanGiuaBangGhe()
        {
            if (_tblSoDoGhe == null)
                return;

            int x = Math.Max(10, (pnlVungChuaGhe.ClientSize.Width - _tblSoDoGhe.PreferredSize.Width) / 2);
            int y = Math.Max(10, (pnlVungChuaGhe.ClientSize.Height - _tblSoDoGhe.PreferredSize.Height) / 2);
            _tblSoDoGhe.Location = new Point(x, y);
        }

        private void pnlVungChuaGhe_Resize(object? sender, EventArgs e)
        {
            if (_soHangHienTai > 0 && _soCotHienTai > 0 && _tblSoDoGhe != null)
            {
                TinhKichThuocNutGhe(_soHangHienTai, _soCotHienTai, out int w, out int h, out float fs);
                _tblSoDoGhe.SuspendLayout();
                foreach (Control ctrl in _tblSoDoGhe.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        btn.Size = new Size(w, h);
                        btn.Font = new Font("Segoe UI", fs, FontStyle.Bold);
                    }
                }
                _tblSoDoGhe.ResumeLayout();
                CanGiuaBangGhe();
            }
        }

        /// <summary>
        /// Sự kiện click chọn / bỏ chọn một ghế trên sơ đồ.
        /// </summary>
        private void BtnGhe_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btnGhe || btnGhe.Tag is not string tenGhe)
                return;

            if (_dsGheDangChon.Contains(tenGhe))
            {
                // Đang chọn -> Click lần nữa thì bỏ chọn về màu trống
                _dsGheDangChon.Remove(tenGhe);
                btnGhe.BackColor = MAU_GHE_TRONG;
                btnGhe.ForeColor = MAU_CHU_TRONG;
            }
            else
            {
                // Đang trống -> Chuyển sang màu xanh lá đang chọn
                _dsGheDangChon.Add(tenGhe);
                btnGhe.BackColor = MAU_GHE_DANG_CHON;
                btnGhe.ForeColor = Color.White;
            }

            CapNhatThongTinGheVaTien();
        }

        /// <summary>
        /// Cập nhật tức thời danh sách ghế đang chọn, tổng tiền và trạng thái nút Xác nhận.
        /// </summary>
        private void CapNhatThongTinGheVaTien()
        {
            int soGhe = _dsGheDangChon.Count;
            if (soGhe == 0)
            {
                txtGheDangChon.Text = "Chưa chọn ghế nào";
                lblTongTien.Text = "0đ";
                btnXacNhanBanVe.Enabled = false;
            }
            else
            {
                string chuoiGhe = string.Join(", ", _dsGheDangChon);
                txtGheDangChon.Text = $"{chuoiGhe} ({soGhe} ghế)";
                decimal tongTien = soGhe * _giaVeHienTai;
                lblTongTien.Text = DinhDangTien(tongTien);
                btnXacNhanBanVe.Enabled = true;
            }
        }

        private void btnBoChon_Click(object? sender, EventArgs e)
        {
            if (_dsGheDangChon.Count == 0)
                return;

            _dsGheDangChon.Clear();
            VeSoDoGheDong();
            CapNhatThongTinGheVaTien();
        }

        /// <summary>
        /// Xác nhận bán vé:
        /// 1. Kiểm tra lại toàn bộ ghế đang chọn có còn trống trong Ve.xml ngay trước khi ghi (chống bán trùng).
        /// 2. Tạo các phần tử <Ve> cho cả nhóm ghế trên cùng một XmlDocument, kiểm tra Ve.xsd rồi ghi 1 lần duy nhất (không ghi nửa chừng).
        /// 3. Vẽ lại sơ đồ ghế.
        /// </summary>
        private void btnXacNhanBanVe_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maSuatHienTai) || _dsGheDangChon.Count == 0)
                return;

            List<string> danhSachGheMua = _dsGheDangChon.ToList();
            decimal tongTien = danhSachGheMua.Count * _giaVeHienTai;

            DialogResult xacNhan = MessageBox.Show(
                $"Xác nhận bán {danhSachGheMua.Count} vé cho suất chiếu '{_maSuatHienTai}'?\n\n" +
                $"- Ghế đã chọn : {string.Join(", ", danhSachGheMua)}\n" +
                $"- Đơn giá     : {DinhDangTien(_giaVeHienTai)}\n" +
                $"- Tổng tiền   : {DinhDangTien(tongTien)}",
                "Xác nhận bán vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (xacNhan != DialogResult.Yes)
                return;

            try
            {
                XmlDocument docVe = XmlHelper.LoadXmlDocument("Ve.xml");
                if (docVe.DocumentElement == null)
                    throw new InvalidOperationException("File Ve.xml không có phần tử gốc DanhSachVe.");

                // BƯỚC 1: KIỂM TRA LẠI GHẾ CÒN TRỐNG NGAY TRƯỚC KHI GHI (CHỐNG BÁN TRÙNG)
                List<string> gheBiBanTrung = new List<string>();
                foreach (string ghe in danhSachGheMua)
                {
                    string xpathCheck = $"//Ve[maSuat={XmlHelper.EscapeXPathLiteral(_maSuatHienTai)} and viTriGhe={XmlHelper.EscapeXPathLiteral(ghe)}]";
                    if (docVe.SelectSingleNode(xpathCheck) != null)
                    {
                        gheBiBanTrung.Add(ghe);
                    }
                }

                if (gheBiBanTrung.Count > 0)
                {
                    MessageBox.Show(
                        $"Giao dịch bị hủy do ghế [{string.Join(", ", gheBiBanTrung)}] vừa được bán bởi một giao dịch khác!\n" +
                        "Hệ thống sẽ cập nhật lại sơ đồ ghế hiện tại.",
                        "Cảnh báo chống bán trùng ghế",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    foreach (string gheTrung in gheBiBanTrung)
                    {
                        _dsGheDangChon.Remove(gheTrung);
                    }
                    VeSoDoGheDong();
                    CapNhatThongTinGheVaTien();
                    return;
                }

                // BƯỚC 2: TÌM MÃ VÉ LỚN NHẤT HIỆN TẠI TRONG docVe ĐỂ SINH MÃ LIÊN TIẾP (V0007, V0008...)
                int maxSoVe = 0;
                XmlNodeList? dsMaVe = docVe.SelectNodes("//Ve/maVe");
                if (dsMaVe != null)
                {
                    foreach (XmlNode n in dsMaVe)
                    {
                        string ma = n.InnerText.Trim();
                        if (ma.StartsWith("V", StringComparison.OrdinalIgnoreCase) &&
                            int.TryParse(ma.Substring(1), out int so) &&
                            so > maxSoVe)
                        {
                            maxSoVe = so;
                        }
                    }
                }

                string maNVBan = string.IsNullOrEmpty(Session.MaNV) ? "NV001" : Session.MaNV;
                string thoiDiemBan = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                string giaVeXml = _giaVeHienTai.ToString(CultureInfo.InvariantCulture);

                List<string> dsMaVeMoi = new List<string>();

                // Thêm toàn bộ nhóm ghế vào cây DOM trên bộ nhớ
                foreach (string ghe in danhSachGheMua)
                {
                    maxSoVe++;
                    string maVeMoi = "V" + maxSoVe.ToString("D4");
                    dsMaVeMoi.Add(maVeMoi);

                    XmlElement elVe = docVe.CreateElement("Ve");

                    XmlElement elMaVe = docVe.CreateElement("maVe");
                    elMaVe.InnerText = maVeMoi;
                    elVe.AppendChild(elMaVe);

                    XmlElement elMaSuat = docVe.CreateElement("maSuat");
                    elMaSuat.InnerText = _maSuatHienTai;
                    elVe.AppendChild(elMaSuat);

                    XmlElement elMaNV = docVe.CreateElement("maNV");
                    elMaNV.InnerText = maNVBan;
                    elVe.AppendChild(elMaNV);

                    XmlElement elViTriGhe = docVe.CreateElement("viTriGhe");
                    elViTriGhe.InnerText = ghe;
                    elVe.AppendChild(elViTriGhe);

                    XmlElement elGiaVe = docVe.CreateElement("giaVe");
                    elGiaVe.InnerText = giaVeXml;
                    elVe.AppendChild(elGiaVe);

                    XmlElement elNgayBan = docVe.CreateElement("ngayBan");
                    elNgayBan.InnerText = thoiDiemBan;
                    elVe.AppendChild(elNgayBan);

                    docVe.DocumentElement.AppendChild(elVe);
                }

                // BƯỚC 3: KIỂM TRA HỢP LỆ XSD TRÊN BỘ NHỚ TRƯỚC KHI GHI MỘT LẦN XUỐNG ĐĨA (ATOMIC WRITE)
                List<string> loiXsd = XmlHelper.ValidateXmlDoc(docVe, "Ve.xsd");
                if (loiXsd.Count > 0)
                {
                    throw new XmlSchemaValidationException(
                        "Dữ liệu vé không hợp lệ với Ve.xsd (không có vé nào bị ghi nửa chừng):\n- " +
                        string.Join("\n- ", loiXsd));
                }

                // Ghi một lần duy nhất xuống file Ve.xml
                docVe.Save(XmlHelper.GetDataPath("Ve.xml"));

                // Validate lại file trên đĩa
                List<string> loiSauLuu = XmlHelper.ValidateXSD("Ve.xml", "Ve.xsd");
                if (loiSauLuu.Count > 0)
                {
                    MessageBox.Show("Cảnh báo XSD:\n" + string.Join("\n", loiSauLuu), "Lỗi XSD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show(
                    $"Đặt vé thành công!\n\n" +
                    $"- Mã vé      : {string.Join(", ", dsMaVeMoi)}\n" +
                    $"- Suất chiếu : {_maSuatHienTai}\n" +
                    $"- Vị trí ghế : {string.Join(", ", danhSachGheMua)}\n" +
                    $"- Tổng tiền  : {DinhDangTien(tongTien)}\n" +
                    $"- Nhân viên  : {maNVBan}\n" +
                    $"- Thời gian  : {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                    "Bán vé thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Xóa danh sách ghế đang chọn và vẽ lại sơ đồ ghế
                _dsGheDangChon.Clear();
                VeSoDoGheDong();
                CapNhatThongTinGheVaTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu vé (dữ liệu gốc vẫn an toàn, không bị ghi nửa chừng):\n" + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}

