using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCache : MonoBehaviour
{
    private bool masked;
    private Animator anim;


    private void Start()
    {
        masked = false;
        anim = this.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collide");
            masked = true;
            anim.SetTrigger("Switch");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("exit");
            masked = false;
            anim.SetTrigger("Switch");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!masked)
            {
                Debug.Log("stay");
                anim.SetTrigger("Switch");
            }
            masked = true;
        }
    }

}
