using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [Tooltip("Holds a reference to the Bullet Prefab to spawn in the world")]
    [SerializeField] public Rigidbody bulletPrefab;

    [Tooltip("The distance in front of player to spawn the Bullet Prefab at")]
    /*[SerializeField]*/
    public float spawnBulletDistance;

    [Tooltip("The speed of the bullet.")]
    [SerializeField] private float speed = 5;

    public void SpawnBulletPrefab(Vector3 _spawnBulletPosition, Vector3 direction)
    {
        Rigidbody rbBulletPrefab = Instantiate(bulletPrefab, _spawnBulletPosition, Quaternion.identity);
        rbBulletPrefab.transform.up = direction;
        rbBulletPrefab.linearVelocity = direction * speed;
    }
}
