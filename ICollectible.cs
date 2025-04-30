namespace DungeonExplorer
{
    public interface ICollectible
    {
        string Name { get; }
        void Use(Player player);
    }
}