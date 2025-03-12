using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    public Vector2 patrolOffset = new Vector2(-1f, 0f);
    public float timePerPatrol = 3f;
    private float timePatrolling = 0f;

    public void Start()
    {
        timePatrolling = timePerPatrol / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        timePatrolling += Time.deltaTime;

        float patrolProgress = timePatrolling / timePerPatrol;

        int directionMultiplier = (int)patrolProgress % 2 == 0 ? 1 : -1;

        transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3)patrolOffset * directionMultiplier, Time.deltaTime);
    }
}
