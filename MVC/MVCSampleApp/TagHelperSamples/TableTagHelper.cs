using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TagHelperSamples
{
    [HtmlTargetElement("table", Attributes = ItemsAttributeName)]
    public class TableTagHelper : TagHelper
    {
        private const string ItemsAttributeName = "items";

        [HtmlAttributeName(ItemsAttributeName)]
        public object Items { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var childContent = output.Content.IsModified ? output.Content.GetContent() :
                (await output.GetChildContentAsync()).GetContent();

            string row = GetRowContent(childContent);

            output.PreContent.SetHtmlContent("<table>");
            output.PostContent.SetHtmlContent("</table>");

            await base.ProcessAsync(context, output);
        }

        private string GetRowContent(string content)
        {
            int start = content.IndexOf("<row>") + 5;
            if (start < 0) throw new ArgumentException("content does not contain <row>");
            int end = content.LastIndexOf("</row>");
            if (end < 0) throw new ArgumentException("content does not contain </row>");
            return content.Substring(start, end - start);
        }

        private IEnumerable<(string propertyname, int index)> GetPropertyNames(string content)
        {
            var items = new List<(string propertyName, int index)>();
            int start = 0;
            do
            {
                start = content.IndexOf("{") + 1;
                int end = content.IndexOf("}");
                string propertyName = content.Substring(start, end - start);
                items.Add((propertyName, start - 1));
            } while (start > 0);

            return items;
        }

    }
}
