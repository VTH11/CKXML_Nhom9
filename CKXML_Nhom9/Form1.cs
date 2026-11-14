using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9
{
    public partial class Form1 : Form
    {
        private TextBox txtKetQua = null!;
        private DataGridView dgvDuLieu = null!;
        private ComboBox cbbChonBang = null!;
        private Button btnKiemTraXSD = null!;

        public Form1()
        {
            InitializeComponent();
            KhoiTaoGiaoDienTestGD1();
        }

        /// <summary>
        /// Giao diện kiểm tra nhanh Giai đoạn 1 (XSD, dữ liệu mẫu, XmlHelper).
        /// </summary>
        private void KhoiTaoGiaoDienTestGD1()
        {
            this.Text = "Kiểm tra Giai đoạn 1 - Hệ thống Quản lý Rạp Chiếu Phim & Bán Vé (XML)";
            this.Size = new Size(920, 620);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblChon = new Label
            {
                Text = "Chọn file XML:",
                Location = new Point(15, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            cbbChonBang = new ComboBox
            {
                Location = new Point(130, 14),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cbbChonBang.Items.AddRange(new object[]
            {
                "TaiKhoan.xml", "NhanVien.xml", "Phim.xml", "PhongChieu.xml", "SuatChieu.xml", "Ve.xml"
            });
            cbbChonBang.SelectedIndexChanged += (s, e) =>
            {
                if (cbbChonBang.SelectedItem is string tenFile)
                {
                    dgvDuLieu.DataSource = XmlHelper.LoadDataTable(tenFile);
                }
            };

            btnKiemTraXSD = new Button
            {
                Text = "Chạy kiểm tra lại XSD & XmlHelper",
                Location = new Point(350, 12),
                Size = new Size(250, 32),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnKiemTraXSD.Click += (s, e) => ChayKiemTraGD1();

            txtKetQua = new TextBox
            {
                Location = new Point(15, 55),
                Size = new Size(870, 230),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 10F)
            };

            dgvDuLieu = new DataGridView
            {
                Location = new Point(15, 300),
                Size = new Size(870, 265),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                Font = new Font("Segoe UI", 9.5F)
            };

            this.Controls.Add(lblChon);
            this.Controls.Add(cbbChonBang);
            this.Controls.Add(btnKiemTraXSD);
            this.Controls.Add(txtKetQua);
            this.Controls.Add(dgvDuLieu);

            this.Load += (s, e) =>
            {
                cbbChonBang.SelectedIndex = 0;
                ChayKiemTraGD1();
            };
        }

        private void ChayKiemTraGD1()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== KẾT QUẢ KIỂM TRA GIAI ĐOẠN 1 ===");

            string[,] capFiles =
            {
                { "TaiKhoan.xml", "TaiKhoan.xsd" },
                { "NhanVien.xml", "NhanVien.xsd" },
                { "Phim.xml", "Phim.xsd" },
                { "PhongChieu.xml", "PhongChieu.xsd" },
                { "SuatChieu.xml", "SuatChieu.xsd" },
                { "Ve.xml", "Ve.xsd" }
            };

            for (int i = 0; i < capFiles.GetLength(0); i++)
            {
                string xml = capFiles[i, 0];
                string xsd = capFiles[i, 1];
                List<string> loi = XmlHelper.ValidateXSD(xml, xsd);
                int soDong = XmlHelper.LoadDataTable(xml).Rows.Count;
                if (loi.Count == 0)
                    sb.AppendLine($"[OK] {xml,-15} hợp lệ với {xsd,-15} ({soDong} bản ghi)");
                else
                    sb.AppendLine($"[LỖI] {xml}: {string.Join("; ", loi)}");
            }

            List<string> loiFK = XmlHelper.ValidateToanVenKhoaNgoai();
            sb.AppendLine(loiFK.Count == 0
                ? "[OK] Kiểm tra toàn vẹn khóa ngoại liên bảng bằng HashSet C#: HỢP LỆ"
                : "[LỖI FK] " + string.Join("; ", loiFK));

            sb.AppendLine();
            sb.AppendLine("--- Kiểm tra SinhMaTuDong (tính theo mã lớn nhất) ---");
            sb.AppendLine($"- Mã Nhân viên tiếp theo : {XmlHelper.SinhMaTuDong("NV", "NhanVien.xml", "maNV")}");
            sb.AppendLine($"- Mã Phim tiếp theo      : {XmlHelper.SinhMaTuDong("P", "Phim.xml", "maPhim")}");
            sb.AppendLine($"- Mã Phòng tiếp theo     : {XmlHelper.SinhMaTuDong("PC", "PhongChieu.xml", "maPhong")}");
            sb.AppendLine($"- Mã Suất chiếu tiếp theo: {XmlHelper.SinhMaTuDong("SC", "SuatChieu.xml", "maSuat")}");
            sb.AppendLine($"- Mã Vé tiếp theo        : {XmlHelper.SinhMaTuDong("V", "Ve.xml", "maVe")}");

            txtKetQua.Text = sb.ToString();
        }
    }
}
