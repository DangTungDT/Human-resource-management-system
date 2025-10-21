using BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Windows.Forms;

namespace GUI
{
    public partial class QuenMatKhau : Form
    {
        private readonly BLLTaiKhoan bllTaiKhoan;
        private readonly string connectionString;
        private string otpGenerated;

        // === Khai báo control toàn cục ===
        private TextBox txtTaiKhoan;
        private Button btnGuiMa;
        private TextBox txtOTP;
        private Button btnXacNhan;
        private Label lblThongBao;

        public QuenMatKhau(string conn)
        {
            if (string.IsNullOrEmpty(conn))
                throw new ArgumentException("Chuỗi kết nối không được để trống.", nameof(conn));

            connectionString = conn;
            InitializeComponent();
            bllTaiKhoan = new BLLTaiKhoan(conn);
            BuildUI();
        }

        private void BuildUI()
        {
            // ==== FORM ====
            this.Text = "Quên mật khẩu";
            this.Size = new Size(420, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            // ==== TIÊU ĐỀ ====
            Label lblTitle = new Label
            {
                Text = "Khôi phục mật khẩu",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.FromArgb(0, 120, 215),
                Location = new Point(110, 20)
            };

            // ==== TÀI KHOẢN ====
            Label lblTaiKhoan = new Label
            {
                Text = "Tài khoản:",
                Location = new Point(50, 80),
                AutoSize = true
            };

            txtTaiKhoan = new TextBox
            {
                Name = "txtTaiKhoan",
                Location = new Point(150, 75),
                Width = 200
            };

            // ==== NÚT GỬI MÃ ====
            btnGuiMa = new Button
            {
                Text = "Gửi mã xác nhận",
                Location = new Point(150, 115),
                Width = 200,
                Height = 50,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuiMa.Click += BtnGuiMa_Click;

            // ==== NHẬP OTP ====
            txtOTP = new TextBox
            {
                Name = "txtOTP",
                Location = new Point(150, 165),
                Width = 200,
                Visible = false
            };

            // ==== NÚT XÁC NHẬN ====
            btnXacNhan = new Button
            {
                Name = "btnXacNhan",
                Text = "Xác nhận OTP",
                Location = new Point(150, 205),
                Width = 200,
                Height = 50,
                BackColor = Color.SeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            btnXacNhan.Click += BtnXacNhan_Click;

            // ==== THÔNG BÁO ====
            lblThongBao = new Label
            {
                Name = "lblThongBao",
                Location = new Point(50, 250),
                AutoSize = true,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };

            // ==== THÊM CONTROL VÀO FORM ====
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblTaiKhoan);
            this.Controls.Add(txtTaiKhoan);
            this.Controls.Add(btnGuiMa);
            this.Controls.Add(txtOTP);
            this.Controls.Add(btnXacNhan);
            this.Controls.Add(lblThongBao);
        }

        private void BtnGuiMa_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text.Trim();
            if (string.IsNullOrEmpty(taiKhoan))
            {
                MessageBox.Show("Vui lòng nhập tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt = bllTaiKhoan.GetByTaiKhoan(taiKhoan);
            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Tài khoản không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string idNhanVien = dt.Rows[0]["Mã Nhân viên"]?.ToString();
            string email = GetEmailFromNhanVien(idNhanVien);

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Không tìm thấy email liên kết với tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            otpGenerated = GenerateOTP();
            SaveOTPToDatabase(taiKhoan, otpGenerated);
            SendOTPEmail(email, otpGenerated);

            // Hiển thị các control OTP
            txtOTP.Visible = true;
            btnXacNhan.Visible = true;
            lblThongBao.Text = "✅ Mã xác nhận đã được gửi. Vui lòng kiểm tra email!";
            lblThongBao.ForeColor = Color.ForestGreen;

            // Tạm ẩn nút gửi để tránh spam
            btnGuiMa.Enabled = false;
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            string otpEntered = txtOTP.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();

            if (string.IsNullOrEmpty(otpEntered))
            {
                lblThongBao.Text = "⚠️ Vui lòng nhập mã xác nhận!";
                lblThongBao.ForeColor = Color.Red;
                return;
            }

            if (IsOTPValid(taiKhoan, otpEntered))
            {
                lblThongBao.Text = "✅ Mã xác nhận đúng!";
                lblThongBao.ForeColor = Color.ForestGreen;
                System.Threading.Thread.Sleep(1000);

                var formDoiMatKhau = new DoiMatKhau(taiKhoan, connectionString);
                formDoiMatKhau.Show();
                this.Hide();
            }
            else
            {
                lblThongBao.Text = "❌ Mã xác nhận không đúng hoặc đã hết hạn!";
                lblThongBao.ForeColor = Color.Red;
            }
        }

        private string GetEmailFromNhanVien(string idNhanVien)
        {
            var bllNhanVien = new BLLNhanVien(connectionString);
            DataTable dt = bllNhanVien.GetById(idNhanVien);
            return dt?.Rows.Count > 0 ? dt.Rows[0]["email"]?.ToString() : null;
        }

        private string GenerateOTP()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        private void SaveOTPToDatabase(string taiKhoan, string otp)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO QuenMatKhau (taiKhoan, otp, thoiGianHetHan, daXacNhan)
                    VALUES (@taiKhoan, @otp, DATEADD(minute, 10, GETDATE()), 0)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                    cmd.Parameters.AddWithValue("@otp", otp);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool IsOTPValid(string taiKhoan, string otpEntered)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*) FROM QuenMatKhau 
                    WHERE taiKhoan = @taiKhoan 
                      AND otp = @otp 
                      AND thoiGianHetHan > GETDATE() 
                      AND daXacNhan = 0";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                    cmd.Parameters.AddWithValue("@otp", otpEntered);
                    conn.Open();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        string update = "UPDATE QuenMatKhau SET daXacNhan = 1 WHERE taiKhoan = @taiKhoan AND otp = @otp";
                        using (SqlCommand up = new SqlCommand(update, conn))
                        {
                            up.Parameters.AddWithValue("@taiKhoan", taiKhoan);
                            up.Parameters.AddWithValue("@otp", otpEntered);
                            up.ExecuteNonQuery();
                        }
                    }
                    return count > 0;
                }
            }
        }

        private void SendOTPEmail(string email, string otp)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                using (SmtpClient smtpServer = new SmtpClient("smtp.gmail.com"))
                {
                    mail.From = new MailAddress("lthanhngan0801@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Mã xác nhận quên mật khẩu";
                    mail.Body = $"Mã xác nhận của bạn là: {otp}\nMã có hiệu lực trong 10 phút.";

                    smtpServer.Port = 587;
                    smtpServer.Credentials = new System.Net.NetworkCredential("lthanhngan0801@gmail.com", "ybxq zmla itgk mrmz");
                    smtpServer.EnableSsl = true;

                    smtpServer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
