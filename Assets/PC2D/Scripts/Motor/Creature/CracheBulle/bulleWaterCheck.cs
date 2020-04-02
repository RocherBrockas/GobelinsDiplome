using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulleWaterCheck : MonoBehaviour
{
    public Bulle bulle;
    public LayerMask waterMask;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(waterMask== (waterMask| (1 << collision.gameObject.layer)))
        {
            bulle.lifespan = 60;
            Debug.Log("yes");
        }
    }

}
