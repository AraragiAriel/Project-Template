using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class WeightedList<T>
{
    [System.Serializable]
    public class Item
    {
        public T obj;
        public float weight;
    }
    
    public List<Item> items = new();

    public T Draw()
    {
        int id = RNG.DrawWeightedIndex(items.Select(i => i.weight).ToList());
        return items[id].obj;
    }

    public T Draw(List<T> filter)
    {
        var filteredList = items.Where(i => filter.Contains(i.obj)).ToList();
        int id = RNG.DrawWeightedIndex(filteredList.Select(i => i.weight).ToList());
        return filteredList[id].obj;
    }

    public WeightedList(){}

    public WeightedList(WeightedList<T> list)
    {
        items = new(list.items);
    }
}