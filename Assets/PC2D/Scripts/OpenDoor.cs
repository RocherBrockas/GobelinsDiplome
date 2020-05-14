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
                anim.SetTrigger("Mask");
            } else
            {
                anim.SetTrigger("Played");
            }

        }
    }
}
