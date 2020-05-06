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
            foreach(CracheBulle c in crachesbulles)
            {
                c.isActive = trigger.active;
                chutes.SetActive(trigger.active);
            }
    }

}
