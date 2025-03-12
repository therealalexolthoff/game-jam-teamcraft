using UnityEngine;

public class EquipProjectile : MonoBehaviour
{
    public GameObject projectileToEquip;

    public AudioClip pickupSound;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerController playerController))
        {
            playerController.SetProjectile(projectileToEquip);

            if (pickupSound != null && AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySound(pickupSound);
            }

            Destroy(gameObject);
        }
    }
}
