using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class PoofRange : MonoBehaviour
{
    public LayerMask triggerMask;
    public float maxScaleRange;
    private Vector3 originalScale;

    public void ResetComponents()
    {
        this.transform.localScale = originalScale;
    }

    public void AnimatePoof()
    {

        StartCoroutine(ScaleOverTime(PerceptionManager.instance.perception.poofDuration));
        // Gerer les cas des différents poofs
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    string layerName = LayerMask.LayerToName(collision.gameObject.layer);
    //    if (triggerMask == (triggerMask | (1 << collision.gameObject.layer)))
    //    {
    //        if (collision.gameObject.GetComponent<PerceptionZone>().perception != null)
    //        {

    //        }
    //        else
    //        {
    //            Debug.Log("Perception Inspirée Nulle");
    //        }
    //    }
    //}

    IEnumerator ScaleOverTime(float time)
    {
        float currentTime = 0.0f;
        Vector3 destinationScale = new Vector3(maxScaleRange, maxScaleRange, maxScaleRange);
      
        do
        {
            this.transform.localScale += Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
    }

    private void Awake()
    {
        originalScale = this.transform.localScale;
    }
}
