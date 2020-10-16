using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace CQRS
{
    public class Inventory
    {
        [XmlIgnore]
        private CommandStore store;

        public uint InventoryItemSeed
        {
            get { return InventoryItem.Seed; }
            set { InventoryItem.Seed = value; }
        }
        public List<InventoryItem> items
        {
            get { return itemsByKey.Values.ToList(); }
            set { itemsByKey = value.ToDictionary(i => i.id, i => i); }
        }
        // we can't serialize Dictionary so we ignore it or an error will pop up
        [XmlIgnore]
        public Dictionary<uint, InventoryItem> itemsByKey =
            new Dictionary<uint, InventoryItem>();

        public Inventory() { }

        public Inventory(CommandStore store)
        {
            this.store = store;
        }

        public void AddItem(InventoryItem item)
        {
            itemsByKey.Add(item.id, item);
            store.Add(new CreateInventoryItemCommand(item));
        }

        public void ProcessCommand(InventoryItemCommand command)
        {
            var ciic = command as CreateInventoryItemCommand;
            if(ciic!=null)
            {
                var item = ciic.Item;
                itemsByKey.Add(item.id,item);
            }
        }
    }
}
