using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PC2D;
using System;

[CreateAssetMenu(fileName = "New Perception Item", menuName = "Perception Item")]
[System.Serializable]
public class PerceptionItem : ScriptableObject
{
    public PerceptionDependance[] dependances = new PerceptionDependance[Globals.PerceptionTotalNumber];

    //private void Awake()
    //{
    //    foreach (PerceptionDependance d in dependances)
    //    {
    //        if (d == null)
    //        {
    //            Debug.LogError("No dependance initialized in PerceptionItem");
    //        }
    //    }
    //}
}
