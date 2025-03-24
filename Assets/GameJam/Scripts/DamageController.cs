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
            this.gameObject.SetActive(false);
            debrisSpawner.SpawnRandomDebri();
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
