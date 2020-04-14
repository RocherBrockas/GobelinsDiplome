using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;
using TMPro;

public class Totem : PerceptionZone
{
    public TotemMemory totemMemory;
    public bool isActive;
    public bool activated;
    public bool cutsceneTotem;
    public GameObject Flux;
    public TotemLock[] totemLocks;
    public GameObject mask;
    public Sprite activatedSprite;
    [TextArea(2,3)]
    public string Passage;
    public Animator canvasGroup;
    public TextMeshProUGUI textmesh;
    private PlatformerMotor2D _motor;

    public void ActivateFlux()
    {
        isActive = true;
        activated = true;
        if (!cutsceneTotem)
        {
            AudioManager.instance.Play("Totem");
            this.GetComponent<SpriteRenderer>().sprite = activatedSprite;
        }
        mask.SetActive(false);
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
        textmesh.text = Passage;
        isActive = totemMemory.isActive;
        activated = totemMemory.activated;

        if (activated)
        {
            ActivateFlux();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!cutsceneTotem && collision.gameObject.CompareTag("Player"))
        {
            textmesh.text = Passage;
            canvasGroup.SetTrigger("Start");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!cutsceneTotem && collision.gameObject.CompareTag("Player"))
        {
            canvasGroup.SetTrigger("Fade");
        }
    }
}
