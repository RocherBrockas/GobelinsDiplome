using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecupMasque : MonoBehaviour
{
    public PauseMenu menu;
    public int index;
    public Animator animUI;

    private void Start()
    {
        menu = FindObjectOfType<PauseMenu>();
        Debug.Log(menu.morceauMasque[index].activeSelf);
        if (menu.morceauMasque[index].activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            menu.morceauMasque[index].SetActive(true);
            animUI.SetTrigger("Start");
            StartCoroutine(SmallWait(0.7f));
            this.gameObject.SetActive(false);
            //play cutscene ui mask
        }
    }


    IEnumerator SmallWait( float f)
    {
        yield return new WaitForSeconds(f);
        animUI.SetTrigger("Fade");
        animUI.gameObject.SetActive(false);
    }
}
