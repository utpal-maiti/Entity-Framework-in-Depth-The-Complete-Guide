
using System;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            PlutoContext context = new PlutoContext();
            var course = context.Courses.Single(c => c.Id == 2);


            foreach (var tag in course.Tags)
            {
                Console.WriteLine(tag.Name);
            }

            Console.Read();
        }
    }
}
