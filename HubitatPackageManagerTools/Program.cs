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
                with.HelpWriter = System.Console.Error;
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

            return result
                .MapResult((RepositoryCreateOptions opts) => RepositoryCreateExecutor.Execute(opts)
                , errs => {
                    var helpText = HelpText.AutoBuild(result, h =>
                    {
                        h.AutoHelp = true; //hide --help
                        h.AutoVersion = false; //hide --version		
                        return h;
                    },
                    e => e);
                    Console.Error.WriteLine(helpText);
                    return -1;
                });
        }
    }
}
