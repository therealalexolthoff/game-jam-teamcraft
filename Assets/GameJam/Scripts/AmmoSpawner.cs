using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [Tooltip("A list of Debris Prefabs to randomly spawn")]
    [SerializeField] private List<GameObject> ammoPrefabs;

    public void SpawnRandomAmmo()
    {
        // generate a random integer between 0 and length of List - 1
        int indexOfList = Random.Range(0, ammoPrefabs.Count);

        Vector3 positionOffset = new Vector3(0.75f, 0.75f, 0f);
        Instantiate(ammoPrefabs[indexOfList], this.transform.position + positionOffset, Quaternion.identity);
    }
}
