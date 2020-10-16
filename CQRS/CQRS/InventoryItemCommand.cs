using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace CQRS
{
    // Event store
    public class CommandStore:Collection<InventoryItemCommand>
    {

    }

    public class CommandStoreHelper
    {
        private static string filename = "store.txt";

        public static CommandStore Load()
        {
            if (!File.Exists(filename)) return new CommandStore();

            var xs = new XmlSerializer(typeof(CommandStore));
            using (var fs = new FileStream(filename, FileMode.Open,
                FileAccess.Read))
            {
                return (CommandStore)xs.Deserialize(fs);
            }
        }

        public static void Save(CommandStore store)
        {
            var xs = new XmlSerializer(typeof(CommandStore));
            using (var fs = new FileStream(filename, FileMode.Create,
                FileAccess.Write))
            {
                xs.Serialize(fs, store);
            }
        }
    }

    [XmlInclude(typeof(CreateInventoryItemCommand))]
    public class InventoryItemCommand
    {
        public enum InventoryCommandType
        {
            Create,
            Update,
            Delete
        }

        public InventoryCommandType Type;
    }

    public class CreateInventoryItemCommand:InventoryItemCommand
    {
        public InventoryItem Item;

        public CreateInventoryItemCommand() {
            this.Type = InventoryCommandType.Create;
        }

        public CreateInventoryItemCommand(InventoryItem item)
            :this()
        {
            this.Item = item;
        }
    }
}
