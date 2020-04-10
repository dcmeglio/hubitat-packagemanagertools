using System;
using CommandLine;
using CommandLine.Text;
using HubitatPackageManagerTools.Executors;
using HubitatPackageManagerTools.Options;

namespace HubitatPackageManagerTools
{
    class Program
    {
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
                RepositoryAddPackageOptions,
                RepositoryCreateOptions,
                RepositoryModifyOptions,
                RepositoryModifyPackageOptions,
                RepositoryRemovePackageOptions
            >(args);
            try
            {
                return result
                    .MapResult(
                        (RepositoryCreateOptions opts) => new RepositoryCreateExecutor().Execute(opts),
                        (RepositoryModifyOptions opts) => new RepositoryModifyExecutor().Execute(opts),
                        (RepositoryAddPackageOptions opts) => new RepositoryAddPackageExecutor().Execute(opts),
                        (RepositoryRemovePackageOptions opts) => new RepositoryRemovePackageExecutor().Execute(opts),
                        (RepositoryModifyPackageOptions opts) => new RepositoryModifyPackageExecutor().Execute(opts),
                        (ManifestCreateOptions opts) => new ManifestCreateExecutor().Execute(opts),
                        (ManifestModifyOptions opts) => new ManifestModifyExecutor().Execute(opts),
                        (ManifestAddAppOptions opts) => new ManifestAddAppExecutor().Execute(opts),
                        (ManifestAddDriverOptions opts) => new ManifestAddDriverExecutor().Execute(opts),
                        (ManifestRemoveAppOptions opts) => new ManifestRemoveAppExecutor().Execute(opts),
                        (ManifestRemoveDriverOptions opts) => new ManifestRemoveDriverExecutor().Execute(opts),
                        (ManifestModifyAppOptions opts) => new ManifestModifyAppExecutor().Execute(opts),
                        (ManifestModifyDriverOptions opts) => new ManifestModifyDriverExecutor().Execute(opts)
                    , errs =>
                    {
                        var helpText = HelpText.AutoBuild(result, h =>
                        {
                            h.AutoHelp = true;
                            h.AutoVersion = false;
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
