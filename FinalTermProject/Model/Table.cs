using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTermProject.EntityClass
{
    public partial class Table
    {
        public Table()
        {
            this.Orders = new HashSet<Order>();
            this.BookingDetails = new HashSet<BookingDetail>();
        }

        public int TableID { get; set; }
        public string TableName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
