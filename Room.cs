namespace DungeonExplorer
{

    public class Room
    {
        public string Name { get; }
        private string _description;
        private Item _item;
        private Monster _monster;

        public Room(string name, string description, Item item = null, Monster monster = null)
        {
            Name = name;
            _description = description;
            _item = item;
            _monster = monster;
        }

        public string GetDescription() =>
            $"{_description}\n" +
            (_item != null ? $"You see a {_item.Name}.\n" : "") +
            (_monster != null ? $"A {_monster.Name} dwells here!\n" : "");

        public Item TakeItem()
        {
            var item = _item;
            _item = null;
            return item;
        }

        public Monster GetMonster() => _monster;
    }
}