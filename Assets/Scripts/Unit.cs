using UnityEngine;

public class Unit
{
    public string unitName { get; }
    public int health { get; private set; }

    private int minDamage { get; }
    private int maxDamage { get; }

    public bool isAlive => health > 0;

    public Unit(string unitName, int health, int minDamage, int maxDamage)
    {
        this.unitName = unitName;
        this.health = health;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }

    public int Attack()
    {
        return Random.Range(minDamage, maxDamage + 1);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }
}