using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TotemMemory : ScriptableObject
{
    public bool isActive;
    public bool activated;
    public TotemMemory[] nextTotems;
    public TotemLock[] totemLocks;

    public void ActivateFlux()
    {
        activated = true;
        foreach(TotemMemory t in nextTotems)
        {
            t.isActive = true;
        }
        if (totemLocks.Length != 0)
        {
            foreach(TotemLock tl in totemLocks)
            {
                tl.UnlockTotem();
            }
        }
    }
}
