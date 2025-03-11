using System.Collections.Generic;

namespace DungeonExplorer
{
    // Represents the player, tracking their name, health, and inventory.
    public class Player
    {
        // The player's name, which is read-only after initialisation.
        public string Name { get; private set; }
        // The player's health, which is read-only after initialisation.
        public int Health { get; private set; }
        // The player's inventory, in form of a list of items.
        private List<string> inventory = new List<string>();

        // Initialises the player with a name and health.
        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        // Adds an item to the player's inventory if it's not already present there.
        public void PickUpItem(string item)
        {
            if (!inventory.Contains(item))
            {
                inventory.Add(item);
            }
        }
        // Returns a string representation of the player's inventory.
        public string InventoryContents()
        {
            // Return "Empty" if the inventory is empty, otherwise join items with commas.
            return inventory.Count == 0 ? "Empty" : string.Join(", ", inventory);
        }
    }
}