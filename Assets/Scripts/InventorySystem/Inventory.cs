using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    [System.Serializable]
    public class InventoryItem
    {
        public string item;
        public int count;
    }

    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventoryItem> m_startItems = new();

        private InventoryEngine m_inventory;

        private void Start()
        {
            if (m_startItems.Count == 0)
            {
                m_startItems = new List<InventoryItem>
                {
                    new InventoryItem { item = "Меч", count = 1 },
                };
            }

            m_inventory = new InventoryEngine(m_startItems);
            m_inventory.AddItem("Лук", 1);
            m_inventory.AddItem("Меч", 2);
            m_inventory.AddItem("Зелье", 5);
            m_inventory.AddItem("Щит");
            m_inventory.AddItem("Гримуар", 3);
            m_inventory.AddItem("Стрела", 9);
            m_inventory.RemoveItem("Меч");
            m_inventory.RemoveItem("Лук", 2);
            m_inventory.RemoveItem("Стрела", 2);
            m_inventory.RemoveItem("Полено", 2);
            m_inventory.AddItem("Ключ", 9);

            m_inventory.Print();
        }
    }
}