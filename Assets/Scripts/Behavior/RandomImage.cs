using UnityEngine;

public class RandomImage : MonoBehaviour
{
    [Tooltip("On start, this game object's SpriteRenderer will have their sprite set to a random sprite listed here.")]
    [SerializeField] private Sprite[] spritesToChooseFrom;

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spritesToChooseFrom[Random.Range(0, spritesToChooseFrom.Length)];
    }
}