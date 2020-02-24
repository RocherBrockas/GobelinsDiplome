using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalTypes
{
    //Matching layer names for readability purpose
    public enum GroundType
    {
        None,
        LevelGeometry,
        OneWayPlatform,
        MovingPlatform,
        Collapsable,
        JumpPad
    }

    public enum EffectorType
    {
        None,
        Ladder,
        TractorBeam,
        FloatZone
    }
}
