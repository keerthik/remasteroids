using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State {
    public class PlayerControllerSettings : ScriptableObject
    {
        public float version;

        public float maxSpeed;
        public float minSpeed;
        public float acceleration;
        public float deceleration;
    }
}