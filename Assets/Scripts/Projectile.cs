using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float freezeDuration = 5f; // Duration of freeze effect
    private bool isFrozen = false; // To check if the boulder is already frozen

    private void OnCollisionEnter(Collision collision)
    {
        // Log the collision event
        Debug.Log("Projectile collided with: " + collision.gameObject.name);

        // Check if the projectile collided with the boulder
        if (collision.gameObject.CompareTag("Boulder") && !isFrozen)
        {
            // Log when the projectile hits the boulder
            Debug.Log("Projectile hit the boulder, freezing it!");

            // Freeze the boulder
            BoulderFreeze(collision.gameObject);

            // Destroy the projectile after collision
            Destroy(gameObject);
        }
    }

    private void BoulderFreeze(GameObject boulder)
    {
        // Get the Rigidbody component of the boulder and freeze it
        Rigidbody rb = boulder.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Make the boulder stop moving (freeze it)
            isFrozen = true; // Mark the boulder as frozen

            // Optionally, change color to indicate it's frozen
            Renderer renderer = boulder.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.blue; // Change color to blue for frozen effect
            }

            // Start a coroutine to unfreeze the boulder after the specified duration
            StartCoroutine(UnfreezeBoulder(boulder, freezeDuration));
        }
    }

    private IEnumerator UnfreezeBoulder(GameObject boulder, float duration)
    {
        // Wait for the specified freeze duration
        yield return new WaitForSeconds(duration);

        // Log unfreezing process
        Debug.Log("Unfreezing the boulder!");

        // Unfreeze the boulder after the freeze duration
        Rigidbody rb = boulder.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Restore physics behavior (unfreeze)
        }

        // Reset the visual indicator (optional)
        Renderer renderer = boulder.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white; // Reset color to white
        }

        isFrozen = false; // Mark the boulder as unfrozen
    }
}
