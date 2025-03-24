using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    /*
    * Goal: Upon destruction, a random debris should spawn
    * --> debris = DebrisPrefab (GameObject)
    * --> pick from an array of debris (list) what to spawn.
    * --> spawn selected debris
    */
    [Tooltip("A list of Debris Prefabs to randomly spawn")]
    [SerializeField] private List<GameObject> debrisPrefabs;

    public void SpawnRandomDebri()
    {
        // generate a random integer between 0 and length of List - 1
        Debug.LogWarning("Size of List = " + debrisPrefabs.Count);
        int indexOfList = Random.Range(0, debrisPrefabs.Count - 1);
        Debug.LogWarning("Random number = " + indexOfList);
    }
}
