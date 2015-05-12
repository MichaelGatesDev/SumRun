using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    // ========================================================================================\\
    
    public float interpVelocity;            // velocity of the interpolation
    public float minDistance;               // minimum distance from target
    public float followDistance;            // follow distance from target
    public GameObject target;               // target GameObject
    public Vector3 offset;                  // offset of camera (x,y,z)
    Vector3 targetPos;                      // position of the target
    
    
    // ========================================================================================\\
    
    
    // Use this for initialization
    void Start()
    {
        // target position is where the target object is positioned
        targetPos = transform.position;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // if target exists
        if (target)
        {
            // where to put the camera
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            
            // direction to aim camera
            Vector3 targetDirection = (target.transform.position - posNoZ);
            
            // velocity to interpolate
            interpVelocity = targetDirection.magnitude * 5f;
            
            // target position with special slurp
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 
            
            // place camera at lerped position
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        } else
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
                return;
            target = player;
        }
    }
    
    // ========================================================================================\\
}
