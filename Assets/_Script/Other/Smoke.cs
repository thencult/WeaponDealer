using UnityEngine;

public class Smoke : MonoBehaviour
{
    void DestroyUponEnding() {
        Destroy(this.gameObject);
    }
}
