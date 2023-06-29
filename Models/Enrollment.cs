using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Enrollment
    {
        public Guid Id { get; set; }

        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        public virtual Product Product { get; set; }    
        public virtual Category Category { get; set; } 

    }
}
