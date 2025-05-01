namespace DungeonExplorer
{

    // Abstract base class for all in-game items.
    // Implements ICollectible interface for inventory interactions.
    public abstract class Item : ICollectible
    {
        // Displays name of the item 
        public string Name { get; }
        // Base constructor for items
        protected Item(string name) => Name = name;
        // Defines behaviour when the item is used and must be implemented by derived classes
        public abstract void Use(Player player);

        // Equality comparison for inventory management
        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   Name == item.Name &&
                   GetType() == obj.GetType();
        }

        // Generates hash code for dictionary storage
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, GetType());
        }
    }  

    // Weapon items that deal damage in combat
    public class Weapon : Item
    {
        public int Damage { get; }
        public Weapon(string name, int damage) : base(name) => Damage = damage;
        public override void Use(Player player)
        {
            Console.WriteLine($"No enemies to attack with {Name}.");
        }   
    }

    // Consumable item that restores player character's health
    public class Potion : Item
    {
        public int HealAmount { get; }
        public Potion(string name, int heal) : base(name) => HealAmount = heal;
        // Potion uses behaviour with health cap validation
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