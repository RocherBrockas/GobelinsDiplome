using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCache : MonoBehaviour
{
    public GameObject masquant;
    public GameObject toMaskInsideZone;

    private bool masked;
    private SpriteRenderer[] _spriteMasquants;
    private SpriteRenderer[] _spriteToMaskInsideZone;

    //OSEF ON FERA DES ANIMS

    private void Start()
    {
        
        masked = false;
        _spriteMasquants = masquant.GetComponentsInChildren<SpriteRenderer>();
        _spriteToMaskInsideZone = toMaskInsideZone.GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(FadeIn(_spriteToMaskInsideZone));
        StartCoroutine(FadeOut(_spriteMasquants));
    }

    IEnumerator FadeIn( SpriteRenderer[] sprites)
    {
        Debug.Log("oui");
        foreach (SpriteRenderer s in sprites)
        {
            for(float f = 0.05f; f <= 1; f += 0.05f)
            {

                Color c = s.material.color;
                c.a = f;
                s.material.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    IEnumerator FadeOut(SpriteRenderer[] sprites)
    {
        Debug.Log("non");
        foreach (SpriteRenderer s in sprites)
        {
            for (float f = 0.05f; f <= 1; f += 0.05f)
            {

                Color c = s.material.color;
                c.a = 1-f;
                s.material.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collide");
            masked = true;
            StartCoroutine(FadeIn(_spriteToMaskInsideZone));
            StartCoroutine(FadeOut(_spriteMasquants));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("exit");
            masked = false;
            StartCoroutine(FadeIn(_spriteMasquants));
            StartCoroutine(FadeOut(_spriteToMaskInsideZone));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!masked)
            {
                Debug.Log("stay");
                StartCoroutine(FadeIn(_spriteToMaskInsideZone));
                StartCoroutine(FadeOut(_spriteMasquants));
            }
            masked = true;
        }
    }

}
