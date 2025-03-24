using System.Collections.Generic;
using UnityEngine;
using System.Collections;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    //Singleton implementation
    private static GameManager _instance;
    /// <summary>
    /// Singleton implementation of the GameManager class. Whenever the reference is first called,
    /// the Instance is initialized.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindFirstObjectByType<GameManager>();
            return _instance;
        }
    }

    //Inspector
    [Tooltip("The length of the level from the start to the end location.")]
    [SerializeField] private int levelSize = 350;

    [Tooltip("Delay before respawning.")]
    [SerializeField] private float respawnDelay = 1.25f;

    //Public
    public int LevelSize => levelSize;
    public PlayerMovement Player { get; set; }
    public Collapse Collapse { get; set; }
    public Dictionary<int, EnemyController> enemies = new();
    public Dictionary<int, Asteroid> asteroids = new();
    public Dictionary<int, AmmoPickup> ammunition = new();

    private void Awake()
    {
        //TODO: Remove awake method once Ivan completes player script to prevent merge conflicts
        Player = FindFirstObjectByType<PlayerMovement>();
    }

    private void Start()
    {
        //Always reset the level when the scene starts running
        //Restart();
    }

    /// <summary>
    /// Ends the level. If the level ends in a loss, restart.
    /// </summary>
    /// <param name="_win">True if the player reached the planet.</param>
    public void EndLevel(bool _win)
    {
        //TODO: Make screen fade out
        if (_win)
        {
            //Player wins, exit program
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #endif

            Application.Quit();
        }
        else
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private void Restart()
    {
        Collapse.ResetCollapse();
        Player.ResetPlayer();
        
        foreach (var item in asteroids)
        {
            item.Value.ResetAsteroid();
        }

        foreach (var item in enemies)
        {
            item.Value.ResetEnemy();
        }

        foreach (var item in ammunition)
        {
            item.Value.ResetPickup();
        }
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        Restart();
    }
}
