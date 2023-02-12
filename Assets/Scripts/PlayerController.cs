using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PlayerController : MonoBehaviour, IFrameProcessor
{
    const float maxSpeed = 81f;
    Vector2 heading = new(0, 0);


    void Accelerate() {
        if (heading.sqrMagnitude < maxSpeed) {
            Debug.Log("We will speed up slightly!");
        }
    }

    public void ProcessInputFrame(InputFrame iframe) {
        if (iframe.HasNoGameInput) return;
        if (iframe.HasInput(GameAction.Accelerate)) {
            Accelerate();
        }
    }
}
