using Unity.VisualScripting;
using UnityEngine;

public class SceneBoundaries : MonoBehaviour
{
    [Tooltip("Reference to the left edge of the scene")]
    [SerializeField] private Transform leftEdgeBoundary;


    [Tooltip("Reference to the right edge of the scene")]
    [SerializeField] private Transform rightEdgeBoundary;

    // Update is called once per frame
    void Update()
    {
        //float distanceToEdge = Vector3.Distance(leftEdgeBoundary.position, this.transform.position);
        //Debug.Log(distanceToEdge);
    }
}
