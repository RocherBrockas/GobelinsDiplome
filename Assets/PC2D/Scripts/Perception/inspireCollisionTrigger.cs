using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspireCollisionTrigger : MonoBehaviour
{
    public bool detectedPerception = false;
    public Perception feltPerception;
    public LayerMask triggerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (triggerMask == (triggerMask | (1 << collision.gameObject.layer)))
        {

            if (collision.gameObject.GetComponent<PerceptionZone>().perception != null && collision.gameObject.GetComponent<PerceptionZone>().canBeInspired)
            {

                detectedPerception = true;
                feltPerception = collision.gameObject.GetComponent<PerceptionZone>().perception;
            }
            else
            {
                Debug.Log("Perception Inspirée Nulle");
            }
        }
    }

}
