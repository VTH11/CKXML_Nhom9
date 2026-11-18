<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes" />
  <xsl:decimal-format name="vn" grouping-separator="." decimal-separator="," />

  <!-- Tham số truyền qua XsltArgumentList -->
  <xsl:param name="tuNgay" select="''" />
  <xsl:param name="denNgay" select="''" />
  <xsl:param name="tuNgayVN" select="$tuNgay" />
  <xsl:param name="denNgayVN" select="$denNgay" />
  <xsl:param name="ngayLap" select="''" />

  <!-- Muenchian Grouping: Khóa nhóm theo Mã phim và theo Ngày bán -->
  <xsl:key name="kVeTheoPhim" match="/DuLieuTongHop/DanhSachVe/Ve" use="maPhim" />
  <xsl:key name="kVeTheoNgay" match="/DuLieuTongHop/DanhSachVe/Ve" use="ngayBanDate" />

  <xsl:template match="/">
    <xsl:variable name="dsVe" select="/DuLieuTongHop/DanhSachVe/Ve" />
    <xsl:variable name="tongSoVe" select="count($dsVe)" />
    <xsl:variable name="tongDoanhThu" select="sum($dsVe/giaVe)" />

    <html>
      <head>
        <meta charset="utf-8" />
        <title>Báo Cáo Doanh Thu Rạp Chiếu Phim</title>
        <style>
          body {
            font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
            background-color: #f1f5f9;
            color: #1e293b;
            margin: 0;
            padding: 24px;
          }
          .container {
            max-width: 960px;
            margin: 0 auto;
            background-color: #ffffff;
            border-radius: 10px;
            padding: 28px 34px;
            box-shadow: 0 4px 14px rgba(0,0,0,0.08);
          }
          .header {
            text-align: center;
            border-bottom: 2px solid #1e293b;
            padding-bottom: 16px;
            margin-bottom: 22px;
          }
          .header h1 {
            margin: 0;
            font-size: 22px;
            color: #1e293b;
            text-transform: uppercase;
          }
          .header p {
            margin: 6px 0 0 0;
            font-size: 14px;
            color: #475569;
          }
          .summary-cards {
            display: flex;
            gap: 18px;
            margin-bottom: 24px;
          }
          .card {
            flex: 1;
            background-color: #f8fafc;
            border: 1px solid #cbd5e1;
            border-left: 5px solid #2563eb;
            border-radius: 6px;
            padding: 14px 18px;
          }
          .card.green {
            border-left-color: #16a34a;
          }
          .card .label {
            font-size: 13px;
            color: #64748b;
            font-weight: 600;
          }
          .card .value {
            font-size: 22px;
            font-weight: bold;
            color: #0f172a;
            margin-top: 4px;
          }
          h2 {
            font-size: 16.5px;
            color: #1e3a8a;
            margin: 24px 0 10px 0;
            border-left: 4px solid #2563eb;
            padding-left: 10px;
          }
          table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 18px;
          }
          th, td {
            border: 1px solid #cbd5e1;
            padding: 9px 12px;
            font-size: 14px;
          }
          th {
            background-color: #1e293b;
            color: #ffffff;
            font-weight: bold;
            text-align: center;
          }
          tr:nth-child(even) td {
            background-color: #f8fafc;
          }
          .text-center {
            text-align: center;
          }
          .text-right {
            text-align: right;
          }
          .total-row td {
            background-color: #fef3c7 !important;
            font-weight: bold;
            font-size: 14.5px;
            color: #b91c1c;
          }
          .badge-top {
            display: inline-block;
            background-color: #d97706;
            color: #ffffff;
            padding: 2px 10px;
            border-radius: 12px;
            font-size: 12.5px;
            font-weight: bold;
          }
          .empty-message {
            background-color: #fff1f2;
            border: 1px solid #fecdd3;
            color: #be123c;
            padding: 28px;
            text-align: center;
            border-radius: 8px;
            font-size: 16px;
            font-weight: bold;
            margin: 20px 0;
          }
          .action-bar {
            text-align: right;
            margin-bottom: 12px;
          }
          .btn-print {
            background-color: #2563eb;
            color: #ffffff;
            border: none;
            padding: 8px 18px;
            font-size: 13.5px;
            font-weight: bold;
            border-radius: 5px;
            cursor: pointer;
          }
          .footer {
            margin-top: 24px;
            text-align: right;
            font-size: 13px;
            color: #64748b;
          }
          @media print {
            body { background-color: #ffffff; padding: 0; }
            .container { box-shadow: none; padding: 0; }
            .no-print { display: none !important; }
          }
        </style>
      </head>
      <body>
        <div class="container">
          <div class="action-bar no-print">
            <button class="btn-print" onclick="window.print()">In báo cáo (Print)</button>
          </div>

          <div class="header">
            <h1>BÁO CÁO DOANH THU BÁN VÉ RẠP CHIẾU PHIM</h1>
            <p>
              Kỳ báo cáo: Từ ngày <strong><xsl:value-of select="$tuNgayVN" /></strong>
              đến ngày <strong><xsl:value-of select="$denNgayVN" /></strong>
            </p>
          </div>

          <xsl:choose>
            <xsl:when test="$tongSoVe &gt; 0">
              <!-- Tóm tắt tổng quan -->
              <div class="summary-cards">
                <div class="card">
                  <div class="label">TỔNG SỐ VÉ ĐÃ BÁN</div>
                  <div class="value"><xsl:value-of select="$tongSoVe" /> vé</div>
                </div>
                <div class="card green">
                  <div class="label">TỔNG DOANH THU (sum)</div>
                  <div class="value">
                    <xsl:value-of select="format-number($tongDoanhThu, '#.##0', 'vn')" />đ
                  </div>
                </div>
              </div>

              <!-- 1. Bảng Doanh thu theo Phim (xếp theo doanh thu giảm dần) -->
              <h2>1. Thống kê doanh thu theo Phim (Xếp theo doanh thu giảm dần)</h2>
              <table>
                <thead>
                  <tr>
                    <th style="width: 8%;">STT</th>
                    <th style="width: 14%;">Mã phim</th>
                    <th style="width: 38%;">Tên bộ phim</th>
                    <th style="width: 16%;">Số vé bán</th>
                    <th style="width: 24%;">Doanh thu</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="$dsVe[generate-id() = generate-id(key('kVeTheoPhim', maPhim)[1])]">
                    <xsl:sort select="sum(key('kVeTheoPhim', maPhim)/giaVe)" data-type="number" order="descending" />
                    <xsl:variable name="nhomPhim" select="key('kVeTheoPhim', maPhim)" />
                    <tr>
                      <td class="text-center"><xsl:value-of select="position()" /></td>
                      <td class="text-center"><xsl:value-of select="maPhim" /></td>
                      <td><strong><xsl:value-of select="tenPhim" /></strong></td>
                      <td class="text-center"><xsl:value-of select="count($nhomPhim)" /></td>
                      <td class="text-right">
                        <xsl:value-of select="format-number(sum($nhomPhim/giaVe), '#.##0', 'vn')" />đ
                      </td>
                    </tr>
                  </xsl:for-each>
                  <tr class="total-row">
                    <td colspan="3" class="text-right">TỔNG CỘNG:</td>
                    <td class="text-center"><xsl:value-of select="count($dsVe)" /></td>
                    <td class="text-right">
                      <xsl:value-of select="format-number(sum($dsVe/giaVe), '#.##0', 'vn')" />đ
                    </td>
                  </tr>
                </tbody>
              </table>

              <!-- 2. Bảng Doanh thu theo Ngày -->
              <h2>2. Thống kê doanh thu theo Ngày bán</h2>
              <table>
                <thead>
                  <tr>
                    <th style="width: 10%;">STT</th>
                    <th style="width: 35%;">Ngày bán (dd/MM/yyyy)</th>
                    <th style="width: 25%;">Số vé bán</th>
                    <th style="width: 30%;">Doanh thu</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="$dsVe[generate-id() = generate-id(key('kVeTheoNgay', ngayBanDate)[1])]">
                    <xsl:sort select="ngayBanDate" data-type="text" order="ascending" />
                    <xsl:variable name="nhomNgay" select="key('kVeTheoNgay', ngayBanDate)" />
                    <tr>
                      <td class="text-center"><xsl:value-of select="position()" /></td>
                      <td class="text-center"><strong><xsl:value-of select="ngayBanDateVN" /></strong></td>
                      <td class="text-center"><xsl:value-of select="count($nhomNgay)" /></td>
                      <td class="text-right">
                        <xsl:value-of select="format-number(sum($nhomNgay/giaVe), '#.##0', 'vn')" />đ
                      </td>
                    </tr>
                  </xsl:for-each>
                  <tr class="total-row">
                    <td colspan="2" class="text-right">TỔNG CỘNG:</td>
                    <td class="text-center"><xsl:value-of select="count($dsVe)" /></td>
                    <td class="text-right">
                      <xsl:value-of select="format-number(sum($dsVe/giaVe), '#.##0', 'vn')" />đ
                    </td>
                  </tr>
                </tbody>
              </table>

              <!-- 3. Danh sách Phim ăn khách (Top 3 theo số vé) -->
              <h2>3. Danh sách "Phim ăn khách" (Top 3 bộ phim có số vé bán cao nhất)</h2>
              <table>
                <thead>
                  <tr>
                    <th style="width: 14%;">Thứ hạng</th>
                    <th style="width: 14%;">Mã phim</th>
                    <th style="width: 36%;">Tên bộ phim</th>
                    <th style="width: 16%;">Số vé bán</th>
                    <th style="width: 20%;">Doanh thu</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="$dsVe[generate-id() = generate-id(key('kVeTheoPhim', maPhim)[1])]">
                    <xsl:sort select="count(key('kVeTheoPhim', maPhim))" data-type="number" order="descending" />
                    <xsl:sort select="sum(key('kVeTheoPhim', maPhim)/giaVe)" data-type="number" order="descending" />
                    <xsl:if test="position() &lt;= 3">
                      <xsl:variable name="nhomTop" select="key('kVeTheoPhim', maPhim)" />
                      <tr>
                        <td class="text-center">
                          <span class="badge-top">TOP <xsl:value-of select="position()" /></span>
                        </td>
                        <td class="text-center"><xsl:value-of select="maPhim" /></td>
                        <td><strong><xsl:value-of select="tenPhim" /></strong></td>
                        <td class="text-center"><strong><xsl:value-of select="count($nhomTop)" /> vé</strong></td>
                        <td class="text-right">
                          <xsl:value-of select="format-number(sum($nhomTop/giaVe), '#.##0', 'vn')" />đ
                        </td>
                      </tr>
                    </xsl:if>
                  </xsl:for-each>
                </tbody>
              </table>
            </xsl:when>
            <xsl:otherwise>
              <div class="empty-message">
                Không có dữ liệu bán vé trong khoảng thời gian từ <xsl:value-of select="$tuNgayVN" /> đến <xsl:value-of select="$denNgayVN" />.
              </div>
            </xsl:otherwise>
          </xsl:choose>

          <div class="footer">
            Thời điểm kết xuất báo cáo: <xsl:value-of select="$ngayLap" />
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

