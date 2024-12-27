
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            PlutoContext context = new PlutoContext();
           
            //Best for WPF or Desktop Appication
            var authors = context.Authors.ToList();
            var author = authors.Single(a => a.Id == 1);
            Course course = new Course
            {
                Name = "New Course 1",
                Description = "Description 1",
                FullPrice = 20.05f,
                //Web Application
                AuthorId =1
                //WPF
                //Author = author       //new Author {  Id = 1,   Name = "Mosh Hamedani"}
                
            };
            context.Courses.Add(course);
            context.SaveChanges();

        }
    }
}
