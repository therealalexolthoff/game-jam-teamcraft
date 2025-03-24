using System;
using System.Collections;
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

    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float flashTime = 0.125f;
    [SerializeField] private List<Renderer> renderers;

    private int maxHealth;
    private Material defaultMat;
    private IEnumerator hitRoutine = null;

    //Public
    public event Action<int> OnDamaged;

    private void Awake()
    {
        defaultMat = renderers[0].material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the object should not take damage from this collision, return
        if (!collidableTags.Contains(collision.transform.tag))
            return;

        TriggerDamage(1);
        //Debug.LogWarning(gameObject.name + " has taken damage from " + collision.gameObject.name);
        
    }

    public void TriggerDamage(int amt)
    {
        maxHealth -= amt;
        OnDamaged?.Invoke(maxHealth);
        AudioManager.Instance.PlaySound(damageSFX);

        if (hitRoutine != null)
            StopCoroutine(hitRoutine);
        hitRoutine = DamageFlash();
        StartCoroutine(hitRoutine);
    }

    private IEnumerator DamageFlash()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = hitMaterial;
        }

        yield return new WaitForSeconds(flashTime);

        foreach (Renderer renderer in renderers)
        {
            renderer.material = defaultMat;
        }

        if (maxHealth <= 0)
        {
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
