using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    //[Tooltip("GameObject to ignore")]
    //[SerializeField] private GameObject objectToIgnore;

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
        Debug.LogWarning(this.gameObject.name + " has taken damage");
        currentHealth--;
    }
}
