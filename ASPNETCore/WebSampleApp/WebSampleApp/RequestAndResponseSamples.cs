using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Encodings.Web;

namespace WebSampleApp
{
    public static class RequestAndResponseSamples
    {
        public static string GetRequestInformation(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append("scheme".Div(request.Scheme));
            sb.Append("host".Div(request.Host.HasValue ? request.Host.Value : "no host"));
            sb.Append("path".Div(request.Path));
            sb.Append("query string".Div(request.QueryString.HasValue ? request.QueryString.Value : "no query string"));
            sb.Append("method".Div(request.Method));
            sb.Append("protocol".Div(request.Protocol));
            return sb.ToString();
        }

        public static string GetHeaderInformation(HttpRequest request)
        {
            var sb = new StringBuilder();
            foreach (var header in request.Headers)
            {
                sb.Append(header.Key.Div(string.Join("; ", header.Value)));
            }
            return sb.ToString();
        }

        // use the query string sample
        public static string QueryString(HttpRequest request)
        {
            string xtext = request.Query["x"];
            string ytext = request.Query["y"];
            if (xtext == null || ytext == null)
            {
                return "x and y must be set".Div();
            }
            if (!int.TryParse(xtext, out int x))
            {
                return $"Error parsing {xtext}".Div();
            }
            if (!int.TryParse(ytext, out int y))
            {
                return $"Error parsing {ytext}".Div();
            }
            return $"{x} + {y} = {x + y}".Div();
        }

        public static string Content(HttpRequest request) => request.Query["data"];

        public static string ContentEncoded(HttpRequest request) =>
            HtmlEncoder.Default.Encode(request.Query["data"]);

        public static string GetForm(HttpRequest request)
        {
            string result = string.Empty;
            switch (request.Method)
            {
                case "GET":
                    result = GetForm().HtmlDocument("Form Input");
                    break;
                case "POST":
                    result = ShowForm(request);
                    break;
                default:
                    break;
            }
            return result;
        }

        private static string GetForm() =>
          "<form method=\"post\" action=\"form\">" +
            "<input type=\"text\" name=\"text1\" />" +
            "<input type=\"submit\" value=\"Submit\" />" +
          "</form>";

        private static string ShowForm(HttpRequest request)
        {
            var sb = new StringBuilder();
            if (request.HasFormContentType)
            {
                IFormCollection coll = request.Form;
                foreach (var key in coll.Keys)
                {
                    sb.Append(key.Div(HtmlEncoder.Default.Encode(coll[key])));
                }
                return sb.ToString();
            }
            else return "no form".Div();
        }

        public static string WriteCookie(HttpResponse response)
        {
            response.Cookies.Append("color", "red",
              new CookieOptions
              {
                  Path = "/",
                  Expires = DateTime.Now.AddDays(1)
              });
            return "cookie written".Div();
        }

        public static string ReadCookie(HttpRequest request)
        {
            var sb = new StringBuilder();
            IRequestCookieCollection cookies = request.Cookies;
            foreach (var key in cookies.Keys)
            {
                sb.Append(key.Div(cookies[key]));
            }
            return sb.ToString();
        }

        public static string GetJson(HttpResponse response)
        {
            var b = new
            {
                Title = "Professional C# 7",
                Publisher = "Wrox Press",
                Author = "Christian Nagel"
            };

            string json = JsonConvert.SerializeObject(b);
            response.ContentType = "application/json";
            return json;
        }
    }
}