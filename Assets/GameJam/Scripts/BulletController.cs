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
            Debug.Log(timeToDestroyBullet);
            Invoke(nameof(TrackVisibility), timeToDestroyBullet);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        Destroy(this.gameObject);
    }

    private void TrackVisibility()
    {
        Debug.Log($"{timeToDestroyBullet}");
        Destroy(this.gameObject);
    }
}
