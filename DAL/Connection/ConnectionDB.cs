using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Connection
{
    public class ConnectionDB
    {
        static string _conn;
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
                                    _conn = connect;
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
            return _conn;
        }
    }
}
