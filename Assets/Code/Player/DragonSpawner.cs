using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    //TODO: Replace DragonAI with Entity/Enemy interface
    [SerializeField]
    private DragonAI dragonPrefab;

    [SerializeField]
    private int maxDragons = 50;

    private List<Transform> spawnPoints;
    private List<DragonAI> dragonsPool;

    private void OnEnable()
    {
        GameEvents.onGameLoading.AddListener(Initialise);
        GameEvents.onGameStart.AddListener(StartSpawningCourutine);
    }
    
    private void OnDisable()
    {
        GameEvents.onGameLoading.RemoveListener(Initialise);
        GameEvents.onGameStart.RemoveListener(StartSpawningCourutine);
    }
    
    void Initialise()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Dragon Spawn Point")
            .Select(x => x.transform)
            .ToList();
        
        dragonsPool = new List<DragonAI>();
        for (int i = 0; i < maxDragons; i++)
        {
            var dragon = Instantiate(dragonPrefab, transform);
            dragonsPool.Add(dragon);
            dragon.gameObject.SetActive(false);
        }
        
    }

    void StartSpawningCourutine()
    {
        StartCoroutine(SpawningCourutine());
    }

    IEnumerator SpawningCourutine()
    {
        //TODO: Check if this is the way how designer wants to spawn dragons
        
        yield return new WaitForSeconds(10f);
        while (true)
        {
            int liveDragons = GameStats.Instance.EnemyCount;
            int deadDragons = GameStats.Instance.Kills;
            if (!(liveDragons >= maxDragons)) SpawnDragon();

            var t = Mathf.Max(3 + liveDragons / 10 - deadDragons / 10, 1);
            yield return new WaitForSeconds(t);
        }
    }

    void SpawnDragon()
    {
        var spawnPoint = spawnPoints[0];
        var dragon = dragonsPool[0];
        dragon.Spawn(spawnPoint.position);

        spawnPoints.RemoveAt(0);
        spawnPoints.Add(spawnPoint);
        
        dragonsPool.RemoveAt(0);
        dragonsPool.Add(dragon);
        
        GameEvents.onEnemySpawn.Invoke();
    }
}