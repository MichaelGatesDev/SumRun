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

    // ========================================================================================\\

    // Use this for initialization
    void Start()
    {
        SpawnPlayer();
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
    
    // ========================================================================================\\

    private void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            Object o = Instantiate(playerPrefab, spawnPosition.transform.position, Quaternion.identity);
            o.name = "Player";
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
