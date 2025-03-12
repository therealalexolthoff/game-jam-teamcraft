using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    [SerializeField] private float minTimeBetweenSpawns = 1;
    [SerializeField] private float maxTimeBetweenSpawns = 2;
    [SerializeField] private Vector2 spawnRange = new Vector2(5, 1);

    private float timeUntilNextSpawn;

    public void Awake()
    {
        timeUntilNextSpawn += Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
    }

    public void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn > 0) return;
        timeUntilNextSpawn += Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
        GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        Instantiate(randomPrefab, randomPosition, Quaternion.identity);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnRange * 2);
    }
}