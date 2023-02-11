using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float maxSpeed = 81f;
    Vector2 speed = new Vector2(0, 0);

    void MoveFrame(Vector2 direction) {
        speed += direction * Time.deltaTime;
    }

    void Update() {

    }
}
