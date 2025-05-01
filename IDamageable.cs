namespace DungeonExplorer
{

    // Interface for entities that can receive damage.
    // Implemented by both Player and Monster classes.
    public interface IDamageable
    {
        // Applies damage to the entity
        void TakeDamage(int damage);
    }
}