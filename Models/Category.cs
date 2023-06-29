namespace ProductApi.Models
{
    public class Category
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
