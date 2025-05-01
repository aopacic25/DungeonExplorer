namespace DungeonExplorer
{

    // Represents a location in the game world containing items and monsters.
    // Part of the GameMap navigation system.
    public class Room
    {
        // Room display name
        public string Name { get; }
        // Description of the room when player enters
        private string _description;
        // Item present in the room
        private Item _item;
        // Monster present in the room
        private Monster _monster;

        // Removes the current monster from the room
        public void RemoveMonster()
        {
            _monster = null;
        }

        // Creates a new room instance
        public Room(string name, string description, Item item = null, Monster monster = null)
        {
            Name = name;
            _description = description;
            _item = item;
            _monster = monster;
        }

        // Generates description including contents
        public string GetDescription() =>
            $"{_description}\n" +
            (_item != null ? $"You see a {_item.Name}.\n" : "") +
            (_monster != null && _monster.Health > 0 ? $"A {_monster.Name} dwells here.\n" : "");

        // Removes and returns the room's item, if any
        public Item TakeItem()
        {
            var item = _item;
            _item = null;
            return item;
        }

        // Gets the room's current monster, if any
        public Monster GetMonster() => _monster;
    }
}