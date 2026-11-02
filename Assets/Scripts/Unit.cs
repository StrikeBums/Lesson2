using UnityEngine;

public class Unit
{
    public string unitName { get; }
    public int health { get; private set; }

    private int m_minDamage { get; }
    private int m_maxDamage { get; }

    public bool IsAlive => health > 0;

    public Unit(string unitName, int health, int minDamage, int maxDamage)
    {
        this.unitName = unitName;
        this.health = health;
        m_minDamage = minDamage;
        m_maxDamage = maxDamage;
    }

    public int Attack()
    {
        return Random.Range(m_minDamage, m_maxDamage + 1);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }
}