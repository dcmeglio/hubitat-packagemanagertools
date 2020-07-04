using HubitatPackageManagerTools.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestConvertExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestConvertOptions options, Settings settings)
        {
            using var file = File.OpenText(options.SmartThingsFile);
            var stFile = (JObject)JToken.ReadFrom(new JsonTextReader(file));

			var newManifestContents = new JObject
			{
				["packageName"] = stFile["name"],
				["author"] = stFile["author"],
				["minimumHEVersion"] = "0.0",
				["dateReleased"] = DateTime.Now.ToString("yyyy-MM-dd"),
				["apps"] = new JArray(),
				["drivers"] = new JArray()
			};

			if (!string.IsNullOrEmpty(stFile["docUrl"]?.ToString()))
				newManifestContents["documentationLink"] = stFile["docUrl"];

			var stParentApp = stFile["smartApps"]["parent"];
			var parentApp = JObject.FromObject(new
			{
				id = Guid.NewGuid(),
				name = stParentApp["name"],
				@namespace = stFile["namespace"],
				version = stParentApp["version"],
				location = $"https://raw.githubusercontent.com/{stFile["repoOwner"]}/{stFile["repoName"]}/{stFile["repoBranch"]}/{stParentApp["appUrl"]}",
				required = !((bool?)stParentApp["optional"] ?? true),
				oauth = (bool?)stParentApp["oAuth"] ?? false
			});
			(newManifestContents["apps"] as JArray).Add(parentApp);
			foreach (var stApp in stFile["smartApps"]["children"])
			{
				var app = JObject.FromObject(new
				{
					id = Guid.NewGuid(),
					name = stApp["name"],
					@namespace = stFile["namespace"],
					version = stApp["version"],
					location = $"https://raw.githubusercontent.com/{stFile["repoOwner"]}/{stFile["repoName"]}/{stFile["repoBranch"]}/{stApp["appUrl"]}",
					required = !((bool?)stApp["optional"] ?? true),
					oauth = (bool?)stApp["oAuth"] ?? false
				});
				(newManifestContents["apps"] as JArray).Add(app);
			}

			foreach (var dth in stFile["deviceHandlers"])
			{
				var driver = JObject.FromObject(new
				{
					id = Guid.NewGuid(),
					name = dth["name"],
					@namespace = stFile["namespace"],
					version = dth["version"],
					location = $"https://raw.githubusercontent.com/{stFile["repoOwner"]}/{stFile["repoName"]}/{stFile["repoBranch"]}/{dth["appUrl"]}",
					required = !((bool?)dth["optional"] ?? true)
				});
				(newManifestContents["drivers"] as JArray).Add(driver);
			}

			SaveManifest(options, newManifestContents);

            return 0;
        }
    }
}
