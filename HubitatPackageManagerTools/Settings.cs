using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HubitatPackageManagerTools
{
    internal class Settings
    {
        private List<string> categories = new List<string>();
        private List<string> tags = new List<string>();
        public Settings(string settings)
        {
            var settingsJson = (JObject)JToken.Parse(settings);
            foreach (var category in settingsJson["categories"])
            {
                categories.Add(category.ToString());
            }
            foreach (var tag in settingsJson["tags"])
            {
                tags.Add(tag.ToString());
            }
            categories.Sort();
            tags.Sort();
        }

        public bool ValidateCategory(string category)
        {
            return categories.BinarySearch(category) > -1;
        }

        public bool ValidateTag(string tag)
        {
            return tags.BinarySearch(tag) > -1;
        }

        public bool ValidateTags(IEnumerable<string> tagsToValidate)
        {
            foreach (var tag in tagsToValidate)
            {
                if (tags.BinarySearch(tag) < 0)
                    return false;
            }
            return true;
        }
    }
}
