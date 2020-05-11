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
    public RecupMasque masque;
    public PerceptionTypes perceptionTypes;
    private bool displayingMask = false;
    private bool _activeOnce = true;

    public void RemoveUI()
    {
        motor.frozen = false;
        UIAnimator.SetTrigger("Fade");
    }

    public void DisplayUI()
    {
        if (!displayingMask)
        {
            motor.frozen = true;
            played = true;
            textBox.text = contenu.texte[0];
            topImage.sprite = contenu.imageTop;
            bottomImage.sprite = contenu.phraseBottom;
            AudioManager.instance.Play("Totem");
            UIAnimator.SetTrigger("Start");
            StartCoroutine(SmallWait());
        }
    }

    public void Update()
    {
        if (UnityEngine.Input.GetButtonDown(PC2D.Input.INTERACT))
        {
            if(masque.played && _activeOnce )
            {
                _activeOnce = false;
                masque.FadeOut();
                UnfreezeMotor();
                //played = false;
            }
        }
        if (motor.frozen && played && UnityEngine.Input.GetButtonDown(PC2D.Input.INTERACT))
        {
            if (!masque.played || !displayingMask)
                RemoveUI();
            StartCoroutine(WaitRead());
            if (!tuto)
            {
                played = true;
                if (!masque.played)
                {
                    motor.frozen = true;
                    displayingMask = true;
                    masque.FadeIn();
                }
            }
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

    private void UnfreezeMotor()
    {
        motor.frozen = false;
        StartCoroutine(SmallWaitDisplay());
    }

    IEnumerator SmallWait()
    {
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator SmallWaitDisplay()
    {
        yield return new WaitForSeconds(0.2f);
        displayingMask = false;
    }

    IEnumerator WaitRead()
    {
        yield return new WaitForSeconds(0.5f);
        if (!tuto)
        {
            played = false;
        }
    }

}
