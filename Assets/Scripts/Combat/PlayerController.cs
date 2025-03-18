using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
     *
     * This is an example PlayerController for our game. Nothing too tragically fancy here. Customize to your heart's content!
     * 
     */
    
    [Tooltip("Which prefab to spawn on fire")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float distanceToSpawnProjectile = 1f;
    private ProjectileSettings projectilePrefabSettings;
    
    [Tooltip("How fast the player moves back and forth")]
    public float moveSpeed = 3f;

    [Tooltip("How far the player can move left and right")]
    public float maximumDistance = 5f;

    // A timestamp of the moment we last fired
    private float lastTimeFired;

    public void Awake()
    {
        SetProjectile(projectilePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Vector2 newPosition = transform.position;
            newPosition.x -= moveSpeed * Time.deltaTime;
            if (newPosition.x < -maximumDistance) newPosition.x = -maximumDistance;
            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Vector2 newPosition = transform.position;
            newPosition.x += moveSpeed * Time.deltaTime;
            if (newPosition.x > maximumDistance) newPosition.x = maximumDistance;
            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Fire();
        }
    }

    public void SetProjectile(GameObject projectilePrefab)
    {
        ProjectileSettings projectilePrefabSettings = projectilePrefab.GetComponent<ProjectileSettings>();
        if (projectilePrefabSettings == null) Debug.LogWarning($"The projectile prefab {projectilePrefab.name} does not have a ProjectilePrefabs component!");
        this.projectilePrefabSettings = projectilePrefabSettings;
        this.projectilePrefab = projectilePrefab;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 projectileSpawnPoint = GetProjectileSpawnPoint();
        Gizmos.DrawLine(projectileSpawnPoint, projectileSpawnPoint + new Vector3(0.2f, -0.2f));
        Gizmos.DrawLine(projectileSpawnPoint, projectileSpawnPoint + new Vector3(-0.2f, -0.2f));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.left * maximumDistance, transform.position + Vector3.right * maximumDistance);
    }

    public void Fire()
    {
        if (projectilePrefabSettings == null)
        {
            Debug.LogWarning("The equipped projectile is missing settings.");
        }
        else if (Time.time - lastTimeFired > projectilePrefabSettings.firingSpeed)
        {
            lastTimeFired = Time.time;
            Instantiate(projectilePrefab, GetProjectileSpawnPoint(), Quaternion.identity);
        }
    }
    
    private Vector3 GetProjectileSpawnPoint()
    {
        return transform.position + Vector3.up * distanceToSpawnProjectile;
    }
}
