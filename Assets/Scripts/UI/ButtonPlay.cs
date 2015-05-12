using UnityEngine;
using System.Collections;

public class ButtonPlay : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }


    public void OnPress()
    {
        Application.LoadLevel("Level");
    }

}
