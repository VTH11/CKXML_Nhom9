using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;
using CKXML_Nhom9.Helpers;

namespace CKXML_Nhom9.Forms
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        private bool _dangKhoiTao = false;

        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void frmBaoCaoDoanhThu_Load(object? sender, EventArgs e)
        {
            // Bảo vệ phân quyền: CHỈ Quản lý (quyền 1) mới được phép truy cập
            if (!Session.LaQuanLy)
            {
                MessageBox.Show(
                    "Bạn không có quyền truy cập chức năng Báo cáo doanh thu (chỉ dành cho Quản lý)!",
                    "Từ chối truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _dangKhoiTao = true;
            DateTime homNay = DateTime.Today;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = new DateTime(homNay.Year, homNay.Month, DateTime.DaysInMonth(homNay.Year, homNay.Month));
            _dangKhoiTao = false;

            CapNhatThongKeNhanh();
        }

        private void KhoangNgay_ValueChanged(object? sender, EventArgs e)
        {
            if (_dangKhoiTao)
                return;
            CapNhatThongKeNhanh();
        }

        /// <summary>
        /// Tính nhanh tổng số vé, tổng doanh thu và danh sách vé bán trong khoảng ngày hiển thị ngay trên form.
        /// </summary>
        private void CapNhatThongKeNhanh()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date;

                string tuXml = tuNgay.ToString("yyyy-MM-dd");
                string denXml = denNgay.ToString("yyyy-MM-dd");

                string fileTam = BaoCaoHelper.TaoXmlTongHop(tuXml, denXml);
                XmlDocument doc = new XmlDocument();
                doc.Load(fileTam);

                XmlNodeList? dsVe = doc.SelectNodes("/DuLieuTongHop/DanhSachVe/Ve");

                DataTable dt = new DataTable();
                dt.Columns.Add("maVe", typeof(string));
                dt.Columns.Add("ngayBanVN", typeof(string));
                dt.Columns.Add("tenPhim", typeof(string));
                dt.Columns.Add("tenPhong", typeof(string));
                dt.Columns.Add("maSuat", typeof(string));
                dt.Columns.Add("viTriGhe", typeof(string));
                dt.Columns.Add("tenNV", typeof(string));
                dt.Columns.Add("giaVeHienThi", typeof(string));

                int tongVe = 0;
                decimal tongTien = 0;

                if (dsVe != null)
                {
                    foreach (XmlNode v in dsVe)
                    {
                        tongVe++;
                        string giaStr = v.SelectSingleNode("giaVe")?.InnerText.Trim() ?? "0";
                        decimal.TryParse(giaStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal gia);
                        tongTien += gia;

                        dt.Rows.Add(
                            v.SelectSingleNode("maVe")?.InnerText,
                            v.SelectSingleNode("ngayBanVN")?.InnerText,
                            v.SelectSingleNode("tenPhim")?.InnerText,
                            v.SelectSingleNode("tenPhong")?.InnerText,
                            v.SelectSingleNode("maSuat")?.InnerText,
                            v.SelectSingleNode("viTriGhe")?.InnerText,
                            v.SelectSingleNode("tenNV")?.InnerText,
                            frmBanVe.DinhDangTien(gia));
                    }
                }

                lblTongSoVe.Text = $"{tongVe} vé";
                lblTongDoanhThu.Text = frmBanVe.DinhDangTien(tongTien);

                dgvChiTietVe.DataSource = dt;
                if (dgvChiTietVe.Columns.Contains("maVe"))
                    dgvChiTietVe.Columns["maVe"]!.HeaderText = "Mã vé";
                if (dgvChiTietVe.Columns.Contains("ngayBanVN"))
                    dgvChiTietVe.Columns["ngayBanVN"]!.HeaderText = "Thời điểm bán";
                if (dgvChiTietVe.Columns.Contains("tenPhim"))
                    dgvChiTietVe.Columns["tenPhim"]!.HeaderText = "Bộ phim";
                if (dgvChiTietVe.Columns.Contains("tenPhong"))
                    dgvChiTietVe.Columns["tenPhong"]!.HeaderText = "Phòng chiếu";
                if (dgvChiTietVe.Columns.Contains("maSuat"))
                    dgvChiTietVe.Columns["maSuat"]!.HeaderText = "Mã suất";
                if (dgvChiTietVe.Columns.Contains("viTriGhe"))
                    dgvChiTietVe.Columns["viTriGhe"]!.HeaderText = "Ghế";
                if (dgvChiTietVe.Columns.Contains("tenNV"))
                    dgvChiTietVe.Columns["tenNV"]!.HeaderText = "Nhân viên bán";
                if (dgvChiTietVe.Columns.Contains("giaVeHienThi"))
                    dgvChiTietVe.Columns["giaVeHienThi"]!.HeaderText = "Giá vé";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính thống kê doanh thu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemBaoCao_Click(object? sender, EventArgs e)
        {
            if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu ('Từ ngày') không được lớn hơn ngày kết thúc ('Đến ngày')!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTuNgay.Focus();
                return;
            }

            try
            {
                BaoCaoHelper.XuatVaMoBaoCaoDoanhThu(dtpTuNgay.Value.Date, dtpDenNgay.Value.Date);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo doanh thu HTML bằng XSLT:\n" + ex.Message, "Lỗi XSLT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}

