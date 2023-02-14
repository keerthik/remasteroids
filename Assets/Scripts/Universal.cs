using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universal
{
    public static float gameplayDeltaTime => Time.fixedDeltaTime;
    public static float uiDeltaTime => Time.fixedUnscaledDeltaTime;
    public static float frameTime => Time.fixedUnscaledTime;
    public static float realTime => Time.realtimeSinceStartup;

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
