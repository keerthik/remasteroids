using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class SceneLoader : MonoBehaviour
{
    // Loads a file from disk for seeding this scene
    // Writes a file to disk when saving this scene

    private SceneData data;
    void Start()
    {  
        Debug.Log($"Scene loader started at time {Time.time}");
        
    }
}
