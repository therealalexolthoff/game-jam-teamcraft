using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Tooltip("Time, in seconds, to destroy Bullet Prefab after spawn")]
    [SerializeField] private float timeToDestroyBullet = 2.0f;

    private bool isBulletVisible = false;

    private void OnBecameVisible()
    {
        isBulletVisible = true;
    }

    private void Update()
    {
        if (!isBulletVisible)
        {
            return;
        }
        else
        {
            Invoke(nameof(DestroyBullet), timeToDestroyBullet);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        Destroy(this.gameObject);
    }

    /// <summary>
    /// DestroyBullet
    /// <para> brief: This functions destroys the owner GameObject when called. </para>
    /// <para> params: NONE </para>
    /// <para> return: NONE </para>
    /// </summary>
    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
