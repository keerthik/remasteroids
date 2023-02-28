using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State {
    public class PlayerControllerSettings : ScriptableObject
    {
        public float gameVersion;

        public float velocityReplacementFactor;

        public float maxSpeed;
        public float acceleration;
        public float deceleration;
        
        public float maxAngleVelocity;
        public float angleAcceleration;
        public float angleDeceleration;
    }
}