using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinalTermProject.EntityClass
{
    public partial class Product
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
