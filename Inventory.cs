namespace DungeonExplorer{

public class Inventory
{
    private List<Item> _items = new List<Item>();

    public void Add(Item item) => _items.Add(item);
    public void Remove(Item item) => _items.Remove(item);

    public IEnumerable<Item> GetAllItems() => _items;
    public IEnumerable<Weapon> GetWeapons() => _items.OfType<Weapon>();
    public IEnumerable<Potion> GetPotions() => _items.OfType<Potion>();

    public string Contents() =>
        _items.Count == 0 ? "Empty" : string.Join(", ", _items.Select(i => i.Name));
}
}