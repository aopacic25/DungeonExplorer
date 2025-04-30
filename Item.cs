namespace DungeonExplorer
{

    public abstract class Item : ICollectible
    {
        public string Name { get; }
        protected Item(string name) => Name = name;
        public abstract void Use(Player player);
    }

    public class Weapon : Item
    {
        public int Damage { get; }
        public Weapon(string name, int damage) : base(name) => Damage = damage;
        public override void Use(Player player) => Console.WriteLine($"You attack with {Name} (Damage: {Damage})");    
    }

    public class Potion : Item
    {
        public int HealAmount { get; }
        public Potion(string name, int heal) : base(name) => HealAmount = heal;
        public override void Use(Player player)
        {
            player.Heal(HealAmount);
            Console.WriteLine($"You drank {Name} and healed {HealAmount} HP!");
        }
    
    }

    public class Key : Item
    {
        public Key(string name) : base(name) {}
        public override void Use(Player player) {}

    }
}