using UnityEngine;

public class Unit
{
    public string UnitName { get; }
    public int Health { get; private set; }

    public int MinDamage { get; }
    public int MaxDamage { get; }

    public bool IsAlive => Health > 0;

    public Unit(string unitName, int health, int minDamage, int maxDamage)
    {
        UnitName = unitName;
        Health = health;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
    }

    public int Attack()
    {
        return Random.Range(MinDamage, MaxDamage + 1);
    }

    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(0, Health - damage);
    }
}