﻿using CommandLine;

namespace HubitatPackageManagerTools.Options
{
    internal class ManifestOptionsBase
    {
        [Value(0, HelpText = "The local path to the package manifest JSON.", MetaName = "manifestFile")]
        public string ManifestFile { get; set; }
    }
}
