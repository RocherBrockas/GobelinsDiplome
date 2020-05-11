using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupMasque : MonoBehaviour
{
    public PauseMenu menu;
    public int index;
    public bool played;
    public Animator animUI;

    private void Start()
    {
        played = false;
        menu = FindObjectOfType<PauseMenu>();
        Debug.Log(menu.morceauMasque[index].activeSelf);
        if (menu.morceauMasque[index].activeSelf)
        {
            played = true;
            this.gameObject.SetActive(false);
        }
    }


    public void FadeIn()
    {
        menu.morceauMasque[index].SetActive(true);
        played = true;
        this.gameObject.SetActive(false);
        animUI.SetTrigger("Start");
    }

    public void FadeOut()
    {
        animUI.SetTrigger("Fade");
        //played = false;
        animUI.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }


}
