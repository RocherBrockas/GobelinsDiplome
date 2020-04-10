using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class SimpleRigidBodyHandle : MonoBehaviour
{

    public PerceptionTypes onlyInteractiveOnPerception;
    public LayerMask expirationMask;
    private Vector3 originPosition;
    public float mass;
    private float originMass;
    public bool isMovingPlatform;
    public bool isFallingPlatform;
    public bool isBubble;
    private PerceptionTypes currentPoofPerception = PerceptionTypes.None;
    private new Rigidbody2D rigidbody;

    public void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody.bodyType == RigidbodyType2D.Dynamic)
            {
                originMass = rigidbody.mass;
            }
        }
        else
        {
            rigidbody = GetComponentInParent<Rigidbody2D>();
        }
        originPosition = GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PoofRange>() != null)
        {
            if (expirationMask == (expirationMask | (1 << collision.gameObject.layer)) && PerceptionManager.instance.perception.perceptionType == onlyInteractiveOnPerception)
            {
                currentPoofPerception = onlyInteractiveOnPerception;
                if (isMovingPlatform || isFallingPlatform)
                {
                    rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    rigidbody.mass = mass;
                    if (GetComponent<MovingPlatformMotor2D>() != null)
                    {
                        GetComponent<MovingPlatformMotor2D>().enabled = false;
                    }
                }
                else if (isBubble)
                {
                    GetComponentInParent<Bulle>().goDown = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (expirationMask == (expirationMask | (1 << collision.gameObject.layer)) && currentPoofPerception == onlyInteractiveOnPerception)
        {
            if (isMovingPlatform)
            {
                rigidbody.velocity = Vector2.zero;
                GetComponent<MovingPlatformMotor2D>().enabled = true;
                rigidbody.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<MovingPlatformMotor2D>().needReset = true;
            }
            else if (isBubble)
            {
                GetComponentInParent<Bulle>().goDown = false;
            }
            if (isFallingPlatform && originMass != 0)
            {
                rigidbody.mass = originMass;
            }
        }
    }

}
