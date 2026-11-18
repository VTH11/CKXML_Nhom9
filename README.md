# HỆ THỐNG QUẢN LÝ RẠP CHIẾU PHIM VÀ BÁN VÉ

Phần mềm quản lý rạp chiếu phim và bán vé tại quầy được xây dựng bằng C# Windows Forms, lưu trữ dữ liệu hoàn toàn bằng các tệp XML thay vì sử dụng hệ quản trị cơ sở dữ liệu SQL. Hệ thống phục vụ hai nhóm người dùng là **Quản lý rạp** (quản trị danh mục, xếp lịch chiếu, xem báo cáo doanh thu) và **Nhân viên bán vé** (xem lịch chiếu, chọn ghế trực quan và xuất vé cho khách). Đây là đồ án môn học **Công nghệ XML** (lớp học phần `126CNX02`) do **Nhóm 9** thực hiện dưới sự hướng dẫn của **Th.S Ngô Lê Quân** tại **Trường Đại học Sư phạm Kỹ thuật – Đại học Đà Nẵng**.

---

## 1. Chức năng chính

### Phân theo vai trò người dùng
- **Quản lý (Quyền 1)**:
  - Quản lý danh mục **Phim**, **Phòng chiếu**, **Nhân viên** và **Tài khoản hệ thống** (thêm, sửa, xóa, tìm kiếm, kiểm tra toàn vẹn dữ liệu).
  - Xếp lịch **Suất chiếu** theo phim, phòng và khung giờ.
  - Xem và kết xuất **Báo cáo doanh thu** theo khoảng thời gian (thống kê theo phim, theo ngày và xếp hạng Top 3 phim ăn khách).
  - Sử dụng đầy đủ các chức năng bán vé và xem lịch chiếu như nhân viên.
- **Nhân viên bán vé (Quyền 2)**:
  - Chỉ hiển thị chức năng **Bán vé xem phim**, **Xem lịch chiếu**, **Đổi mật khẩu** và **Đăng xuất** (toàn bộ chức năng quản trị và báo cáo doanh thu được ẩn và khóa truy cập).

### Hai thuật toán nghiệp vụ nổi bật
1. **Thuật toán 1 – Phát hiện xung đột lịch chiếu (`KiemTraXungDot`)**: Tự động tính giờ kết thúc dự kiến (`= Giờ bắt đầu + Thời lượng phim + 15 phút dọn rạp`), chặn các suất chiếu vắt qua nửa đêm hoặc bị chồng lấn khung giờ trong cùng một phòng chiếu vào cùng một ngày. Có tích hợp sẵn bộ 6 ca kiểm thử tự động ngay trên giao diện.
2. **Thuật toán 2 – Sinh sơ đồ ghế động (`VeSoDoGheDong`)**: Đọc số hàng và số cột của từng phòng chiếu để tự động vẽ lưới ghế (`A01`, `A02`...), phân biệt màu ghế trống (xám), ghế đang chọn (xanh lá) và ghế đã bán (đỏ), tự động tính tiền tức thời, chống bán trùng ghế và xuất vé HTML ngay sau khi xác nhận.

---

## 2. Công nghệ sử dụng

- **XML (`XmlDocument`)**: Lưu trữ toàn bộ 6 bảng dữ liệu của hệ thống dưới dạng cây tài liệu có cấu trúc trong thư mục `Data/`.
- **XSD (`XML Schema`)**: Định nghĩa khung chuẩn để kiểm tra kiểu dữ liệu, giới hạn giá trị, khóa chính (`xs:key`) và cặp giá trị duy nhất (`xs:unique`) trước mỗi lần lưu tệp XML.
- **XPath 1.0**: Truy vấn, lọc dữ liệu dây chuyền (Phim -> Ngày -> Suất chiếu) và tìm kiếm bản ghi không phân biệt chữ hoa/thường.
- **XSLT 1.0 (`XslCompiledTransform`)**: Biến đổi dữ liệu XML thành các trang HTML có định dạng bảng biểu để in vé xem phim, bảng lịch chiếu và báo cáo doanh thu.
- **C# Windows Forms (`.NET 10`)**: Xây dựng giao diện người dùng trên nền tảng Windows, xử lý nghiệp vụ và băm mật khẩu bằng thuật toán SHA-256.

