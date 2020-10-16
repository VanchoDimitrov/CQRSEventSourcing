using System.IO;
using System.Xml.Serialization;

namespace CQRS
{
    public class InventoryHelper
    {
        private static string filename = "inventory.txt";

        // if we delete inventory the info loads from the store.txt
        public static Inventory LoadFromStore(CommandStore store)
        {
            Inventory i = new Inventory(store);

            foreach(var e in store)
            {
                i.ProcessCommand(e);
            }

            return i;
        }

        public static Inventory Load(CommandStore store)
        {
            // If inventory does not exists then take it from the event source,
            // store.txt and create it
            if (!File.Exists(filename)) return new Inventory(store);

            var xs = new XmlSerializer(typeof(Inventory));
            using (var fs = new FileStream(filename, FileMode.Open,
                FileAccess.Read))
            {
                return (Inventory)xs.Deserialize(fs);
            }
        }

        public static void Save(Inventory inventory)
        {
            var xs = new XmlSerializer(typeof(Inventory));
            using (var fs = new FileStream(filename, FileMode.Create,
                FileAccess.Write))
            {
                xs.Serialize(fs, inventory);
            }
        }
    }
}
