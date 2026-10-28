using UnityEngine;

public class DamageCalculator : MonoBehaviour
{ 
    public struct AttackResult
    {
        public int Damage;
        public string ElementModifier;
        public string CriticalModifier;
    }
    public enum Element
    {
        None,
        Fire,
        Ice
        
    }
    [Header("Weapon")]
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private Element weaponElement = Element.None;

    [Range(0f, 1f)]
    [SerializeField] private float criticalChance = 0.1f;

    [Header("Target")]
    [SerializeField] private int armor = 3;
    [SerializeField] private Element targetElement = Element.Ice;

    [Header("Test")]
    [SerializeField] private int attacksCount = 3;

    private void Start()
    {
        CalculateAttacks();
    }
    [ContextMenu("Calculate Attacks")]
    private void CalculateAttacks()
    {
        for (int i = 1; i <= attacksCount; i++)
        {
            AttackResult result = CalculateDamage();

            Debug.Log(
                $"Удар {i}: {result.Damage} урона " +
                $"({result.ElementModifier}, {result.CriticalModifier})");
        }
    }

    public AttackResult CalculateDamage()
    {
        var result = new AttackResult();

        
        int damage = Mathf.Max(1, baseDamage - armor);

        result.ElementModifier = "без стихии";

        
        if (weaponElement != Element.None &&
            targetElement != Element.None)
        {
            if (weaponElement == targetElement)
            {
                damage = 0;
                result.ElementModifier = "стихии совпадают";
            }
            else if (AreOppositeElements(weaponElement, targetElement))
            {
                damage = Mathf.FloorToInt(damage * 1.5f);
                result.ElementModifier = "стихия ×1,5";
            }
        }

        
        bool isCritical = Random.value < criticalChance;

        if (damage > 0 && isCritical)
        {
            damage *= 2;
            result.CriticalModifier = "крит ×2";
        }
        else
        {
            result.CriticalModifier = "без крита";
        }

        result.Damage = damage;

        return result;
    }

    private bool AreOppositeElements(Element first, Element second)
    {
        return (first == Element.Fire && second == Element.Ice) ||
               (first == Element.Ice && second == Element.Fire);
    }

   
}

