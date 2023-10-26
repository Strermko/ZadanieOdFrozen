using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] Weapon weapon;

    void Update() {
        if (Input.GetMouseButton(0))
            weapon.Shoot();
    }

    public void Died()
    {
        GameEvents.onPlayerDeath.Invoke();
    }
}