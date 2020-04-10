﻿using HubitatPackageManagerTools.Options;
using Newtonsoft.Json.Linq;
using System;

namespace HubitatPackageManagerTools.Executors
{
    internal class ManifestAddAppExecutor : ManifestExecutorBase
    {
        public int Execute(ManifestAddAppOptions options)
        {
            JObject manifestContents = OpenExistingManifest(options);

            JArray apps = EnsureArrayExists(manifestContents, "apps");
            var groovyFile = DownloadGroovyFile(options.Location);

            string name;
            string @namespace;
            if (groovyFile != null)
                (name, @namespace) = GetNameAndNamespace(groovyFile);
            else
                throw new ApplicationException($"The app Groovy file {options.Location} either was not found or is not valid.");

            if (name == null || @namespace == null)
                throw new ApplicationException($"The app Groovy file {options.Location} could not be parsed to determine the name and namespace. Please report this as a bug.");
            var app = JObject.FromObject(new
            {
                id = Guid.NewGuid().ToString(),
                name = @name,
                @namespace = @namespace,
                location = options.Location,
                required = options.Required,
                oauth = options.Oauth
            });
            SetNonNullPropertyIfSpecified(app, "version", options.Version);

            apps.Add(app);

            SaveManifest(options, manifestContents);
            return 0;
        }
    }
}
