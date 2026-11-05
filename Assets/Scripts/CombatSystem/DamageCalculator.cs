using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
    public static class DamageCalculator
    {
        public struct AttackResult
        {
            public int damage;
            public string elementModifier;
            public string criticalModifier;
        }

        public static AttackResult CalculateDamage(
            int baseDamage,
            int armor,
            ElementModifier weaponElement,
            ElementModifier targetElement,
            float criticalChance)
        {
            var result = new AttackResult();

            int damage = Mathf.Max(1, baseDamage - armor);

            result.elementModifier = "без стихии";

            if (weaponElement != ElementModifier.None &&
                targetElement != ElementModifier.None)
            {
                if (weaponElement == targetElement)
                {
                    damage = 0;
                    result.elementModifier = "стихии совпадают";
                }
                else if (AreOppositeElements(weaponElement, targetElement))
                {
                    damage = Mathf.FloorToInt(damage * 1.5f);
                    result.elementModifier = "стихия ×1,5";
                }
            }

            bool isCritical = Random.value < criticalChance;

            if (damage > 0 && isCritical)
            {
                damage *= 2;
                result.criticalModifier = "крит ×2";
            }
            else
            {
                result.criticalModifier = "без крита";
            }

            result.damage = damage;

            return result;
        }

        private static bool AreOppositeElements(ElementModifier first, ElementModifier second)
        {
            return (first == ElementModifier.Fire && second == ElementModifier.Ice)
                   || (first == ElementModifier.Ice && second == ElementModifier.Fire);
        }
    }
}