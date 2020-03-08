using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PC2D;

[CreateAssetMenu(fileName = "New Perception", menuName = "Perception")]
[System.Serializable]
public class Perception : ScriptableObject
{


    PerceptionTypes perception;

    //placeholder
    public Color c;

    public float inspireCooldown;
    public float expireCooldown;

    public float groundSpeed;
    public float acceleration;
    public float groundStopDistance;
    public float airSpeed;
    public float airAcceleration;
    public float airStopDistance;
    public float fallSpeed;
    public float gravityMultiplier;
    public float fastFallSpeed;
    public float fastFallGravity;
    public float jumpHeigth;
    public float extraJumpHeigth;
    public int numberOfAirJumps;

    //Slopes ?
    public bool enableWallJump;
    public bool enableWallStick;
    public bool enableWallSlide;
    public bool enableDash;


}
