using System.Collections.Generic;

namespace FrontPagePackages.Entities
{
    public class Package : IDisplayableName
    {
        public Package(string displayName)
        {
            this.DisplayName = displayName;
        }

        public string DisplayName { get; private set; }

        public List<Item> items { get; set; }
    }
}