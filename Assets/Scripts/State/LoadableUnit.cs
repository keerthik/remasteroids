using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadableUnit
{
    public bool IsLoaded = false;
    public float FinishedLoadingAt = -1;
    public void Loaded() {
        IsLoaded = true;
        FinishedLoadingAt = Universal.gameAbsoluteTime;
    }
}
