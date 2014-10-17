using FrontPagePackages.Entities;
using System.Collections.Generic;

namespace FrontPagePackages.DataContext
{
    public class DataProvider
    {
        public List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item("1","Some ToolTie for item 1"),
                new Item("2","Some ToolTie for item 2"),
                new Item("3","Some ToolTie for item 3"),
                new Item("4","Some ToolTie for item 4")
            };
        }

        public List<Package> GetPackages()
        {
            var items = this.GetItems();
            return new List<Package>()
            {
                new Package("MCINFlex")
                {
                    items = new List<Item>(){items[0]}
                },
                new Package("MCINBasis")
                {
                    items = new List<Item>(){items[0], items[1]}
                },
                new Package("MCINPro")
                {
                    items = new List<Item>(){items[0], items[1],items[2], items[3]}
                }
            };
        }
    }
}