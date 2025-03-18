using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //Inspector
    [Tooltip("Minimum speed in meters per second.")]
    [SerializeField] private float minSpeed = 0.1f;
    [Tooltip("Maximum speed in meters per second.")]
    [SerializeField] private float maxSpeed = 4f;
    [Tooltip("The rigidbody component of the asteroid.")]
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
        //Get a random direction and speed, then set the rigidbody's velocity
        Vector2 direction = Random.rotation.eulerAngles;
        float speed = Random.Range(minSpeed, maxSpeed);

        //rb.linearVelocity = direction * speed;

        //Create 3 directions of random rotation with positive or negative direction
        float xRot = Random.Range(minSpeed, maxSpeed) * Random.Range(0, 2) * 2 - 1;
        float yRot = Random.Range(minSpeed, maxSpeed) * Random.Range(0, 2) * 2 - 1;
        float zRot = Random.Range(minSpeed, maxSpeed) * Random.Range(0, 2) * 2 - 1;

        //Set rotation to random values
        rb.angularVelocity = new Vector3(xRot, yRot, zRot);
    }
}
