using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (playerMask == (playerMask| (1 << collision.gameObject.layer)))
        {
            collision.gameObject.GetComponent<PlatformerMotor2D>().ForceJump();
        }
    }
}
