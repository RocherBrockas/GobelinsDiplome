using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class FluxPoint : MonoBehaviour
{
    [HideInInspector]
    public bool played;
    public ParticleSystem idle;
    public ParticleSystem expansion;
    public PerceptionTypes perception;
    public FluxMemory FluxMemory;
    public FluxMemory prevFM;
    public GameObject ParcoursFlux;
    public GameObject PreviousFlux;

    void Start()
    {
        if (PreviousFlux)
        {
            if (prevFM.active)
            {
                PreviousFlux.SetActive(true);
            }
        }
        if (!FluxMemory.active)
        {
            played = false;
            expansion.Stop();
            idle.Play();
            ParcoursFlux.SetActive(false);
        }
        else
        {
            played = true;
            idle.Stop();
            expansion.Stop();
            ParcoursFlux.SetActive(true);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!played && collision.CompareTag("Player"))
        {
            if (PerceptionManager.instance.perception.perceptionType == perception)
            {
                played = true;
                idle.Stop();
                expansion.Play();
                FluxMemory.active = true;
                ParcoursFlux.SetActive(true);
            }
        }
    }
}
