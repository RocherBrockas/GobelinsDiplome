using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PC2D;

[CreateAssetMenu(fileName = "New Perception Item", menuName = "Perception Item")]
[System.Serializable]
public class PerceptionItem : ScriptableObject
{
    public PerceptionDependance[] dependances;

    public class PerceptionDependance 
    {
        PerceptionTypes perception;
        bool active;
    }
}
