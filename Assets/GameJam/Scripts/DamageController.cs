using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    //[Tooltip("GameObject to ignore")]
    //[SerializeField] private GameObject objectToIgnore;
    [Tooltip("A list of tags the object will take damage from when colliding with.")]
    [SerializeField] private List<string> collidableTags;

    // Reference to DebrisSpawner
    [Tooltip("Reference to DebrisSpawner Component")]
    [SerializeField] private DebrisSpawner debrisSpawner;

    // Reference to AmmoSpawner
    [Tooltip("Reference to AmmoSpawner Component")]
    [SerializeField] private AmmoSpawner ammoSpawner;

    [Tooltip("Set value to true if Debris should spawn when GameObject destroyed, false otherwise")]
    [SerializeField] private bool shouldDebrisSpawn;

    [Tooltip("Set value to true if Amo should spawn when GameObject destroyed, false otherwise")]
    [SerializeField] private bool shouldAmmoSpawn;

    private int maxHealth;
    //private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //currentHealth = maxHealth;
        //Debug.LogWarning(currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the object should not take damage from this collision, return
        if (collidableTags.Contains(collision.transform.tag))
            return;

        Debug.LogWarning(this.gameObject.name + " has taken damage from " + collision.gameObject.name);
        maxHealth--;

        if (maxHealth <= 0)
        {
            Debug.LogWarning(this.gameObject.name + " has died!");

            // if the GameObject should spawn debris upon destruction
            if (shouldDebrisSpawn)
                debrisSpawner.SpawnRandomDebri();

            // if the GameObject should spawn ammo upon destruction
            if (shouldAmmoSpawn)
                ammoSpawner.SpawnRandomAmmo();

            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// SetMaxHealth
    /// <para> brief: This function sets the maximum health of the owner GameObject. </para>
    /// <para> params: _maxHealth: int, The maximum health of the owner GameObject. </para>
    /// <para> return: NONE </para>
    /// </summary>
    public void SetMaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
    }
}
