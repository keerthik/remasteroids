using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PlayerController : MonoBehaviour, IFrameProcessor
{
    const float maxSpeed = 81f;
    Vector2 heading = new(0, 0);

    void Awake() {
        InputCoordinator.Shared.RegisterRecipient(this);
    }

    void Accelerate() {
        float speed = heading.sqrMagnitude;
        if (speed < maxSpeed) {
            Debug.Log($"We will speed up slightly! {speed} < {maxSpeed}");
        }
    }

    public void ProcessInputFrame(InputFrame iframe) {
        if (iframe.HasNoGameInput) return;
        if (iframe.HasInput(GameAction.Accelerate)) {
            Accelerate();
        }
    }
}
