using UnityEngine;
using UnityEngine.Serialization;

public class Arena : MonoBehaviour
{
    [SerializeField] private HeroClass m_heroClass = HeroClass.Warrior;
    [SerializeField, Min(1)] private int m_roundLimit = 10;
    
    private Unit m_hero;
    private Unit m_enemy;

    private void Awake()
    {
        m_hero = CreateHero(m_heroClass);
        m_enemy = CreateRandomEnemy();

        Debug.Log($"{m_hero.unitName} ({m_hero.health} HP) против " +
                  $"{m_enemy.unitName} ({m_enemy.health} HP)");
    }

    private void Start()
    {
        StartBattle();
    }
    private Unit CreateHero(HeroClass selectedClass)
    {
        switch (selectedClass)
        {
            case HeroClass.Mage:
                return new Unit("Маг", 130, 6, 12);

            default:
                return new Unit("Воин", 100, 10, 16);
        }
    }

    private Unit CreateRandomEnemy()
    {
        EnemyClass type = (EnemyClass)Random.Range(
            0, System.Enum.GetValues(typeof(EnemyClass)).Length);

        switch (type)
        {
            case EnemyClass.Goblin:
                return new Unit("Гоблин", 50, 4, 8);

            case EnemyClass.Orc:
                return new Unit("Орк", 80, 5, 10);

            default:
                return new Unit("Тролль", 140, 8, 12);
        }
    }

    private void StartBattle()
    {
        for (int round = 1; round <= m_roundLimit; round++)
        {
            {
                if (!m_hero.isAlive || !m_enemy.isAlive)
                    break;
            }
            int heroDamage = m_hero.Attack();
            m_enemy.TakeDamage(heroDamage);

            string roundLog =
                $"Раунд {round}: {m_hero.unitName} наносит {heroDamage}, " +
                $"у противника {m_enemy.health} HP";
            
            if (m_enemy.isAlive)
            {
                int enemyDamage = m_enemy.Attack();
                m_hero.TakeDamage(enemyDamage);

                roundLog += $"; {m_enemy.unitName} наносит {enemyDamage}, " +
                            $"у героя {m_hero.health} HP";
            }

            Debug.Log(roundLog);
        }
        
        LogResult();
    }

    private void LogResult()
    {
        if (m_hero.isAlive && !m_enemy.isAlive)
        {
            Debug.Log($"Победа! У героя осталось {m_hero.health} HP");
        }
        else if (!m_hero.isAlive && m_enemy.isAlive)
        {
            Debug.Log("Поражение!");
        }
        else
        {
            
            Debug.Log("Ничья");
        }
    }
}
