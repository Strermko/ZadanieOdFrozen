using UnityEngine;

public class RespawnOnCollision : MonoBehaviour {
    
    //TODO: Ask designer for purpose of this script
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            SpawnPoint.ResetPlayerPosition();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
            SpawnPoint.ResetPlayerPosition();
    }
}
