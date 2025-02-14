using UnityEngine;

public class BoulderCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the object hit is tagged as the Bridge
        if (collision.gameObject.CompareTag("Bridge"))
        {
            // Call DestroyBridge on the bridge to hide it and record the destruction time
            collision.gameObject.GetComponent<TimeRewindable>().DestroyBridge();
        }
    }
}
