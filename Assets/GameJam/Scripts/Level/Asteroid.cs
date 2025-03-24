using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    //Inspector
    [Tooltip("Minimum speed in meters per second.")]
    [SerializeField] private float minSpeed = 0.1f;
    [Tooltip("Maximum speed in meters per second.")]
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private Rigidbody rb;

    //Private
    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
        GameManager.Instance.asteroids[GetInstanceID()] = this;
        Init();
    }

    /// <summary>
    /// Called upon asteroid creation. Gives the asteroid initial movement and rotation.
    /// </summary>
    public void Init()
    {
        //Add rotational force
        transform.rotation = Random.rotation;
        rb.AddTorque(Random.rotation.eulerAngles * Random.Range(minSpeed / 2, maxSpeed / 2));
        rb.AddForce((Vector2)Random.rotation.eulerAngles.normalized * Random.Range(minSpeed, maxSpeed));
    }

    public void ResetAsteroid()
    {
        transform.position = initialPos;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Init();
    }
}
