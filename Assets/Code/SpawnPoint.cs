using System;
using System.ComponentModel;
using ARFC;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Description("The player prefab to spawn, FP Controller")]
    [SerializeField]
    FPController playerPrefab;
    
    private GameObject player;
    
    private static SpawnPoint _instance;

    private SpawnPoint() { }

    private void OnEnable()
    {
        _instance = this;

        GameEvents.onGameLoading.AddListener(SpawnPlayer);
    }

    private void OnDisable()
    {
        GameEvents.onGameLoading.RemoveListener(SpawnPlayer);
    }

    public void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, _instance.transform.position, Quaternion.identity).gameObject;
        player.name = "Player";
        ResetPlayerPosition();
        gameObject.SetActive(false);
    }

    public static void ResetPlayerPosition()
    {
        if (_instance == null)
        {
            Debug.LogError("No spawn point");
            return;
        }

        _instance.player.transform.position = _instance.transform.position;
        _instance.player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}