using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                            var connect = $"Data Source={Environment.MachineName}\\{instanceName};Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";
                            try
                            {
                                using (SqlConnection connection = new SqlConnection(connect))
                                {
                                    connection.Open();
                                    conn = connect;
                                    break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                }
            }

            return conn;
        }
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
