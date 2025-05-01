namespace DungeonExplorer{

// Tracks collected items with quantity management.
// Provides filtered access to specific item types.
public class Inventory
{
    // Internal storage mapping items to their counts
    private Dictionary<Item, int> _items = new Dictionary<Item, int>();

    // Adds an item to inventory or increments count if already present
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
    
    // Removes one instance of an item from the inventory
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

    // Retrieves all distinct items in inventory
    public IEnumerable<Item> GetAllItems() => _items.Keys;
    // Filters inventory for Weapon items only
    public IEnumerable<Weapon> GetWeapons() => _items.Keys.OfType<Weapon>();
    // Filters inventory for Potion items only
    public IEnumerable<Potion> GetPotions() => _items.Keys.OfType<Potion>();

    // Gets the quantity of a specific item in inventory
    public int GetCount(Item item) => _items.TryGetValue(item, out int count) ? count : 0;

    // Generates a formatted string listing all inventory contents
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