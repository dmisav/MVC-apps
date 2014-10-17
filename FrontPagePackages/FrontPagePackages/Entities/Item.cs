using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontPagePackages.Entities
{
    public class Item
    {
        public Item(string displayName, string toolTieText)
        {
            this.DisplayName = displayName;
            this.ToolTieText = toolTieText;
        }

        public string DisplayName { get; private set; }
        public string ToolTieText { get; private set; }
    }
}