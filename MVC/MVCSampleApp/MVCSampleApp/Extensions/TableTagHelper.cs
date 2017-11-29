using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MVCSampleApp.Extensions
{
    [HtmlTargetElement("table", Attributes = ItemsAttributeName)]
    public class TableTagHelper : TagHelper
    {
        private const string ItemsAttributeName = "items";
        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<object> Items { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var childContent = output.Content.IsModified ? output.Content.GetContent() :
                (await output.GetChildContentAsync()).GetContent();


            output.PreContent.SetHtmlContent("<table>");
            output.PostContent.SetHtmlContent("</table>");
            // output.Content.SetHtmlContent()

        //  var table = new TagBuilder("table");
        //   table.GenerateId(context.UniqueId, "id");
        //   var attributes = context.AllAttributes
        //       .Where(a => a.Name != ItemsAttributeName).ToDictionary(a => a.Name);
        //   table.MergeAttributes(attributes);
            output.Attributes.RemoveAll("items");

            // header row
            var tr = new TagBuilder("tr");
            
            var heading = Items.First();
            PropertyInfo[] properties = heading.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var th = new TagBuilder("th");
                th.InnerHtml.Append(prop.Name);
                th.InnerHtml.AppendHtml(th);
            }
           
            //table.InnerHtml.AppendHtml(tr);

            foreach (var item in Items)
            {
                tr = new TagBuilder("tr");
                foreach (var prop in properties)
                {
                    var td = new TagBuilder("td");
                    td.InnerHtml.Append(prop.GetValue(item).ToString());
                    td.InnerHtml.AppendHtml(td);
                }
              //  table.InnerHtml.AppendHtml(tr);
            }
            //output.Content.Append(table.InnerHtml.ToString());


        }

        //private async Task<string> GetContent(TagHelperOutput output)
        //{
        //    if (Content == null)
        //        return (await output.GetChildContentAsync()).GetContent();

        //    return Content.Model?.ToString();
        //}
    }

}
