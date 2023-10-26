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
        Random.InitState((int)(player.transform.position.x + player.transform.position.y +
                               player.transform.position.z));
        var angleDiff = Random.value * GameStats.Instance.BulletsCount * Time.deltaTime + defaultRotationSpeed;

        gameObject.transform.Rotate(angleDiff, 0, 0);
    }
}