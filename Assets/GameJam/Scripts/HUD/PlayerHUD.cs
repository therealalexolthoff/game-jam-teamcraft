using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Image black;
    [SerializeField] private float fadeTime = 1.25f;

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

    /// <summary>
    /// Set val to true to fade to black, false to remove black.
    /// </summary>
    /// <param name="val"></param>
    public void SetScreenFade(bool val)
    {
        if (val)
        {
            StartCoroutine(ScreenFade());
        }
        else
        {
            black.color = new(0, 0, 0 ,0);
        }
    }

    private IEnumerator ScreenFade()
    {
        float time = 0;
        Color startColor = new(0, 0, 0, 0);
        Color targetColor = Color.black;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            black.color = Color.Lerp(startColor, targetColor, time / fadeTime);
            yield return null;
        }

        black.color = targetColor;
    }

}
