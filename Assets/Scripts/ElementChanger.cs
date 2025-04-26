using UnityEngine;

public class ElementChanger : MonoBehaviour
{
    public int element;
    bool isFrozen { get => element < 0; }
    bool isBurning { get => element > 0; }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ThrowingObject>() ?? false)
        {
            if (isBurning)
            {
                other.GetComponent<ThrowingObject>().element += 
                other.GetComponent<ThrowingObject>().element > 0 ? 0 : 1;
            }
            else if (isFrozen)
            {
                other.GetComponent<ThrowingObject>().element -= 
                other.GetComponent<ThrowingObject>().element < 0 ? 0 : 1;
            }
        }
    }
}