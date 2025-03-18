using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        Destroy(this.gameObject);
    }
}
