using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ConnectionDB
    {
        //Tùng : DESKTOP-6LE6PT2\\SQLEXPRESS
        //Ngân : DESKTOP-UM1I61K\THANHNGAN
        //Tuấn : LAPTOP-PNFFHRG1\MSSQLSERVER01

        public static string conn = "Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";
        public static string TakeConnectionString()
        {
            using (RegistryKey localMachine64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachine64.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL"))
                {
                    if (rk != null)
                    {
                        foreach (string instanceName in rk.GetValueNames())
                        {
                            conn = $"Data Source={Environment.MachineName}\\{instanceName};Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";
                            break;
                        }
                    }
                }
            }

            return conn;
        }

        /// <summary>
        /// ==========================CÁCH 3=========================
        /// Sử dụng App.config
        /// </summary>
        //public static string conn;

        //static ConnectionDB()
        //{
        //    conn = TakeConnectionString();
        //}

        //public static string TakeConnectionString()
        //{
        //    // Lấy chuỗi kết nối từ App.config
        //    string connect = ConfigurationManager.ConnectionStrings["PersonnelDB"]?.ConnectionString;

        //    if (string.IsNullOrWhiteSpace(connect))
        //        throw new Exception("Không tìm thấy cấu hình 'PersonnelDB' trong App.config!");

        //    // Kiểm tra thử kết nối
        //    try
        //    {
        //        using (SqlConnection sqlConn = new SqlConnection(connect))
        //        {
        //            sqlConn.Open();
        //            return connect;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Không thể kết nối tới SQL Server! Chi tiết: {ex.Message}");
        //    }
        //}
        /// <summary>
        /// ==========================CÁCH 2=========================
        /// </summary>
        // Danh sách các cấu hình máy cụ thể (nếu cần chỉ định thủ công)
        private static readonly string defaultDbName = "PersonnelManagement";

        //public static string TakeConnectionString()
        //{
        //    try
        //    {

        //        // === 2. Nếu không tìm thấy, xác định theo tên máy (fallback) ===
        //        string machineName = Environment.MachineName.ToUpper();

        //        switch (machineName)
        //        {
        //            case "DESKTOP-UM1I61K": // Máy của Ngân
        //                conn = $"Data Source={machineName}\\THANHNGAN;Initial Catalog={defaultDbName};Integrated Security=True;Encrypt=False";
        //                break;

        //            case "DESKTOP-6LE6PT2": // Máy của Tùng
        //                conn = $"Data Source={machineName}\\SQLEXPRESS;Initial Catalog={defaultDbName};Integrated Security=True;Encrypt=False";
        //                break;

        //            default:
        //                // Trường hợp không nằm trong danh sách — fallback về localdb
        //                conn = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={defaultDbName};Integrated Security=True;Encrypt=False";
        //                break;
        //        }

        //        return conn;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log lỗi (nếu cần)
        //        conn = $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={defaultDbName};Integrated Security=True;Encrypt=False";
        //        Console.WriteLine($"[ConnectionDB] Lỗi khi lấy chuỗi kết nối: {ex.Message}");
        //        return conn;
        //    }
        //}
    }
    public class EmployeTemp
    {
        public string HoTen { get; set; }
        public string Email { get; set; }
        public bool TrangThaiChamCong { get; set; }
    }

    public class Recruitment
    {
        public string Title { get; set; }
        public string Status { get; set; }
    }

    public class PayPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }

    public class SalaryDetail
    {
        public DateTime PayPeriod { get; set; }
        public string NameEmployee { get; set; }
        public string Status { get; set; }
    }

    public class Candidate
    {
        public DateTime AppliCationDate { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class Timekeeping
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
