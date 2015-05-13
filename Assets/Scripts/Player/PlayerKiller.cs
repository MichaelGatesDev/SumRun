using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour
{
    // ========================================================================================\\

    private GameObject player;
    private GameObject gmObj;
    private LevelManager lm;
    
    // ========================================================================================\\

    void Start()
    {
        gmObj = GameObject.Find("GameManager");
        lm = gmObj.GetComponent<LevelManager>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.gameObject.name.Equals("Player"))
            return;

        lm.KillPlayer();
    }

    // ========================================================================================\\
}
