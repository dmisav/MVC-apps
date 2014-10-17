using System.Collections.Generic;

namespace FrontPagePackages.Entities
{
    public class Package
    {
        public Package(string displayName)
        {
            this.DispayName = displayName;
        }

        public string DispayName { get; private set; }

        public List<Item> items { get; set; }
    }
}