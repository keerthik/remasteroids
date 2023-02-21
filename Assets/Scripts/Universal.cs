using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tools and the like
public static class Universal
{
    public static float gameplayDeltaTime => Time.fixedDeltaTime;
    public static float uiDeltaTime => Time.fixedUnscaledDeltaTime;
    public static float menuTime => Time.fixedUnscaledTime;
    public static float realTime => Time.realtimeSinceStartup;
    public static float gameTime => Time.fixedTime;

    public static bool GT(this float x, float y) {
        return x.CompareTo(y) > 0;
    }

    public static bool LT(this float x, float y) {
        return x.CompareTo(y) < 0;
    }

    public static int Sign(this float x) {
        return x >= 0 ? 1 : -1;
    }

    public static Vector2 XZ(this Vector3 a) {
        return new(a.x, a.z);
    }

    public class SingletonBehaviour : MonoBehaviour {}

    public abstract class SingletonBehaviour<SingletonAssistant> : SingletonBehaviour
        where SingletonAssistant : SingletonBehaviour<SingletonAssistant> {
            private static SingletonAssistant instance = null;
            public static SingletonAssistant Shared {
                private set => instance = value;
                get => instance;
            }

            protected virtual void Awake() {
                instance = (SingletonAssistant)this;
            }
        }
}
