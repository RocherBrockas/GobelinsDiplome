using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionItemManager : MonoBehaviour
{

    public static PerceptionItemManager instance;

    public PerceptionItem[] perceptionItems;

    private void Awake()
    {
        if (instance)
        {
            Debug.Log(" Perception Item Manager Instance already created");
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
