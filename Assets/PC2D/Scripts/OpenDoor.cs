using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool maskOk;
    public GameObject container;
    public FluxMemory fm;

    private Animator anim;

    private void Start()
    {
        anim = container.GetComponent<Animator>();
        if (PauseMenu.instance.activeTab[0] && PauseMenu.instance.activeTab[1] && PauseMenu.instance.activeTab[2])
        {
            container.SetActive(true);
            if (!fm.active)
            {
                AudioManager.instance.Play("temple Open");
                AudioManager.instance.Play("temple Inside");
                AudioManager.instance.SetVolume("temple Inside", 0.65f, 2.5f);
                anim.SetTrigger("Mask");
            } else
            {
                anim.SetTrigger("Played");
            }

        }
    }
}