---

## 3. Cấu trúc thư mục dự án

Kho mã nguồn (repository) được tổ chức thành **2 tầng**:
- **Tầng thư mục gốc**: Chứa tệp giải pháp `CKXML_Nhom9.slnx` (tệp dùng để mở dự án trong Visual Studio), `.gitignore` và `README.md`.
- **Tầng thư mục con `CKXML_Nhom9/`**: Chứa tệp cấu hình project `CKXML_Nhom9.csproj` cùng toàn bộ mã nguồn và dữ liệu.

```text
CKXML_Nhom9/                        # Thư mục gốc của kho mã nguồn
├── CKXML_Nhom9.slnx                # Tệp Solution (MỞ TỆP NÀY BẰNG VISUAL STUDIO)
├── .gitignore                      # Cấu hình loại trừ thư mục tạm (bin, obj, .vs)
├── .gitattributes                  # Cấu hình chuẩn hóa ký tự xuống dòng của Git
├── README.md                       # Tài liệu hướng dẫn sử dụng dự án
└── CKXML_Nhom9/                    # Thư mục chứa project C# Windows Forms
    ├── CKXML_Nhom9.csproj          # Tệp cấu hình project (TargetFramework: net10.0-windows)
    ├── Program.cs                  # Điểm khởi chạy ứng dụng (mở frmDangNhap đầu tiên)
    ├── Data/                       # Chứa 6 tệp dữ liệu .xml gốc và 6 tệp lược đồ .xsd
    │   ├── TaiKhoan.xml / .xsd     # Dữ liệu và lược đồ tài khoản đăng nhập
    │   ├── NhanVien.xml / .xsd     # Dữ liệu và lược đồ hồ sơ nhân viên
    │   ├── Phim.xml / .xsd         # Dữ liệu và lược đồ danh mục phim
    │   ├── PhongChieu.xml / .xsd   # Dữ liệu và lược đồ phòng chiếu
    │   ├── SuatChieu.xml / .xsd    # Dữ liệu và lược đồ lịch suất chiếu
    │   └── Ve.xml / .xsd           # Dữ liệu và lược đồ vé xem phim đã bán
    ├── Xslt/                       # Chứa 3 tệp mẫu XSLT 1.0 để xuất ra trang HTML
    │   ├── VeXemPhim.xslt          # Mẫu in vé xem phim sau khi bán vé
    │   ├── DanhSachSuatChieu.xslt  # Mẫu xuất bảng lịch chiếu phim
    │   └── BaoCaoDoanhThu.xslt     # Mẫu xuất báo cáo doanh thu (dùng Muenchian Grouping)
    ├── Helpers/                    # Các lớp xử lý dùng chung cho toàn hệ thống
    │   ├── XmlHelper.cs            # Đọc/ghi XML, tìm kiếm XPath, sinh mã tự động, kiểm tra XSD
    │   ├── BaoCaoHelper.cs         # Gộp XML trung gian, biến đổi XSLT sang HTML và mở trình duyệt
    │   └── Session.cs              # Lưu thông tin người dùng đang đăng nhập (mã NV, quyền hạn)
    └── Forms/                      # Các màn hình giao diện của ứng dụng
        ├── frmDangNhap.cs          # Màn hình đăng nhập hệ thống
        ├── frmTrangChu.cs          # Màn hình chính và phân quyền theo vai trò
        ├── frmDoiMatKhau.cs        # Màn hình đổi mật khẩu cá nhân
        ├── frmPhim.cs              # Màn hình quản lý danh mục phim
        ├── frmNhanVien.cs          # Màn hình quản lý nhân viên
        ├── frmPhongChieu.cs        # Màn hình quản lý phòng chiếu
        ├── frmTaiKhoan.cs          # Màn hình quản lý tài khoản đăng nhập
        ├── frmSuatChieu.cs         # Màn hình quản lý suất chiếu & xem lịch chiếu
        ├── frmBanVe.cs             # Màn hình bán vé & sơ đồ ghế động
        └── frmBaoCaoDoanhThu.cs    # Màn hình thống kê & xuất báo cáo doanh thu
```

