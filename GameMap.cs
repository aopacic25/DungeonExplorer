namespace DungeonExplorer
{

    // Manages dungeon layout and room connections
    // Handles player navigation between rooms
    public class GameMap
    {
        // List of all rooms in the dungeon
        private List<Room> _rooms = new List<Room>();
        // Directional connections between rooms
        private Dictionary<Room, Dictionary<string, Room>> _connections = new Dictionary<Room, Dictionary<string, Room>>();
        // The player's current location
        public Room CurrentRoom { get; private set; }

        // Adds a room to the game world
        public void AddRoom(Room room) => _rooms.Add(room);
        // Sets the player's current location
        public void SetCurrentRoom(Room room) => CurrentRoom = room;

        // Finds room by name
        public Room GetRoomByName(string name) =>
            _rooms.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        // Retrieves adjacent room in specified direction
        public Room GetAdjacentRoom(Room currentRoom, string direction)
        {
            if (_connections.TryGetValue(currentRoom, out var directions))
            {
                if (directions.TryGetValue(direction, out var nextRoom))
                {
                    return nextRoom;
                }
            }
            return null;
        }

        // Creates a one-way connection between rooms
        public void ConnectRooms(Room fromRoom, string direction, Room toRoom)
        {
            if (!_connections.ContainsKey(fromRoom))
                _connections[fromRoom] = new Dictionary<string, Room>();

            _connections[fromRoom][direction] = toRoom;
        }
    }
}