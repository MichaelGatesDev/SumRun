using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    // ========================================================================================\\

    public GameObject playerPrefab;
    public GameObject spawnPosition;
    //
    private int score;
    private bool alive;
    private GameObject player;

    // ========================================================================================\\

    // Use this for initialization
    void Start()
    {
        SpawnPlayer();
    }
    
    // Update is called once per frame
    void Update()
    {
        GameObject.Find("DeathBarrier").transform.position = player.transform.position - new Vector3(0, 10, 0);
    }
    
    // ========================================================================================\\

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            Object o = Instantiate(playerPrefab, spawnPosition.transform.position, Quaternion.identity);
            o.name = "Player";

            player = GameObject.Find("Player");
        }
    }
    
    // ========================================================================================\\

    public void AddScore(int n)
    {
        this.score += n;
    }

    public void SetAlive(bool b)
    {
        this.alive = b;
    }
    
    // ========================================================================================\\


    public bool IsAlive()
    {
        return alive;
    }

    public int GetScore()
    {
        return score;
    }


    // ========================================================================================\\
}
