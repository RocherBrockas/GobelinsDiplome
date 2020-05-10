using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class FluxPoint : MonoBehaviour
{
    [HideInInspector]
    public bool played;
    public LayerMask expirationMask;
    public ParticleSystem idle;
    public ParticleSystem idleBack;
    public ParticleSystem blobiness;
    public SpriteRenderer spriteRenderer;
    public Sprite eteint;
    public Sprite allume;
    public ParticleSystem expansion;
    public PerceptionTypes perception;
    public FluxMemory FluxMemory;
    public FluxMemory prevFM;
    public GameObject ParcoursFlux;
    public GameObject PreviousFlux;
    private bool needExpiration;

    private void Deactivate()
    {
        spriteRenderer.sprite = eteint;
        idle.Stop();
        idleBack.Stop();
        blobiness.Stop();
        expansion.Stop();
    }

    void Start()
    {
        needExpiration = false;
        if (PreviousFlux)
        {
            PreviousFlux.SetActive(prevFM.active);
            needExpiration = true;
            Deactivate();
        }
        if (!FluxMemory.active)
        {
            spriteRenderer.sprite = eteint;
            played = false;
            expansion.Stop();
            idle.Play();
            idleBack.Play();
            ParcoursFlux.SetActive(false);
        }
        else
        {
            played = true;
            idle.Stop();
            idleBack.Stop();
            blobiness.Play();
            spriteRenderer.sprite = allume;
            expansion.Stop();
            ParcoursFlux.SetActive(true);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!played && ((!needExpiration && collision.CompareTag("Player") || needExpiration && (expirationMask == (expirationMask | (1 << collision.gameObject.layer))))))
        {
            if (PerceptionManager.instance.perception.perceptionType == perception)
            {
                played = true;
                idle.Stop();
                idleBack.Stop();
                expansion.Play();
                blobiness.Play();
                spriteRenderer.sprite = allume;
                FluxMemory.active = true;
                ParcoursFlux.SetActive(true);
            }
        }
    }
}
