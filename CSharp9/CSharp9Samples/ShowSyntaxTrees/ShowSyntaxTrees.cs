using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Text;

namespace ShowSyntaxTreesGeneration
{
    [Generator]
    public class ShowSyntaxTrees : ISourceGenerator
    {
        public void Execute(SourceGeneratorContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using System;
namespace ShowSyntaxTrees
{
  public static class SyntaxTrees
  {
    public static void Show()
    {

");


            var syntaxTrees = context.Compilation.SyntaxTrees;
            foreach (var syntaxTree in syntaxTrees)
            {
                sourceBuilder.Append($@"Console.WriteLine(@""{syntaxTree.FilePath}"");");                
            }

            sourceBuilder.Append(@"
    }
  }
}");

            context.AddSource("syntaxtrees", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(InitializationContext context)
        {

        }
    }
}
