using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCollisionHandle : MonoBehaviour
{
    public GrabCollisionHandle instance;
    public LayerMask includeLayers;

    private bool gotGrabPoint;
    private Transform grabPointTransform;

    public Transform getPointTransform()
    {
        return grabPointTransform;
    }

    public bool getHadGrabbed()
    {
        return gotGrabPoint;
    }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("GrabCollider has an instance when it should not.");
        } else
        {
            instance = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (includeLayers == (includeLayers | (1 << collision.gameObject.layer)))
        {
            Debug.Log("Oui");
            gotGrabPoint = true;
            grabPointTransform = collision.transform;
        }
    }

    public void Reset()
    {
        gotGrabPoint = false;
    }
}


