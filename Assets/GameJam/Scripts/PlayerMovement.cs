using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Controls how fast the player moves")]
    [SerializeField] private float playerSpeed = 5.0f;

    //private Rigidbody playerRigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMoveTest = Input.GetAxis("Horizontal");
        //Vector3 horizontalForceValue = new Vector3(horizontalMoveTest, 0f, 0f);
        Vector3 currentPlayerPosition = transform.position;
        Vector3 newPlayerPosition = currentPlayerPosition;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Debug.Log(horizontalMoveTest);

            newPlayerPosition += new Vector3(horizontalMoveTest, 0f, 0f) * playerSpeed * Time.deltaTime;

            transform.position = newPlayerPosition;
        }
    }
}
