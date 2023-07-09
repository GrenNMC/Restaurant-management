using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalTermProject.EntityClass
{
    public partial class Customer
    {
        public Customer()
        {
            this.BookingDetails = new HashSet<BookingDetail>();
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int CustomerID { get; set; }
        public string ContactName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Nationality { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
