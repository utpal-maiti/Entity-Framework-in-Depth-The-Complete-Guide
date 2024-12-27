using System;
using System.Linq;

namespace Queries
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PlutoContext context = new PlutoContext();

            //Aggregating
            var Count = context.Courses.Count();
            var Count1 = context.Courses.Count(c => c.Level == 1);
            var max = context.Courses.Max(c => c.FullPrice);
            var min = context.Courses.Min(c => c.FullPrice);
            var avg = context.Courses.Average(c => c.FullPrice);
            var sum = context.Courses.Sum(c => c.FullPrice);
            Console.WriteLine(sum);
            //Quantifiying
            var allInLevel1 = context.Courses.All(c => c.Level == 1);
            var allInLevel2 = context.Courses.Any(c => c.Level == 1);

            //Element Operators
            var EleOperator = context.Courses.First<Course>(c => c.Level == 1);
            var EleOperator1 = context.Courses.FirstOrDefault();
            //var EleOperator2 = context.Courses.Last();
            //var EleOperator3 = context.Courses.LastOrDefault();
            //var EleOperator4 = context.Courses.Single(c => c.Id == 1);
            var EleOperator5 = context.Courses.SingleOrDefault(c => c.Id == 1);

            //Partitioning
            var Partitioning = context.Courses.Skip(20).Take(10);

            //Cross Join

            context.Authors.SelectMany(a => context.Courses, (author, courses) => new
            {
                AuthorName = author.Name,
                Courses = courses.Name
            });

            //Group Join 
            var groupjoin = context.Authors
                   .GroupJoin(context.Courses,
                   a => a.Id, c => c.AuthorId, (author, courses) => new
                   {
                       AuthorName = author.Name,
                       Courses = courses
                   });

            //Inner Join
            var authors = context.Authors.Join(context.Courses,
                    a => a.Id,                //	key	on	the	left	side
                    c => c.AuthorId,           //	key	on	the	right	side,
                    (author, course) =>        //	what	to	do	once	matched
                        new
                        {
                            AuthorName = author.Name,
                            CourseName = course.Name
                        }
);
            var qurry3 = context.Courses
                .GroupBy(c => c.Level);     //Grouping

            foreach (var t in qurry3)
            {
                Console.WriteLine("Key: " + t.Key);
                foreach (var course in t)
                {
                    Console.WriteLine("\t " + course.Name);
                }
            }

            var tags = context.Courses
                .Where(c => c.Level == 1)       //Projection
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Level)
                .ThenByDescending(c => c.Id)
                //.Select(c=> new { CourseName=c.Name, AuthorName=c.Author.Name});
                .SelectMany(c => c.Tags)
                .Distinct();                //set Operator

            foreach (var t in tags)
            {
                Console.WriteLine(t.Name);
            }

            var qurry2 = context.Courses
                .Where(c => c.Level == 1)       //Projection
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Level)
                .ThenByDescending(c => c.Id)
                //.Select(c=> new { CourseName=c.Name, AuthorName=c.Author.Name});
                .Select(c => c.Tags);
            foreach (var c in qurry2)
            {
                foreach (var tag in c)
                {
                    Console.WriteLine(tag.Name);
                }
            }

            var qurry1 = context.Courses
                .Where(c => c.Level == 1)       //Ordering
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Level)
                .ThenByDescending(c => c.Id);

            var qurry = context.Courses.Where(c => c.Level == 1);//Restriction
        }
    }
}