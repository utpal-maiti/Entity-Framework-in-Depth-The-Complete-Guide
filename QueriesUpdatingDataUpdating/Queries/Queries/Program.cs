using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            PlutoContext context = new PlutoContext();

            //Adding an Object
            context.Authors.Add(new Author { Name = "New Author CT" });

            context.SaveChanges();
            //Update an Object
            var author = context.Authors.Find(2);
            author.Name = "Updated CT";

            //Removing an Object
            var another = context.Authors.Find(4);
            context.Authors.Remove(another);

            var entries = context.ChangeTracker.Entries();

            foreach (var entry in entries)
            {

                Console.WriteLine(entry.State);
            }






            //Removing Object

            //WithOut Cascade Delete
           // var author = context.Authors.Include(a => a.Courses).Single(a => a.Id == 2);
           // //var authors = context.Authors.RemoveRange(author.Courses);
           // context.Authors.Remove(author);
           ////var author =context.Authors.Find(4);   // if Cascade Delete is Turn Off it throw a Exception
           // context.Authors.Remove(author);


            ////With Cascade Delete
            //var course = context.Courses.Find(4);
            //context.Courses.Remove(course);
            //context.SaveChanges();
            
            //Updating Object
            //var course = context.Courses.Find(4);   //Single(c=>c.Id==4)
            ////var course = context.Courses.Find(4,2,3,4); if there Present Composite Key
            //course.Name = "New COurse 3";
            //course.AuthorId = 3;

            context.SaveChanges();
        }
    }
}
