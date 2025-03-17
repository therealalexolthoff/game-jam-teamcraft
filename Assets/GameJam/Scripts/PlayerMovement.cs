using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Controls how fast the player moves")]
    [SerializeField] private float playerSpeed = 5.0f;

    // Update is called once per frame
    private void Update()
    {
        // Horizontal/x-axis 
        float horizontalMoveTest = Input.GetAxis("Horizontal");
        Vector3 currentPlayerPosition = transform.position;
        Vector3 newPlayerPosition = currentPlayerPosition;

        // Vertical/y-axis 
        float verticalMoveTest = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Debug.Log(horizontalMoveTest);

            newPlayerPosition += new Vector3(horizontalMoveTest, 0f, 0f) * playerSpeed * Time.deltaTime;

            transform.position = newPlayerPosition;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            Debug.Log(verticalMoveTest);

            newPlayerPosition += new Vector3(0f, verticalMoveTest, 0f) * playerSpeed * Time.deltaTime;

            transform.position = newPlayerPosition;
        }
    }
}
