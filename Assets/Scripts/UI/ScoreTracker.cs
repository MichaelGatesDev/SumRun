using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    // ========================================================================================\\

    private GameObject gmObj;
    private LevelManager lm;
    public GameObject textObj;

    // ========================================================================================\\

    // Use this for initialization
    void Start()
    {
        gmObj = GameObject.Find("GameManager");
        lm = gmObj.GetComponent<LevelManager>();

    }
    
    // Update is called once per frame
    void Update()
    {
        textObj.GetComponent<Text>().text = getScore() + "";
    }

    
    // ========================================================================================\\

    private int getScore()
    {
        return lm.GetScore();
    }
    
    // ========================================================================================\\
}
