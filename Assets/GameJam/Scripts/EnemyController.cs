using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Reference to ShootComponent")]
    [SerializeField] private ShootComponent shootComponent;

    [Tooltip("The rate at which Enemy shoots a bullet in seconds")]
    [SerializeField] private float fireBulletRate = 0.5f;
    private float internalFireBulletRate = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootComponent = GetComponent<ShootComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > internalFireBulletRate)
        {
            internalFireBulletRate = Time.time + fireBulletRate;
            //Invoke(nameof(FireBullet), fireBulletRate);
            FireBullet();
        }
    }

    private void FireBullet()
    {
        float spawnBulletDistance = shootComponent.spawnBulletDistance;
        shootComponent.SpawnBulletPrefab(new Vector3(transform.position.x, transform.position.y + spawnBulletDistance, transform.position.z));
    }
}
