using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : PerceptionZone
{
    public TotemMemory totemMemory;
    public GameObject Flux;

    public void ActivateFlux()
    {
        Flux.SetActive(true);
    }

    public void Awake()
    {
        if (totemMemory.activated)
        {
            ActivateFlux();
        }
    }
}
