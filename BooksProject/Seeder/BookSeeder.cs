using BooksProject.Context;
using BooksProject.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BooksProject.Seeder
{
    public class BookSeeder : IBookSeeder
    {
        private const string CsvPath = "C:\\Users\\GRIGS\\Desktop\\API4\\bestsellers with categories.csv";
        private static IEnumerable<T> GetItemsFromCsv<T>(string directory)
        {
            IEnumerable<T> items;

            using (var reader = new StreamReader(directory))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                items = csv.GetRecords<T>().ToArray();
            }

            return items;
        }

        private readonly AppDbContext dbContext;

        public BookSeeder(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SeedAsync()
        {
            bool isSeedRequred = !await this.dbContext.Books.AnyAsync();

            if (!isSeedRequred)
            {
                return;
            }

            IEnumerable<BookCSVModel> BookCsvModels = GetItemsFromCsv<BookCSVModel>(CsvPath);

            var BookDataModels = BookCsvModels
                .Select(x => new Book
                {
                    Name = x.Name,
                    Author = x.Author,
                    Year = x.Year,
                    Genre = x.Genre,
                    Price = x.Price,
                    Rating = x.Rating,
                    Reviews = x.Reviews,
                })
                .ToList();

            this.dbContext.AddRange(BookDataModels);

            this.dbContext.SaveChanges();
        }
    }
}
    

