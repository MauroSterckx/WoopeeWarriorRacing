using System.Collections.Generic;
using UnityEngine;

public class WaypointContainer : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    void Awake()
    {
        foreach (Transform tr in gameObject.GetComponentsInChildren<Transform>())
        {
            if (tr != transform) // Exclude the parent object itself
            {
                waypoints.Add(tr);
            }
        }
    }
}
