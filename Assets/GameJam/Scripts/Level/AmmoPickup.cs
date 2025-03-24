using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Tooltip("Minimum amount for pickup.")]
    [SerializeField] private int minAmmo = 5;
    [Tooltip("Maximum amount for pickup.")]
    [SerializeField] private int maxAmmo = 15;
    [Tooltip("Sound effects played on pickup.")]
    [SerializeField] private List<AudioClip> pickupSFX;

    [Tooltip("True if the ammo pickup was initially placed in the lelve.")]
    [SerializeField] private bool sceneObject = false;

    private void Awake()
    {
        GameManager.Instance.ammunition[GetInstanceID()] = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Player.PickupAmmunition(Random.Range(minAmmo, maxAmmo + 1));
            if (pickupSFX.Count > 0)
            {
                AudioManager.Instance.PlaySound(pickupSFX[Random.Range(0, pickupSFX.Count)]);
            }
            
            gameObject.SetActive(false);
        }
    }

    public bool ResetPickup()
    {
        if (sceneObject)
        {
            gameObject.SetActive(true);
            return false;
        }
        else
        {
            Destroy(gameObject);
            return true;
        }
    }
}
