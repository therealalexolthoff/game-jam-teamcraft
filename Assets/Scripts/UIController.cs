using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameObject playerObject;

    public TMP_Text scoreText;

    public string defaultScoreText = "SCORE: ";

    public TMP_Text healthText;

    public string defaultHealthText = "HEALTH: ";

    public GameObject loseScreen;

    private int currentScore;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        loseScreen.SetActive(false);
        scoreText.text = defaultScoreText + currentScore;
    }

    public void Update()
    {
        if(playerObject == null && loseScreen != null)
        {
            loseScreen.SetActive(true);
            healthText.text = defaultHealthText + "0";
        }

        if (playerObject != null && playerObject.GetComponent<HealthPool>())
        {
            healthText.text = defaultHealthText + playerObject.GetComponent<HealthPool>().GetCurrentHitPoints();
        }
    }

    // Start is called before the first frame update
    public void ChangeScore(int scoreChange)
    {
        currentScore += scoreChange;
        scoreText.text = defaultScoreText + currentScore;
    }
}
