using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon weapon;

    enum PlayerStatus
    {
        Alive,
        Dead
    }

    private PlayerStatus playerStatus = PlayerStatus.Alive;

    void Update()
    {
        if (Input.GetMouseButton(0))
            weapon.Shoot();
    }

    public void Died()
    {
        if (!playerStatus.Equals(PlayerStatus.Dead))
        {
            playerStatus = PlayerStatus.Dead;
            GameEvents.onPlayerDeath.Invoke();
        }
    }
}