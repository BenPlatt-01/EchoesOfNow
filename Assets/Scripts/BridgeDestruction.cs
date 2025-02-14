using UnityEngine;

public class BridgeDestruction : MonoBehaviour
{
    
    private bool isDestroyed = false;
    public GameObject destructionParticles;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boulder") && !isDestroyed)
        {
            DestroyBridge();
        }
    }

    void DestroyBridge()
    {
        gameObject.SetActive(false); // Hides instead of destroying

        
    }

    public void RestoreBridge()
    {
        isDestroyed = false;
        gameObject.SetActive(true); // Restore the bridge
    }
}
