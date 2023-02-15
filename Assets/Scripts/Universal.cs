using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tools and the like
public static class Universal
{
    public static float gameplayDeltaTime => Time.fixedDeltaTime;
    public static float uiDeltaTime => Time.fixedUnscaledDeltaTime;
    public static float frameTime => Time.fixedUnscaledTime;
    public static float realTime => Time.realtimeSinceStartup;

    public static bool GT(this float x, float y) {
        return x.CompareTo(y) > 0;
    }

    public static bool LT(this float x, float y) {
        return x.CompareTo(y) < 0;
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
