<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" indent="yes" />
  <xsl:decimal-format name="vn" grouping-separator="." decimal-separator="," />

  <!-- Tham số truyền qua XsltArgumentList: ngayChieu ('TATCA' hoặc 'yyyy-MM-dd') -->
  <xsl:param name="ngayChieu" select="'TATCA'" />
  <xsl:param name="ngayChieuVN" select="'Tất cả các ngày'" />
  <xsl:param name="ngayLap" select="''" />

  <xsl:template match="/">
    <xsl:variable name="dsSuat" select="/DuLieuTongHop/DanhSachSuatChieu/SuatChieu[$ngayChieu = 'TATCA' or ngayChieu = $ngayChieu]" />

    <html>
      <head>
        <meta charset="utf-8" />
        <title>Danh Sách Suất Chiếu - Cinema XML</title>
        <style>
          body {
            font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
            background-color: #f1f5f9;
            color: #1e293b;
            margin: 0;
            padding: 24px;
          }
          .container {
            max-width: 1000px;
            margin: 0 auto;
            background-color: #ffffff;
            border-radius: 10px;
            padding: 26px 32px;
            box-shadow: 0 4px 14px rgba(0,0,0,0.08);
          }
          .header {
            text-align: center;
            border-bottom: 2px solid #1e293b;
            padding-bottom: 14px;
            margin-bottom: 20px;
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
          table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 12px;
          }
          th, td {
            border: 1px solid #cbd5e1;
            padding: 9px 11px;
            font-size: 13.5px;
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
          .seat-badge {
            display: inline-block;
            background-color: #dcfce7;
            color: #15803d;
            padding: 3px 10px;
            border-radius: 12px;
            font-weight: bold;
          }
          .empty-message {
            background-color: #fff1f2;
            border: 1px solid #fecdd3;
            color: #be123c;
            padding: 26px;
            text-align: center;
            border-radius: 8px;
            font-size: 15.5px;
            font-weight: bold;
            margin: 20px 0;
          }
          .action-bar {
            text-align: right;
            margin-bottom: 10px;
          }
          .btn-print {
            background-color: #0e7490;
            color: #ffffff;
            border: none;
            padding: 8px 18px;
            font-size: 13.5px;
            font-weight: bold;
            border-radius: 5px;
            cursor: pointer;
          }
          .footer {
            margin-top: 20px;
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
            <button class="btn-print" onclick="window.print()">In lịch chiếu (Print)</button>
          </div>

          <div class="header">
            <h1>BẢNG LỊCH CHIẾU PHIM</h1>
            <p>
              Ngày chiếu: <strong><xsl:value-of select="$ngayChieuVN" /></strong>
              (Tổng số: <strong><xsl:value-of select="count($dsSuat)" /></strong> suất chiếu)
            </p>
          </div>

          <xsl:choose>
            <xsl:when test="count($dsSuat) &gt; 0">
              <table>
                <thead>
                  <tr>
                    <th style="width: 5%;">STT</th>
                    <th style="width: 8%;">Mã suất</th>
                    <th style="width: 16%;">Phòng chiếu</th>
                    <th style="width: 22%;">Bộ phim</th>
                    <th style="width: 10%;">Ngày chiếu</th>
                    <th style="width: 8%;">Giờ BĐ</th>
                    <th style="width: 14%;">Kết thúc dự kiến (gồm 15 phút dọn rạp)</th>
                    <th style="width: 9%;">Giá vé</th>
                    <th style="width: 8%;">Ghế trống / Tổng</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="$dsSuat">
                    <xsl:sort select="ngayChieu" data-type="text" order="ascending" />
                    <xsl:sort select="maPhong" data-type="text" order="ascending" />
                    <xsl:sort select="gioBatDau" data-type="text" order="ascending" />
                    <tr>
                      <td class="text-center"><xsl:value-of select="position()" /></td>
                      <td class="text-center"><strong><xsl:value-of select="maSuat" /></strong></td>
                      <td><xsl:value-of select="tenPhong" /> (<xsl:value-of select="maPhong" />)</td>
                      <td>
                        <strong><xsl:value-of select="tenPhim" /></strong>
                        <br />
                        <span style="font-size: 12px; color: #64748b;">
                          <xsl:value-of select="theLoai" /> (<xsl:value-of select="thoiLuong" /> phút)
                        </span>
                      </td>
                      <td class="text-center"><xsl:value-of select="ngayChieuVN" /></td>
                      <td class="text-center"><strong><xsl:value-of select="gioBatDau" /></strong></td>
                      <td class="text-center"><xsl:value-of select="gioKetThuc" /></td>
                      <td class="text-right">
                        <strong><xsl:value-of select="format-number(giaVe, '#.##0', 'vn')" />đ</strong>
                      </td>
                      <td class="text-center">
                        <span class="seat-badge">
                          <xsl:value-of select="soGheTrong" /> / <xsl:value-of select="tongSoGhe" />
                        </span>
                      </td>
                    </tr>
                  </xsl:for-each>
                </tbody>
              </table>
            </xsl:when>
            <xsl:otherwise>
              <div class="empty-message">
                Không có dữ liệu suất chiếu nào cho ngày <xsl:value-of select="$ngayChieuVN" />.
              </div>
            </xsl:otherwise>
          </xsl:choose>

          <div class="footer">
            Thời điểm kết xuất: <xsl:value-of select="$ngayLap" />
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

