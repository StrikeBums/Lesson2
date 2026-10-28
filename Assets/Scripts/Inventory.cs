using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string item;
    public int count;
}
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> startItems = new();

    private InventoryEngine inventory;

    private void Start()
    {
        if (startItems.Count == 0)
        {
            startItems = new List<InventoryItem>
            {
                new InventoryItem {item = "Меч", count = 1},
            };
        }

        inventory = new InventoryEngine(startItems);
        
        
        inventory.AddItem("Зелье", 5);
        inventory.AddItem("Щит" );
        inventory.AddItem("Гримуар", 3);
        inventory.RemoveItem("Меч" );
        inventory.RemoveItem("Лук", 2);
        inventory.RemoveItem("Стрела", 2);
        inventory.RemoveItem("Полено", 2);
        
        inventory.Print();
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
            inventory.RemoveItem("Стрела", 2);
        inventory.Print();
    }
}

