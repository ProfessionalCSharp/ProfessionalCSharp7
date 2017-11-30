using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TagHelperSamples
{
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdownfile")]
    public class MarkdownTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            string markdown = string.Empty;
            if (MarkdownFile != null)
            {
                string filename = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", MarkdownFile);
                markdown = File.ReadAllText(filename);
            }
            else
            {
                markdown = (await output.GetChildContentAsync()).GetContent();
            }
            output.Content.SetHtmlContent(Markdown.ToHtml(markdown));
        }

        [HtmlAttributeName("markdownfile")]
        public string MarkdownFile { get; set; }
    }
}
