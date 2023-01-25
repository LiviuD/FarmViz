using FarmVizModels;
using FarmVizServices;
using FarmVizUI;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace FarmViz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string sourceFilePath =string.Empty;
            bool sourceFilePathIsValid = false;
            bool doContinue = true;

            if (args.Length > 0 && IsFileValid(args[0]))
            {
                sourceFilePath = args[0];
                sourceFilePathIsValid = true;
            }
            else
            {
                AnsiConsole.MarkupLine("[italic][yellow]HINT: You can drag&drop the desired JSON file onto the executable file to quickly import it.[/][/]");
                do
                {
                    sourceFilePathIsValid = GetSourceFilePath(out sourceFilePath,out doContinue);
                }
                while (!sourceFilePathIsValid && doContinue);
            }

            if (doContinue && sourceFilePathIsValid)
            {
                RunReports(sourceFilePath);
            }

            Console.ReadLine();
        }

        private static void RunReports(string sourceFilePath)
        {
            IFileImport fileService = new FileImport();
            var farms = new List<Farm>();
            //AnsiConsole.Markup("[green]Please wait....Importing File...[/]");
            AnsiConsole.Status()
                        .Spinner(Spinner.Known.BouncingBall)
                        .Start("Importing file...", ctx =>
                        {
                            farms = fileService.Import(sourceFilePath);
                        });

            var farmReports = new FarmsReports(farms);
            var report = farmReports.ShowBestProducingAnimal();
            if (report is not null)
            {
                Console.WriteLine();
                AnsiConsole.Write(report);
            }
        }

        private static bool GetSourceFilePath(out string sourceFilePath, out bool doContinue)
        {
            var filePath = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Please enter the path to the source JSON file:[/]")
                                .PromptStyle("green"));
            var sourceFilePathIsValid = IsFileValid(filePath);
            if (!sourceFilePathIsValid)
            {
                AnsiConsole.Markup("[red]The file does not exists or is not a valid JSON file.[/]");
                Console.WriteLine();
                if (!AnsiConsole.Confirm("Do you want to continue?"))
                {
                    doContinue = false;
                }
            }
            sourceFilePath = filePath;
            return sourceFilePathIsValid;
        }

        private static bool IsFileValid(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath) 
                && Path.HasExtension(filePath) 
                && Path.GetExtension(filePath).ToLower() == ".json")
            {
                return true;
            }

            return false;
        }
    }
}