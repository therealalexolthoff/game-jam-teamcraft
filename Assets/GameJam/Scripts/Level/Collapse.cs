using UnityEngine;

public class Collapse : MonoBehaviour
{
    //Inspector
    [Header("Mechanics")]
    [Tooltip("The speed in meters per second that the collapse moves up the level.")]
    [SerializeField] private float speed = 1;

    [SerializeField] private float distance = 30f;

    //Private
    private Vector3 initialPosition;

    private void Awake()
    {
        //Initialize collapse in the game manager
        GameManager.Instance.Collapse = this;
        initialPosition = transform.position;
    }

    private void Update()
    {
        //Move the collapse up the screen very frame
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        if (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) > distance)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }

        //TODO: visualize collapse with bubbles
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //End the level in a loss if the player touches the collapse
            other.GetComponent<DamageController>().TriggerDamage(10);
            GameManager.Instance.EndLevel(false);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<DamageController>().TriggerDamage(10);
        }
    }

    /// <summary>
    /// Resets the position of the collapse on level restart.
    /// </summary>
    public void ResetCollapse()
    {
        transform.position = initialPosition;
    }
}
