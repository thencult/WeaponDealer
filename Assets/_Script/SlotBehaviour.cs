using NUnit.Framework;
using UnityEngine;

public class SlotBehaviour : MonoBehaviour
{

    public string acceptTag;
    bool hasTouchedCard = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(acceptTag))
        {
            hasTouchedCard = !hasTouchedCard;
            if (hasTouchedCard)
            {
                other.transform.position = gameObject.transform.position;
                other.transform.parent = gameObject.transform;
                print("collision");
            }
        }
    }
}