using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    [Tooltip("Holds a reference to the Bullet Prefab to spawn in the world")]
    [SerializeField] public GameObject bulletPrefab;

    [Tooltip("The distance in front of player to spawn the Bullet Prefab at")]
    /*[SerializeField]*/
    public float spawnBulletDistance;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    /*[SerializeField]*/
    public Vector3 bulletVerticalForce;

    public Quaternion quaternion;

    /// <summary>
    /// DestroyBullet
    /// <para> brief: This function spawns a Bullet Prefab and adds a forward force to it. </para>
    /// <para> params: _spawnBulletPosition The position to spawn the Bullet Prefab in the world. </para>
    /// <para> return: NONE </para>
    /// </summary>

    public void SpawnBulletPrefab(Vector3 _spawnBulletPosition)
    {
        GameObject instantiatedBulletPrefab = Instantiate(bulletPrefab, _spawnBulletPosition, Quaternion.identity);
        if (instantiatedBulletPrefab.TryGetComponent<Rigidbody>(out Rigidbody rbBulletPrefab))
        {
            //bulletVerticalForce.z = 0f;
            Debug.LogWarning(bulletVerticalForce);
            rbBulletPrefab.AddForce(bulletVerticalForce, ForceMode.VelocityChange);
        }
    }
}
