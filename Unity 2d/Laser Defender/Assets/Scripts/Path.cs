using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "Scriptables/Path")]
public class Path : ScriptableObject {

    [SerializeField] private GameObject waypointsObject;   

    public List<Transform> GetWayPoints()
    {
        List <Transform> waypoints = new List<Transform>();
        foreach (Transform child in waypointsObject.transform)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

}
