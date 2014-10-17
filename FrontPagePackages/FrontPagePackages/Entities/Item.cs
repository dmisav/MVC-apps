namespace FrontPagePackages.Entities
{
    public class Item : IDisplayableName
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