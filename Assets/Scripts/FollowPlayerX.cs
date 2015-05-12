using UnityEngine;
using System.Collections;

public class FollowPlayerX : MonoBehaviour
{
    private GameObject player;
    public int xOffset;

    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        float x = player.transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;


        transform.position = new Vector3(x + xOffset, y, z);
    }
}
