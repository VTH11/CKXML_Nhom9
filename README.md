# HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM VÀ BÁN VÉ (ĐỒ ÁN CÔNG NGHỆ XML - NHÓM 9)

## 1. Giới thiệu
**Hệ thống quản lý rạp chiếu phim và bán vé** là ứng dụng Windows Forms (C#) được xây dựng cho môn học **Công nghệ XML**. Toàn bộ dữ liệu của hệ thống được lưu trữ và xử lý **thuần XML 100%** (không sử dụng SQL Server hay bất kỳ hệ quản trị cơ sở dữ liệu quan hệ nào), kết hợp đầy đủ các công nghệ cốt lõi:
- **DOM (`XmlDocument`) & XPath 1.0**: Truy vấn, lọc dây chuyền, tìm kiếm không phân biệt hoa/thường (có chống XPath Injection) và thực hiện thêm/sửa/xóa trực tiếp trên file XML.
- **XML Schema (`XSD`)**: Kiểm tra cấu trúc, kiểu dữ liệu, miền giá trị, khóa chính (`xs:key`) và ràng buộc duy nhất (`xs:unique`) trước mỗi thao tác ghi file; kết hợp kiểm tra toàn vẹn khóa ngoại liên bảng bằng `HashSet` trong C#.
- **XSLT 1.0 (`XslCompiledTransform` & `XsltArgumentList`)**: Kết xuất vé xem phim, lịch chiếu và báo cáo doanh thu (sử dụng kỹ thuật **Muenchian Grouping** `xsl:key` + `generate-id`) ra định dạng HTML chuẩn UTF-8 mở trực tiếp trên trình duyệt.

---

## 2. Yêu cầu hệ thống & Cách chạy
- **Hệ điều hành**: Windows 10 / Windows 11.
- **Môi trường chạy**: .NET Desktop Runtime 10.0 (`net10.0-windows`) và Visual Studio 2022 trở lên.
- **Không cần cài đặt SQL Server**, không sử dụng thư viện ngoài (NuGet bên thứ ba).
- **Cách chạy**:
  1. Mở file giải pháp `CKXML_Nhom9.slnx` (hoặc `CKXML_Nhom9/CKXML_Nhom9.csproj`) bằng Visual Studio.
  2. Nhấn **F5** (hoặc chạy lệnh `dotnet run --project CKXML_Nhom9` trong Terminal).

---

## 3. Tài khoản đăng nhập mẫu

Mật khẩu trong `TaiKhoan.xml` được băm bằng thuật toán **SHA-256** (chuỗi hex 64 ký tự chữ thường):

| Tên đăng nhập | Mật khẩu gốc | Quyền hạn | Mã NV gắn kèm | Họ tên nhân viên |
| :--- | :--- | :--- | :--- | :--- |
| **`admin`** | **`admin123`** | `1` (Quản lý - Toàn quyền) | `NV001` | Nguyễn Văn An |
| **`nhanvien1`** | **`123456`** | `2` (Nhân viên - Chỉ Bán vé & Xem lịch chiếu) | `NV002` | Trần Thị Bích |

---

## 4. Cấu trúc thư mục & Nơi lưu trữ dữ liệu khi chạy

```text
CKXML_Nhom9/
├── Data/                   # 6 file dữ liệu XML gốc và 6 file lược đồ XSD tương ứng
├── Xslt/                   # 3 file XSLT 1.0 phục vụ xuất vé, lịch chiếu và báo cáo HTML
├── Helpers/                # Các lớp dùng chung: XmlHelper.cs, Session.cs, BaoCaoHelper.cs
├── Forms/                  # Các Form giao diện chức năng của ứng dụng
└── Program.cs              # Điểm khởi đầu của chương trình (chạy frmDangNhap)
```

### Nơi lưu dữ liệu thực tế khi chạy chương trình:
- Tất cả các file trong `Data/` và `Xslt/` được cấu hình `Copy to Output Directory = PreserveNewest` (*Copy if newer*).
- Khi chạy ứng dụng, chương trình đọc và ghi trực tiếp tại thư mục thực thi (`Application.StartupPath`):
  - **Thư mục dữ liệu XML & XSD khi chạy**:
    `CKXML_Nhom9\bin\Debug\net10.0-windows\Data\`
  - **Thư mục chứa file XSLT khi chạy**:
    `CKXML_Nhom9\bin\Debug\net10.0-windows\Xslt\`
  - **Thư mục lưu file XML trung gian và file báo cáo / vé HTML xuất ra**:
    `CKXML_Nhom9\bin\Debug\net10.0-windows\Temp\`
    *(Mỗi lần xuất vé hoặc báo cáo sẽ sinh file có hậu tố thời gian `yyyyMMdd_HHmmss_fff` như `TongHop_20261118_154000_120.xml`, `VeXemPhim_20261118_154000_125.html` để không ghi đè dữ liệu gốc và không bị khóa file).*

### Cách đặt lại (Reset) dữ liệu mẫu về trạng thái ban đầu:
- Nếu sau quá trình thêm/sửa/xóa hoặc bán vé thử nghiệm bạn muốn khôi phục lại bộ dữ liệu mẫu gốc:
  1. Đóng ứng dụng đang chạy.
  2. Xóa thư mục `CKXML_Nhom9\bin\Debug\net10.0-windows\Data` (và `Temp` nếu muốn).
  3. Bấm **Build -> Rebuild Solution** (hoặc nhấn **F5**) trong Visual Studio để hệ thống tự động copy lại bộ file `.xml` mẫu gốc từ thư mục mã nguồn `CKXML_Nhom9\Data\` sang thư mục `bin\`.

---

## 5. Danh mục toàn bộ các file trong dự án (Phục vụ mục IV.1 Báo cáo)

### A. Thư mục `Data/` (12 file XML & XSD)
1. `Data/TaiKhoan.xml`: Lưu danh sách tài khoản đăng nhập (`tenDangNhap`, `matKhau` băm SHA-256, `quyenHan`, `maNV`).
2. `Data/TaiKhoan.xsd`: Lược đồ kiểm tra cấu trúc, độ dài mật khẩu hex 64 ký tự, miền giá trị quyền hạn (1 hoặc 2) và khóa chính `tenDangNhap`.
3. `Data/NhanVien.xml`: Lưu danh sách hồ sơ nhân viên (`maNV`, `hoTen`, `gioiTinh`, `ngaySinh`, `diaChi`, `soDienThoai`, `chucVu`).
4. `Data/NhanVien.xsd`: Lược đồ kiểm tra định dạng mã `NVxxx`, giới tính `Nam/Nữ`, ngày sinh `xs:date`, số điện thoại đúng 10 chữ số và khóa chính `maNV`.
5. `Data/Phim.xml`: Lưu danh mục bộ phim (`maPhim`, `tenPhim`, `theLoai`, `thoiLuong`, `daoDien`, `apPhich`).
6. `Data/Phim.xsd`: Lược đồ kiểm tra định dạng mã `Pxxx`, thời lượng nguyên dương (`> 0`) và khóa chính `maPhim`.
7. `Data/PhongChieu.xml`: Lưu danh mục phòng chiếu (`maPhong`, `tenPhong`, `soHang`, `soCot`, `tongSoGhe`).
8. `Data/PhongChieu.xsd`: Lược đồ kiểm tra định dạng mã `PCxx`, số hàng (`1-26`), số cột (`1-30`), tổng số ghế và khóa chính `maPhong`.
9. `Data/SuatChieu.xml`: Lưu lịch suất chiếu (`maSuat`, `maPhim`, `maPhong`, `ngayChieu`, `gioBatDau`, `giaVe`).
10. `Data/SuatChieu.xsd`: Lược đồ kiểm tra định dạng mã `SCxxx`, kiểu ngày `xs:date`, kiểu giờ `xs:time`, giá vé `>= 0` và khóa chính `maSuat`.
11. `Data/Ve.xml`: Lưu danh sách vé đã bán (`maVe`, `maSuat`, `maNV`, `viTriGhe`, `giaVe`, `ngayBan`).
12. `Data/Ve.xsd`: Lược đồ kiểm tra định dạng mã `Vxxxx`, mẫu vị trí ghế `[A-Z]\d{2}`, khóa chính `maVe` và ràng buộc duy nhất `xs:unique` cho cặp `(maSuat, viTriGhe)`.

### B. Thư mục `Xslt/` (3 file XSLT 1.0)
13. `Xslt/VeXemPhim.xslt`: Biến đổi dữ liệu vé vừa bán thành trang HTML tấm vé xem phim (gộp các ghế trong cùng giao dịch, tính tổng tiền bằng `sum()`, hỗ trợ in `window.print()`).
14. `Xslt/BaoCaoDoanhThu.xslt`: Biến đổi dữ liệu vé theo kỳ thành báo cáo doanh thu HTML (dùng Muenchian Grouping nhóm theo phim, theo ngày, tính tổng cộng bằng `sum()` và xếp hạng Top 3 phim ăn khách).
15. `Xslt/DanhSachSuatChieu.xslt`: Biến đổi dữ liệu lịch chiếu thành bảng lịch chiếu HTML theo ngày hoặc toàn bộ (hiển thị giờ kết thúc dự kiến gồm 15 phút dọn rạp và số ghế trống/tổng ghế).

### C. Thư mục `Helpers/` (3 file C#)
16. `Helpers/XmlHelper.cs`: Lớp dùng chung xử lý đọc/ghi `XmlDocument`, tìm kiếm XPath, `EscapeXPathLiteral` chống XPath Injection, `SinhMaTuDong`, băm mật khẩu SHA-256, kiểm tra `ValidateXSD` và kiểm tra toàn vẹn khóa ngoại liên bảng.
17. `Helpers/Session.cs`: Lớp tĩnh lưu trữ thông tin phiên đăng nhập hiện tại (`TenDangNhap`, `MaNV`, `QuyenHan`, `LaQuanLy`, `DangXuat()`).
18. `Helpers/BaoCaoHelper.cs`: Lớp hỗ trợ gộp 5 file XML thành file XML trung gian (`TaoXmlTongHop`), biến đổi XSLT sang HTML UTF-8 (`XuatHtml`) và mở trình duyệt web (`MoTrinhDuyet`).

### D. Thư mục `Forms/` & Gốc dự án (19 file C#)
19. `Program.cs`: Điểm khởi chạy ứng dụng (`Main`), mở `frmDangNhap` đầu tiên.
20. `Forms/frmDangNhap.cs` & `Forms/frmDangNhap.Designer.cs`: Giao diện và xử lý đăng nhập, đối chiếu mật khẩu băm SHA-256, kiểm tra tính hợp lệ của tài khoản và khởi tạo `Session`.
21. `Forms/frmTrangChu.cs` & `Forms/frmTrangChu.Designer.cs`: Màn hình điều khiển chính có `MenuStrip`, các ô chức năng theo nhóm màu và phân quyền chặt chẽ giữa Quản lý (quyền 1) và Nhân viên (quyền 2).
22. `Forms/frmDoiMatKhau.cs` & `Forms/frmDoiMatKhau.Designer.cs`: Giao diện và xử lý đổi mật khẩu cho người dùng đang đăng nhập.
23. `Forms/frmPhim.cs` & `Forms/frmPhim.Designer.cs`: Quản lý danh mục phim (Thêm, Sửa, Xóa có kiểm tra ràng buộc suất chiếu, Tìm kiếm XPath, hiển thị ảnh áp phích).
24. `Forms/frmNhanVien.cs` & `Forms/frmNhanVien.Designer.cs`: Quản lý nhân viên (Thêm, Sửa, Xóa có kiểm tra ràng buộc tài khoản và vé đã bán, kiểm tra đủ 16 tuổi và SĐT 10 số).
25. `Forms/frmPhongChieu.cs` & `Forms/frmPhongChieu.Designer.cs`: Quản lý phòng chiếu (Tự động tính tổng ghế = hàng x cột, chặn sửa kích thước khi phòng đã bán vé, chặn xóa khi phòng đang có suất chiếu).
26. `Forms/frmTaiKhoan.cs` & `Forms/frmTaiKhoan.Designer.cs`: Quản lý tài khoản hệ thống (Thêm, Đổi quyền, Đặt lại mật khẩu, Xóa; bảo vệ không cho tự xóa chính mình hoặc xóa/hạ quyền admin cuối cùng).
27. `Forms/frmSuatChieu.cs` & `Forms/frmSuatChieu.Designer.cs`: Quản lý suất chiếu và Xem lịch chiếu, cài đặt **Thuật toán 1 (`KiemTraXungDot`)** kèm bộ 6 ca kiểm thử tự động trên bộ nhớ và xuất lịch chiếu HTML.
28. `Forms/frmBanVe.cs` & `Forms/frmBanVe.Designer.cs`: Nghiệp vụ bán vé xem phim, cài đặt **Thuật toán 2 (`VeSoDoGheDong`)** sinh sơ đồ ghế động co giãn, chống bán trùng ghế, ghi nguyên tử và tự động xuất vé HTML.
29. `Forms/frmBaoCaoDoanhThu.cs` & `Forms/frmBaoCaoDoanhThu.Designer.cs`: Thống kê nhanh số vé, doanh thu theo khoảng ngày và kết xuất báo cáo doanh thu HTML bằng XSLT (chỉ dành cho Quản lý).
30. `Form1.cs` & `Form1.Designer.cs`: Form kiểm tra kỹ thuật ban đầu của Giai đoạn 1 (kiểm tra XSD và toàn vẹn khóa ngoại).

---

## 6. Các hạn chế còn tồn tại & Hướng phát triển (Phục vụ Chương 3 Báo cáo)

1. **Hạn chế về truy xuất đồng thời trên file XML phẳng**:
   - Do dữ liệu lưu trực tiếp trên các file `.xml` cục bộ không qua hệ quản trị CSDL клиент-server, nếu triển khai qua mạng LAN cho nhiều quầy bán vé ghi cùng lúc vào `Ve.xml` có thể xảy ra tranh chấp khóa file (file lock) ở cấp hệ điều hành.
2. **Chưa có nghiệp vụ Hủy vé / Đổi ghế sau khi thanh toán**:
   - Hiện tại vé sau khi xác nhận bán sẽ khóa ghế vĩnh viễn cho suất chiếu đó (`Enabled = false`), chưa hỗ trợ quy trình hoàn tiền / hủy vé trên giao diện.
3. **In vé thông qua trình duyệt thay vì kết nối thẳng máy in nhiệt POS**:
   - Vé được kết xuất ra HTML bằng XSLT và dùng hộp thoại in của trình duyệt (`window.print()`), chưa tích hợp lệnh in ESC/POS trực tiếp ra máy in hóa đơn nhiệt chuyên dụng tại quầy.
4. **Thư mục `Temp/` lưu lịch sử các file kết xuất chưa tự động dọn dẹp định kỳ**:
   - Mỗi lần xuất vé hoặc báo cáo sẽ tạo một cặp file `.xml` trung gian và `.html` có gắn timestamp trong `Temp/` để tránh xung đột file đang mở; về lâu dài cần cơ chế tự động xóa các file tạm cũ hơn $N$ ngày.
5. **Hiệu năng khi dữ liệu lịch sử rất lớn**:
   - `XmlDocument` nạp toàn bộ cây DOM vào bộ nhớ RAM và chưa phân trang trên `DataGridView`, phù hợp tốt với quy mô đồ án và dữ liệu vài nghìn bản ghi nhưng sẽ giảm tốc độ nếu lưu trữ hàng trăm nghìn vé qua nhiều năm.

