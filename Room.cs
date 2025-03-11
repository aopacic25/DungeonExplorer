namespace DungeonExplorer
{
    // Represents a room in the dungeon, with a name, description, and item.
    public class Room
    {
        // The room's name, which is read-only after initialisation.
        public string Name { get; private set; }
        // The base description of the room, minus the item.
        private string roomBaseDescription;
        // The item part of the room description.
        private string roomItemDescription;
        // The item in the room.
        private string item;

        // Initialise the room with a name, base + item description, and item itself.
        public Room(string name, string roomBaseDescription, string roomItemDescription, string item)
        {
            Name = name;
            this.roomBaseDescription = roomBaseDescription;
            this.roomItemDescription = roomItemDescription;
            this.item = item;
        }

        // Return the room's description, both the base and item part included, if the item hasn't been picked up.
        public string GetDescription()
        {
            // If the item is still in the room, include it in the description
            if (item != null)
            {
                return $"{roomBaseDescription} {roomItemDescription}";
            }
            // Otherwise, return only the base description
            return roomBaseDescription;
        }

        // Returns the item in the room, or null if the item has been picked up.
        public string GetItem()
        {
            return item;
        }

        // Removes the item from the room by setting it to null.
        public void RemoveItem()
        {
            item = null;
        }
    }
}