using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalTermProject.EntityClass
{
    public partial class BookingDetail
    {
        [Key, Column(Order = 0)]
        public int CustomerID { get; set; }

        [Key, Column(Order = 1)]
        public int TableID { get; set; }
        public System.DateTime BookingDate { get; set; }
        public System.DateTime ReservationDate { get; set; }
        public System.TimeSpan BeginTime { get; set; }
        public System.TimeSpan EndTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Table Table { get; set; }
    }
}
