using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace CKXML_Nhom9.Helpers
{
    /// <summary>
    /// Lớp hỗ trợ gộp dữ liệu XML trung gian, biến đổi XSLT 1.0 sang HTML (UTF-8) và mở trên trình duyệt.
    /// </summary>
    public static class BaoCaoHelper
    {
        private const int PHUT_DON_RAP = 15;

        /// <summary>
        /// Lấy thư mục tạm để lưu file XML trung gian và file HTML xuất ra (tự tạo nếu chưa có).
        /// </summary>
        public static string LayThuMucTam()
        {
            string thuMuc = Path.Combine(Application.StartupPath, "Temp");
            try
            {
                if (!Directory.Exists(thuMuc))
                    Directory.CreateDirectory(thuMuc);
                return thuMuc;
            }
            catch (Exception exPrimary)
            {
                try
                {
                    string fallback = Path.Combine(Path.GetTempPath(), "CKXML_Nhom9_Temp");
                    if (!Directory.Exists(fallback))
                        Directory.CreateDirectory(fallback);
                    return fallback;
                }
                catch (Exception exFallback)
                {
                    throw new IOException(
                        $"Không thể tạo thư mục tạm tại '{thuMuc}' ({exPrimary.Message}) hoặc '{Path.GetTempPath()}' ({exFallback.Message}).",
                        exFallback);
                }
            }
        }

        /// <summary>
        /// Tạo file XML trung gian gộp dữ liệu từ Ve.xml + SuatChieu.xml + Phim.xml + PhongChieu.xml + NhanVien.xml.
        /// Nếu truyền tuNgay và denNgay (yyyy-MM-dd) thì chỉ đưa các vé thuộc khoảng ngày đó vào DanhSachVe.
        /// Trả về đường dẫn tuyệt đối tới file XML trung gian vừa tạo.
        /// </summary>
        public static string TaoXmlTongHop(string? tuNgay = null, string? denNgay = null)
        {
            XmlDocument docVe = XmlHelper.LoadXmlDocument("Ve.xml");
            XmlDocument docSuat = XmlHelper.LoadXmlDocument("SuatChieu.xml");
            XmlDocument docPhim = XmlHelper.LoadXmlDocument("Phim.xml");
            XmlDocument docPhong = XmlHelper.LoadXmlDocument("PhongChieu.xml");
            XmlDocument docNV = XmlHelper.LoadXmlDocument("NhanVien.xml");

            // 1. Đưa Phim vào Dictionary để join nhanh
            var mapPhim = new Dictionary<string, (string TenPhim, string TheLoai, int ThoiLuong)>(StringComparer.OrdinalIgnoreCase);
            XmlNodeList? listPhim = docPhim.SelectNodes("//Phim");
            if (listPhim != null)
            {
                foreach (XmlNode p in listPhim)
                {
                    string maP = p.SelectSingleNode("maPhim")?.InnerText.Trim() ?? string.Empty;
                    string tenP = p.SelectSingleNode("tenPhim")?.InnerText.Trim() ?? string.Empty;
                    string theLoai = p.SelectSingleNode("theLoai")?.InnerText.Trim() ?? string.Empty;
                    int.TryParse(p.SelectSingleNode("thoiLuong")?.InnerText.Trim(), out int tl);
                    if (!string.IsNullOrEmpty(maP))
                        mapPhim[maP] = (tenP, theLoai, tl);
                }
            }

            // 2. Đưa Phòng chiếu vào Dictionary
            var mapPhong = new Dictionary<string, (string TenPhong, int TongSoGhe)>(StringComparer.OrdinalIgnoreCase);
            XmlNodeList? listPhong = docPhong.SelectNodes("//PhongChieu");
            if (listPhong != null)
            {
                foreach (XmlNode pc in listPhong)
                {
                    string maPC = pc.SelectSingleNode("maPhong")?.InnerText.Trim() ?? string.Empty;
                    string tenPC = pc.SelectSingleNode("tenPhong")?.InnerText.Trim() ?? string.Empty;
                    int.TryParse(pc.SelectSingleNode("tongSoGhe")?.InnerText.Trim(), out int tongGhe);
                    if (!string.IsNullOrEmpty(maPC))
                        mapPhong[maPC] = (tenPC, tongGhe);
                }
            }

            // 3. Đưa Nhân viên vào Dictionary
            var mapNV = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            XmlNodeList? listNV = docNV.SelectNodes("//NhanVien");
            if (listNV != null)
            {
                foreach (XmlNode nv in listNV)
                {
                    string maNV = nv.SelectSingleNode("maNV")?.InnerText.Trim() ?? string.Empty;
                    string hoTen = nv.SelectSingleNode("hoTen")?.InnerText.Trim() ?? string.Empty;
                    if (!string.IsNullOrEmpty(maNV))
                        mapNV[maNV] = hoTen;
                }
            }

            // 4. Đếm số ghế đã bán theo từng maSuat
            var demGheBanTheoSuat = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            XmlNodeList? listVe = docVe.SelectNodes("//Ve");
            if (listVe != null)
            {
                foreach (XmlNode v in listVe)
                {
                    string maS = v.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                    if (!string.IsNullOrEmpty(maS))
                    {
                        demGheBanTheoSuat.TryGetValue(maS, out int c);
                        demGheBanTheoSuat[maS] = c + 1;
                    }
                }
            }

            // 5. Đưa Suất chiếu vào Dictionary và chuẩn bị danh sách xuất
            var mapSuat = new Dictionary<string, (string MaPhim, string MaPhong, string NgayChieu, string GioBatDau, string GiaVe)>(StringComparer.OrdinalIgnoreCase);
            XmlNodeList? listSuat = docSuat.SelectNodes("//SuatChieu");
            if (listSuat != null)
            {
                foreach (XmlNode sc in listSuat)
                {
                    string maS = sc.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                    string maP = sc.SelectSingleNode("maPhim")?.InnerText.Trim() ?? string.Empty;
                    string maPC = sc.SelectSingleNode("maPhong")?.InnerText.Trim() ?? string.Empty;
                    string ngay = sc.SelectSingleNode("ngayChieu")?.InnerText.Trim() ?? string.Empty;
                    string gio = sc.SelectSingleNode("gioBatDau")?.InnerText.Trim() ?? "00:00:00";
                    string gia = sc.SelectSingleNode("giaVe")?.InnerText.Trim() ?? "0";
                    if (!string.IsNullOrEmpty(maS))
                        mapSuat[maS] = (maP, maPC, ngay, gio, gia);
                }
            }

            // 6. Khởi tạo XmlDocument trung gian <DuLieuTongHop>
            XmlDocument docOut = new XmlDocument();
            XmlDeclaration decl = docOut.CreateXmlDeclaration("1.0", "utf-8", null);
            docOut.AppendChild(decl);

            XmlElement root = docOut.CreateElement("DuLieuTongHop");
            docOut.AppendChild(root);

            // 6a. Tạo nhánh <DanhSachVe>
            XmlElement elDanhSachVe = docOut.CreateElement("DanhSachVe");
            root.AppendChild(elDanhSachVe);

            if (listVe != null)
            {
                foreach (XmlNode v in listVe)
                {
                    string maVe = v.SelectSingleNode("maVe")?.InnerText.Trim() ?? string.Empty;
                    string maSuat = v.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                    string maNV = v.SelectSingleNode("maNV")?.InnerText.Trim() ?? string.Empty;
                    string viTriGhe = v.SelectSingleNode("viTriGhe")?.InnerText.Trim() ?? string.Empty;
                    string giaVe = v.SelectSingleNode("giaVe")?.InnerText.Trim() ?? "0";
                    string ngayBan = v.SelectSingleNode("ngayBan")?.InnerText.Trim() ?? string.Empty;

                    // Tách phần ngày yyyy-MM-dd từ ngayBan (xs:dateTime)
                    string ngayBanDate = ngayBan.Length >= 10 ? ngayBan.Substring(0, 10) : ngayBan;
                    string ngayBanDateVN = DinhDangNgayVN(ngayBanDate);
                    string ngayBanVN = DinhDangNgayGioVN(ngayBan);

                    // Nếu có lọc theo khoảng ngày [tuNgay, denNgay]
                    if (!string.IsNullOrEmpty(tuNgay) && string.Compare(ngayBanDate, tuNgay, StringComparison.Ordinal) < 0)
                        continue;
                    if (!string.IsNullOrEmpty(denNgay) && string.Compare(ngayBanDate, denNgay, StringComparison.Ordinal) > 0)
                        continue;

                    mapSuat.TryGetValue(maSuat, out var infoSuat);
                    string maPhim = infoSuat.MaPhim ?? string.Empty;
                    string maPhong = infoSuat.MaPhong ?? string.Empty;
                    string ngayChieu = infoSuat.NgayChieu ?? string.Empty;
                    string gioBD = infoSuat.GioBatDau ?? "00:00:00";

                    mapPhim.TryGetValue(maPhim, out var infoPhim);
                    mapPhong.TryGetValue(maPhong, out var infoPhong);
                    mapNV.TryGetValue(maNV, out string? tenNV);

                    string gioBDNgan = gioBD.Length >= 5 ? gioBD.Substring(0, 5) : gioBD;
                    string gioKTNgan = TinhGioKetThuc(gioBD, infoPhim.ThoiLuong, false);

                    XmlElement elVe = docOut.CreateElement("Ve");
                    ThemTheCon(docOut, elVe, "maVe", maVe);
                    ThemTheCon(docOut, elVe, "maSuat", maSuat);
                    ThemTheCon(docOut, elVe, "maPhim", maPhim);
                    ThemTheCon(docOut, elVe, "tenPhim", infoPhim.TenPhim ?? maPhim);
                    ThemTheCon(docOut, elVe, "theLoai", infoPhim.TheLoai ?? string.Empty);
                    ThemTheCon(docOut, elVe, "thoiLuong", infoPhim.ThoiLuong.ToString());
                    ThemTheCon(docOut, elVe, "maPhong", maPhong);
                    ThemTheCon(docOut, elVe, "tenPhong", infoPhong.TenPhong ?? maPhong);
                    ThemTheCon(docOut, elVe, "ngayChieu", ngayChieu);
                    ThemTheCon(docOut, elVe, "ngayChieuVN", DinhDangNgayVN(ngayChieu));
                    ThemTheCon(docOut, elVe, "gioBatDau", gioBDNgan);
                    ThemTheCon(docOut, elVe, "gioKetThuc", gioKTNgan);
                    ThemTheCon(docOut, elVe, "maNV", maNV);
                    ThemTheCon(docOut, elVe, "tenNV", tenNV ?? maNV);
                    ThemTheCon(docOut, elVe, "viTriGhe", viTriGhe);
                    ThemTheCon(docOut, elVe, "giaVe", giaVe);
                    ThemTheCon(docOut, elVe, "ngayBan", ngayBan);
                    ThemTheCon(docOut, elVe, "ngayBanDate", ngayBanDate);
                    ThemTheCon(docOut, elVe, "ngayBanDateVN", ngayBanDateVN);
                    ThemTheCon(docOut, elVe, "ngayBanVN", ngayBanVN);

                    elDanhSachVe.AppendChild(elVe);
                }
            }

            // 6b. Tạo nhánh <DanhSachSuatChieu>
            XmlElement elDanhSachSuat = docOut.CreateElement("DanhSachSuatChieu");
            root.AppendChild(elDanhSachSuat);

            if (listSuat != null)
            {
                foreach (XmlNode sc in listSuat)
                {
                    string maSuat = sc.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                    string maPhim = sc.SelectSingleNode("maPhim")?.InnerText.Trim() ?? string.Empty;
                    string maPhong = sc.SelectSingleNode("maPhong")?.InnerText.Trim() ?? string.Empty;
                    string ngayChieu = sc.SelectSingleNode("ngayChieu")?.InnerText.Trim() ?? string.Empty;
                    string gioBD = sc.SelectSingleNode("gioBatDau")?.InnerText.Trim() ?? "00:00:00";
                    string giaVe = sc.SelectSingleNode("giaVe")?.InnerText.Trim() ?? "0";

                    mapPhim.TryGetValue(maPhim, out var infoPhim);
                    mapPhong.TryGetValue(maPhong, out var infoPhong);
                    demGheBanTheoSuat.TryGetValue(maSuat, out int soDaBan);

                    int tongGhe = infoPhong.TongSoGhe;
                    int soTrong = Math.Max(0, tongGhe - soDaBan);

                    string gioBDNgan = gioBD.Length >= 5 ? gioBD.Substring(0, 5) : gioBD;
                    string gioKTDuKien = TinhGioKetThuc(gioBD, infoPhim.ThoiLuong, true); // + 15 phút dọn rạp

                    XmlElement elSuat = docOut.CreateElement("SuatChieu");
                    ThemTheCon(docOut, elSuat, "maSuat", maSuat);
                    ThemTheCon(docOut, elSuat, "maPhim", maPhim);
                    ThemTheCon(docOut, elSuat, "tenPhim", infoPhim.TenPhim ?? maPhim);
                    ThemTheCon(docOut, elSuat, "theLoai", infoPhim.TheLoai ?? string.Empty);
                    ThemTheCon(docOut, elSuat, "thoiLuong", infoPhim.ThoiLuong.ToString());
                    ThemTheCon(docOut, elSuat, "maPhong", maPhong);
                    ThemTheCon(docOut, elSuat, "tenPhong", infoPhong.TenPhong ?? maPhong);
                    ThemTheCon(docOut, elSuat, "ngayChieu", ngayChieu);
                    ThemTheCon(docOut, elSuat, "ngayChieuVN", DinhDangNgayVN(ngayChieu));
                    ThemTheCon(docOut, elSuat, "gioBatDau", gioBDNgan);
                    ThemTheCon(docOut, elSuat, "gioKetThuc", gioKTDuKien);
                    ThemTheCon(docOut, elSuat, "giaVe", giaVe);
                    ThemTheCon(docOut, elSuat, "tongSoGhe", tongGhe.ToString());
                    ThemTheCon(docOut, elSuat, "soGheDaBan", soDaBan.ToString());
                    ThemTheCon(docOut, elSuat, "soGheTrong", soTrong.ToString());

                    elDanhSachSuat.AppendChild(elSuat);
                }
            }

            string thuMucTam = LayThuMucTam();
            string tenFileTam = $"TongHop_{DateTime.Now:yyyyMMdd_HHmmss_fff}.xml";
            string duongDanXmlTam = Path.Combine(thuMucTam, tenFileTam);
            docOut.Save(duongDanXmlTam);
            return duongDanXmlTam;
        }

        private static void ThemTheCon(XmlDocument doc, XmlElement parent, string name, string value)
        {
            XmlElement child = doc.CreateElement(name);
            child.InnerText = value ?? string.Empty;
            parent.AppendChild(child);
        }

        private static string TinhGioKetThuc(string gioBDStr, int thoiLuongPhut, bool cong15PhutDonRap)
        {
            if (!TimeSpan.TryParse(gioBDStr, out TimeSpan bd))
                return "--:--";

            int tongPhut = thoiLuongPhut + (cong15PhutDonRap ? PHUT_DON_RAP : 0);
            TimeSpan kt = bd.Add(TimeSpan.FromMinutes(tongPhut));
            if (kt >= TimeSpan.FromHours(24))
                kt = kt.Subtract(TimeSpan.FromHours(24));
            return kt.ToString(@"hh\:mm");
        }

        public static string DinhDangNgayVN(string ngayXml)
        {
            if (DateTime.TryParseExact(ngayXml, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return dt.ToString("dd/MM/yyyy");
            return ngayXml;
        }

        public static string DinhDangNgayGioVN(string ngayGioXml)
        {
            if (DateTime.TryParse(ngayGioXml, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                return dt.ToString("dd/MM/yyyy HH:mm:ss");
            return ngayGioXml;
        }

        /// <summary>
        /// Thực hiện biến đổi XSLT 1.0 từ file XML trung gian và XsltArgumentList sang file HTML (UTF-8).
        /// </summary>
        public static void XuatHtml(string xsltFile, string xmlFile, XsltArgumentList? thamSo, string htmlOut)
        {
            string fullXsltPath = XmlHelper.GetXsltPath(xsltFile);
            if (!File.Exists(fullXsltPath))
                throw new FileNotFoundException("Không tìm thấy file XSLT: " + fullXsltPath);

            if (!File.Exists(xmlFile))
                throw new FileNotFoundException("Không tìm thấy file XML trung gian: " + xmlFile);

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(fullXsltPath);

            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (FileStream fs = new FileStream(htmlOut, FileMode.Create, FileAccess.Write, FileShare.Read))
            using (StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false)))
            using (XmlWriter xw = XmlWriter.Create(sw, writerSettings))
            {
                xslt.Transform(xmlFile, thamSo, xw);
            }
        }

        /// <summary>
        /// Mở file HTML bằng trình duyệt web thực tế (Edge, Chrome, Firefox), nếu không có mới dùng ứng dụng mặc định.
        /// </summary>
        public static void MoTrinhDuyet(string htmlFile)
        {
            if (!File.Exists(htmlFile))
                throw new FileNotFoundException("Không tìm thấy file HTML để mở: " + htmlFile);

            string fileUri = new Uri(Path.GetFullPath(htmlFile)).AbsoluteUri;

            string[] cacTrinhDuyetMau =
            {
                @"%ProgramFiles(x86)%\Microsoft\Edge\Application\msedge.exe",
                @"%ProgramFiles%\Microsoft\Edge\Application\msedge.exe",
                @"%ProgramFiles%\Google\Chrome\Application\chrome.exe",
                @"%ProgramFiles(x86)%\Google\Chrome\Application\chrome.exe",
                @"%LocalAppData%\Google\Chrome\Application\chrome.exe",
                @"%ProgramFiles%\Mozilla Firefox\firefox.exe"
            };

            List<string> dsLoi = new List<string>();

            foreach (string mau in cacTrinhDuyetMau)
            {
                string duongDanTrinhDuyet = Environment.ExpandEnvironmentVariables(mau);
                if (File.Exists(duongDanTrinhDuyet))
                {
                    try
                    {
                        ProcessStartInfo psiBrowser = new ProcessStartInfo
                        {
                            FileName = duongDanTrinhDuyet,
                            Arguments = "\"" + fileUri + "\"",
                            UseShellExecute = false
                        };
                        Process.Start(psiBrowser);
                        return;
                    }
                    catch (Exception exBrowser)
                    {
                        dsLoi.Add($"{Path.GetFileName(duongDanTrinhDuyet)}: {exBrowser.Message}");
                    }
                }
            }

            // Nếu không tìm thấy trình duyệt nào ở trên thì dùng ứng dụng mặc định của hệ điều hành
            try
            {
                ProcessStartInfo psiDefault = new ProcessStartInfo
                {
                    FileName = htmlFile,
                    UseShellExecute = true
                };
                Process.Start(psiDefault);
            }
            catch (Exception exDefault)
            {
                dsLoi.Add($"Default shell: {exDefault.Message}");
                throw new InvalidOperationException(
                    "Không thể mở file HTML bằng trình duyệt hoặc ứng dụng mặc định:\n- " +
                    string.Join("\n- ", dsLoi),
                    exDefault);
            }
        }

        /// <summary>
        /// Tạo file Vé xem phim HTML cho danh sách mã vé vừa bán và trả về đường dẫn đầy đủ của file HTML.
        /// </summary>
        public static string TaoFileVeXemPhimHtml(IEnumerable<string> danhSachMaVe)
        {
            string dsGhep = "," + string.Join(",", danhSachMaVe) + ",";
            string xmlTam = TaoXmlTongHop();
            string htmlOut = Path.Combine(LayThuMucTam(), $"VeXemPhim_{DateTime.Now:yyyyMMdd_HHmmss_fff}.html");

            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("dsMaVe", "", dsGhep);
            args.AddParam("tenRap", "", "RẠP CHIẾU PHIM CINEMA XML - NHÓM 9");

            XuatHtml("VeXemPhim.xslt", xmlTam, args, htmlOut);
            return htmlOut;
        }

        /// <summary>
        /// Tạo và mở trang Vé xem phim HTML cho danh sách mã vé vừa bán, trả về đường dẫn file HTML.
        /// </summary>
        public static string XuatVaMoVeXemPhim(IEnumerable<string> danhSachMaVe)
        {
            string htmlOut = TaoFileVeXemPhimHtml(danhSachMaVe);
            MoTrinhDuyet(htmlOut);
            return htmlOut;
        }

        /// <summary>
        /// Tạo và mở bảng Lịch chiếu HTML theo ngày (hoặc tất cả nếu ngayChieuXml rỗng / 'TATCA').
        /// </summary>
        public static void XuatVaMoDanhSachSuatChieu(string? ngayChieuXml = null)
        {
            string thamSoNgay = string.IsNullOrWhiteSpace(ngayChieuXml) ? "TATCA" : ngayChieuXml.Trim();
            string thamSoNgayVN = thamSoNgay == "TATCA" ? "Tất cả các ngày" : DinhDangNgayVN(thamSoNgay);

            string xmlTam = TaoXmlTongHop();
            string htmlOut = Path.Combine(LayThuMucTam(), $"DanhSachSuatChieu_{DateTime.Now:yyyyMMdd_HHmmss_fff}.html");

            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("ngayChieu", "", thamSoNgay);
            args.AddParam("ngayChieuVN", "", thamSoNgayVN);
            args.AddParam("ngayLap", "", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            XuatHtml("DanhSachSuatChieu.xslt", xmlTam, args, htmlOut);
            MoTrinhDuyet(htmlOut);
        }

        /// <summary>
        /// Tạo và mở Báo cáo doanh thu HTML theo khoảng ngày [tuNgay, denNgay].
        /// </summary>
        public static void XuatVaMoBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string tuNgayXml = tuNgay.ToString("yyyy-MM-dd");
            string denNgayXml = denNgay.ToString("yyyy-MM-dd");

            string xmlTam = TaoXmlTongHop(tuNgayXml, denNgayXml);
            string htmlOut = Path.Combine(LayThuMucTam(), $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss_fff}.html");

            XsltArgumentList args = new XsltArgumentList();
            args.AddParam("tuNgay", "", tuNgayXml);
            args.AddParam("denNgay", "", denNgayXml);
            args.AddParam("tuNgayVN", "", tuNgay.ToString("dd/MM/yyyy"));
            args.AddParam("denNgayVN", "", denNgay.ToString("dd/MM/yyyy"));
            args.AddParam("ngayLap", "", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            XuatHtml("BaoCaoDoanhThu.xslt", xmlTam, args, htmlOut);
            MoTrinhDuyet(htmlOut);
        }
    }
}

