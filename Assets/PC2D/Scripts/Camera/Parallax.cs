﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length, _startposX;
    private Vector3 newpos = new Vector3();

    public GameObject Camera;
    public float parallaxRatio;

    // Start is called before the first frame update
    void Start()
    {
        newpos = Vector3.zero;
        _startposX = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }   

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (Camera.transform.position.x * parallaxRatio);

        newpos.x = _startposX + dist;
        newpos.y = transform.position.y;
        newpos.z = transform.position.z;
        transform.position = newpos;
    }
}