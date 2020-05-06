using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTrigger : MonoBehaviour
{
    public CracheBulle[] cb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(CracheBulle c in cb)
            {
                c.isActive = true;
            }
            this.gameObject.SetActive(false);
        }
    }
}
