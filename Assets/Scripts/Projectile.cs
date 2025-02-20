
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float freezeDuration = 5f;

    private float lifetime = 6f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            BoulderFreeze(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void BoulderFreeze(GameObject boulder)
    {
        Boulder boulderScript = boulder.GetComponent<Boulder>();
        if (boulderScript != null)
        {
            boulderScript.FreezeBoulder(freezeDuration); // Pass the duration to Boulder script
            Renderer renderer = boulder.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.blue; // Change color to blue
            }
        }
    }
}
