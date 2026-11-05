using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    public class InventoryEngine
    {
        private const int MaxUniqueItems = 10;
        private const int MaxStackItems = 10;
        private Dictionary<string, int> m_items = new();

        public InventoryEngine(List<InventoryItem> startItems)
        {
            foreach (var item in startItems)
            {
                AddItem(item.item, item.count);
            }
        }

        public bool AddItem(string item, int count = 1)
        {
            if (count <= 0)
            {
                Debug.LogWarning($"Нельзя добавить отрицательное или нулевое количество предмета \"{item}\"");
                return false;
            }

            if (m_items.ContainsKey(item))
            {
                if (m_items[item] + count > MaxStackItems)
                {
                    Debug.LogWarning(
                        $"Нельзя добавить {count} {item}: в ячейке уже {m_items[item]}, максимум {MaxStackItems}");
                    return false;

                }

                m_items[item] += count;
            }
            else
            {
                if (m_items.Count >= MaxUniqueItems)
                {
                    Debug.LogWarning(
                        $"Нет места для \"{item}\": занято {m_items.Count} из {MaxUniqueItems}");
                    return false;
                }

                if (count > MaxStackItems)
                {
                    Debug.LogWarning(
                        $"Нельзя создать ячейку {item} размером {count}: максимум {MaxStackItems}");
                    return false;
                }

                m_items.Add(item, count);
            }

            Debug.Log($"Добавлено: {item} × {count}");
            return true;
        }

        public bool RemoveItem(string item, int count = 1)
        {
            if (count <= 0)
            {
                Debug.LogWarning($"Некорректное количество для удаления: {count}");
                return false;
            }

            if (!m_items.ContainsKey(item))
            {
                Debug.LogWarning($"Нельзя убрать \"{item}\": его нет в инвентаре");
                return false;
            }

            if (m_items[item] < count)
            {
                Debug.LogWarning($"Нельзя убрать \"{item}\" × {count}: имеется только {m_items[item]}");
                return false;
            }

            m_items[item] -= count;

            Debug.Log($"Удалено: {item} × {count}");

            if (m_items[item] == 0)
            {
                m_items.Remove(item);
                
                Debug.Log($"Предмет \"{item}\" полностью удалён из инвентаря");
            }
            return true;
        }
        

        public List<InventoryItem> GetItems()
        {
            var result = new List<InventoryItem>();

            foreach (var entry in m_items)
            {
                result.Add(new InventoryItem
                {
                    item = entry.Key, count = entry.Value

                });
            }

            return result;
        }

        public void Print()
        {
            Debug.Log("===== ИНВЕНТАРЬ =====");

            int totalCount = 0;

            foreach (var pair in m_items)
            {
                Debug.Log($"{pair.Key} × {pair.Value}");
                totalCount += pair.Value;
            }

            Debug.Log("============");

            Debug.Log($"Кол-во уникальных предметов: {m_items.Count}, общее кол-во: {totalCount}");

            Debug.Log($"Занято ячеек: {m_items.Count} из {MaxUniqueItems}");
        }
    }
}