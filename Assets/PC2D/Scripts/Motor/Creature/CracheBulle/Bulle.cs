﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulle : MonoBehaviour
{
    public bool isDestroyable;
    public bool goDown;
    public float lifespan;
    public float speedThreshold;
    public float floatBoost;
    public float forceLancement;
    private Vector2 force;
    private bool _containsPlayer = false;
    [SerializeField]
    private LayerMask collisionMask;
    private PlayerController2D _playerController;
    private Rigidbody2D rb;

    public void Pop()
    {
        GetComponentInChildren<bulleWaterCheck>().destroyWithBubble();
        if (_containsPlayer)
        {
            this.transform.DetachChildren();
            _containsPlayer = false;
            _playerController.isInBubble = false;
        }
        Destroy(this.gameObject);

    }

    private void Start()
    {
        force = new Vector2(forceLancement * 5, 0);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(force);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_containsPlayer)
        {
            if (rb.velocity.magnitude < speedThreshold)
            {
                rb.AddForce(Vector2.up * floatBoost);
            }
        }
        else if (goDown)
        {
            if (rb.velocity.magnitude < speedThreshold)
            {
                rb.AddForce(Vector2.up * -floatBoost/2);
            }
        }

        if (isDestroyable)
        {
            if (lifespan > 0)
            {
                lifespan--;

            }
            else
            {
                if (_containsPlayer)
                {
                    _playerController.JumpOutOfBubble();
                }
                else
                {
                    Pop();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionMask == (collisionMask | (1 << collision.gameObject.layer)) && collision.collider.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController2D>() != null)
            {
                _playerController = collision.gameObject.GetComponent<PlayerController2D>();
                _playerController.isInBubble = true;
                _playerController.Setbubble(this);
                collision.transform.SetParent(this.transform);
                collision.transform.position = this.transform.position;
                this.GetComponent<CircleCollider2D>().isTrigger = true;
                _containsPlayer = true;
                isDestroyable = true;
            }
        }
    }
}