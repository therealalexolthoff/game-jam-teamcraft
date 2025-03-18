using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [Tooltip("Holds a reference to the Bullet Prefab to spawn in the world")]
    [SerializeField] private GameObject bulletPrefab;

    [Tooltip("The distance in front of player to spawn the Bullet Prefab at")]
    [SerializeField] private float spawnBulletDistance = 1.75f;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    [SerializeField] private float bulletVerticalForce = 5.0f;

    [Tooltip("Time, in seconds, to destroy Bullet Prefab after spawn")]
    [SerializeField] private float timeToDestroyBullet = 0.5f;
    private float internalTimeToDestroyBullet = 0.0f;

    //private List<GameObject> listOfSpawnBulletPrefabs;

    private void Start()
    {
        //listOfSpawnBulletPrefabs = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If player presses SPACE
        // ... instantiate/spawn a bullet in front of player
        Vector3 spawnBulletPosition = new Vector3(transform.position.x, transform.position.y + spawnBulletDistance, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject instantiatedBulletPrefab = Instantiate(bulletPrefab, spawnBulletPosition, Quaternion.identity);
            //listOfSpawnBulletPrefabs.Add(instantiatedBulletPrefab);
            if (instantiatedBulletPrefab.TryGetComponent<Rigidbody>(out Rigidbody rbBulletPrefab))
            {
                rbBulletPrefab.AddForce(new Vector3(0f, bulletVerticalForce, 0f), ForceMode.Impulse);
            }
        }
    }
}
