using System.Collections.Generic;
using UnityEngine;

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

    //Public
    public int LevelSize => levelSize;
    public Transform Player { get; set; }
    public Collapse Collapse { get; set; }
    //Add in later once enemies are implemented
    //public Dictionary<int, EnemyClass> enemyList = new();

    private void Awake()
    {
        //TODO: Remove awake method once Ivan completes player script to prevent merge conflicts
        Player = FindFirstObjectByType<PlayerMovement>().transform;
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
            //Player wins, exit program
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #endif

            Application.Quit();
        }
        else
        {
            Restart();
        }
    }

    private void Restart()
    {
        Collapse.ResetCollapse();
        Player.position = Vector3.zero;
        //TODO: reset player health
        //TODO: reset enemies and obstacles
        //TODO: (if using resources) reset resources
    }
}
