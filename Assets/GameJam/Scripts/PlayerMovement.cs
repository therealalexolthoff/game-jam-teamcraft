using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Controls how fast the player moves")]
    [SerializeField] private float playerSpeed = 10.0f;
    [Tooltip("Acceleration in meters per second squared.")]
    [SerializeField] private float acceleration = 3.33f;

    [Tooltip("The distance in front of self to spawn the Bullet Prefab")]
    [SerializeField] private float spawnBulletDistance = 1.75f;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    [SerializeField] private float bulletVerticalForce = 25.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 2;

    [Tooltip("The number of shots the player starts with.")]
    [SerializeField] private int startingAmmunition = 30;

    //Component Cache
    private ShootComponent shootComponent;
    private DamageController damageController;
    private Rigidbody rb;
    private PlayerHUD HUD;

    //Private
    private int _ammunition;

    //Public
    public int Ammunition
    {
        get => _ammunition;
        set
        {
            _ammunition = value;
            HUD.UpdateAmmoText(_ammunition);
        }
    }

    private void Start()
    {
        // Get references to script components ShootComponent and DamageController
        shootComponent = GetComponent<ShootComponent>();
        damageController = GetComponent<DamageController>();
        rb = GetComponent<Rigidbody>();
        HUD = FindFirstObjectByType<PlayerHUD>();

        // set values in components to values specified in self/this
        shootComponent.spawnBulletDistance = spawnBulletDistance;
        shootComponent.bulletVerticalForce = bulletVerticalForce;
        damageController.maxHealth = maxHealth;
        Ammunition = startingAmmunition;
    }

    // Update is called once per frame
    private void Update()
    {
        // Horizontal/x-axis 
        /*
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
        */

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 direction = new(x, y);

        if (direction != Vector2.zero)
        {
            //Accelerate the player each frame
            rb.AddForce(acceleration * direction.normalized);

            //Clamp maximum speed if player is moving too fast
            if (rb.linearVelocity.magnitude > playerSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * playerSpeed;
            }
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime);
        }
        
        //Firing controls
        if (Input.GetKeyDown(KeyCode.Space) && Ammunition > 0)
        {
            Vector3 spawnBulletPosition = new(transform.position.x, transform.position.y + shootComponent.spawnBulletDistance, transform.position.z);
            shootComponent.SpawnBulletPrefab(spawnBulletPosition);
            Ammunition--;
        }
    }

    public void PickupAmmunition(int amt)
    {
        Ammunition += amt;
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
    }
}
