using Newtonsoft.Json;
using System.Collections.Generic;

namespace BooksCacheModel
{
    public static class BookExtensions
    {
        public static string ToJson(this Book book) =>
            JsonConvert.SerializeObject(book);

        public static string ToJson(this IEnumerable<Book> books) =>
            JsonConvert.SerializeObject(books);

        public static Book ToBook(this string json) =>
            JsonConvert.DeserializeObject<Book>(json);

        public static IEnumerable<Book> ToBooks(this string json) =>
            JsonConvert.DeserializeObject<IEnumerable<Book>>(json);
    }
}
