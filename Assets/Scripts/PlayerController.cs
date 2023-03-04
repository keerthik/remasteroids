using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;
using State;

public class PlayerController : Universal.SingletonBehaviour<PlayerController>, IFrameProcessor
{
    [SerializeField] PlayerControllerSettings settings;

    [SerializeField] Vector3 heading;
    [SerializeField] float speed = 0;
    [SerializeField] Vector3 velocity = Vector3.zero;
    [SerializeField] float angleVelocity = 0;
    [SerializeField] bool maxSpeed = false;
    [SerializeField] GameAction action;

    public LoadableUnit loading = new();

    void Awake() {
        base.Awake();
        heading = transform.forward;
        Debug.Log("Facing is " + heading);
    }

    void Start() {
        InputCoordinator.Shared.RegisterRecipient(this);
        loading.Loaded();
    }

    void Thrust(int forward) {
        // We remove some of our existing thrust and replace it with the user input
        velocity -= Universal.gameplayDeltaTime * settings.velocityReplacementFactor * settings.acceleration * velocity.normalized;
        velocity += forward * Universal.gameplayDeltaTime * settings.velocityReplacementFactor * settings.acceleration * heading;
        if ((forward * speed).LT(settings.maxSpeed)) {
            velocity += forward * Universal.gameplayDeltaTime * settings.acceleration * heading;
            maxSpeed = false;
        } else {
            maxSpeed = true;
        }
    }

    void Yaw(int direction) {
        if (direction < 0 && angleVelocity.GT(-settings.maxAngleVelocity)) angleVelocity -= Universal.gameplayDeltaTime * settings.angleAcceleration;
        if (direction > 0 && angleVelocity.LT(settings.maxAngleVelocity)) angleVelocity += Universal.gameplayDeltaTime * settings.angleAcceleration;
    }

    // Drift back towards all 0s
    void ReturnToNormal() {
        if (speed.LT(0)) velocity += Universal.gameplayDeltaTime * settings.deceleration * heading;
        if (speed.GT(0)) velocity -= Universal.gameplayDeltaTime * settings.deceleration * heading;
        if (angleVelocity.GT(0)) angleVelocity -= Universal.gameplayDeltaTime * settings.angleDeceleration;
        if (angleVelocity.LT(0)) angleVelocity += Universal.gameplayDeltaTime * settings.angleDeceleration;
    }

    void DrawDebug() {
        DrawVector.ForDebug(transform.position, velocity, Color.white);
    }

    void ManageMovement() {
        heading = transform.forward;
        ReturnToNormal();
        DrawDebug();
        Vector3 moveInSpace = Universal.gameplayDeltaTime * velocity;
        transform.Translate(moveInSpace, Space.World);
        transform.Rotate(transform.up, angleVelocity * Universal.gameplayDeltaTime);

        speed = velocity.sqrMagnitude * Vector3.Dot(velocity, heading).Sign();
    }

    public void ProcessInputFrame(InputFrame iframe) {
        if (iframe.HasNoGameInput) return;
        if (iframe.HasInput(GameAction.Accelerate)) {
            action = GameAction.Accelerate;
            Thrust(1);
        }
        if (iframe.HasInput(GameAction.Decelerate)) {
            action = GameAction.Decelerate;
            Thrust(-1);
        }
        if (iframe.HasInput(GameAction.TurnRight)) {
            action = GameAction.TurnRight;
            Yaw(1);
        }
        if (iframe.HasInput(GameAction.TurnLeft)) {
            action = GameAction.TurnLeft;
            Yaw(-1);
        }
        if (iframe.HasInput(GameAction.PauseMenu)) {
            action = GameAction.PauseMenu;
            PauseMenu.Shared.PauseAndSaveReplay();
        }
    }

    public LoadableUnit GetLoadableUnit() {
        return loading;
    }

    public void FixedUpdate() {
        ManageMovement();
    }
}
