using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float bulletsPerSeconds = 5;
    [SerializeField] float bulletSpeed = 100;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletSpawnTransform;

    float shotDeadline;
    private Camera mainCamera;
    private int bulletPoolSize; // Instantiate this amount of bullets based on bulletsPerSeconds value
    private List<Bullet> bullets = new List<Bullet>();

    private void Start()
    {
        bulletPoolSize = (int)(bulletsPerSeconds * 10);
        if (bulletPoolSize > 200 || bulletPoolSize < 10)
        {
            bulletPoolSize = 200;
            Debug.LogError("Shooting speed is enormous, setting bullet pool to default value of 200");
        }

        mainCamera = Camera.main;

        for (int i = 0; i < bulletPoolSize; i++)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity, transform);
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public void Shoot()
    {
        if (IsReadyToShoot())
            return;
        SpawnBullet();
        SetNextShootDeadline();
    }

    bool IsReadyToShoot()
    {
        return shotDeadline > Time.timeSinceLevelLoad;
    }

    void SpawnBullet()
    {
        var bulletGameObject = bullets[0];

        bulletGameObject.Init(BulletVelocity(), bulletSpawnTransform.position);

        bullets.RemoveAt(0);
        bullets.Add(bulletGameObject);
    }

    Vector3 BulletVelocity()
    {
        var direction = mainCamera.ViewportPointToRay(new Vector3(0.49f, 0.51f, 0)).direction;
        return direction * bulletSpeed;
    }

    void SetNextShootDeadline()
    {
        shotDeadline = Time.timeSinceLevelLoad + 1 / bulletsPerSeconds;
    }
}