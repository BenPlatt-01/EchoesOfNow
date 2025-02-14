using UnityEngine;
using System.Collections.Generic;

public class TimeRewindable : MonoBehaviour
{
    private bool isRewinding = false;
    private List<TimeFrame> timeFrames = new List<TimeFrame>();
    private Rigidbody rb;
    private float timeDestroyed = -1f;  // Track when the bridge was destroyed
    private bool isDestroyed = false;   // To track destruction status

    private Vector3 destructionPosition; // Store the position where destruction happened
    private GameObject boulder;          // Reference to the boulder
    private Vector3 boulderInitialPosition; // Store boulder's position at destruction time
    private float thresholdDistance = 0.1f;  // Minimum distance the boulder must move before restoring bridge
    private bool rewindComplete = false; // Track when the rewind process is complete

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boulder = GameObject.FindWithTag("Boulder"); // Assuming you have tagged the boulder as "Boulder"
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    // Start the rewind process
    public void StartRewind()
    {
        isRewinding = true;
        rewindComplete = false; // Reset rewind complete flag

        if (rb != null)
        {
            rb.isKinematic = true;  // Disable physics while rewinding
        }
    }

    // Stop the rewind process
    public void StopRewind()
    {
        isRewinding = false;
        rewindComplete = true; // Mark rewind as complete

        if (rb != null)
        {
            rb.isKinematic = false; // Re-enable physics
        }

        // After rewind is complete, check boulder's position to restore the bridge
        CheckBoulderAndRestoreBridge();
    }

    // Rewind the position and rotation of the bridge
    void Rewind()
    {
        if (timeFrames.Count > 0)
        {
            TimeFrame frame = timeFrames[0];

            // Skip restoring the bridge until the rewind is complete
            transform.position = frame.position;
            transform.rotation = frame.rotation;
            timeFrames.RemoveAt(0);
        }
        else
        {
            StopRewind();  // Stop rewinding when all frames are processed
        }
    }

    // Record the position and rotation of the bridge
    void Record()
    {
        if (timeFrames.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            timeFrames.RemoveAt(timeFrames.Count - 1);  // Keep a max of 5 seconds of history
        }
        timeFrames.Insert(0, new TimeFrame(transform.position, transform.rotation));  // Add the current frame
    }

    // Destroy the bridge and record the destruction time and position
    public void DestroyBridge()
    {
        if (!isDestroyed)
        {
            timeDestroyed = Time.time;  // Record destruction time
            destructionPosition = transform.position; // Save the position where the bridge is destroyed
            boulderInitialPosition = boulder.transform.position; // Save boulder's position at destruction time
            isDestroyed = true;         // Mark bridge as destroyed
            gameObject.SetActive(false); // Disable the bridge immediately
        }
    }

    // Check the boulder's position after the rewind and restore the bridge if necessary
    private void CheckBoulderAndRestoreBridge()
    {
        if (boulder != null)
        {
            // Calculate the distance between the boulder's current position and its initial position at destruction
            float distance = Vector3.Distance(boulder.transform.position, boulderInitialPosition);

            // Restore the bridge if the boulder has moved past the threshold
            if (distance > thresholdDistance)
            {
                gameObject.SetActive(true);  // Restore the bridge
                isDestroyed = false;  // Mark bridge as no longer destroyed
            }
        }
    }

    // Structure to store position and rotation for each time frame
    private struct TimeFrame
    {
        public Vector3 position;
        public Quaternion rotation;

        public TimeFrame(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
}
