using System.Collections.Generic;

namespace Queries
{
    public class Author
    {
        public Author()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeletedAuhor { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
