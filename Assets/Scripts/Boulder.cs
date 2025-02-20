using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFrozen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FreezeBoulder(float freezeDuration)
    {
        if (!isFrozen) // Prevent multiple calls
        {
            isFrozen = true; // Mark the boulder as frozen
            rb.isKinematic = true; // Stop movement
            StartCoroutine(UnfreezeBoulder(freezeDuration)); // Start coroutine to unfreeze
        }
    }

    private IEnumerator UnfreezeBoulder(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the freeze duration

        isFrozen = false; // Mark the boulder as unfrozen
        rb.isKinematic = false; // Restore physics behavior
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white; // Reset color to original
        }
    }
}
