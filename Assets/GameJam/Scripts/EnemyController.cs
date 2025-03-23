using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Reference to ShootComponent")]
    /*[SerializeField]*/
    private ShootComponent shootComponent;

    [Tooltip("Reference to DamageController script")]
    /*[SerializeField]*/
    private DamageController damageController;

    [Tooltip("The rate at which Enemy shoots a bullet in seconds")]
    [SerializeField] private float fireBulletRate = 0.5f;
    private float internalFireBulletRate = 0.0f;

    [Tooltip("The distance in front of self to spawn the Bullet Prefab")]
    [SerializeField] private float spawnBulletDistance = -3.0f;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    [SerializeField] private float bulletVerticalForce = -25.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 2;

    [Tooltip("Reference to target to face")]
    [SerializeField] private Transform targetToFace;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootComponent = GetComponent<ShootComponent>();
        damageController = GetComponent<DamageController>();

        shootComponent.spawnBulletDistance = spawnBulletDistance;
        shootComponent.bulletVerticalForce = bulletVerticalForce;
        damageController.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float spawnBulletDistance = shootComponent.spawnBulletDistance;
        Vector3 enemyPosition = new Vector3(transform.position.x, transform.position.y + spawnBulletDistance, transform.position.z);
        if (Time.time > internalFireBulletRate)
        {
            internalFireBulletRate = Time.time + fireBulletRate;
            FireBullet(enemyPosition);
        }

        // rotate to face player
    }

    private void FireBullet(Vector3 _positionToSpawnBullet)
    {
        shootComponent.SpawnBulletPrefab(_positionToSpawnBullet);
    }
}
