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
        public override void Use(Player player)
        {
            Console.WriteLine($"No enemies to attack with {Name}.");
        }   
    }

    public class Potion : Item
    {
        public int HealAmount { get; }
        public Potion(string name, int heal) : base(name) => HealAmount = heal;
        public override void Use(Player player)
        {
            if (player.Health >= Creature.MaxHealth)
            {
                Console.WriteLine($"You're already at full health ({Creature.MaxHealth}/{Creature.MaxHealth}).");
                return;
            }

            int oldHealth = player.Health;
            player.Heal(HealAmount);
            int actualHeal = player.Health - oldHealth;

            if (actualHeal < HealAmount)
            {
                Console.WriteLine($"The potion healed you to full health ({Creature.MaxHealth}/{Creature.MaxHealth}).");
            }
            
        }
    
    }

    public class Key : Item
    {
        public Key(string name) : base(name) {}
        public override void Use(Player player) {}

    }
}