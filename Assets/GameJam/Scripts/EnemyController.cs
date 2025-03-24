using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Inspector
    [Tooltip("The rate at which Enemy shoots a bullet in seconds")]
    [SerializeField] private float fireBulletRate = 0.5f;
    private float internalFireBulletRate = 0.0f;

    [Tooltip("The distance in front of self to spawn the Bullet Prefab")]
    [SerializeField] private float spawnBulletDistance = -3.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 5;

    [Tooltip("Reference to target to face")]
    [SerializeField] private Transform targetToFace;

    [Tooltip("The maximum distance at which an enemy will shoot.")]
    [SerializeField] private float fireRange = 25;

    [Tooltip("Rotation offset to fix Enemy's rotation when facing Player")]
    [SerializeField] private Vector3 rotationOffset;

    [Tooltip("Position/Location to spawn Bullet Prefab")]
    [SerializeField] private Transform spawnBulletPosition;

    [Tooltip("Distance from the player from when to start backing up.")]
    [SerializeField] private float minPlayerDistance = 15;

    [Tooltip("Distance at which the enemy will move towards the player.")]
    [SerializeField] private float maxPlayerdistance = 20;

    [Tooltip("Distance at which the enemy will notice the player.")]
    [SerializeField] private float followRange = 30;

    [Tooltip("Speed at which the enemy backs up from the player.")]
    [SerializeField] private float speed = 7.5f;

    [SerializeField] private List<AudioClip> fireSFX;

    //Private
    private Vector3 initialPos;

    //Component cache
    private Rigidbody rb;
    private ShootComponent shootComponent;
    private DamageController damageController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        shootComponent = GetComponent<ShootComponent>();
        damageController = GetComponent<DamageController>();
        rb = GetComponent<Rigidbody>();

        shootComponent.spawnBulletDistance = spawnBulletDistance;
        damageController.SetMaxHealth(maxHealth);

        initialPos = transform.position;
        GameManager.Instance.enemies[GetInstanceID()] = this;
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

        //Fire if the player is in range and the time is okay
        if (Time.time > internalFireBulletRate && Vector2.Distance(transform.position, targetToFace.position) <= fireRange)
        {
            AudioManager.Instance.PlaySound(fireSFX[Random.Range(0, fireSFX.Count)]);
            internalFireBulletRate = Time.time + fireBulletRate;
            shootComponent.SpawnBulletPrefab(spawnBulletPosition.position, direction);
        }

        //Have the enemy ship back away from the player
        float distance = Vector3.Distance(targetToFace.position, transform.position);

        if (distance < minPlayerDistance)
            rb.AddForce(-1 * speed * direction);
        else if (distance > maxPlayerdistance && distance < followRange)
        {
            rb.AddForce(speed * direction);
        }
        else
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, Time.deltaTime);
    }

    public void ResetEnemy()
    {
        transform.position = initialPos;
        damageController.SetMaxHealth(maxHealth);
        gameObject.SetActive(true);
        rb.linearVelocity = Vector3.zero;
    }
}
