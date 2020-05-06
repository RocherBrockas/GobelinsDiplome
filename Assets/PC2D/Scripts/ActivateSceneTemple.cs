using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSceneTemple : MonoBehaviour
{
    public FluxMemory fm;
    // Start is called before the first frame update
    void Start()
    {
        if (fm.active)
        {
            this.GetComponent<Animator>().SetTrigger("TempleActivate");
        }
    }
}
