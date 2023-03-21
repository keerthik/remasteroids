using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inputs;

public class PauseMenu : Universal.SingletonBehaviour<PauseMenu>, IFrameProcessor
{
    public LoadableUnit loading = new();
    public void PauseAndSaveReplay() {
        CoreDirector.Shared.IsInGame = false;
        // Graphics for showing menu
        if (InputCoordinator.Shared.recording) InputCoordinator.Shared.replay.SaveDataToDisk("test1");
    }

    public void ProcessInputFrame(InputFrame iframe) {
        if (iframe.HasNoMenuInput) return;
        if (iframe.HasInput(MenuAction.DismissMenu)) {
            
            CoreDirector.Shared.IsInGame = true;
        }
    }

    public LoadableUnit GetLoadableUnit() {
        return loading;
    }
}
