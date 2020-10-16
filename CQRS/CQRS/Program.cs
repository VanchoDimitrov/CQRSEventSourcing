using System;

namespace CQRS
{
    public class Program
    {
        //Usage of the InventoryHelper

        private static Inventory inventory;
        private static CommandStore store;

        static void Main(string[] args)
        {
            // Load firstly the records
            store = CommandStoreHelper.Load();
            inventory = InventoryHelper.LoadFromStore(store);

            //var phone = new InventoryItem("iPhone", "Mobile Phone");
            //inventory.AddItem(phone);

            InventoryHelper.Save(inventory);
            CommandStoreHelper.Save(store);
        }
    }
}
