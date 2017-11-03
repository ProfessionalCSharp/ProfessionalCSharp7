using System.Text;

namespace WebSampleApp
{
    public static class HtmlExtensions
    {
        public static string Div(this string value) =>
            $"<div>{value}</div>";

        public static string Span(this string value) =>
            $"<span>{value}</span>";

        public static string Div(this string key, string value) =>
            $"{key.Span()}{value.Span()}".Div();

        public static string LI(this string value, string url) =>
            $@"<li><a href=""{url}"">{value}</a></li>";

        public static string UL(this string value) =>
            $"<ul>{value}</ul>";

        public static string HtmlDocument(this string content, string title)
        {
            var sb = new StringBuilder();
            sb.Append("<!DOCTYPE HTML>");
            sb.Append("<head><meta charset=\"utf-8\"><title>{title}</title></head>");
            sb.Append("<body>");
            sb.Append(content);
            sb.Append("</body>");
            return sb.ToString();
        }
    }
}
