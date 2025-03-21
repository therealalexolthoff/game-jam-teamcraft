using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Controls how fast the player moves")]
    [SerializeField] private float playerSpeed = 10.0f;

    [Tooltip("Reference to ShootComponent script")]
    /*[SerializeField]*/
    private ShootComponent shootComponent;

    [Tooltip("Reference to DamageController script")]
    /*[SerializeField]*/
    private DamageController damageController;

    [Tooltip("The distance in front of self to spawn the Bullet Prefab")]
    [SerializeField] private float spawnBulletDistance = 1.75f;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    [SerializeField] private float bulletVerticalForce = 25.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 2;

    private void Start()
    {
        // Get references to script components ShootComponent and DamageController
        shootComponent = GetComponent<ShootComponent>();
        damageController = GetComponent<DamageController>();

        // set values in components to values specified in self/this
        shootComponent.spawnBulletDistance = spawnBulletDistance;
        shootComponent.bulletVerticalForce = bulletVerticalForce;
        damageController.maxHealth = maxHealth;
    }

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
            newPlayerPosition += new Vector3(horizontalMoveTest, 0f, 0f) * playerSpeed * Time.deltaTime;

            transform.position = newPlayerPosition;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
        {
            newPlayerPosition += new Vector3(0f, verticalMoveTest, 0f) * playerSpeed * Time.deltaTime;

            transform.position = newPlayerPosition;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnBulletPosition = new Vector3(transform.position.x, transform.position.y +
                shootComponent.spawnBulletDistance, transform.position.z);
            shootComponent.SpawnBulletPrefab(spawnBulletPosition);
        }
    }
}
