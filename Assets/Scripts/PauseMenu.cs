using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PauseMenu : Universal.SingletonBehaviour<PauseMenu>
{
    public void PauseAndSaveReplay() {
        if (InputCoordinator.Shared.recording) InputCoordinator.Shared.replay.SaveDataToDisk("test1");
    }
}
