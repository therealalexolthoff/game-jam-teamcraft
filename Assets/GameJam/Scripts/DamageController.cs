using System;
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

    //Public
    public event Action<int> OnDamaged;

    private void OnCollisionEnter(Collision collision)
    {
        //If the object should not take damage from this collision, return
        if (!collidableTags.Contains(collision.transform.tag))
            return;

        //Debug.LogWarning(gameObject.name + " has taken damage from " + collision.gameObject.name);
        maxHealth--;
        OnDamaged?.Invoke(maxHealth);

        if (maxHealth <= 0)
        {
            Debug.LogWarning(gameObject.name + " has died!");

            // if the GameObject should spawn debris upon destruction
            if (shouldDebrisSpawn)
                debrisSpawner.SpawnRandomDebris();

            // if the GameObject should spawn ammo upon destruction
            if (shouldAmmoSpawn)
                ammoSpawner.SpawnRandomAmmo();

            gameObject.SetActive(false);
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
