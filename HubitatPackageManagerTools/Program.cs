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
            var result = parser.ParseArguments<
                ManifestAddAppOptions,
                ManifestAddDriverOptions,
                ManifestCreateOptions,
                ManifestModifyAppOptions,
                ManifestModifyDriverOptions,
                ManifestModifyOptions,
                ManifestRemoveAppOptions,
                ManifestRemoveDriverOptions,
                ManifestConvertOptions,
                RepositoryAddPackageOptions,
                RepositoryCreateOptions,
                RepositoryModifyOptions,
                RepositoryModifyPackageOptions,
                RepositoryRemovePackageOptions
            >(args);
            try
            {
                WebClient wc = new WebClient();
                var settingsFileContents = wc.DownloadString(settingsJson);
                Settings settings = new Settings(settingsFileContents);
                
                return result
                    .MapResult(
                        (RepositoryCreateOptions opts) => new RepositoryCreateExecutor().Execute(opts, settings),
                        (RepositoryModifyOptions opts) => new RepositoryModifyExecutor().Execute(opts, settings),
                        (RepositoryAddPackageOptions opts) => new RepositoryAddPackageExecutor().Execute(opts, settings),
                        (RepositoryRemovePackageOptions opts) => new RepositoryRemovePackageExecutor().Execute(opts, settings),
                        (RepositoryModifyPackageOptions opts) => new RepositoryModifyPackageExecutor().Execute(opts, settings),
                        (ManifestCreateOptions opts) => new ManifestCreateExecutor().Execute(opts, settings),
                        (ManifestModifyOptions opts) => new ManifestModifyExecutor().Execute(opts, settings),
                        (ManifestAddAppOptions opts) => new ManifestAddAppExecutor().Execute(opts, settings),
                        (ManifestAddDriverOptions opts) => new ManifestAddDriverExecutor().Execute(opts, settings),
                        (ManifestRemoveAppOptions opts) => new ManifestRemoveAppExecutor().Execute(opts, settings),
                        (ManifestRemoveDriverOptions opts) => new ManifestRemoveDriverExecutor().Execute(opts, settings),
                        (ManifestModifyAppOptions opts) => new ManifestModifyAppExecutor().Execute(opts, settings),
                        (ManifestModifyDriverOptions opts) => new ManifestModifyDriverExecutor().Execute(opts, settings),
                        (ManifestConvertOptions opts) => new ManifestConvertExecutor().Execute(opts, settings)
                    , errs =>
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
                        return -1;
                    }
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"An error occurred: {e.Message}");
                return -1;
            }
        }
    }
}
