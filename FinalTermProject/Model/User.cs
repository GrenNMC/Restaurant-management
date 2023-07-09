using System;
using System.ComponentModel.DataAnnotations;

namespace FinalTermProject.EntityClass
{
    public partial class User
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsManager { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> LastedLoginDate { get; set; }
    }
}
