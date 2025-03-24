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
                //TODO: Play audio clip
            }
            gameObject.SetActive(false);
        }
    }

    public void ResetPickup()
    {
        gameObject.SetActive(true);
    }
}