### Bảng tra cứu nhanh: "Muốn tìm gì thì mở file nào?"

| Nội dung cần xem | Đường dẫn tệp | Tên hàm / Sự kiện chính trong code |
| :--- | :--- | :--- |
| **Thuật toán 1: Kiểm tra xung đột suất chiếu** | `CKXML_Nhom9/Forms/frmSuatChieu.cs` | `KiemTraXungDot()`, `CapNhatGioKetThucDuKien()` |
| **6 ca kiểm thử tự động cho Thuật toán 1** | `CKXML_Nhom9/Forms/frmSuatChieu.cs` | `btnKiemThuThuatToan_Click()`, `HienThiHopThoaiKetQuaKiemThu()` |
| **Thuật toán 2: Vẽ sơ đồ ghế động & Bán vé** | `CKXML_Nhom9/Forms/frmBanVe.cs` | `VeSoDoGheDong()`, `BtnGhe_Click()`, `btnXacNhanBanVe_Click()` |
| **Thao tác XML, XPath, kiểm tra XSD, SHA-256** | `CKXML_Nhom9/Helpers/XmlHelper.cs` | `LoadDataTable()`, `TimKiemXPath()`, `Them()`, `Sua()`, `Xoa()`, `SinhMaTuDong()`, `EscapeXPathLiteral()`, `ValidateXSD()`, `ValidateToanVenKhoaNgoai()`, `BamMatKhauSHA256()` |
| **Gộp XML trung gian & Xuất HTML bằng XSLT** | `CKXML_Nhom9/Helpers/BaoCaoHelper.cs` | `TaoXmlTongHop()`, `XuatHtml()`, `MoTrinhDuyet()` |
| **Phân quyền Quản lý / Nhân viên** | `CKXML_Nhom9/Forms/frmTrangChu.cs` | `ApDungPhanQuyen()`, `HienThiThongTinPhien()` |

---

## 4. Tóm tắt luồng dữ liệu

1. **Đọc và truy vấn**: Ứng dụng nạp các tệp `.xml` từ thư mục `Data/` vào bộ nhớ bằng `XmlDocument` và lọc dữ liệu thông qua biểu thức **XPath**.
2. **Kiểm tra và ghi**: Khi thêm, sửa hoặc xóa, dữ liệu mới được kiểm tra ràng buộc khóa ngoại bằng C# và đối chiếu với tệp lược đồ `.xsd` tương ứng; chỉ khi hợp lệ hoàn toàn mới được ghi xuống tệp `.xml`.
3. **Kết xuất báo cáo và vé**: Do XSLT 1.0 không hỗ trợ nối trực tiếp nhiều tệp, lớp `BaoCaoHelper` gộp dữ liệu từ 5 tệp XML gốc thành một tệp XML trung gian trong thư mục `Temp/`, sau đó dùng `XslCompiledTransform` kết hợp với các tệp `.xslt` trong `Xslt/` để xuất ra tệp `.html` (chuẩn UTF-8) và mở trên trình duyệt web.

---

## 5. Hướng dẫn cài đặt và chạy chương trình

### Yêu cầu môi trường
- Hệ điều hành Windows 10 hoặc Windows 11.
- **.NET 10.0 SDK** (hoặc **.NET Desktop Runtime 10.0** vì dự án cấu hình `<TargetFramework>net10.0-windows</TargetFramework>` trong `CKXML_Nhom9.csproj`).
- **Visual Studio bản mới có hỗ trợ .NET 10**. Không cần cài đặt SQL Server.

### Các bước chạy từng bước
1. Tải hoặc clone kho mã nguồn về máy tính.
2. Mở tệp **`CKXML_Nhom9.slnx`** ở thư mục gốc bằng Visual Studio.
3. Nhấn phím **F5** (hoặc nút **Start**) để biên dịch và khởi chạy ứng dụng.
4. Đăng nhập vào hệ thống bằng một trong hai tài khoản mẫu có sẵn dưới đây:

