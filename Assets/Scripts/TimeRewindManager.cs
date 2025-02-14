using UnityEngine;

public class TimeRewindManager : MonoBehaviour
{
    private TimeRewindable[] rewindables;

    void Start()
    {
        rewindables = FindObjectsOfType<TimeRewindable>(); // Find all rewindable objects
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRewindAll();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            StopRewindAll();
        }
    }

    void StartRewindAll()
    {
        foreach (TimeRewindable obj in rewindables)
        {
            obj.StartRewind();
        }
    }

    void StopRewindAll()
    {
        foreach (TimeRewindable obj in rewindables)
        {
            obj.StopRewind();
        }
    }
}
