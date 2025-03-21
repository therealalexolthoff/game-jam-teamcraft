using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    //[Tooltip("GameObject to ignore")]
    //[SerializeField] private GameObject objectToIgnore;
    [Tooltip("A list of tags the object will take damage from when colliding with.")]
    [SerializeField] private List<string> collidableTags;

    public int maxHealth;
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.LogWarning(this.gameObject.name + " has died!");
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If the object should not take damage from this collision, return
        if (!collidableTags.Contains(collision.transform.tag))
            return;

        Debug.LogWarning(this.gameObject.name + " has taken damage");
        currentHealth--;
    }
}
