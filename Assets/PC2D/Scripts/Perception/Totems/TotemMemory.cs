using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TotemMemory : ScriptableObject
{
    public bool isActive;
    public bool activated;
    public TotemMemory[] nextTotems;


    public void ActivateFlux()
    {
        activated = true;
        foreach(TotemMemory t in nextTotems)
        {
            t.isActive = true;
        }
    }
}
