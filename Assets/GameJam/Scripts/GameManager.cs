using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

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

    [SerializeField] private Planet planet;

    //Public
    public int LevelSize => levelSize;
    public PlayerMovement Player { get; set; }
    public Collapse Collapse { get; set; }
    public Dictionary<int, EnemyController> enemies = new();
    public Dictionary<int, Asteroid> asteroids = new();
    public Dictionary<int, AmmoPickup> ammunition = new();
    public Dictionary<int, Debris> debris = new();

    private void Awake()
    {
        //TODO: Remove awake method once Ivan completes player script to prevent merge conflicts
        Player = FindFirstObjectByType<PlayerMovement>();
    }

    private void Start()
    {
        //Always reset the level when the scene starts running
        Restart();
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
            StartCoroutine(WinRoutine());
            Player.ReachPlanet();
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
        planet.ResetPlanet();
        
        foreach (var item in asteroids)
        {
            item.Value.ResetAsteroid();
        }

        foreach (var item in enemies)
        {
            item.Value.ResetEnemy();
        }

        for (int i = 0; i < ammunition.Count; i++)
        {
            var item = ammunition.ElementAt(i);
            if (item.Value.ResetPickup())
            {
                ammunition.Remove(item.Key);
                i--;
            }
        }

        for (int i = 0; i < debris.Count; i++)
        {
            var item = debris.ElementAt(i);
            if (item.Value.ResetDebris())
            {
                debris.Remove(item.Key);
                i--;
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        Restart();
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(respawnDelay * 2);
        Restart();
    }
}
