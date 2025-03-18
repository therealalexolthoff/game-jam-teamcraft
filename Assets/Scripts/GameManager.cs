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

    //Private
    //Add in later once enemies are implemented
    //private Dictionary<int, EnemyClass> enemyList = new();

    private void Awake()
    {
        Player = FindFirstObjectByType<PlayerMovement>().transform;
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
            //restart
        }
    }

    //Remove after testing
    private void Update()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            input += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            input += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            input += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            input += Vector2.right;

        Camera.main.transform.position += 1.5f * Time.deltaTime * (Vector3)input;
    }
}
