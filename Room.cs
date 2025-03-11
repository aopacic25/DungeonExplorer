namespace DungeonExplorer
{
    // Represents a room in the dungeon, with a name, description, and item.
    public class Room
    {
        // The room's name, which is read-only after initialisation.
        public string Name { get; private set; }
        // The base description of the room, minus the item.
        private string _roomBaseDescription;
        // The item part of the room description.
        private string _roomItemDescription;
        // The item in the room.
        private string _item;

        // Initialise the room with a name, base + item description, and item itself.
        public Room(string name, string roomBaseDescription, string roomItemDescription, string item)
        {
            Name = name;
            this._roomBaseDescription = roomBaseDescription;
            this._roomItemDescription = roomItemDescription;
            this._item = item;
        }

        // Return the room's description, both the base and item part included, if the item hasn't been picked up.
        public string GetDescription()
        {
            // If the item is still in the room, include it in the description
            if (_item != null)
            {
                return $"{_roomBaseDescription} {_roomItemDescription}";
            }
            // Otherwise, return only the base description
            return _roomBaseDescription;
        }

        // Returns the item in the room, or null if the item has been picked up.
        public string GetItem()
        {
            return _item;
        }

        // Removes the item from the room by setting it to null.
        public void RemoveItem()
        {
            _item = null;
        }
    }
}