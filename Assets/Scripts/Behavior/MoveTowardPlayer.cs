using UnityEngine;

namespace Behavior
{
    public class MoveTowardPlayer : MonoBehaviour
    {
        [Tooltip("If false, the direction will only be set on start. If true, this will constantly move towards the player.")]
        [SerializeField] private bool updateDirectionEveryFrame = false;
        [SerializeField] private float moveSpeed = 1f;
        [Tooltip("If true, this will be destroyed immediately if it is below the player.")]
        [SerializeField] private bool destroyIfSpawnedBelowPlayer = true;

        private Vector3 direction;

        public void Start()
        {
            if (destroyIfSpawnedBelowPlayer)
            {
                PlayerController player = FindFirstObjectByType<PlayerController>();
                if (player == null || transform.position.y - player.transform.position.y < 1)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            
            direction = GetDirectionToPlayer();
        }

        public void Update()
        {
            if (updateDirectionEveryFrame) direction = GetDirectionToPlayer();
            
            transform.position += direction * (moveSpeed * Time.deltaTime);
        }

        private Vector3 GetDirectionToPlayer()
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player == null) return Vector3.down;
            return (player.transform.position - transform.position).normalized;
        }
    }
}