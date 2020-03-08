using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PC2D;

public class PerceptionManager : MonoBehaviour
{
    public Perception perception;
    public SpriteRenderer sprite;

    /// <summary>
    /// mask with all layers that trigger events should fire when intersected
    /// </summary>
    public LayerMask triggerMask;

    public bool enablePoof = true;
    public bool enableInspire = true;


    public bool canPoof;
    public bool canInspire;

}
