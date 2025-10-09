using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ConnectionDB
    {
        //Tùng : DESKTOP-6LE6PT2\\SQLEXPRESS
        //Ngân : DESKTOP-UM1I61K\THANHNGAN
        public static string conn = "Data Source=DESKTOP-UM1I61K\\THANHNGAN;Initial Catalog=PersonnelManagement;Integrated Security=True;Encrypt=False";

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
        public string Status { get; set;}
    }

    public class SalaryDetail
    {
        public DateTime PayPeriod { get; set; }
        public string NameEmployee {  get; set; }
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
