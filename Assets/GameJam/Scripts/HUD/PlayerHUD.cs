using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Tooltip("Text for ammo count.")]
    [SerializeField] private TMP_Text ammoCounter;

    [Tooltip("Text for health display.")]
    [SerializeField] private TMP_Text healthText;

    [Tooltip("Text for messages to be displayed.")]
    [SerializeField] private TMP_Text messageText;

    [Tooltip("Amount of time for the message to be displayed.")]
    [SerializeField] private float messageDisplayTime = 1.25f;

    /// <summary>
    /// Sets the display for ammo count to amt.
    /// </summary>
    /// <param name="_amt"></param>
    public void UpdateAmmoText(int _amt)
    {
        ammoCounter.text = "Rounds: " + _amt;
    }

    public void UpdateHealthText(int _health)
    {
        healthText.text = "Health: " + _health;
    }

    public void SetMessageText(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
        StartCoroutine(MessageRoutine());
    }

    private IEnumerator MessageRoutine()
    {
        yield return new WaitForSeconds(messageDisplayTime);
        messageText.gameObject.SetActive(false);
    }
    
    // Player Shield Generator (Life Display Pending)
}
