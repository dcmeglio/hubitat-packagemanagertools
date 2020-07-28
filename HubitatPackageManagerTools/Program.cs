using System;
using System.Net;
using CommandLine;
using CommandLine.Text;
using HubitatPackageManagerTools.Executors;
using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;

namespace HubitatPackageManagerTools
{
    class Program
    {
        private const string settingsJson = "https://raw.githubusercontent.com/dcmeglio/hubitat-packagerepositories/master/settings.json";
        static int Main(string[] args)
        {
            var parser = new Parser(with =>
            {
                with.AutoVersion = false;
                with.AutoHelp = true;
                with.CaseSensitive = false;
                with.EnableDashDash = true;
                with.IgnoreUnknownArguments = false;
                with.HelpWriter = null;
            });
            var result = parser.ParseArguments(args,
                typeof(ManifestAddAppOptions),
                typeof(ManifestAddDriverOptions),
                typeof(ManifestAddFileOptions),
                typeof(ManifestCreateOptions),
                typeof(ManifestModifyAppOptions),
                typeof(ManifestModifyDriverOptions),
                typeof(ManifestModifyFileOptions),
                typeof(ManifestModifyOptions),
                typeof(ManifestRemoveAppOptions),
                typeof(ManifestRemoveDriverOptions),
                typeof(ManifestRemoveFileOptions),
                typeof(ManifestConvertOptions),
                typeof(RepositoryAddPackageOptions),
                typeof(RepositoryCreateOptions),
                typeof(RepositoryModifyOptions),
                typeof(RepositoryModifyPackageOptions),
                typeof(RepositoryRemovePackageOptions));
            try
            {
                WebClient wc = new WebClient();
                var settingsFileContents = wc.DownloadString(settingsJson);
                Settings settings = new Settings(settingsFileContents);
                bool failed = false;
                result.WithParsed((RepositoryCreateOptions opts) => new RepositoryCreateExecutor().Execute(opts, settings))
                    .WithParsed((RepositoryCreateOptions opts) => new RepositoryCreateExecutor().Execute(opts, settings))
                    .WithParsed((RepositoryModifyOptions opts) => new RepositoryModifyExecutor().Execute(opts, settings))
                    .WithParsed((RepositoryAddPackageOptions opts) => new RepositoryAddPackageExecutor().Execute(opts, settings))
                    .WithParsed((RepositoryRemovePackageOptions opts) => new RepositoryRemovePackageExecutor().Execute(opts, settings))
                    .WithParsed((RepositoryModifyPackageOptions opts) => new RepositoryModifyPackageExecutor().Execute(opts, settings))
                    .WithParsed((ManifestCreateOptions opts) => new ManifestCreateExecutor().Execute(opts, settings))
                    .WithParsed((ManifestModifyOptions opts) => new ManifestModifyExecutor().Execute(opts, settings))
                    .WithParsed((ManifestAddAppOptions opts) => new ManifestAddAppExecutor().Execute(opts, settings))
                    .WithParsed((ManifestAddDriverOptions opts) => new ManifestAddDriverExecutor().Execute(opts, settings))
                    .WithParsed((ManifestAddFileOptions opts) => new ManifestAddFileExecutor().Execute(opts, settings))
                    .WithParsed((ManifestRemoveAppOptions opts) => new ManifestRemoveAppExecutor().Execute(opts, settings))
                    .WithParsed((ManifestRemoveDriverOptions opts) => new ManifestRemoveDriverExecutor().Execute(opts, settings))
                    .WithParsed((ManifestRemoveFileOptions opts) => new ManifestRemoveFileExecutor().Execute(opts, settings))
                    .WithParsed((ManifestModifyAppOptions opts) => new ManifestModifyAppExecutor().Execute(opts, settings))
                    .WithParsed((ManifestModifyDriverOptions opts) => new ManifestModifyDriverExecutor().Execute(opts, settings))
                    .WithParsed((ManifestModifyFileOptions opts) => new ManifestModifyFileExecutor().Execute(opts, settings))
                    .WithNotParsed(errs =>
                    {
                        var helpText = HelpText.AutoBuild(result, h =>
                        {
                            h.AutoHelp = true;
                            h.AutoVersion = false;
                            h.MaximumDisplayWidth = 120;
                            return HelpText.DefaultParsingErrorsHandler(result, h);
                        },
                        e => e);
                        Console.Error.WriteLine(helpText);
                        failed = true;
                    });
                return failed ? -1 : 0;
                
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return -1;
            }
        }
    }
}
