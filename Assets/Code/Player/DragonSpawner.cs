using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    //TODO: Replace DragonAI with Entity/Enemy interface
    [SerializeField]
    private DragonAI dragonPrefab;

    private List<Transform> spawnPoints;
    IEnumerator Start()
    {
        //TODO: Check if this is the way how designer wants to spawn dragons
        spawnPoints = GameObject.FindGameObjectsWithTag("Dragon Spawn Point")
            .Select(x => x.transform)
            .ToList();

        yield return new WaitForSeconds(10f);
        while (true)
        {
            SpawnDragon();
            int liveDragons = GameStats.Instance.EnemyCount;
            int deadDragons = GameStats.Instance.Kills;

            var t = Mathf.Max(3 + liveDragons / 10 - deadDragons / 10, 1);
            yield return new WaitForSeconds(t);
        }
    }

    void SpawnDragon()
    {
        var spawnPoint = spawnPoints[0];
        spawnPoints.RemoveAt(0);
        spawnPoints.Add(spawnPoint);

        Instantiate(dragonPrefab, spawnPoint.position, Quaternion.identity);
        
        GameEvents.onDragonSpawn.Invoke();
    }
}