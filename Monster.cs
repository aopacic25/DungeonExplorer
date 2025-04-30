namespace DungeonExplorer
{

    public class Monster : Creature
    {
        public int Damage { get; }
        public Monster(string name, int health, int damage) : base(name, health)
        {
        Damage = damage;
        }

        public override void Die()
        {
            Console.WriteLine($"{Name} has been defeated!");
        }

        public void Attack(IDamageable target)
        {
            Console.WriteLine($"{Name} attacks for {Damage} damage!");
            target.TakeDamage(Damage);
        }
    }
}