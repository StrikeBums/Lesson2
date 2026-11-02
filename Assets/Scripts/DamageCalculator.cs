using UnityEngine;

public class DamageCalculator : MonoBehaviour
{ 
    public struct AttackResult
    {
        public int damage;
        public string elementModifier;
        public string criticalModifier;
    }
    
    [Header("Weapon")]
    [SerializeField] private int m_baseDamage = 10;
    [SerializeField] private ElementModifier m_weaponElement = ElementModifier.None;
    
    [Range(0f, 1f)]
    [SerializeField] private float m_criticalChance = 0.1f;
    
    [Header("Target")]
    [SerializeField] private int m_armor = 3;
     [SerializeField] private ElementModifier m_targetElement = ElementModifier.Ice;
    
    [Header("Test")]
    [SerializeField] private int m_attacksCount = 3;

    public AttackResult CalculateDamage()
    {
        
        var result = new AttackResult();

        
        int damage = Mathf.Max(1, m_baseDamage - m_armor);

        result.elementModifier = "без стихии";

        
        if (m_weaponElement != ElementModifier.None &&
            m_targetElement != ElementModifier.None)
            
        {
            if (m_weaponElement == m_targetElement)
            {
                damage = 0;
                result.elementModifier = "стихии совпадают";
            }
            else if (AreOppositeElements(m_weaponElement, m_targetElement))
            {
                damage = Mathf.FloorToInt(damage * 1.5f);
                result.elementModifier = "стихия ×1,5";
            }
        }

        
        var isCritical = Random.value < m_criticalChance;

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
    private void Start()
    {
        CalculateAttacks();
    }
    [ContextMenu("Calculate Attacks")]
    private void CalculateAttacks()
    {
        for (var i = 1; i <= m_attacksCount; i++)
        {
            var result = CalculateDamage();

            Debug.Log(
                $"Удар {i}: {result.damage} урона " +
                $"({result.elementModifier}, {result.criticalModifier})");
        }
    }
    private bool AreOppositeElements(ElementModifier first, ElementModifier second)
    {
        return (first == ElementModifier.Fire && second == ElementModifier.Ice) ||
               (first == ElementModifier.Ice && second == ElementModifier.Fire);
    }
}

