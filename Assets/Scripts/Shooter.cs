using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the Projectile prefab here
    public Transform firePoint; // Assign a Transform object at the position from where projectiles are fired
    public float projectileSpeed = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse button or controller trigger
        {
            ShootProjectile();
        }
    }

    // Modify your shooting method in the shooter script
    private void ShootProjectile()
    {
        // Adjust the spawn position slightly in front of the shooter to avoid collision at instantiation
        Vector3 spawnPosition = transform.position + transform.forward * 0.75f;
        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPosition, transform.rotation);

        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity =  Camera.main.transform.forward * projectileSpeed; // Set initial velocity based on the forward direction
        }
    }

}
