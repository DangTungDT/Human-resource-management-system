using BLL;
using ClosedXML;
using DAL;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmDSLuongNhanVien : Form
    {
        private Dictionary<string, bool> mailDaGui = new Dictionary<string, bool>();

        // Danh sách ngày lễ trong năm (có thể cập nhật theo năm)
        private List<DateTime> dsNgayLe = new List<DateTime>
        {
            new DateTime(2025, 1, 1),   // Tết Dương lịch
            new DateTime(2025, 4, 30),  // Ngày Giải phóng miền Nam
            new DateTime(2025, 5, 1),   // Quốc tế Lao động
            new DateTime(2025, 9, 2),   // Quốc khánh
            // Giỗ Tổ Hùng Vương 10/03 Âm lịch (chuyển sang Dương lịch năm 2025)
            new DateTime(2025, 4, 21),  
            // Tết Nguyên đán 2025 (chuyển sang Dương lịch) ví dụ: 28/01/2025 đến 03/02/2025
            new DateTime(2025, 1, 28),
            new DateTime(2025, 1, 29),
            new DateTime(2025, 1, 30),
            new DateTime(2025, 1, 31),
            new DateTime(2025, 2, 1),
            new DateTime(2025, 2, 2),
            new DateTime(2025, 2, 3),
        };

        private readonly string _con;

        private readonly BLLKyLuong _dbContextKL;
        private readonly BLLKhauTru _dbContextKT;
        private readonly BLLPhongBan _dbContextPB;
        private readonly BLLNhanVien _dbContextNV;
        private readonly BLLChamCong _dbContextCC;
        private readonly BLLChiTietLuong _dbContextCTL;
        private readonly BLLHopDongLaoDong _dbContextHD;
        private readonly BLLNhanVien_KhauTru _dbContextKTNV;
        private bool _checkSendEmail { get; set; } = false;
        public frmDSLuongNhanVien(string con)
        {
            InitializeComponent();

            _con = con;
            _dbContextKT = new BLLKhauTru(con);
            _dbContextKL = new BLLKyLuong(con);
            _dbContextNV = new BLLNhanVien(con);
            _dbContextCC = new BLLChamCong(con);
            _dbContextPB = new BLLPhongBan(con);
            _dbContextCTL = new BLLChiTietLuong(con);
            _dbContextHD = new BLLHopDongLaoDong(con);
            _dbContextKTNV = new BLLNhanVien_KhauTru(con);
        }

        private void frmDSLuongNhanVien_Load(object sender, EventArgs e)
        {
            btnGuiLuongNV.Visible = true;
            DataTable dt = GetDataFromSP();

            reportViewer1.Reset();
            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.rptBaoCaoDSLuongNV.rdlc";


            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        private DataTable GetDataFromSP(string idNhanVien = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_con))
            using (SqlCommand cmd = new SqlCommand("sp_BaoCao_Luong", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Gui mail phieu luong cho nhan vien
        public async Task SendSalaryEmail(string emailTo, string tenNhanVien,
                              decimal luongCoBan, decimal phuCap, double soNgayCong,
                              decimal thuong, decimal phat, decimal khoanTruBH, decimal thueTNCN, decimal luongThucLanh)
        {
            string email = "tuanthichthucung@gmail.com";
            string appPassword = "hpny voyz kbna hudk";

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email, "Phòng Nhân Sự - Công ty TNHH 3 Thành Viên");
            mail.To.Add(emailTo);
            mail.Subject = $"Thông báo bảng lương tháng {DateTime.Now.Month}/{DateTime.Now.Year}";

            mail.Body = $@"
                    <html>
                    <head>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'/>
                        <style>
                            body {{
                                font-family: Arial, sans-serif; 
                                color: #333; 
                                margin: 0; 
                                padding: 0; 
                                background-color: #f9f9f9;
                            }}
                            .container {{
                                width: 100%; 
                                max-width: 600px; 
                                margin: 20px auto; 
                                background-color: #ffffff; 
                                padding: 20px; 
                                border-radius: 8px; 
                                box-shadow: 0 0 10px rgba(0,0,0,0.1);
                            }}
                            h2 {{
                                color: #2E86C1; 
                                text-align: center;
                            }}
                            table {{
                                width: 100%; 
                                border-collapse: collapse; 
                                margin-top: 15px;
                            }}
                            th, td {{
                                padding: 12px; 
                                border: 1px solid #ddd; 
                                text-align: left;
                            }}
                            th {{
                                background-color: #f2f2f2;
                            }}
                            tr:nth-child(even) {{
                                background-color: #f9f9f9;
                            }}
                            .highlight {{
                                font-weight: bold; 
                                color: #C0392B;
                                font-size: 1.1em;
                            }}
                            @media screen and (max-width: 480px) {{
                                .container {{
                                    padding: 10px;
                                }}
                                th, td {{
                                    padding: 8px;
                                }}
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h2>Bảng lương tháng {DateTime.Now.Month}/{DateTime.Now.Year}</h2>
                            <p>Kính gửi anh/chị <strong>{tenNhanVien}</strong>,</p>
                            <p>Phòng Nhân sự xin gửi đến anh/chị thông tin tóm tắt bảng lương như sau:</p>

                            <table>
                                <tr>
                                    <th>Lương cơ bản</th>
                                    <td>{luongCoBan:N0} VND</td>
                                </tr>
                                <tr>
                                    <th>Phụ cấp</th>
                                    <td>{phuCap:N0} VND</td>
                                </tr>
                                <tr>
                                    <th>Số ngày công</th>
                                    <td>{soNgayCong} ngày</td>
                                </tr>
                                <tr>
                                    <th>Thưởng / Hoa hồng</th>
                                    <td>{thuong:N0} VND</td>
                                </tr>
                                <tr>
                                    <th>Phạt</th>
                                    <td>{phat:N0} VND</td>
                                </tr>
                                <tr>
                                    <th>Các khoản trừ (BHXH, BHYT, BHTN)</th>
                                    <td>{khoanTruBH:N0} VND</td>
                                </tr>
                                <tr>
                                    <th>Thuế TNCN</th>
                                    <td>{thueTNCN:N0} VND</td>
                                </tr>
                                <tr class='highlight'>
                                    <th>Lương thực lãnh</th>
                                    <td>{luongThucLanh:N0} VND</td>
                                </tr>
                            </table>

                            <p>Trân trọng,<br/>
                            Phòng Nhân sự<br/>
                            Công ty TNHH 3 Thành Viên</p>
                        </div>
                    </body>
                    </html>
                    ";

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(email, appPassword);
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(mail);
        }


        private async void btnGuiLuongNV_Click(object sender, EventArgs e)
        {
            try
            {
                // tinh BH nhan vien
                decimal thueBHYT = 0.08m;
                decimal thueBHXH = 0.03m;
                decimal thueBHTN = 0.01m;

                int nam = DateTime.Now.Date.Year;
                int thang = DateTime.Now.Date.Month;

                var dsKyLuong = _dbContextKL.KtraDsKyLuong();
                var dsKhauTru = _dbContextKT.KtraDsKhauTru();
                var dsChamCong = _dbContextCC.LayDsChamCong();
                var dsLuong = _dbContextCTL.KtraDsChiTietLuong();
                var dsHopDong = _dbContextHD.KtraDsHopDongLaoDong();
                var dsKTNV = _dbContextKTNV.KtraDsNhanVien_KhauTru();

                var dsNhanVien = _dbContextNV.KtraDsNhanVien()
                                            .Join(dsLuong, nv => nv.id, ctl => ctl.idNhanVien, (nv, ctl) => new { nv, ctl })
                                            .Join(dsChamCong, p => p.nv.id, cc => cc.idNhanVien, (p, cc) => new { p.nv, p.ctl, cc })
                                            .Join(dsHopDong, p => p.nv.id, hd => hd.idNhanVien, (p, hd) => new { p.nv, p.ctl, p.cc, hd })
                                            .Join(dsKyLuong, p => p.ctl.idKyLuong, kl => kl.id, (p, kl) => new { p.nv, p.ctl, p.cc, p.hd, kl })
                                            .Where(p => p.kl.ngayKetThuc.Value.Date.Month == DateTime.Now.Date.Month && p.kl.ngayKetThuc.Value.Date.Year == DateTime.Now.Date.Year && p.ctl.capNhatLuong)
                                            .GroupBy(p => p.nv.id).Select(p => p.First()).ToList();

                //var dsNhanVien = _dbContextNV.KtraDsNhanVien()

                //                            .Join(dsKTNV, p => p.id, ktnv => ktnv.idNhanVien, (nv, ktnv) => new { nv, ktnv })
                //                            .Join(dsKhauTru, p => p.ktnv.idKhauTru, kt => kt.id, (p, kt) => new { p.nv, p.ktnv, kt })
                //                            .Join(dsLuong, p => p.nv.id, ctl => ctl.idNhanVien, (p, ctl) => new { p.nv, p.kt, p.ktnv, ctl })
                //                            .Join(dsChamCong, p => p.nv.id, cc => cc.idNhanVien, (p, cc) => new { p.kt, p.ktnv, p.nv, p.ctl, cc, })
                //                            .Join(dsHopDong, p => p.nv.id, hd => hd.idNhanVien, (p, hd) => new { p.kt, p.ktnv, p.nv, p.ctl, p.cc, hd })
                //                            .Join(dsKyLuong, p => p.ctl.idKyLuong, kl => kl.id, (p, kl) => new { p.kt, p.ktnv, p.nv, p.ctl, p.cc, p.hd, kl })
                //                            .Where(p => p.kl.ngayKetThuc.Value.Date.Month == DateTime.Now.Date.Month && p.kl.ngayKetThuc.Value.Date.Year == DateTime.Now.Date.Year && p.ctl.capNhatLuong)
                //                            .GroupBy(p => p.nv.id).Select(p => p.First()).ToList();


                int demNV = dsNhanVien.Select(p => p.nv).ToList().Count;
                if (demNV == 0)
                {
                    MessageBox.Show($"Chưa có nhân viên nào được chấm lương !", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return;
                }

                if (MessageBox.Show($"Bạn có muốn gửi phiếu lương cho {demNV} nhân viên qua email không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Dictionary<string, double> ngayCongNhanVien = NgayCongNhanVien(dsChamCong);
                int tongNV = dsNhanVien.Count;
                int soNVGuiThanhCong = 0;

                btnGuiLuongNV.Enabled = false;
                btnGuiLuongNV.Text = "Đang gửi phiếu lương ...";

                await Task.Run(async () =>
                {
                    foreach (var p in dsNhanVien)
                    {
                        int soNgayLamViecTrongThang = TinhSoNgayLamViecTrongThang(nam, thang);

                        decimal phuCap = p.ctl.tongPhuCap;
                        decimal phat = p.ctl.tongTienPhat;
                        decimal thuong = p.ctl.tongKhenThuong;
                        decimal luongCoBan = p.ctl.luongTruocKhauTru;

                        double soNgayCong = ngayCongNhanVien.ContainsKey(p.nv.id) ? ngayCongNhanVien[p.nv.id] : 0;

                        decimal luongLamCong = luongCoBan / soNgayLamViecTrongThang * (decimal)soNgayCong;

                        decimal khoanTruBH = (luongLamCong + phuCap) * (thueBHYT + thueBHXH + thueBHTN);

                        decimal tinhThuNhapChiuThue = TinhThuNhapChiuThue(luongLamCong, phuCap, thuong, phat, khoanTruBH);
                        decimal thueTNCH = TinhThueTNCN(tinhThuNhapChiuThue);
                        decimal tinhLuongThucLanh = luongLamCong + phuCap - thueTNCH - khoanTruBH;

                        await SendSalaryEmail(p.nv.Email, p.nv.TenNhanVien, luongCoBan, phuCap, soNgayCong, thuong, phat, khoanTruBH, thueTNCH, tinhLuongThucLanh);

                        mailDaGui[p.nv.id] = true;
                        soNVGuiThanhCong++;
                    };
                });

                if (tongNV == soNVGuiThanhCong)
                {
                    btnGuiLuongNV.Enabled = true;
                    btnGuiLuongNV.Text = "Gửi mail lương cho NV";
                    MessageBox.Show($"Đã gửi phiếu lương tháng {DateTime.Now.Month} cho tất cả nhân viên !", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }           
        }

        // tinh thue thu nhap ca nhan theo luy tien
        public decimal TinhThueTNCN(decimal thuNhapChiuThue)
        {
            decimal thue = 0;

            // bac 1: 0 - 5 trieuu
            if (thuNhapChiuThue <= 5_000_000m)
            {
                thue = thuNhapChiuThue * 0.05m;
            }
            else if (thuNhapChiuThue <= 10_000_000m) // bac 2: 5.000.001 - 10.000.000
            {
                thue = 5_000_000m * 0.05m + (thuNhapChiuThue - 5_000_000m) * 0.10m;
            }
            else if (thuNhapChiuThue <= 18_000_000m) // bac 3: 10.000.001 - 18.000.000
            {
                thue = 5_000_000m * 0.05m
                     + 5_000_000m * 0.10m
                     + (thuNhapChiuThue - 10_000_000m) * 0.15m;
            }
            else if (thuNhapChiuThue <= 32_000_000m) // bac 4: 18.000.001 - 32.000.000
            {
                thue = 5_000_000m * 0.05m
                     + 5_000_000m * 0.10m
                     + 8_000_000m * 0.15m
                     + (thuNhapChiuThue - 18_000_000m) * 0.20m;
            }
            else if (thuNhapChiuThue <= 52_000_000m) // bac 5: 32.000.001 - 52.000.000
            {
                thue = 5_000_000m * 0.05m
                     + 5_000_000m * 0.10m
                     + 8_000_000m * 0.15m
                     + 14_000_000m * 0.20m
                     + (thuNhapChiuThue - 32_000_000m) * 0.25m;
            }
            else if (thuNhapChiuThue <= 80_000_000m) // bac 6: 52.000.001 - 80.000.000
            {
                thue = 5_000_000m * 0.05m
                     + 5_000_000m * 0.10m
                     + 8_000_000m * 0.15m
                     + 14_000_000m * 0.20m
                     + 20_000_000m * 0.25m
                     + (thuNhapChiuThue - 52_000_000m) * 0.30m;
            }
            else // bac 7 tren 80.000.000
            {
                thue = 5_000_000m * 0.05m
                     + 5_000_000m * 0.10m
                     + 8_000_000m * 0.15m
                     + 14_000_000m * 0.20m
                     + 20_000_000m * 0.25m
                     + 28_000_000m * 0.30m
                     + (thuNhapChiuThue - 80_000_000m) * 0.35m;
            }

            return thue;
        }

        public decimal TinhThuNhapChiuThue(decimal luongCoBan, decimal phuCapChiuThue, decimal khenThuong, decimal phat, decimal bhNhanVien, decimal giamTruCaNhan = 11_000_000m, int soNguoiPhuThuoc = 0, decimal giamTruNguoiPhuThuoc = 4400000)
        {
            // Tổng thu nhập chịu thuế = lương + phụ cấp chịu thuế
            decimal tongThuNhap = luongCoBan + phuCapChiuThue + khenThuong - phat;

            // Giảm trừ cho người phụ thuộc
            decimal tongGiamTruNguoiPhuThuoc = soNguoiPhuThuoc * giamTruNguoiPhuThuoc;

            // Tính thu nhập chịu thuế
            decimal thuNhapChiuThue = tongThuNhap - bhNhanVien - giamTruCaNhan - tongGiamTruNguoiPhuThuoc;

            // Không được âm
            if (thuNhapChiuThue < 0)
                thuNhapChiuThue = 0;

            return thuNhapChiuThue;
        }


        public int TinhSoNgayLamViecTrongThang(int nam, int thang)
        {
            int soNgayLamViec = 0;
            int soNgayTrongThang = DateTime.DaysInMonth(nam, thang);

            for (int ngay = 1; ngay <= soNgayTrongThang; ngay++)
            {
                DateTime current = new DateTime(nam, thang, ngay);

                // Nếu không phải thứ 7, chủ nhật và không phải ngày lễ
                if (current.DayOfWeek != DayOfWeek.Saturday &&
                    current.DayOfWeek != DayOfWeek.Sunday &&
                    !dsNgayLe.Contains(current))
                {
                    soNgayLamViec++;
                }
            }

            return soNgayLamViec;
        }

        // Tinh so ngay cong cua nhan vien
        private Dictionary<string, double> NgayCongNhanVien(List<ChamCong> dsChamCong)
        {
            return dsChamCong.Where(p => p.NgayChamCong.Date.Month == DateTime.Now.Month && p.NgayChamCong.Date.Year == DateTime.Now.Year)
                            .GroupBy(p => p.idNhanVien) // Gom theo nhân viên
                            .ToDictionary(
                                g => g.Key,
                                g =>
                                {
                                    double tongGio = g.Sum(p =>
                                    {
                                        TimeSpan gioVao = p.GioVao.Value;
                                        TimeSpan gioRa = p.GioRa.Value;

                                        // Nếu ca qua đêm
                                        if (gioRa < gioVao)
                                            gioRa += TimeSpan.FromHours(24);

                                        return (gioRa - gioVao).TotalHours;
                                    });

                                    // 1 ngày công = 8 giờ, làm tròn xuống
                                    return Math.Floor(tongGio / 8.0);
                                }
                            );
        }

        public string FormatCurrency(decimal amount)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:N0} VND", amount);
        }

        private void cmbThang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbPhongBan.DataSource = new List<PhongBan>();
            cmbNhanVienPB.DataSource = new List<NhanVien>();
            DsPhongBan();
        }

        private void cmbNam_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmbThang.DataSource = new List<int>();
        }

        public void DsPhongBan()
        {
            var dsPhongBan = _dbContextPB.KtraDsPhongBan();
            cmbPhongBan.DataSource = dsPhongBan;
            cmbPhongBan.ValueMember = "id";
            cmbPhongBan.DisplayMember = "TenPhongBan";
            cmbPhongBan.SelectedIndex = -1;
        }

    }
}
