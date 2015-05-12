using UnityEngine;
using System.Collections;

public class ItemApple : MonoBehaviour
{
    // ========================================================================================\\

    public bool gold;
    private GameObject gmObj;
    private LevelManager lm;
    //public EffectHandler effectHandler;
    
    // ========================================================================================\\

    // Use this for initialization
    void Start()
    {
        gmObj = GameObject.Find("GameManager");
        lm = gmObj.GetComponent<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D entered)
    {
        lm.AddScore(1 * (gold ? 5 : 1));
        

        Destroy(gameObject);
    }

    
    // ========================================================================================\\
}
