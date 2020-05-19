using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool maskOk;
    public GameObject Door;
    public GameObject container;
    public FluxMemory fm;
    public GameObject fx;

    private Animator anim;

    private void Start()
    {
        Door.SetActive(false);
        anim = container.GetComponent<Animator>();
        if (PauseMenu.instance.activeTab[0] && PauseMenu.instance.activeTab[1] && PauseMenu.instance.activeTab[2])
        {
            container.SetActive(true);
            Door.SetActive(true);
            if (!fm.active)
            {
                AudioManager.instance.Play("temple Open");
                AudioManager.instance.Play("temple Inside");
                AudioManager.instance.SetVolume("temple Inside", 0.65f, 2.5f);
                anim.SetTrigger("Mask");
            } else
            {
                fx.SetActive(false);
                anim.SetTrigger("Played");
            }

        }
    }
}
