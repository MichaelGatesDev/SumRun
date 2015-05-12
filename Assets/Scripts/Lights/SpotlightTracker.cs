using UnityEngine;
using System.Collections;

public class SpotlightTracker : MonoBehaviour
{
    public float[] yWaypoints;
    public float startX;

    public void Move(int waypoint)
    {
        float x = gameObject.transform.position.x;
        float z = gameObject.transform.position.z;
        gameObject.transform.position = new Vector3(x, yWaypoints [waypoint], z);
    }

}
