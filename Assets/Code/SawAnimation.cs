using UnityEngine;

public class SawAnimation : MonoBehaviour
{
    [SerializeField] float defaultRotationSpeed = 10f;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        ChangeSawRotation();
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