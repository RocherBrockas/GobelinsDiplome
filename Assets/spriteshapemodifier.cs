using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class spriteshapemodifier : MonoBehaviour
{
    public SpriteShapeController shape;
    public int index;
    

    // Update is called once per frame
    void Update()
    {
        shape.spline.SetPosition( index, this.transform.position - shape.gameObject.transform.position);
    }
}
