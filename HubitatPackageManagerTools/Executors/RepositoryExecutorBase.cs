﻿using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryExecutorBase : ExecutorBase
    {
        protected JObject OpenExistingRepository(RepositoryOptionsBase options)
        {
            using var file = File.OpenText(options.RepositoryFile);
            return (JObject)JToken.ReadFrom(new JsonTextReader(file));
        }

        protected void SaveRepository(RepositoryOptionsBase options, JToken contents)
        {
            File.WriteAllText(options.RepositoryFile, contents.ToString());
        }
    }
}
