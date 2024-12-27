
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            PlutoContext context = new PlutoContext();

            IQueryable<Course> x;
             //X.Where(c => c.Level == 1); ;

            IEnumerable<Course> Y;
            //var M = Y.Where(c => c.Level == 1);

         //IQueryable<Course> courses = context.Courses;
         IEnumerable<Course> courses = context.Courses;

            var filttered = courses.Where(c => c.Level == 1);

            foreach (var course in filttered)
            {
                System.Console.WriteLine(course.Name);
            }

            ////var courses = context.Courses.Where(c => c.IsBegineer== true); //Error Translation Error
            //var courses = context.Courses.ToList(). Where(c => c.IsBegineer== true);
            ////var courses = context.Courses.Where(c => c.Level == 1).OrderBy(c => c.Name);

            //foreach (var c in courses.ToList())
            //{
            //    System.Console.WriteLine(c.Name);

            //    foreach (var x in courses.ToList())
            //    {

            //    }
            //}
            System.Console.Read();

        }
    }
}
