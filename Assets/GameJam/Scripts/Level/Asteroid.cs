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

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Called upon asteroid creation. Gives the asteroid initial movement and rotation.
    /// </summary>
    public void Init()
    {
        //Create 3 directions of random rotation with positive or negative direction
        float xRot = Random.Range(minSpeed, maxSpeed) * Random.Range(0, 2) * 2 - 1;
        float yRot = Random.Range(minSpeed, maxSpeed) * Random.Range(0, 2) * 2 - 1;
        float zRot = Random.Range(minSpeed, maxSpeed) * Random.Range(0, 2) * 2 - 1;

        //Add rotational force
        transform.rotation = Random.rotation;
        rb.AddTorque(Random.rotation.eulerAngles * Random.Range(minSpeed, maxSpeed));
    }
}
