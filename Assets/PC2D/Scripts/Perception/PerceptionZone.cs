using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class PerceptionZone : MonoBehaviour
{
    public Perception perception;
    public bool canBeInspired;
    public bool needCollidingChange;
    public bool CoupeFlux;

    private ParticleSystem fx;

    private void Start()
    {
        if (fx == null && CoupeFlux)
        {
            fx = this.GetComponentInChildren<ParticleSystem>();
        }
    }

    public void PlayFx()
    {
        if (fx != null)
        {
            fx.Play();
        } else
        {
            Debug.Log("No Fx on this component");
        }
    }
}
