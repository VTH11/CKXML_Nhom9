<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes" />
  <xsl:decimal-format name="vn" grouping-separator="." decimal-separator="," />

  <!-- Tham số truyền từ C# qua XsltArgumentList: danh sách mã vé dạng ,V0007,V0008, -->
  <xsl:param name="dsMaVe" select="''" />
  <xsl:param name="tenRap" select="'RẠP CHIẾU PHIM CINEMA XML - NHÓM 9'" />

  <xsl:template match="/">
    <xsl:variable name="veVuaBan" select="/DuLieuTongHop/DanhSachVe/Ve[contains($dsMaVe, concat(',', maVe, ','))]" />
    <html>
      <head>
        <meta charset="utf-8" />
        <title>Vé Xem Phim - Cinema XML</title>
        <style>
          body {
            font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
            background-color: #f1f5f9;
            color: #1e293b;
            margin: 0;
            padding: 25px;
          }
          .ticket-wrapper {
            max-width: 520px;
            margin: 0 auto;
          }
          .action-bar {
            text-align: center;
            margin-bottom: 16px;
          }
          .btn-print {
            background-color: #16a34a;
            color: #ffffff;
            border: none;
            padding: 10px 24px;
            font-size: 14px;
            font-weight: bold;
            border-radius: 6px;
            cursor: pointer;
          }
          .btn-print:hover {
            background-color: #15803d;
          }
          .ticket-card {
            background-color: #ffffff;
            border: 2px dashed #334155;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
          }
          .ticket-header {
            background-color: #1e293b;
            color: #ffffff;
            text-align: center;
            padding: 16px 20px;
          }
          .ticket-header h1 {
            margin: 0;
            font-size: 18px;
            letter-spacing: 1px;
          }
          .ticket-header p {
            margin: 4px 0 0 0;
            font-size: 13px;
            color: #cbd5e1;
          }
          .ticket-body {
            padding: 20px 24px;
          }
          .movie-title {
            font-size: 20px;
            font-weight: bold;
            color: #1d4ed8;
            margin: 0 0 14px 0;
            text-align: center;
            text-transform: uppercase;
          }
          .info-grid {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 16px;
          }
          .info-grid td {
            padding: 5px 4px;
            font-size: 14px;
          }
          .info-label {
            color: #64748b;
            width: 38%;
          }
          .info-value {
            font-weight: 600;
            color: #0f172a;
          }
          table.seat-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            margin-bottom: 16px;
          }
          table.seat-table th, table.seat-table td {
            border: 1px solid #cbd5e1;
            padding: 8px 10px;
            font-size: 13.5px;
          }
          table.seat-table th {
            background-color: #f8fafc;
            font-weight: bold;
            text-align: center;
          }
          .text-center {
            text-align: center;
          }
          .text-right {
            text-align: right;
          }
          .total-row td {
            background-color: #f0fdf4;
            font-weight: bold;
            font-size: 15px;
            color: #dc2626;
          }
          .ticket-footer {
            border-top: 1px dashed #94a3b8;
            padding: 12px 24px;
            background-color: #f8fafc;
            font-size: 12.5px;
            color: #475569;
            text-align: center;
          }
          .empty-box {
            padding: 30px;
            text-align: center;
            color: #dc2626;
            font-weight: bold;
          }
          @media print {
            body {
              background-color: #ffffff;
              padding: 0;
            }
            .no-print {
              display: none !important;
            }
            .ticket-card {
              box-shadow: none;
              border: 2px dashed #000000;
            }
          }
        </style>
      </head>
      <body>
        <div class="ticket-wrapper">
          <div class="action-bar no-print">
            <button class="btn-print" onclick="window.print()">In vé xem phim (Print)</button>
          </div>

          <div class="ticket-card">
            <div class="ticket-header">
              <h1><xsl:value-of select="$tenRap" /></h1>
              <p>VÉ XEM PHIM KIÊM HÓA ĐƠN THANH TOÁN</p>
            </div>

            <xsl:choose>
              <xsl:when test="count($veVuaBan) &gt; 0">
                <xsl:variable name="veDau" select="$veVuaBan[1]" />
                <div class="ticket-body">
                  <div class="movie-title">
                    <xsl:value-of select="$veDau/tenPhim" />
                  </div>

                  <table class="info-grid">
                    <tr>
                      <td class="info-label">Phòng chiếu:</td>
                      <td class="info-value"><xsl:value-of select="$veDau/tenPhong" /> (<xsl:value-of select="$veDau/maPhong" />)</td>
                    </tr>
                    <tr>
                      <td class="info-label">Suất chiếu:</td>
                      <td class="info-value"><xsl:value-of select="$veDau/maSuat" /> | <xsl:value-of select="$veDau/gioBatDau" /> - <xsl:value-of select="$veDau/gioKetThuc" /></td>
                    </tr>
                    <tr>
                      <td class="info-label">Ngày chiếu:</td>
                      <td class="info-value"><xsl:value-of select="$veDau/ngayChieuVN" /></td>
                    </tr>
                    <tr>
                      <td class="info-label">Danh sách ghế:</td>
                      <td class="info-value">
                        <xsl:for-each select="$veVuaBan">
                          <xsl:sort select="viTriGhe" />
                          <xsl:value-of select="viTriGhe" />
                          <xsl:if test="position() != last()">, </xsl:if>
                        </xsl:for-each>
                        (<xsl:value-of select="count($veVuaBan)" /> ghế)
                      </td>
                    </tr>
                    <tr>
                      <td class="info-label">Nhân viên bán:</td>
                      <td class="info-value"><xsl:value-of select="$veDau/tenNV" /> (<xsl:value-of select="$veDau/maNV" />)</td>
                    </tr>
                    <tr>
                      <td class="info-label">Thời điểm bán:</td>
                      <td class="info-value"><xsl:value-of select="$veDau/ngayBanVN" /></td>
                    </tr>
                  </table>

                  <table class="seat-table">
                    <thead>
                      <tr>
                        <th style="width: 14%;">STT</th>
                        <th style="width: 28%;">Mã vé</th>
                        <th style="width: 26%;">Vị trí ghế</th>
                        <th style="width: 32%;">Đơn giá</th>
                      </tr>
                    </thead>
                    <tbody>
                      <xsl:for-each select="$veVuaBan">
                        <xsl:sort select="viTriGhe" />
                        <tr>
                          <td class="text-center"><xsl:value-of select="position()" /></td>
                          <td class="text-center"><strong><xsl:value-of select="maVe" /></strong></td>
                          <td class="text-center"><strong><xsl:value-of select="viTriGhe" /></strong></td>
                          <td class="text-right">
                            <xsl:value-of select="format-number(giaVe, '#.##0', 'vn')" />đ
                          </td>
                        </tr>
                      </xsl:for-each>
                      <tr class="total-row">
                        <td colspan="3" class="text-right">TỔNG CỘNG (<xsl:value-of select="count($veVuaBan)" /> vé):</td>
                        <td class="text-right">
                          <xsl:value-of select="format-number(sum($veVuaBan/giaVe), '#.##0', 'vn')" />đ
                        </td>
                      </tr>
                    </tbody>
                  </table>
                </div>

                <div class="ticket-footer">
                  Quý khách vui lòng vào phòng chiếu trước giờ chiếu 10 phút.<br />
                  Vé đã mua xin miễn đổi hoặc trả lại. Chúc quý khách xem phim vui vẻ!
                </div>
              </xsl:when>
              <xsl:otherwise>
                <div class="empty-box">
                  Không tìm thấy dữ liệu vé tương ứng với mã giao dịch vừa chọn.
                </div>
              </xsl:otherwise>
            </xsl:choose>
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

