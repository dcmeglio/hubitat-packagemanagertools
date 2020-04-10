using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestExecutorBase : ExecutorBase
    {
        protected static Regex nameMatcher = new Regex("definition\\s*?\\(.*?name:\\s*\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
        protected static Regex namespaceMatcher = new Regex("definition\\s*?\\(.*?namespace:\\s*\"([^\"]*)\"", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.Singleline);
        protected JObject OpenExistingManifest(ManifestOptionsBase options)
        {
            using var file = File.OpenText(options.ManifestFile);
            return (JObject)JToken.ReadFrom(new JsonTextReader(file));
        }
        protected void SaveManifest(ManifestOptionsBase options, JToken contents)
        {
            File.WriteAllText(options.ManifestFile, contents.ToString());
        }

        protected string DownloadGroovyFile(string url)
        {
            try
            {
                using WebClient wc = new WebClient();
                return wc.DownloadString(url).Replace("\r", "").Replace("\n", "");
            }
            catch
            {
                return null;
            }
        }
    }
}
