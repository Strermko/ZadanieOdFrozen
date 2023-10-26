using UnityEngine.Events;

public static class GameEvents
{
    //TODO: Separate events to different classes if project will grow
    
    //Events for game state
    public static UnityEvent onGameStart = new UnityEvent();
    public static UnityEvent onGameEnd = new UnityEvent();
    
    //Stats events
    public static UnityEvent onGameStatsChange = new UnityEvent();
    
    // Events for Dragon Enemy
    public static UnityEvent onDragonDeath = new UnityEvent();
    public static UnityEvent onDragonSpawn = new UnityEvent();
    
    //Events for Player
    public static UnityEvent onPlayerDeath = new UnityEvent();
}
