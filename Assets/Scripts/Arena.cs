using UnityEngine;

public class Arena : MonoBehaviour
{
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
    [SerializeField] private HeroClass heroClass = HeroClass.Warrior;
    [SerializeField, Min(1)] private int roundLimit = 10;

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

            int heroDamage = hero.Attack();
            enemy.TakeDamage(heroDamage);

            string roundLog =
                $"Раунд {round}: {hero.UnitName} наносит {heroDamage}, " +
                $"у противника {enemy.Health} HP";

            
            if (enemy.IsAlive)
            {
                int enemyDamage = enemy.Attack();
                hero.TakeDamage(enemyDamage);

                roundLog += $"; {enemy.UnitName} наносит {enemyDamage}, " +
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
