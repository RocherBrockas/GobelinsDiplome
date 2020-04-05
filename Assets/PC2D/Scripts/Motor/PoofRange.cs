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
    private bool _scalePoof;
    private float _poofTimer;
    private Vector3 destinationScale;
    private SimpleRigidBodyHandle collided;

    public void ResetComponents()
    {
        _poofTimer = 0;
        _scalePoof = false;
        this.transform.localScale = originalScale;
    }

    public void AnimatePoof()
    {
        if (PerceptionManager.instance.perception.perceptionType != PerceptionTypes.Death)
        {
            this.transform.SetParent(null);
            _scalePoof = true;
            _poofTimer =PerceptionManager.instance.perception.poofDuration * 30;
        } else
        {
            if (PerceptionManager.instance.activeTotem != null)
            GetComponentInParent<PlayerController2D>().transform.position = PerceptionManager.instance.activeTotem.transform.position;
        }

    }


    private void FixedUpdate()
    {
        if( _scalePoof)
        {
            if (this.transform.localScale.x < maxScaleRange && this.transform.localScale.y < maxScaleRange && this.transform.localScale.z < maxScaleRange)
            {
                this.transform.localScale += Vector3.Lerp(originalScale, destinationScale,Time.deltaTime/_poofTimer);
            }
        }
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
        destinationScale = new Vector3(maxScaleRange, maxScaleRange, maxScaleRange);
    }
}
