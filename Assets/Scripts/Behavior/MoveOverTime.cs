using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    [Tooltip("The direction, x and y, that the object moves")]
    public Vector2 direction = new Vector2(0f, -1f);

    [Tooltip("If more than 0, a random value between 0 and randomAmount will be added to the magnitude of direction on start.")]
    [SerializeField] private float randomAmount = 0;

    void Awake()
    {
        if (randomAmount <= 0) return;
        
        direction += direction.normalized * Random.Range(0, randomAmount);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = transform.position;

        Vector2 distanceToMove = direction * Time.deltaTime;

        newPosition += distanceToMove;

        transform.position = newPosition;
    }
}
