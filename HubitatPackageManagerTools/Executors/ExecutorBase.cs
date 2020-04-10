using HubitatPackageManagerTools.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace HubitatPackageManagerTools.Executors
{
    internal class ExecutorBase
    {
        protected JObject DownloadJsonFile(string url)
        {
            try
            {
                StringReader sr = new StringReader(DownloadFile(url));
                return (JObject)JToken.ReadFrom(new JsonTextReader(sr));
            }
            catch
            {
                return null;
            }
        }

        protected string DownloadFile(string url)
        {
            try
            {
                using WebClient wc = new WebClient();
                return wc.DownloadString(url);
            }
            catch
            {
                return null;
            }
        }

        protected void SetNullableProperty(JObject json, string property, string value)
        {
            if (value.IsSpecified())
                json[property] = value;
            else if (value.IsNullValue())
                json.Remove(property);
        }
        protected void SetNonNullPropertyIfSpecified(JToken json, string property, string value)
        {
            if (!string.IsNullOrEmpty(value))
                json[property] = value;
        }

        protected JArray EnsureArrayExists(JObject json, string arrayName)
        {
            var array = json[arrayName] as JArray;
            if (array == null)
            {
                array = new JArray();
                json.Add(arrayName, array);
                return array;
            }
            else
                return array;
        }
    }
}
