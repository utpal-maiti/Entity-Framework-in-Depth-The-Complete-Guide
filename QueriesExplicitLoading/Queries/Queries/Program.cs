
using System.Linq;
using System.Data.Entity;
namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            PlutoContext context = new PlutoContext();

            var authors = context.Authors.ToList();
            var autorIds = authors.Select(a => a.Id);
            context.Courses.Where(c => autorIds.Contains(c.AuthorId) && c.FullPrice == 0).Load();

            //var author = context.Authors.Single(c => c.Id == 1);
            ////MSDN WAY
            //context.Entry(author).Collection(c => c.Courses).Load();
            //context.Entry(author).Collection(c => c.Courses).Query().Where(c=>c.FullPrice==0).Load();

            ////MOsh Way
            //context.Courses.Where(c => c.AuthorId == author.Id).Load();
            //context.Courses.Where(c => c.AuthorId == author.Id && c.FullPrice == 0).Load();

            //foreach (var course in author.Courses)
            //{
            //    System.Console.WriteLine($"{course.Name}");
            //}
        }
    }
}