| Tên đăng nhập | Mật khẩu gốc | Quyền hạn | Mã NV | Ghi chú |
| :--- | :--- | :--- | :--- | :--- |
| **`admin`** | **`admin123`** | `1` (Quản lý) | `NV001` | Có toàn quyền quản trị, xếp lịch, bán vé, xem báo cáo |
| **`nhanvien1`** | **`123456`** | `2` (Nhân viên) | `NV002` | Chỉ có quyền Bán vé xem phim và Xem lịch chiếu |

*(Lưu ý: Dữ liệu suất chiếu và vé mẫu được tạo ở ngày **12/11/2026** và **13/11/2026** để thuận tiện kiểm thử ngay khi mở chương trình).*

---

## 6. Nơi lưu dữ liệu khi chạy & Cách đặt lại dữ liệu mẫu

- **Nơi lưu dữ liệu khi chạy**: Khi biên dịch, Visual Studio tự động sao chép thư mục `Data/` và `Xslt/` vào thư mục chạy thực tế tại:
  - Dữ liệu XML & XSD đang dùng: `CKXML_Nhom9\bin\Debug\net10.0-windows\Data\`
  - Tệp vé và báo cáo HTML xuất ra: `CKXML_Nhom9\bin\Debug\net10.0-windows\Temp\`
- **Cách đặt lại (Reset) dữ liệu mẫu về ban đầu**:
  1. Đóng chương trình đang chạy.
  2. Xóa thư mục `CKXML_Nhom9\bin\Debug\net10.0-windows\Data`.
  3. Chạy lại chương trình (**F5** hoặc **Build -> Rebuild Solution**), Visual Studio sẽ tự động chép lại bộ dữ liệu mẫu gốc từ thư mục `CKXML_Nhom9\Data\`.

---

## 7. Hạn chế và hướng phát triển

1. **Truy xuất đồng thời trên tệp XML cục bộ**: Vì lưu trữ trực tiếp trên tệp `.xml` không qua hệ quản trị cơ sở dữ liệu tập trung, nếu nhiều máy tính cùng ghi vào `Ve.xml` qua mạng nội bộ có thể xảy ra xung đột khóa tệp.
2. **Chưa có chức năng Hủy vé / Đổi ghế**: Vé sau khi xác nhận bán sẽ khóa ghế vĩnh viễn cho suất chiếu đó, chưa hỗ trợ quy trình hoàn vé trên giao diện.
3. **In vé thông qua trình duyệt web**: Hệ thống xuất vé ra trang HTML và sử dụng chức năng in của trình duyệt (`window.print()`), chưa kết nối trực tiếp với máy in nhiệt chuyên dụng tại quầy.
4. **Thư mục `Temp/` chưa tự động dọn dẹp**: Các tệp XML trung gian và HTML xuất ra được gắn nhãn thời gian riêng để tránh khóa tệp nhưng chưa có cơ chế tự động xóa các tệp tạm cũ định kỳ.
5. **Hiệu năng khi dữ liệu lịch sử quá lớn**: `XmlDocument` nạp toàn bộ cấu trúc tệp vào bộ nhớ RAM và chưa phân trang trên bảng dữ liệu, phù hợp với quy mô đồ án nhưng cần tối ưu nếu lưu trữ hàng trăm nghìn vé.

---

## 8. Thông tin nhóm thực hiện (Nhóm 9)

- **Sinh viên thực hiện**:
  1. **Nguyễn Tấn Thắng** – Mã sinh viên: `2415141122119`
  2. **Văn Thanh Hoàng**
- **Học phần**: Công nghệ XML – **Lớp học phần**: `126CNX02`
- **Giảng viên hướng dẫn**: **Th.S Ngô Lê Quân**
- **Trường**: Trường Đại học Sư phạm Kỹ thuật – Đại học Đà Nẵng
