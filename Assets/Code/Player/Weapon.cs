using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField] float bulletsPerSeconds = 10;
    [SerializeField] float bulletSpeed = 100;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletSpawnTransform;

    float shotDeadline;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Shoot() {
        if (IsReadyToShoot())
            return;
        SpawnBullet();
        SetNextShootDeadline();
    }

    bool IsReadyToShoot() {
        return shotDeadline > Time.timeSinceLevelLoad;
    }

    void SpawnBullet() {
        var bulletGameObject = Instantiate(bulletPrefab, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
        bulletGameObject.Init(BulletVelocity());
        
        GameEvents.onWeaponShoot.Invoke();
    }

    Vector3 BulletVelocity() {
        var direction = mainCamera.ViewportPointToRay(new Vector3(0.49f, 0.51f, 0)).direction;
        return direction * bulletSpeed;
    }

    void SetNextShootDeadline() {
        shotDeadline = Time.timeSinceLevelLoad + 1 / bulletsPerSeconds;
    }
}