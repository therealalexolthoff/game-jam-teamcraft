using UnityEngine;

public class Debris : MonoBehaviour
{
    //Inspector
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool sceneObject = false;

    //Private
    private Vector3 initialPos;
    private Quaternion initialRot;

    private void Awake()
    {
        GameManager.Instance.debris[GetInstanceID()] = this;
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    public void Init()
    {
        transform.rotation = Random.rotation;

        rb.AddForce((Vector2)Random.rotation.eulerAngles.normalized, ForceMode.Impulse);
        rb.AddTorque(Random.rotation.eulerAngles.normalized, ForceMode.Impulse);
    }

    public bool ResetDebris()
    {
        if (sceneObject)
        {
            transform.SetPositionAndRotation(initialPos, initialRot);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return false;
        }
        else
        {
            Destroy(gameObject);
            return true;
        }
    }
}
