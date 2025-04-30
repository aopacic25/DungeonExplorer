namespace DungeonExplorer
{

    public class GameMap
    {
        private List<Room> _rooms = new List<Room>();
        private Dictionary<Room, Dictionary<string, Room>> _connections = new Dictionary<Room, Dictionary<string, Room>>();
        public Room CurrentRoom { get; private set; }

        public void AddRoom(Room room) => _rooms.Add(room);
        public void SetCurrentRoom(Room room) => CurrentRoom = room;

        public Room GetRoomByName(string name) =>
            _rooms.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

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


        public void ConnectRooms(Room fromRoom, string direction, Room toRoom)
        {
            if (!_connections.ContainsKey(fromRoom))
                _connections[fromRoom] = new Dictionary<string, Room>();

            _connections[fromRoom][direction] = toRoom;
        }
    }
}