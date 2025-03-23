using System;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Controls how fast the player moves")]
    [SerializeField] private float playerSpeed = 10.0f;
    [Tooltip("Acceleration in meters per second squared.")]
    [SerializeField] private float acceleration = 3.33f;

    [Tooltip("The position to spawn the Bullet Prefab")]
    [SerializeField] private Transform spawnBulletPosition;

    [Tooltip("The force to apply on the y-axis to the Bullet Prefab")]
    [SerializeField] private float bulletVerticalForce = 25.0f;

    [Tooltip("Maximum health of GameObject")]
    [SerializeField] private int maxHealth = 50;

    [Tooltip("The number of shots the player starts with.")]
    [SerializeField] private int startingAmmunition = 30;

    [Tooltip("The transform for rotating the turret.")]
    [SerializeField] private Transform turret;

    [Tooltip("Farthest left rotation of the turret.")]
    [SerializeField][Range(-180, 0)] private int minRotation = -120;

    [Tooltip("Farthest right rotation of the turret.")]
    [SerializeField][Range(0, 180)] private int maxRotation = 120;

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

        shootComponent.bulletPrefab.GetComponent<BulletController>().objectToIgnore = this.gameObject;
        //damageController.maxHealth = maxHealth;
        damageController.SetMaxHealth(maxHealth);
        Ammunition = startingAmmunition;
    }

    // Update is called once per frame
    private void Update()
    {
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

        //Rotate the turret
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (Vector2)(cursorPos - transform.position).normalized;

        //Set rotation and apply offsets to make it face the cursor correctly
        turret.up = shootDirection;
        Vector3 rotation = turret.localRotation.eulerAngles;
        rotation.z *= -1;
        rotation.z += 180;
        rotation.y = 180;

        //Clamp the rotation, and reapply the values to the transform
        rotation.z = rotation.z < minRotation ? minRotation : rotation.z > maxRotation ? maxRotation : rotation.z;
        turret.localRotation = Quaternion.Euler(rotation);

        //Firing controls
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Ammunition > 0)
        {
            shootComponent.SpawnBulletPrefab(spawnBulletPosition.position, shootDirection);
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
