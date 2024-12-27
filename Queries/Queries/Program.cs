using System;
using System.Linq;

namespace Queries
{
    public class Program
    {

        private static void Main(string[] args)
        {
            var context = new PlutoContext();

            var querry =
                from a in context.Authors
                from c in context.Courses
                select new { AuthorName = a.Name, CourseName = c.Name };

            foreach (var x in querry)
            {
                Console.WriteLine("{0}  ({1})", x.AuthorName, x.CourseName);
            }
            Console.WriteLine("\n");
            //Left Join or Aggregation Function //Group Join
            var query3 =
                from a in context.Authors
                join c in context.Courses on a.Id equals c.AuthorId into g //Group Join
                select new { AuthorName = a.Name, Courses = g.Count() };

            foreach (var x in query3)
            {
                Console.WriteLine("{0}  ({1})", x.AuthorName, x.Courses);
            }

            var query2 =
            from c in context.Courses
            join a in context.Authors on c.AuthorId equals a.Id     //Inner Join
                select new { CourseName = c.Name, AuthorName = c.Author.Name };

            var query1 =
                from c in context.Courses
                group c by c.Level  //Groping
                into g
                select g;

            foreach (var Group in query1)
            {
                Console.WriteLine(Group.Key + "  (" + Group.Count() + ")");  //Group.Count() Aggregate Function
                foreach (var course in Group)
                {
                    Console.WriteLine("\t{0}", course.Name);
                }
            }
            var q =
                from c in context.Courses
                where c.Author.Id == 1 && c.Level == 1  //Restrition
                orderby c.Level descending, c.Name     //Odering
                select new { Name = c.Name, Author = c.Author.Name };  //Projection

            //LINQ Syntax
            var Q = from c in context.Courses
                        //where c.Name.Contains("C#")
                    orderby c.Name
                    select c;

            //if (Q.Count() == 0)
            //{
            //    Console.WriteLine("No Data Present.");
            //}
            //else
            //{
            //    //foreach (var course in Q.ToList())
            //    //{
            //    //    Console.WriteLine(course.Name + " " + course.Description + " " + course.FullPrice);
            //    //}
            //}
            //Extension Methods
            var courses = context.Courses
                .Where(c => c.Name.Contains("C#"))
                .OrderBy(c => c.Name);

            //foreach (var course in courses.ToList())
            //{
            //    Console.WriteLine(course.Name + " " + course.Description + " " + course.FullPrice);
            //}

            Console.Read();
        }
    }
}