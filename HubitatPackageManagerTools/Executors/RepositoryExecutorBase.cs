using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryExecutorBase : ExecutorBase
    {
        protected JObject OpenExistingRepository(RepositoryOptionsBase options)
        {
            using var file = File.OpenText(options.RepositoryFile);
            var fileContents = (JObject)JToken.ReadFrom(new JsonTextReader(file));
            // Check if all packages have an ID
            if (fileContents["packages"] != null)
            {
                foreach (var package in fileContents["packages"])
                {
                    if (package["id"] == null)
                        package["id"] = Guid.NewGuid().ToString();
                }
            }
            return fileContents;
        }

        protected void SaveRepository(RepositoryOptionsBase options, JToken contents)
        {
            File.WriteAllText(options.RepositoryFile, contents.ToString());
        }
    }
}
