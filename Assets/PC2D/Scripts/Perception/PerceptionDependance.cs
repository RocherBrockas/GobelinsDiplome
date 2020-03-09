using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;


[CreateAssetMenu(fileName = "New Perception Dependance", menuName = "Perception Dependance")]
[System.Serializable]
public class PerceptionDependance : ScriptableObject
{
        public PerceptionTypes perception;
        public bool active;
}
