using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace CKXML_Nhom9.Helpers
{
    /// <summary>
    /// Lớp dùng chung thao tác trực tiếp trên các file XML bằng XmlDocument, XPath và XSD.
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Lấy đường dẫn tuyệt đối tới file trong thư mục Data dựa trên Application.StartupPath.
        /// </summary>
        public static string GetDataPath(string fileName)
        {
            if (Path.IsPathRooted(fileName))
                return fileName;
            return Path.Combine(Application.StartupPath, "Data", fileName);
        }

        /// <summary>
        /// Lấy đường dẫn tuyệt đối tới file trong thư mục Xslt dựa trên Application.StartupPath.
        /// </summary>
        public static string GetXsltPath(string fileName)
        {
            if (Path.IsPathRooted(fileName))
                return fileName;
            return Path.Combine(Application.StartupPath, "Xslt", fileName);
        }

        /// <summary>
        /// Xử lý ký tự nháy đơn (') và nháy kép (") khi dựng biểu thức XPath để chống lỗi cú pháp và XPath Injection.
        /// </summary>
        public static string EscapeXPathLiteral(string value)
        {
            if (value == null)
                return "''";

            // Nếu không chứa dấu nháy đơn thì bọc trong '...'
            if (!value.Contains("'"))
                return "'" + value + "'";

            // Nếu chứa nháy đơn nhưng không chứa nháy kép thì bọc trong "..."
            if (!value.Contains("\""))
                return "\"" + value + "\"";

            // Nếu chứa cả hai thì dùng hàm concat() của XPath
            string[] parts = value.Split('\'');
            StringBuilder sb = new StringBuilder("concat(");
            for (int i = 0; i < parts.Length; i++)
            {
                if (i > 0)
                    sb.Append(", \"'\", ");
                sb.Append("'").Append(parts[i]).Append("'");
            }
            sb.Append(")");
            return sb.ToString();
        }

        private const string CHU_HOA_VIET = "ABCDEFGHIJKLMNOPQRSTUVWXYZÀÁẠẢÃÂẦẤẬẨẪĂẰẮẶẲẴÈÉẸẺẼÊỀẾỆỂỄÌÍỊỈĨÒÓỌỎÕÔỒỐỘỔỖƠỜỚỢỞỠÙÚỤỦŨƯỪỨỰỬỮỲÝỴỶỸĐ";
        private const string CHU_THUONG_VIET = "abcdefghijklmnopqrstuvwxyzàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ";

        /// <summary>
        /// Tạo biểu thức điều kiện XPath contains() không phân biệt chữ hoa/thường (hỗ trợ tiếng Việt) và chống XPath Injection.
        /// </summary>
        public static string BieuThucChuaXPath(string tenTruong, string tuKhoa)
        {
            string tuKhoaThuong = (tuKhoa ?? string.Empty).Trim().ToLowerInvariant();
            string escaped = EscapeXPathLiteral(tuKhoaThuong);
            return $"contains(translate({tenTruong}, '{CHU_HOA_VIET}', '{CHU_THUONG_VIET}'), {escaped})";
        }
        public static string BamMatKhauSHA256(string matKhau)
        {
            if (matKhau == null)
                matKhau = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(matKhau);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder(64);
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Đọc file XML thành đối tượng XmlDocument.
        /// </summary>
        public static XmlDocument LoadXmlDocument(string tenFile)
        {
            string fullPath = GetDataPath(tenFile);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Không tìm thấy file dữ liệu XML: " + fullPath);

            XmlDocument doc = new XmlDocument();
            doc.Load(fullPath);
            return doc;
        }

        /// <summary>
        /// Đọc toàn bộ bản ghi trong file XML lên DataTable.
        /// </summary>
        public static DataTable LoadDataTable(string tenFile)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            if (doc.DocumentElement == null)
                return new DataTable();

            return ChuyenNodeListSangDataTable(doc.DocumentElement.ChildNodes, doc.DocumentElement.Name);
        }

        /// <summary>
        /// Chuyển danh sách các XmlNode bản ghi sang DataTable.
        /// </summary>
        public static DataTable ChuyenNodeListSangDataTable(XmlNodeList? nodeList, string tableName = "BangDuLieu")
        {
            DataTable dt = new DataTable(tableName);
            if (nodeList == null || nodeList.Count == 0)
                return dt;

            // Tạo các cột từ thẻ con của bản ghi đầu tiên là Element
            XmlNode? firstRecord = null;
            foreach (XmlNode node in nodeList)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    firstRecord = node;
                    break;
                }
            }

            if (firstRecord == null)
                return dt;

            foreach (XmlNode child in firstRecord.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element && !dt.Columns.Contains(child.Name))
                {
                    dt.Columns.Add(child.Name, typeof(string));
                }
            }

            // Đổ dữ liệu từng dòng
            foreach (XmlNode record in nodeList)
            {
                if (record.NodeType != XmlNodeType.Element)
                    continue;

                DataRow row = dt.NewRow();
                foreach (XmlNode field in record.ChildNodes)
                {
                    if (field.NodeType == XmlNodeType.Element)
                    {
                        if (!dt.Columns.Contains(field.Name))
                            dt.Columns.Add(field.Name, typeof(string));

                        row[field.Name] = field.InnerText;
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        }

        /// <summary>
        /// Tìm kiếm các phần tử theo biểu thức XPath và trả về DataTable.
        /// </summary>
        public static DataTable TimKiemXPath(string tenFile, string bieuThucXPath)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            XmlNodeList? nodes = doc.SelectNodes(bieuThucXPath);
            return ChuyenNodeListSangDataTable(nodes, doc.DocumentElement?.Name ?? "KetQua");
        }

        /// <summary>
        /// Kiểm tra giá trị có bị trùng trong cột chỉ định của file XML hay không.
        /// </summary>
        public static bool KiemTraTrung(string tenFile, string tenTheBanGhi, string tenCot, string giaTri)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            string xpath = $"//{tenTheBanGhi}[{tenCot}={EscapeXPathLiteral(giaTri)}]";
            XmlNode? node = doc.SelectSingleNode(xpath);
            return node != null;
        }

        /// <summary>
        /// Kiểm tra một giá trị có tồn tại theo biểu thức XPath tùy chỉnh hay không.
        /// </summary>
        public static bool TonTaiXPath(string tenFile, string bieuThucXPath)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            XmlNode? node = doc.SelectSingleNode(bieuThucXPath);
            return node != null;
        }

        /// <summary>
        /// Lấy giá trị chuỗi của một node đầu tiên khớp biểu thức XPath.
        /// </summary>
        public static string LayGiaTriXPath(string tenFile, string bieuThucXPath)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            XmlNode? node = doc.SelectSingleNode(bieuThucXPath);
            return node != null ? node.InnerText : string.Empty;
        }

        /// <summary>
        /// Tự động sinh mã kế tiếp dựa trên mã lớn nhất hiện có (ví dụ: P001 -> P002, PC01 -> PC02, V0001 -> V0002).
        /// </summary>
        public static string SinhMaTuDong(string tienTo, string tenFile, string tenCot, int doDaiSoMacDinh = 3)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            XmlNodeList? nodes = doc.SelectNodes($"//{tenCot}");

            int maxNumber = 0;
            int detectedLength = doDaiSoMacDinh;

            if (nodes != null && nodes.Count > 0)
            {
                foreach (XmlNode node in nodes)
                {
                    string ma = node.InnerText.Trim();
                    if (ma.StartsWith(tienTo, StringComparison.OrdinalIgnoreCase))
                    {
                        string phanSo = ma.Substring(tienTo.Length);
                        if (phanSo.Length > 0 && int.TryParse(phanSo, out int so))
                        {
                            if (phanSo.Length > detectedLength || maxNumber == 0)
                                detectedLength = phanSo.Length;

                            if (so > maxNumber)
                                maxNumber = so;
                        }
                    }
                }
            }
            else
            {
                // Quy ước độ dài phần số mặc định theo tiền tố nếu file đang rỗng
                if (string.Equals(tienTo, "PC", StringComparison.OrdinalIgnoreCase))
                    detectedLength = 2;
                else if (string.Equals(tienTo, "V", StringComparison.OrdinalIgnoreCase))
                    detectedLength = 4;
                else
                    detectedLength = 3;
            }

            int nextNumber = maxNumber + 1;
            return tienTo + nextNumber.ToString(new string('0', detectedLength));
        }

        /// <summary>
        /// Danh sách thứ tự thẻ con chuẩn theo đặc tả XSD cho từng loại bản ghi.
        /// </summary>
        private static readonly Dictionary<string, string[]> ThuTuTheChuan = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            { "TaiKhoan", new[] { "tenDangNhap", "matKhau", "quyenHan", "maNV" } },
            { "NhanVien", new[] { "maNV", "hoTen", "gioiTinh", "ngaySinh", "diaChi", "soDienThoai", "chucVu" } },
            { "Phim", new[] { "maPhim", "tenPhim", "theLoai", "thoiLuong", "daoDien", "apPhich" } },
            { "PhongChieu", new[] { "maPhong", "tenPhong", "soHang", "soCot", "tongSoGhe" } },
            { "SuatChieu", new[] { "maSuat", "maPhim", "maPhong", "ngayChieu", "gioBatDau", "giaVe" } },
            { "Ve", new[] { "maVe", "maSuat", "maNV", "viTriGhe", "giaVe", "ngayBan" } }
        };

        /// <summary>
        /// Thêm một bản ghi mới vào file XML đúng thứ tự thẻ XSD, kiểm tra hợp lệ XSD trước khi lưu xuống đĩa.
        /// </summary>
        public static void Them(string tenFile, string tenTheBanGhi, Dictionary<string, string> duLieu)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            if (doc.DocumentElement == null)
                throw new InvalidOperationException("File XML không có phần tử gốc.");

            XmlElement newElement = doc.CreateElement(tenTheBanGhi);

            if (ThuTuTheChuan.TryGetValue(tenTheBanGhi, out string[]? thuTu))
            {
                // Ghi các thẻ theo đúng thứ tự đặc tả XSD
                foreach (string tenThe in thuTu)
                {
                    if (duLieu.TryGetValue(tenThe, out string? giaTri))
                    {
                        XmlElement child = doc.CreateElement(tenThe);
                        child.InnerText = giaTri ?? string.Empty;
                        newElement.AppendChild(child);
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> kvp in duLieu)
                {
                    XmlElement child = doc.CreateElement(kvp.Key);
                    child.InnerText = kvp.Value ?? string.Empty;
                    newElement.AppendChild(child);
                }
            }

            doc.DocumentElement.AppendChild(newElement);
            LuuVaValidate(doc, tenFile);
        }

        /// <summary>
        /// Sửa một bản ghi trong file XML dựa trên cột khóa chính, kiểm tra XSD trước khi lưu.
        /// </summary>
        public static bool Sua(string tenFile, string tenTheBanGhi, string tenCotKhoa, string giaTriKhoa, Dictionary<string, string> duLieuMoi)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            string xpath = $"//{tenTheBanGhi}[{tenCotKhoa}={EscapeXPathLiteral(giaTriKhoa)}]";
            XmlNode? recordNode = doc.SelectSingleNode(xpath);
            if (recordNode == null)
                return false;

            foreach (KeyValuePair<string, string> kvp in duLieuMoi)
            {
                XmlNode? fieldNode = recordNode.SelectSingleNode(kvp.Key);
                if (fieldNode != null)
                {
                    fieldNode.InnerText = kvp.Value ?? string.Empty;
                }
                else
                {
                    XmlElement newChild = doc.CreateElement(kvp.Key);
                    newChild.InnerText = kvp.Value ?? string.Empty;
                    recordNode.AppendChild(newChild);
                }
            }

            LuuVaValidate(doc, tenFile);
            return true;
        }

        /// <summary>
        /// Xóa một bản ghi trong file XML dựa trên cột khóa chính, kiểm tra XSD trước khi lưu.
        /// </summary>
        public static bool Xoa(string tenFile, string tenTheBanGhi, string tenCotKhoa, string giaTriKhoa)
        {
            XmlDocument doc = LoadXmlDocument(tenFile);
            string xpath = $"//{tenTheBanGhi}[{tenCotKhoa}={EscapeXPathLiteral(giaTriKhoa)}]";
            XmlNode? recordNode = doc.SelectSingleNode(xpath);
            if (recordNode == null || recordNode.ParentNode == null)
                return false;

            recordNode.ParentNode.RemoveChild(recordNode);
            LuuVaValidate(doc, tenFile);
            return true;
        }

        /// <summary>
        /// Kiểm tra XmlDocument với file XSD tương ứng rồi mới lưu xuống đĩa.
        /// </summary>
        private static void LuuVaValidate(XmlDocument doc, string tenFileXml)
        {
            string tenFileXsd = Path.ChangeExtension(tenFileXml, ".xsd");
            string fullXsdPath = GetDataPath(tenFileXsd);

            if (File.Exists(fullXsdPath))
            {
                List<string> errors = ValidateXmlDoc(doc, tenFileXsd);
                if (errors.Count > 0)
                {
                    throw new XmlSchemaValidationException(
                        "Dữ liệu không hợp lệ theo XSD (" + Path.GetFileName(tenFileXsd) + "):\n- " +
                        string.Join("\n- ", errors));
                }
            }

            string fullXmlPath = GetDataPath(tenFileXml);
            doc.Save(fullXmlPath);
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của một đối tượng XmlDocument trên bộ nhớ với file XSD.
        /// </summary>
        public static List<string> ValidateXmlDoc(XmlDocument doc, string tenFileXsd)
        {
            List<string> dsLoi = new List<string>();
            string fullXsdPath = GetDataPath(tenFileXsd);

            if (!File.Exists(fullXsdPath))
            {
                dsLoi.Add("Không tìm thấy file schema XSD: " + fullXsdPath);
                return dsLoi;
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, fullXsdPath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
            settings.ValidationEventHandler += (sender, args) =>
            {
                dsLoi.Add(args.Message);
            };

            using (StringReader sr = new StringReader(doc.OuterXml))
            using (XmlReader reader = XmlReader.Create(sr, settings))
            {
                while (reader.Read()) { }
            }

            return dsLoi;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của file XML trên đĩa với file XSD, trả về danh sách lỗi (rỗng nếu hợp lệ).
        /// </summary>
        public static List<string> ValidateXSD(string tenFileXml, string tenFileXsd)
        {
            List<string> dsLoi = new List<string>();
            string fullXmlPath = GetDataPath(tenFileXml);
            string fullXsdPath = GetDataPath(tenFileXsd);

            if (!File.Exists(fullXmlPath))
            {
                dsLoi.Add("Không tìm thấy file XML: " + fullXmlPath);
                return dsLoi;
            }

            if (!File.Exists(fullXsdPath))
            {
                dsLoi.Add("Không tìm thấy file XSD: " + fullXsdPath);
                return dsLoi;
            }

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, fullXsdPath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
            settings.ValidationEventHandler += (sender, args) =>
            {
                dsLoi.Add($"[Dòng {args.Exception?.LineNumber}] {args.Message}");
            };

            using (XmlReader reader = XmlReader.Create(fullXmlPath, settings))
            {
                while (reader.Read()) { }
            }

            return dsLoi;
        }

        /// <summary>
        /// Đọc tập hợp các giá trị khóa chính của một bảng XML vào HashSet để tra cứu nhanh O(1).
        /// </summary>
        public static HashSet<string> DocTapKhoaChinh(string tenFile, string tenTheBanGhi, string tenCotKhoa)
        {
            HashSet<string> tapKhoa = new HashSet<string>(StringComparer.Ordinal);
            XmlDocument doc = LoadXmlDocument(tenFile);
            XmlNodeList? nodes = doc.SelectNodes($"//{tenTheBanGhi}/{tenCotKhoa}");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string giaTri = node.InnerText.Trim();
                    if (!string.IsNullOrEmpty(giaTri))
                    {
                        tapKhoa.Add(giaTri);
                    }
                }
            }
            return tapKhoa;
        }

        /// <summary>
        /// Kiểm tra toàn vẹn khóa ngoại liên bảng bằng C# (đọc tập khóa bảng cha vào HashSet rồi đối chiếu):
        /// - TaiKhoan.maNV -> NhanVien.maNV
        /// - SuatChieu.maPhim -> Phim.maPhim
        /// - SuatChieu.maPhong -> PhongChieu.maPhong
        /// - Ve.maSuat -> SuatChieu.maSuat
        /// - Ve.maNV -> NhanVien.maNV
        /// Trả về danh sách lỗi chi tiết từng bản ghi vi phạm (rỗng nếu tất cả hợp lệ).
        /// </summary>
        public static List<string> ValidateToanVenKhoaNgoai()
        {
            List<string> dsLoi = new List<string>();

            // 1. Đọc tập khóa chính của các bảng cha vào HashSet
            HashSet<string> dsMaNV = DocTapKhoaChinh("NhanVien.xml", "NhanVien", "maNV");
            HashSet<string> dsMaPhim = DocTapKhoaChinh("Phim.xml", "Phim", "maPhim");
            HashSet<string> dsMaPhong = DocTapKhoaChinh("PhongChieu.xml", "PhongChieu", "maPhong");
            HashSet<string> dsMaSuat = DocTapKhoaChinh("SuatChieu.xml", "SuatChieu", "maSuat");

            // 2. Kiểm tra TaiKhoan.maNV -> NhanVien.maNV
            XmlDocument docTaiKhoan = LoadXmlDocument("TaiKhoan.xml");
            XmlNodeList? listTaiKhoan = docTaiKhoan.SelectNodes("//TaiKhoan");
            if (listTaiKhoan != null)
            {
                foreach (XmlNode tk in listTaiKhoan)
                {
                    string tenDN = tk.SelectSingleNode("tenDangNhap")?.InnerText.Trim() ?? "(rỗng)";
                    string maNV = tk.SelectSingleNode("maNV")?.InnerText.Trim() ?? string.Empty;
                    if (!dsMaNV.Contains(maNV))
                    {
                        dsLoi.Add($"TaiKhoan [tenDangNhap={tenDN}] trỏ tới maNV='{maNV}' không tồn tại trong NhanVien.xml");
                    }
                }
            }

            // 3. Kiểm tra SuatChieu.maPhim -> Phim.maPhim và SuatChieu.maPhong -> PhongChieu.maPhong
            XmlDocument docSuatChieu = LoadXmlDocument("SuatChieu.xml");
            XmlNodeList? listSuatChieu = docSuatChieu.SelectNodes("//SuatChieu");
            if (listSuatChieu != null)
            {
                foreach (XmlNode sc in listSuatChieu)
                {
                    string maSuat = sc.SelectSingleNode("maSuat")?.InnerText.Trim() ?? "(rỗng)";
                    string maPhim = sc.SelectSingleNode("maPhim")?.InnerText.Trim() ?? string.Empty;
                    string maPhong = sc.SelectSingleNode("maPhong")?.InnerText.Trim() ?? string.Empty;

                    if (!dsMaPhim.Contains(maPhim))
                    {
                        dsLoi.Add($"SuatChieu [maSuat={maSuat}] trỏ tới maPhim='{maPhim}' không tồn tại trong Phim.xml");
                    }
                    if (!dsMaPhong.Contains(maPhong))
                    {
                        dsLoi.Add($"SuatChieu [maSuat={maSuat}] trỏ tới maPhong='{maPhong}' không tồn tại trong PhongChieu.xml");
                    }
                }
            }

            // 4. Kiểm tra Ve.maSuat -> SuatChieu.maSuat và Ve.maNV -> NhanVien.maNV
            XmlDocument docVe = LoadXmlDocument("Ve.xml");
            XmlNodeList? listVe = docVe.SelectNodes("//Ve");
            if (listVe != null)
            {
                foreach (XmlNode ve in listVe)
                {
                    string maVe = ve.SelectSingleNode("maVe")?.InnerText.Trim() ?? "(rỗng)";
                    string maSuat = ve.SelectSingleNode("maSuat")?.InnerText.Trim() ?? string.Empty;
                    string maNV = ve.SelectSingleNode("maNV")?.InnerText.Trim() ?? string.Empty;

                    if (!dsMaSuat.Contains(maSuat))
                    {
                        dsLoi.Add($"Ve [maVe={maVe}] trỏ tới maSuat='{maSuat}' không tồn tại trong SuatChieu.xml");
                    }
                    if (!dsMaNV.Contains(maNV))
                    {
                        dsLoi.Add($"Ve [maVe={maVe}] trỏ tới maNV='{maNV}' không tồn tại trong NhanVien.xml");
                    }
                }
            }

            return dsLoi;
        }
    }
}

