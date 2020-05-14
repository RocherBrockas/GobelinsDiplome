using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupMasque : MonoBehaviour
{
    //public PauseMenu menu;
    public int index;
    public bool played;
    public Animator animUI;

    private void Start()
    {
        played = false;
        if (PauseMenu.instance.morceauMasque[index].activeSelf)
        {
            played = true;
            this.gameObject.SetActive(false);
        }
    }


    public void FadeIn()
    {
        
        PauseMenu.instance.morceauMasque[index].SetActive(true);
        PauseMenu.instance.activeTab[index] = true;
        played = true;
        this.gameObject.SetActive(false);
        AudioManager.instance.Play("Maskpickup");
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
