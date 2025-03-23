using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("The rate at which Enemy shoots a bullet in seconds")]
    [SerializeField] private float fireBulletRate = 0.5f;
    private float internalFireBulletRate = 0.0f;

    [Tooltip("The distance in front of self to spawn the Bullet Prefab")]
    [SerializeField] private float spawnBulletDistance = -3.0f;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    [SerializeField] private Vector3 bulletVerticalForce; // = -25.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 5;

    [Tooltip("Reference to target to face")]
    [SerializeField] private Transform targetToFace;

    [Tooltip("Rotation offset to fix Enemy's rotation when facing Player")]
    [SerializeField] private Vector3 rotationOffset;

    [Tooltip("Position/Location to spawn Bullet Prefab")]
    [SerializeField] private Transform spawnBulletPosition;

    [Tooltip("Distance from the player from when to start backing up.")]
    [SerializeField] private float minPlayerDistance = 15;

    [Tooltip("Speed at which the enemy backs up from the player.")]
    [SerializeField] private float speed = 7.5f;

    //Component cache
    private Rigidbody rb;
    private ShootComponent shootComponent;
    private DamageController damageController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootComponent = GetComponent<ShootComponent>();
        damageController = GetComponent<DamageController>();
        rb = GetComponent<Rigidbody>();

        shootComponent.spawnBulletDistance = spawnBulletDistance;
        //damageController.maxHealth = maxHealth;
        damageController.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Face the player 
        Vector3 direction = (targetToFace.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Fix rotation so that Enemy only rotates on z-axis
        Quaternion correctedRotation = lookRotation * Quaternion.Euler(rotationOffset);

        transform.rotation = correctedRotation;

        if (Time.time > internalFireBulletRate)
        {
            internalFireBulletRate = Time.time + fireBulletRate;
            shootComponent.SpawnBulletPrefab(spawnBulletPosition.position, direction);
        }

        //Have the enemy ship back away from the player
        if (Vector3.Distance(targetToFace.position, transform.position) < minPlayerDistance)
            rb.AddForce(-1 * speed * direction);
        else
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime);
    }
}
