namespace DungeonExplorer
{

    // Interface for objects that can be collected in inventory.
    // Forces implementation of Use() method for all collectibles.
    public interface ICollectible
    {
        // Display name of the collectible
        string Name { get; }
        // Defines behaviour when the item is used by a player
        void Use(Player player);
    }
}