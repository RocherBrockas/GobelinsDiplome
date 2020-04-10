using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : PerceptionZone
{
    public TotemMemory totemMemory;
    public GameObject Flux;
    public TotemLock[] totemLocks;

    public void ActivateFlux()
    {
        Flux.SetActive(true);
        if (totemLocks.Length != 0)
        {
            foreach (TotemLock tl in totemLocks)
            {
                tl.UnlockTotemCheck();
            }
        }
    }

    public void Awake()
    {
        if (totemMemory.activated)
        {
            ActivateFlux();
        }
    }
}
