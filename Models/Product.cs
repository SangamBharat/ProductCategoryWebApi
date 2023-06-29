namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public float Price { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
