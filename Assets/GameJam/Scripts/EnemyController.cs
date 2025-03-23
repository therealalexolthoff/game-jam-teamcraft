using System;
using Unity.Mathematics;
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
    [SerializeField] private Vector3 bulletVerticalForce; // = -25.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 2;

    [Tooltip("Reference to target to face")]
    [SerializeField] private Transform targetToFace;

    [Tooltip("Rotation offset to fix Enemy's rotation when facing Player")]
    [SerializeField] private Vector3 rotationOffset;

    [Tooltip("Position/Location to spawn Bullet Prefab")]
    [SerializeField] private Transform spawnBulletPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootComponent = GetComponent<ShootComponent>();
        damageController = GetComponent<DamageController>();

        shootComponent.spawnBulletDistance = spawnBulletDistance;
        //shootComponent.bulletVerticalForce = spawnBulletPosition.forward * 25f;
        damageController.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        shootComponent.bulletVerticalForce = spawnBulletPosition.forward * 25f;
        //shootComponent.quaternion = Quaternion.LookRotation(transform.eulerAngles);
        Debug.Log(shootComponent.bulletVerticalForce);
        //float spawnBulletDistance = shootComponent.spawnBulletDistance;
        Vector3 enemyPosition = new Vector3(transform.position.x, transform.position.y + spawnBulletDistance, transform.position.z);
        if (Time.time > internalFireBulletRate)
        {
            internalFireBulletRate = Time.time + fireBulletRate;
            FireBullet(spawnBulletPosition.position);
        }

        if (targetToFace == null)
        {
            Debug.LogWarning("Not looking at " + targetToFace.name);
            return;
        }

        // Face the player 
        Vector3 direction = targetToFace.position - this.transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Fix rotation so that Enemy only rotates on z-axis
        Quaternion correctedRotation = lookRotation * Quaternion.Euler(rotationOffset);

        this.transform.rotation = correctedRotation;
    }

    private void FireBullet(Vector3 _positionToSpawnBullet)
    {
        shootComponent.SpawnBulletPrefab(_positionToSpawnBullet);
    }
}
