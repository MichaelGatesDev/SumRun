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
    private GameObject deathBarrier;

    // ========================================================================================\\

    // Use this for initialization
    void Start()
    {
        SpawnPlayer();

        deathBarrier = GameObject.Find("DeathBarrier");
    }
    
    // Update is called once per frame
    void Update()
    {




        deathBarrier.transform.position = new Vector3(player.transform.position.x, deathBarrier.transform.position.y, 0);
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

    public void KillPlayer()
    {
        Debug.Log("death to ye!");
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
