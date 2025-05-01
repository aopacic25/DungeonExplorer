namespace DungeonExplorer{

public class Inventory
{
    private Dictionary<Item, int> _items = new Dictionary<Item, int>();

    public void Add(Item item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item]++;
        }
        else
        {
            _items[item] = 1;
        }
    }
    public void Remove(Item item)
    {
        if (_items.ContainsKey(item))
        {
            _items[item]--;
            {
                if (_items[item] <= 0)
                {
                    _items.Remove(item);
                }
            }
        }
    }

    public IEnumerable<Item> GetAllItems() => _items.Keys;
    public IEnumerable<Weapon> GetWeapons() => _items.Keys.OfType<Weapon>();
    public IEnumerable<Potion> GetPotions() => _items.Keys.OfType<Potion>();

    public int GetCount(Item item) => _items.TryGetValue(item, out int count) ? count : 0;

    public string Contents()
    {
        if (_items.Count == 0) return "Empty";

        var contents = new List<string>();
        foreach (var kvp in _items)
        {
            if (kvp.Value > 1)
            {
                contents.Add($"{kvp.Key.Name} x{kvp.Value}");
            }
            else
            {
                contents.Add(kvp.Key.Name);
            }
        }
        return string.Join(", ", contents);
    }
        
}
}