using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalTermProject.EntityClass
{
    public partial class Employee
    {
        public Employee()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public string Address { get; set; }

        [MaxLength(11)]
        public string HomePhone { get; set; }
        public Nullable<long> Salary { get; set; }
        public string Nationality { get; set; }
        public Nullable<bool> Gender { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
