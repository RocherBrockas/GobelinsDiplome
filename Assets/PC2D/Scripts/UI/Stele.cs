using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PC2D;


public class Stele : MonoBehaviour
{
    public Animator UIAnimator;
    public TMPro.TextMeshProUGUI textBox;
    public Image topImage;
    public Image bottomImage;
    public Dialogue contenu;
    public PlatformerMotor2D motor;
    public bool played;
    public bool tuto;
    public PerceptionTypes perceptionTypes;

    public void RemoveUI()
    {
        motor.frozen = false;
        UIAnimator.SetTrigger("Fade");
    }

    public void DisplayUI()
    {
        motor.frozen = true;
        played = true;
        textBox.text = contenu.texte[0];
        topImage.sprite = contenu.imageTop;
        bottomImage.sprite = contenu.phraseBottom;
        AudioManager.instance.Play("Totem");
        UIAnimator.SetTrigger("Start");
    }

    public void FixedUpdate()
    {
        if (motor.frozen && played && UnityEngine.Input.GetButtonDown(PC2D.Input.INTERACT))
        {
            RemoveUI();
            StartCoroutine(WaitRead());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!played && tuto && PerceptionManager.instance.perception.perceptionType == perceptionTypes && collision.CompareTag("Player"))
        {
            played = true;
            DisplayUI();
        }
    }

    IEnumerator WaitRead()
    {
        yield return new WaitForSeconds(1.0f);
        if (!tuto)
        {
            played = false;
        }
    }

}
