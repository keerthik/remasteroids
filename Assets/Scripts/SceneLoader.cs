using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Loads a file from disk for seeding this scene
    // Writes a file to disk when saving this scene

    class SceneData {
        string sceneName;
        string gameVersion;
    }

    private SceneData data;
    void Start()
    {  
        Debug.Log($"Scene loader started at time {Time.time}");
        
    }
}
