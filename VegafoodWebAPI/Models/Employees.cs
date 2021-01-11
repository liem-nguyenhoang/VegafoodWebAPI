using System;
using System.Collections.Generic;

namespace VegafoodWebAPI.Models
{
    public partial class Employees
    {
        public Employees()
        {
            InverseReportsToNavigation = new HashSet<Employees>();
            Orders = new HashSet<Orders>();
        }

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        public string Email { get; set; }

        public virtual Employees ReportsToNavigation { get; set; }
        public virtual ICollection<Employees> InverseReportsToNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
