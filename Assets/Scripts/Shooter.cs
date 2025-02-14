using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;    // Reference to the projectile prefab
    public Transform shootingPoint;        // The point from where the projectile is shot
    public float fireRate = 1f;            // Fire rate in seconds (e.g., 1 shot per second)
    private float nextFireTime = 0f;       // Keeps track of the next available time to fire

    void Update()
    {
        // Check if the fire button (mouse button 1) is pressed and if enough time has passed since the last shot
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + fireRate;  // Set the next fire time based on the fire rate
        }
    }

    // Function to shoot the projectile
    void ShootProjectile()
    {
        if (projectilePrefab != null && shootingPoint != null)
        {
            // Instantiate the projectile at the shooting point with the same rotation
            GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);

            // Optionally, you can add a force or velocity to the projectile if it has a Rigidbody
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shootingPoint.forward * 10f, ForceMode.VelocityChange);  // Change the 10f to control the speed of the projectile
            }
        }
    }
}
