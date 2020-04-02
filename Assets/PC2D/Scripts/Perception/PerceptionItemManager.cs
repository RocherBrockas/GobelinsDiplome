using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class PerceptionItemManager : MonoBehaviour
{

    public static PerceptionItemManager instance;


    public GameObject[] perceptionItems = new GameObject[Globals.PerceptionTotalNumber];
    private Perception _currentPlayerPerception;

    private void GetPlayerPerception()
    {
        _currentPlayerPerception = PerceptionManager.instance.perception;
    }

    private void Awake()
    {
        if (instance)
        {
            Debug.Log(" Perception Item Manager Instance already created");
        }
        else
        {
            instance = this;
            foreach (GameObject d in perceptionItems)
            {
                if (d == null)
                {
                    Debug.LogError("No Game Object initialized in PerceptionItemManager");
                }
            }
        }
    }

    public void ChangeLayout()
    {
        GetPlayerPerception();
        foreach (GameObject g in perceptionItems)
        {
            g.SetActive(true);
            PerceptionItem p = g.GetComponent<PerceptionItemDependance>().perceptionItem;
            foreach (PerceptionDependance dep in p.dependances)
            {
                if (_currentPlayerPerception.perceptionType == dep.perception)
                {
                    //Debug.Log(g.name + " " + dep.name + " current dependance perception: " + dep.perception + "  current player perception :"+ _currentPlayerPerception);
                    g.SetActive(dep.active);
                } 
            }
        }
    }
}
