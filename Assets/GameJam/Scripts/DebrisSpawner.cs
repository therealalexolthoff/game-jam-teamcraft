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
    [SerializeField] private List<Debris> debrisPrefabs;

    public void SpawnRandomDebris()
    {
        // generate a random integer between 0 and length of List - 1
        int indexOfList = Random.Range(0, debrisPrefabs.Count);

        Debris debris = Instantiate(debrisPrefabs[Random.Range(0, debrisPrefabs.Count)], transform.position, Quaternion.identity);
        debris.Init();
    }
}
