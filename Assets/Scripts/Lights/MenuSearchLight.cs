using UnityEngine;
using System.Collections;

public class MenuSearchLight : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float speed = 1.0f;
    public float behindPadding = 10.0f;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        if (startPos == null || endPos == null)
            return;


        if (transform.position.x < endPos.position.x)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } else
        {
            transform.position = new Vector3(startPos.position.x - behindPadding, startPos.position.y, startPos.position.z);
        }

    }
}
