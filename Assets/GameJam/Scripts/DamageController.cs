using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [Tooltip("Max Health Value")]
    [SerializeField] private int maxHealth = 1;
    private int currentHealth = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.LogWarning(this.gameObject.name + " has died!");
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning(this.gameObject.name + " has taken damage");
        currentHealth--;
    }
}
