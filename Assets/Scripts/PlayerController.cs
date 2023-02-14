using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PlayerController : MonoBehaviour, IFrameProcessor
{
    const float maxSpeed = 5f;
    const float acceleration = 1f;

    [SerializeField] Vector3 heading;
    [SerializeField] float velocity = 0;

    void Awake() {
        heading = transform.forward;
    }
    void Start() {
        InputCoordinator.Shared.RegisterRecipient(this);
    }

    void Forward() {
        if (velocity < maxSpeed) {
            Debug.Log($"We will speed up slightly! {velocity} < {maxSpeed}");
            velocity += acceleration;
        }
    }

    public void ProcessInputFrame(InputFrame iframe) {
        if (iframe.HasNoGameInput) return;
        if (iframe.HasInput(GameAction.Accelerate)) {
            Forward();
        }
    }

    public void FixedUpdate() {
        Vector3 moveInSpace = Universal.gameplayDeltaTime * velocity * new Vector3(heading.x, heading.y, 0);
        transform.Translate(moveInSpace, Space.World);
    }
}
