using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFrozen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // You can add any specific behavior for the boulder here
    void Update()
    {
        if (isFrozen)
        {
            // Prevent boulder from moving when it's frozen
            rb.linearVelocity = Vector3.zero;  // Stop movement
        }
    }

    // Set the frozen state from the projectile
    public void FreezeBoulder()
    {
        isFrozen = true;
        rb.isKinematic = true;  // Freeze the boulder
    }

    // Set the unfrozen state once time has passed
    public void UnfreezeBoulder()
    {
        isFrozen = false;
        rb.isKinematic = false;  // Unfreeze the boulder
    }
}
