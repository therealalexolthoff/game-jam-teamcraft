using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Tooltip("Text for ammo count.")]
    [SerializeField] private TMP_Text ammoCounter;

    /// <summary>
    /// Sets the display for ammo count to amt.
    /// </summary>
    /// <param name="_amt"></param>
    public void UpdateAmmoText(int _amt)
    {
        ammoCounter.text = _amt.ToString();
    }
    
    // Player Shield Generator (Life Display Pending)
}
