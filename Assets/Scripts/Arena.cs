using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private HeroClass heroClass = HeroClass.Warrior;
    [SerializeField, Min(1)] private int roundLimit = 10;
    public enum HeroClass
    {
        Warrior,
        Mage
    }

    private enum EnemyType
    {
        Goblin,
        Troll,
        Orc
    }

    private Unit hero;
    private Unit enemy;

    private void Awake()
    {
        hero = CreateHero(heroClass);
        enemy = CreateRandomEnemy();

        Debug.Log($"{hero.UnitName} ({hero.Health} HP) против " +
                  $"{enemy.UnitName} ({enemy.Health} HP)");
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
        EnemyType type = (EnemyType)Random.Range(
            0, System.Enum.GetValues(typeof(EnemyType)).Length);

        switch (type)
        {
            case EnemyType.Goblin:
                return new Unit("Гоблин", 50, 4, 8);

            case EnemyType.Orc:
                return new Unit("Орк", 80, 5, 10);

            default:
                return new Unit("Тролль", 140, 8, 12);
        }
    }

    private void StartBattle()
    {
        for (int round = 1; round <= roundLimit; round++)
        {
            if (!hero.IsAlive || !enemy.IsAlive)
                break;

            var heroAttack = heroDamageCalculator.CalculateDamage();
            enemy.TakeDamage(heroAttack.Damage);

            string roundLog =
                $"Раунд {round}: {hero.UnitName} наносит {heroDamage} Урона, " +
                $"у противника {enemy.Health} HP";

            
            if (enemy.IsAlive)
            {
                var enemyAttack = enemyDamageCalculator.CalculateDamage();
                hero.TakeDamage(enemyAttack.Damage);

                roundLog +=
                    $"; {enemy.UnitName} наносит {enemyAttack.Damage} урона " +
                    $"({enemyAttack.ElementModifier}, {enemyAttack.CriticalModifier}), " +
                    $"у героя {hero.Health} HP";
            }

            Debug.Log(roundLog);
        }

        LogResult();
    }

    private void LogResult()
    {
        if (hero.IsAlive && !enemy.IsAlive)
        {
            Debug.Log($"Победа! У героя осталось {hero.Health} HP");
        }
        else if (!hero.IsAlive && enemy.IsAlive)
        {
            Debug.Log("Поражение!");
        }
        else
        {
            
            Debug.Log("Ничья");
        }
    }
}
