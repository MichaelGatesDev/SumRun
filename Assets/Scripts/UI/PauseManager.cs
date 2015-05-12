using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    private bool paused = false;
    private float normalTime;
    private Animator pma;
    private Animator ppa;
    public GameObject[] mainUI;

    // Use this for initialization
    void Start()
    {
        pma = GameObject.Find("PauseCanvas").GetComponent<Animator>();
        ppa = GameObject.Find("PausePanel").GetComponent<Animator>();
    }
    
    public void Pause()
    {
        StartCoroutine("DoPause");
        Debug.Log("Pause");
    }

    public void Unpause()
    {
        StartCoroutine("DoUnpause");
        Debug.Log("Unpause");
    }
    
    private IEnumerator DoPause()
    {
        paused = true;
        normalTime = Time.timeScale;

        ppa.Play("PausePanelFadeIn");
        pma.Play("PauseMenuFall"); //play fade in animation

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject go in mainUI)
        {
            go.SetActive(false);
        }

        Time.timeScale = 0.0f;
        yield break;
    }

    private IEnumerator DoUnpause()
    {
        paused = false;
        
        Time.timeScale = normalTime;

        ppa.Play("PausePanelFadeOut");
        pma.Play("PauseMenuRise"); //play fade in animation
        
        yield return new WaitForSeconds(0.5f);
        
        foreach (GameObject go in mainUI)
        {
            go.SetActive(true);
        }

        yield break;
    }

    public bool IsPaused()
    {
        return paused;
    }

}
