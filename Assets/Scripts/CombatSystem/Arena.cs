using UnityEngine;

namespace Assets.Scripts.CombatSystem
{
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
                    return new Unit("Маг",
                        100,
                        3,
                        10,
                        20,
                        0.2f,
                        ElementModifier.Fire);

                default:
                    return new Unit("Воин",
                        120,
                        6,
                        13,
                        18,
                        0.6f,
                        ElementModifier.None);
            }
        }

        private Unit CreateRandomEnemy()
        {
            EnemyClass type = (EnemyClass)Random.Range(
                0, System.Enum.GetValues(typeof(EnemyClass)).Length);

            switch (type)
            {
                case EnemyClass.Goblin:
                    return new Unit("Гоблин",
                        50,
                        2,
                        5,
                        10,
                        0.1f,
                        ElementModifier.None);

                case EnemyClass.Orc:
                    return new Unit("Орк",
                        80,
                        5,
                        8,
                        15,
                        0.3f,
                        ElementModifier.Ice);

                default:
                    return new Unit("Тролль",
                        140,
                        6,
                        12,
                        20,
                        0.4f,
                        ElementModifier.Ice);
            }
        }

        private void StartBattle()
        {
            for (int round = 1; round <= m_roundLimit; round++)
            {
                if (!m_hero.isAlive || !m_enemy.isAlive)
                {
                    break;
                }

                var heroAttackResult = DamageCalculator.CalculateDamage(
                    m_hero.Attack(),
                    m_enemy.armor,
                    m_hero.element,
                    m_enemy.element,
                    m_hero.criticalChance);

                m_enemy.TakeDamage(heroAttackResult.damage);

                string roundLog =
                    $"Раунд {round}: {m_hero.unitName} наносит {heroAttackResult.damage} урона," +
                    $"({heroAttackResult.elementModifier}, {heroAttackResult.criticalModifier}), " +
                    $"у противника {m_enemy.health} HP";

                if (m_enemy.isAlive)
                {
                    var enemyAttackResult = DamageCalculator.CalculateDamage(
                        m_enemy.Attack(),
                        m_hero.armor,
                        m_enemy.element,
                        m_hero.element,
                        m_enemy.criticalChance);

                    m_hero.TakeDamage(enemyAttackResult.damage);

                    roundLog += $"; {m_enemy.unitName} наносит {enemyAttackResult.damage} урона, " +
                                $"({enemyAttackResult.elementModifier}, {enemyAttackResult.criticalModifier}), " +
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
}