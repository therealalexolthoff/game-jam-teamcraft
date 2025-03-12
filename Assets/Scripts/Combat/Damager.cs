using UnityEngine;

public class Damager : MonoBehaviour
{
    [Tooltip("How much damage we will do every time we touch a HealthPool")]
    public float damage = 1;

    [Tooltip("Damagers don't hurt HealthPools of the same faction")]
    public bool isPlayerFaction;

    [Tooltip("Whether or not this object should delete itself after it deals damage")]
    public bool deleteAfterCollision = false;

    public void Start()
    {
        if( GetComponent<Collider2D>() == null )
        {
            Debug.LogWarning("Something you attached a Damager to is missing a collider!");
        }
    }

    //If we touch something...
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the thing that touched us has a HealthPool and if it is a different faction
        if (other.TryGetComponent(out HealthPool healthPool) && healthPool.isPlayerFaction != isPlayerFaction)
        {
            //Deal damage to the HealthPool we touched!
            healthPool.TakeDamage(damage);

            // If this is a bullet, destroy ourselves after touching that thingy
            if(deleteAfterCollision)
            {
                Destroy(gameObject);
            }

            // After that, check if the thing we damaged should be invincible for a little while
            if (other.TryGetComponent(out InvincibleOnHit invincibleOnHit))
            {
                invincibleOnHit.InvincibleStart();
            }
        }
    }
}