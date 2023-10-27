using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SawAnimation : MonoBehaviour
{
    [SerializeField] float defaultRotationSpeed = 10f;

    private PlayerController player;
    private bool initialized;

    private void OnEnable()
    {
        GameEvents.onGameStart.AddListener(Initialise);
    }
    
    private void OnDisable()
    {
        GameEvents.onGameStart.RemoveListener(Initialise);
    }

    void Update()
    {
        if (!initialized) return;
        ChangeSawRotation();
    }

    void Initialise()
    {
        player = FindObjectOfType<PlayerController>();
        initialized = true;
    }

    void ChangeSawRotation()
    {
        //TODO: Ask designer for reason of speed based on bullets count and Random sead
        var playerPosition = player.transform.position;
        Random.InitState((int)(playerPosition.x + playerPosition.y + playerPosition.z));
        var angleDiff = Random.value * Time.deltaTime + defaultRotationSpeed;

        gameObject.transform.Rotate(angleDiff, 0, 0);
    }
}