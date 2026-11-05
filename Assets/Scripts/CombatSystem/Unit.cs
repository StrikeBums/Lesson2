using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
    public class Unit
    {
        public string unitName { get; }
        public int health { get; private set; }
        public ElementModifier element { get; }
        public int armor { get; }
        public float criticalChance { get; }
        public bool isAlive => health > 0;
        private int minDamage { get; }
        private int maxDamage { get; }


        public Unit(string unitName,
            int health,
            int armor,
            int minDamage,
            int maxDamage,
            float criticalChance,
            ElementModifier element)
        {
            this.unitName = unitName;
            this.health = health;
            this.armor = armor;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
            this.criticalChance = criticalChance;
            this.element = element;
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
}