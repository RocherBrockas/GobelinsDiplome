using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class SimpleRigidBodyHandle : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private float gravity = 9.81f;
    public PerceptionTypes onlyInteractiveOnPerception;
    public LayerMask expirationMask;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        rigidbody.mass = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PoofRange>() != null)
        {
            Debug.Log("oui");
        }
    }
}
