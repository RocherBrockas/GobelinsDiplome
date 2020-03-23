using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class PoofRange : MonoBehaviour
{
    public LayerMask triggerMask;
    public float maxScaleRange;
    private Vector3 originalScale;
    private Vector3 originalPos;

    private SimpleRigidBodyHandle collided;

    public void ResetComponents()
    {
        this.transform.localScale = originalScale;
    }

    public void AnimatePoof()
    {
        this.transform.SetParent(null);
        StartCoroutine(ScaleOverTime(PerceptionManager.instance.perception.poofDuration));
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (triggerMask == (triggerMask | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Check");
        }
    }

    private void Awake()
    {
        originalScale = this.transform.localScale;
    }
}
