
using System;
using System.Linq;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();

            // Action movies sorted by name
            Console.WriteLine();
            Console.WriteLine("ACTION MOVIES SORTED BY NAME");
            var actionMovies = context.Videos
                .Where(v => v.Genre.Name == "Action")
                .OrderBy(v => v.Name);

            foreach (var v in actionMovies)
                Console.WriteLine(v.Name);

            // Gold drama movies sorted by release date (newest first)
            var dramaMovies = context.Videos
                .Where(v => v.Genre.Name == "Drama" && v.Classification == Classification.Gold)
                .OrderByDescending(v => v.ReleaseDate);

            Console.WriteLine();
            Console.WriteLine("GOLD DRAMA MOVIES SORTED BY RELEASE DATE (NEWEST FIRST)");
            foreach (var v in dramaMovies)
                Console.WriteLine(v.Name);

            // All movies projected into an anonymous type
            var projected = context.Videos
                .Select(v => new {MovieName = v.Name, Genre = v.Genre.Name});

            Console.WriteLine();
            Console.WriteLine("ALL MOVIES PROJECTED INTO AN ANONYMOUS TYPE");
            foreach (var v in projected)
                Console.WriteLine(v.MovieName);

            // All movies grouped by classification
            var groups = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Classification = g.Key.ToString(), 
                    Videos = g.OrderBy(v => v.Name)
                });

            Console.WriteLine();
            Console.WriteLine("ALL MOVIES GROUPED BY CLASSIFICATION");
            foreach (var g in groups)
            {
                Console.WriteLine("Classification: " + g.Classification);

                foreach (var v in g.Videos)
                    Console.WriteLine("\t" + v.Name);
            }

            // Classifications and number of videos in them
            var classifications = context.Videos
                .GroupBy(v => v.Classification)
                .Select(g => new
                {
                    Name = g.Key.ToString(),
                    VideosCount = g.Count()
                })
                .OrderBy(c => c.Name);

            Console.WriteLine();
            Console.WriteLine("CLASSIFICATIONS AND NUMBER OF VIDEOS IN THEM");
            foreach (var c in classifications)
                Console.WriteLine("{0} ({1})", c.Name, c.VideosCount);


            // Genres and number of videos in them
            var genres = context.Genres
                .GroupJoin(context.Videos, g => g.Id, v => v.GenreId, (genre, videos) => new
                {
                    Name = genre.Name,
                    VideosCount = videos.Count()
                })
                .OrderByDescending(g => g.VideosCount);

            Console.WriteLine();
            Console.WriteLine("GENRES AND NUMBER OF VIDEOS IN THEM");
            foreach (var g in genres)
                Console.WriteLine("{0} ({1})", g.Name, g.VideosCount);


        }
    }
}
