using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TagHelperSamples
{
    [HtmlTargetElement("table", Attributes = ItemsAttributeName)]
    public class TableTagHelper : TagHelper
    {
        private const string ItemsAttributeName = "items";

        [HtmlAttributeName(ItemsAttributeName)]
        public IEnumerable<object> Items { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var table = new TagBuilder("table");

            table.GenerateId(context.UniqueId, "id");
            
            var attributes = context.AllAttributes.Where(a => a.Name != ItemsAttributeName).ToDictionary(a => a.Name);
            table.MergeAttributes(attributes);

            PropertyInfo[] properties = CreateHeading(table);

            foreach (var item in Items)
            {
                var tr = new TagBuilder("tr");
                foreach (var prop in properties)
                {
                    var td = new TagBuilder("td");
                    td.InnerHtml.Append(prop.GetValue(item).ToString());
                    tr.InnerHtml.AppendHtml(td);
                }
                table.InnerHtml.AppendHtml(tr);
            }
            output.Content.AppendHtml(table);
        }

        private PropertyInfo[] CreateHeading(TagBuilder table)
        {
            var tr = new TagBuilder("tr");
            var heading = Items.First();
            PropertyInfo[] properties = heading.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var th = new TagBuilder("th");
                th.InnerHtml.Append(prop.Name);
                tr.InnerHtml.AppendHtml(th);
            }
            table.InnerHtml.AppendHtml(tr);
            return properties;
        }
    }
}
