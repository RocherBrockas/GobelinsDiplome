using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class spriteshapemodifier : MonoBehaviour
{
    public SpriteShapeController shape;
    public int index;
    public float parallaxEffect = 1;
    [Range(0.75f, 2f)]
    public float startingHeigth;
    //public Vector3 tangentMin;
    //public Vector3 tnagentMax;
    private bool growth;


    public void Start()
    {
        startingHeigth = Random.Range(0.1f, 2f);
        growth = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(shape.spline.GetLeftTangent(index));
        shape.spline.SetPosition( index, (this.transform.position - shape.gameObject.transform.position)*parallaxEffect);
        shape.spline.SetHeight(index, startingHeigth);
        if (startingHeigth < 0.40f)
        {
            growth = true;
        } 
        if (startingHeigth > 1.25f)
        {
            growth = false;
        }
        startingHeigth += (growth ? Random.Range(0.005f, +0.03f) : -Random.Range(0.005f, +0.03f));
        //shape.spline.SetRightTangent(index, 
        //    (growth ? Vector3.Lerp(shape.spline.GetRightTangent(index), tnagentMax, Time.deltaTime) : Vector3.Lerp(shape.spline.GetRightTangent(index), tangentMin, Time.deltaTime)));
        //if (shape.spline.GetLeftTangent(index) != null)
        //{
        //    shape.spline.SetRightTangent(index, (growth ? Vector3.Lerp(shape.spline.GetLeftTangent(index), -tnagentMax, Time.deltaTime) : Vector3.Lerp(shape.spline.GetLeftTangent(index), -tangentMin, Time.deltaTime)));
        //}
    }
}
