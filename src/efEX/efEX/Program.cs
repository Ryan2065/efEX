using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace efEX
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new RootCommand 
            {
                new Option<string?>(new[]{"--project", "-p" }, "Path to the project"),
                new Option(new[] {"--verbose", "-v" }, "Verbose output")
            };
            cmd.Handler = CommandHandler.Create<string?, bool, IConsole>(HandleMigrationSqlAdd);
        }
        static void HandleMigrationSqlAdd(string? projectPath, bool verbose, IConsole console)
        {
            var efLog = new EfEXLog(console, verbose);
            var project = new EFProject(projectPath, efLog);
            efLog.LogVerbose("Validated all required project paths");
            var migrations = new EFExSqlDesigner(project, efLog);

        }
    }
}
