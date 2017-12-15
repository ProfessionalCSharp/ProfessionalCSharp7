using Microsoft.Extensions.CommandLineUtils;

namespace EnumerableSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.FullName = "LINQ Sample App";
            LinqSamples.Register(app);

            app.Command("help", cmd =>
            {
                cmd.Description = "Get help for the application";
                CommandArgument commandArgument = cmd.Argument("<COMMAND>", "The command to get help for");
                cmd.OnExecute(() =>
                {
                    app.ShowHelp(commandArgument.Value);
                    return 0;
                });
            });

            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 0;
            });

            app.Execute(args);
        }
    }
}
