using UnityEngine;

public class Planet : MonoBehaviour
{
    //Inspector
    [Tooltip("When the player starts the level, the planet will be at this scale.")]
    [SerializeField] private float planetMinScale = 1;
    [Tooltip("When the player ends the level, the planet will be at this scale.")]
    [SerializeField] private float planetMaxScale = 15;
    [Tooltip("The multiplier for the parallax of the planet.")]
    [SerializeField] private float parallaxMultiplier = 1;
    [Tooltip("The GameObject that represents the visuals of the planet. Should be parented to the camera.")]
    [SerializeField] private Transform planetGraphic = null;

    //Private
    /// <summary>
    /// Multiplies the distance the planet graphic is moved based on the player's distance.
    /// </summary>
    private const float PARALLAX_MULT_INTERNAL = -0.0025f;
    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = planetGraphic.localPosition;
    }

    private void Update()
    {
        //The percentage of the level the player has currently traversed (from 0 to 1)
        float distanceScale = Mathf.Clamp01(1 - (transform.position.y - GameManager.Instance.Player.transform.position.y) / GameManager.Instance.LevelSize);

        //Scale up the planet graphic as the player gets closer
        float scale = Mathf.Lerp(planetMinScale, planetMaxScale, distanceScale);
        planetGraphic.localScale = new(scale, scale, scale);

        //Move the planet graphic across the screen, faster when the player is closer to the end of the level.
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            input += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            input += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            input += Vector2.right;

        planetGraphic.localPosition += PARALLAX_MULT_INTERNAL * parallaxMultiplier * (0.5f + distanceScale / 2) * (Vector3)input * 60 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //End the level in a win if the player reaches the planet
            Debug.Log("You Win!");
            GameManager.Instance.EndLevel(true);
        }
    }

    public void ResetPlanet()
    {
        planetGraphic.localPosition = initialPos;
    }
}
