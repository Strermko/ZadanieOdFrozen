using UnityEngine.Events;
using UnityEngine.UI;

public static class GameEvents
{
    //TODO: Separate events to different classes
    
    //Events for game state
    public static UnityEvent onGameStart = new UnityEvent();
    public static UnityEvent onGameEnd = new UnityEvent();
    public static UnityEvent onGameStatsChange = new UnityEvent();
    
    // Events for Dragon Enemy
    public static UnityEvent onDragonDeath = new UnityEvent();
    public static UnityEvent onDragonSpawn = new UnityEvent();
    
    //Events for Player
    public static UnityEvent onPlayerDeath = new UnityEvent();
    
    //Events for Weapon
    public static UnityEvent onWeaponShoot = new UnityEvent();
    public static UnityEvent onBulletDestroy = new UnityEvent();

}
