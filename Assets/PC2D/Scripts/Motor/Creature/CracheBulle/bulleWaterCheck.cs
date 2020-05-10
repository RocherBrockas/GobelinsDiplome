using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulleWaterCheck : MonoBehaviour
{
    public Bulle bulle;
    public LayerMask waterMask;

    public void destroyWithBubble()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(waterMask== (waterMask| (1 << collision.gameObject.layer)))
        {
            //bulle.lifespan = 180;
        }
    }

}
