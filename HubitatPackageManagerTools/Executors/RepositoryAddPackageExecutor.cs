using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;

namespace HubitatPackageManagerTools.Executors
{
    internal class RepositoryAddPackageExecutor : RepositoryExecutorBase
    {
        public int Execute(RepositoryAddPackageOptions options)
        {
            JObject repositoryContents = OpenExistingRepository(options);

            JArray packages = new JArray();
            if (repositoryContents["packages"] == null)
                repositoryContents.Add("packages", packages);
            else
                packages = repositoryContents["packages"] as JArray;

            string name = options.Name;

            if (name == null)
            {
                var manifestContents = DownloadJsonFile(options.Manifest);

                if (manifestContents != null)
                    name = manifestContents["packageName"].ToString();

            }
            packages.Add(JObject.FromObject(new
            {
                name = name,
                category = options.Category,
                location = options.Manifest,
                description = options.Description
            }));


            SaveRepository(options, repositoryContents);
            return 0;
        }
    }
}
