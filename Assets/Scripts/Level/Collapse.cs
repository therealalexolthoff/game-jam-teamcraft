using System.Collections;
using UnityEngine;

public class Collapse : MonoBehaviour
{
    //Inspector
    [Header("Mechanics")]
    [Tooltip("The speed in meters per second that the collapse moves up the level.")]
    [SerializeField] private float speed = 1;

    [Header("Visuals")]
    [Tooltip("The distance the player must be for visuals to show up in the collapse.")]
    [SerializeField] private float visualDistance = 20;
    [Tooltip("Minimum size for a bubble.")]
    [SerializeField] private float minBubblesize = 1.5f;
    [Tooltip("Maximum size for a bubble.")]
    [SerializeField] private float maxBubbleSize = 5;
    [Tooltip("Prefab for bubbles.")]
    [SerializeField] private CollapseBubble bubblePrefab;

    //Private
    private IEnumerator collapseBubblingRoutine = null;

    private void Update()
    {
        //Move the collapse up the screen very frame
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        //TODO: visualize collapse with bubbles
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //End the level in a loss if the player touches the collapse
            GameManager.Instance.EndLevel(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            //Destroy enemy ship if it touches the collapse
            /* TODO:
            if (other.TryGetComponent<EnemyShip>(out var enemy))
            {
                //Destroy enemy ship without spawning loot
                enemy.DestroyShip(false);
            }
            */
            Destroy(other.gameObject);
        }
    }
}
