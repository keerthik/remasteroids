using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using State;

public class PlayerController : Universal.SingletonBehaviour<PlayerController>, IFrameProcessor
{
    [SerializeField] PlayerControllerSettings settings;

    [SerializeField] Vector3 heading;
    [SerializeField] float velocity = 0;

    void Awake() {
        base.Awake();
        heading = transform.forward;
    }
    void Start() {
        InputCoordinator.Shared.RegisterRecipient(this);
    }

    void SpeedUp() {
        if (velocity.LT(settings.maxSpeed)) {
            // Debug.Log($"We will speed up slightly! {velocity} < {settings.maxSpeed}");
            velocity += Universal.gameplayDeltaTime * settings.acceleration;
        }
    }

    void SlowDown(float decel) {
        if (velocity.GT(settings.minSpeed)) {
            Debug.Log($"We will slow down slightly! {velocity} > {settings.minSpeed}");
            velocity += Universal.gameplayDeltaTime * decel;
        }
        if (velocity.LT(0)) {
            // Reset towards 0 speed slowly (and at a fixed pace)
            velocity += -2f * Universal.gameplayDeltaTime * settings.deceleration;
        }
    }

    void ManageMovement() {
        SlowDown(settings.deceleration);
        Vector3 moveInSpace = Universal.gameplayDeltaTime * velocity * new Vector3(heading.x, heading.y, 0);
        transform.Translate(moveInSpace, Space.World);
    }

    public void ProcessInputFrame(InputFrame iframe) {
        if (iframe.HasNoGameInput) return;
        if (iframe.HasInput(GameAction.Accelerate)) {
            SpeedUp();
        }
        if (iframe.HasInput(GameAction.Decelerate)) {
            SlowDown(-settings.acceleration);
        }
    }


    public void FixedUpdate() {
        ManageMovement();
    }
}
