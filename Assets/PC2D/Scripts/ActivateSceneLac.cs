using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSceneLac : MonoBehaviour
{
    public CracheBulle[] crachesbulles;
    public GameObject chutes;
    public FluxMemory trigger;
    // Start is called before the first frame update
    void Start()
    {
        if (trigger.active)
        {
            foreach(CracheBulle c in crachesbulles)
            {
                c.isActive = true;
                chutes.SetActive(true);
            }
        }
    }

}
