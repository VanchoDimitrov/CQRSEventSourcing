namespace CQRS
{
    public class InventoryItem
    {
        public static uint Seed = 0;
        public uint id;
        public string Name;
        public string Category;

        public InventoryItem() { }

        public InventoryItem(string name, string category)
        {
            this.Name = name;
            this.Category = category;
            this.id = ++Seed;
        }
    }
}
