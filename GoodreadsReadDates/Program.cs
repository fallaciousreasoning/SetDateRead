using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goodreads;

namespace GoodreadsReadDates
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Run();
            task.Wait();
        }

        static async Task Run()
        {
            var config = Config.Load();

            var client  = GoodreadsClient.CreateAuth(config.ApiKey, config.ApiSecret, config.AccessToken, config.AccessTokenSecret);

            var userId = await client.Users.GetAuthenticatedUserId();

            //TODO actually work with the pagination api
            var reviews = await client.Reviews.GetListByUser(userId, shelfName: "read", pageSize: 1000);

            var undated = reviews.List.Where(r => !r.DateRead.HasValue).ToList();
            Console.WriteLine($"{undated.Count} books do not have a read date!");

            foreach (var review in undated)
            {
                Console.Write($"Would you like to set the read date on {review.Book.Title} to {review.DateAdded}? (y/n) ");
                string text;
                while (!new[] { "y", "n" }.Contains(text = Console.ReadLine()))
                    continue;

                if (text == "n") continue;

                try
                {
                    var result = await client.Reviews.Edit(review.Id, review.Body, review.Rating, review.DateAdded, "read");
                    Console.WriteLine((result ? "Set" : "Failed to set") +
                                      $" the read date on {review.Book.Title} to {review.DateAdded}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to set read date!");
                }
            }

            var shelves = await client.Shelves.GetListOfUserShelves(userId);
            var readShelf = shelves.List.First(shelf => shelf.Name == "read");

            

            Console.ReadKey();
        }
    }
}
