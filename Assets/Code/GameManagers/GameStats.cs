public class GameStats
{
    public int EnemyCount { get; private set; }
    public int Kills { get; private set; }
    
    public int BulletsCount { get; private set; }

    private static GameStats _instance;
    public static GameStats Instance => _instance ??= new GameStats();

    private GameStats() { }

    //TODO: Replace params after implementing save system
    public void Initialize(int enemyCount = 0, int kills = 0)
    {
        //In case we want to reset the state
        _instance ??= new GameStats();
        
        this.EnemyCount = enemyCount;
        this.Kills = kills;
        this.BulletsCount = 0;
        
        GameEvents.onDragonDeath.AddListener(EnemyWasKilled);
        GameEvents.onDragonSpawn.AddListener(EnemyWasSpawned);
        
        GameEvents.onWeaponShoot.AddListener(IncreaseBuletsCount);
        GameEvents.onBulletDestroy.AddListener(DecreaseBuletsCount);
    }

    public void Utilize()
    {
        //In case we want to reset the state
        _instance = null;

        GameEvents.onDragonDeath.RemoveListener(EnemyWasKilled);
        GameEvents.onDragonSpawn.RemoveListener(EnemyWasSpawned);
        
        GameEvents.onWeaponShoot.RemoveListener(IncreaseBuletsCount);
        GameEvents.onBulletDestroy.RemoveListener(DecreaseBuletsCount);
    }

    void EnemyWasKilled()
    {
        EnemyCount--;
        Kills++;

        GameEvents.onGameStatsChange.Invoke();
    }

    void EnemyWasSpawned()
    {
        EnemyCount++;

        GameEvents.onGameStatsChange.Invoke();
    }

    void IncreaseBuletsCount()
    {
        BulletsCount++;
    }
    
    void DecreaseBuletsCount()
    {
        BulletsCount--;
    }
}