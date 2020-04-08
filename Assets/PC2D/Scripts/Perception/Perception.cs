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

    public PerceptionTypes perceptionType;

    public float inspireCooldown;
    public float expireCooldown;
    public float expireRange;
    public float poofDuration;
    public float poofCharge;

    //Faire le systeme de respawn aux petits totems.
    public bool canInspire;

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
    public bool enableCornerGrab;
    public float cornerJumpMultiplier;
}
