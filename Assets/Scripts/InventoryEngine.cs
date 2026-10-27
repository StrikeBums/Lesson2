using System.Collections.Generic;
using UnityEngine;

public class InventoryEngine
{
    private const int MaxUniqueItems = 10;

    private Dictionary<string, int> items = new Dictionary<string, int>();

    public InventoryEngine(List<InventoryItem> startItems)
    {
        foreach (var item in startItems)
        {
            AddItem(item.item, item.count);
        }
    }

    public InventoryEngine(Dictionary<string, int> startItems)
    {
        foreach (var pair in startItems)
        {
            AddItem(pair.Key, pair.Value);
        }
    }
    

    public void AddItem(string item, int count = 1)
    {
        if (count <= 0)
        {
            Debug.LogWarning($"Нельзя добавить отрицательное или нулевое количество предмета \"{item}\"");
            return;
        }

        if (items.ContainsKey(item))
        {
            items[item] += count;
        }
        else
        {
            if (items.Count >= MaxUniqueItems)
            {
                Debug.LogWarning(
                    $"Нет места для \"{item}\": занято {items.Count} из {MaxUniqueItems}");
                return;
            }

            items.Add(item, count);
        }

        Debug.Log($"Добавлено: {item} × {count}");
    }

    public void RemoveItem(string item, int count = 1)
    {
        if (count <= 0)
        {
            Debug.LogWarning($"Некорректное количество для удаления: {count}");
            return;
        }

        if (!items.ContainsKey(item))
        {
            Debug.LogWarning($"Нельзя убрать \"{item}\": его нет в инвентаре");
            return;
        }

        if (items[item] < count)
        {
            Debug.LogWarning($"Нельзя убрать \"{item}\" × {count}: имеется только {items[item]}");
            return;
        }

        items[item] -= count;

        Debug.Log($"Удалено: {item} × {count}");

        if (items[item] == 0)
        {
            items.Remove(item);
            Debug.Log($"Предмет \"{item}\" полностью удалён из инвентаря");
        }
    }
    public List<InventoryItem> GetItems()
    {
        var result = new List<InventoryItem>();

        foreach (var entry in items)
            result.Add(new InventoryItem { item = entry.Key, count = entry.Value });

        return result;
    }

    public void Print()
    {
        Debug.Log("===== ИНВЕНТАРЬ =====");

        int totalCount = 0;

        foreach (var pair in items)
        {
            Debug.Log($"{pair.Key} × {pair.Value}");
            totalCount += pair.Value;
        }
        Debug.Log("============");
        
        Debug.Log($"Кол-во уникальных предметов: {items.Count}, общее кол-во: {totalCount}");

        Debug.Log($"Занято ячеек: {items.Count} из {MaxUniqueItems}");
    }
}