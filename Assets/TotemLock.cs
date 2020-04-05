﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemLock : MonoBehaviour
{
    //rework into scriptable objects for gate and only item.
    //make it into a list of items to activate and deactivate so there are no parts of the environment in it
    public Totem[] requiredTotems;
    public GameObject toActivate;
    public GameObject toDeActivate;

    private bool _unlocked = false;

    public void UnlockTotem()
    {
        bool loopcheck = true; ;
        foreach(Totem t in requiredTotems)
        {
            if (!t.activated)
            {
                loopcheck = false;
            }  
        }
        if (loopcheck)
        {
            if (toActivate != null)
                toActivate.SetActive(true);
           
            if (toDeActivate != null)
                toDeActivate.SetActive(false);
            
        }
    }
}