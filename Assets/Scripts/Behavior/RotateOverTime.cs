using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [Tooltip("The degrees that this object rotates per second.")]
    public float degreesPerSecond = 0;

    [Tooltip("If more than 0, a random value between -randomAmount and randomAmount will be added to degreesPerSecond at start.")]
    [SerializeField] private float randomAmount = 0;

    void Awake()
    {
        if (randomAmount <= 0) return;
        degreesPerSecond += Random.Range(-randomAmount, randomAmount);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
    }
}
