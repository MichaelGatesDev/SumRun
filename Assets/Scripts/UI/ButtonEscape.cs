using UnityEngine;
using System.Collections;

public class ButtonEscape : MonoBehaviour
{
    private GameObject gm;
    private PauseManager pm;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager");
        pm = gm.GetComponent<PauseManager>();
    }

    public void OnPress()
    {
        pm.Pause();
    }

}
