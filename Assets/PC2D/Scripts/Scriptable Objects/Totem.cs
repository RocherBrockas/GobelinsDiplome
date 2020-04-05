using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : PerceptionZone
{
    public bool isActive;
    public bool activated;
    public Totem[] nextTotems;
    public GameObject flux;
    public TotemLock[] totemLocks;

    public void ActivateFlux()
    {
        activated = true;
        flux.SetActive(true);
        foreach(Totem t in nextTotems)
        {
            t.isActive = true;
        }
        if (totemLocks.Length != 0)
        {
            foreach(TotemLock tl in totemLocks)
            {
                tl.UnlockTotem();
            }
        }
    }
}
