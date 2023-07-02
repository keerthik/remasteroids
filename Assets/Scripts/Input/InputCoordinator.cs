using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EditorAux;

namespace Inputs {
    public class InputCoordinator : Universal.SingletonBehaviour<InputCoordinator>
    {
        public InputSource currentSource;
        [DrawIf("currentSource", 2, ComparisonType.Equals, DisablingType.ReadOnly)]
        public bool recording;
        public InputMapping controls;
        public ReplayData replay;
        // We are starting with a single player game, so a single frame should be sufficient
        private InputFrame currentFrame;
        private List<IFrameProcessor> gameInputRecipients;

        public LoadableUnit loading = new();
        // public float gameInputFrameDeltaTime => Universal.gameTime - currentFrame.Time;

        protected override void Awake() {
            base.Awake();
            gameInputRecipients = new();
            replay = new();
            if (InputSource.Replay == currentSource) replay.LoadData("test1");
            if (InputSource.Player == currentSource) replay.Prepare();
        }

        public void RegisterRecipient(IFrameProcessor recipient) {
            if (gameInputRecipients.Contains(recipient)) return;
            gameInputRecipients.Add(recipient);
        }

        void FixedUpdate() {
            if (CoreDirector.Shared.IsInGame) {
                CaptureDirectInputForPlayer1();
                DispatchRecordedInputForPlayer1();
            }
        }

        void Update() {
            // Check if loaded
            if (loading.IsLoaded) return;
            foreach (IFrameProcessor unit in gameInputRecipients) {
                loading.IsLoaded &= unit.GetLoadableUnit().IsLoaded;
            }
            if (replay.loading.IsLoaded) {
                loading.Loaded();
                Universal.gameStartTime = Universal.gameAbsoluteTime;
            }
            
        }
        void DispatchGameInputs(InputFrame frame) {
            foreach (IFrameProcessor recipient in gameInputRecipients) {
                recipient.ProcessInputFrame(frame);
            }
        }

        void CaptureDirectInputForPlayer1() {
            if (InputSource.Player != currentSource) return;
            currentFrame = new(Universal.gameTime);
            foreach (KeyValuePair<GameAction, KeyCode> kvp in controls.gameControls) {
                if (Input.GetKey(kvp.Value)) {
                    currentFrame.AddGameInput(kvp.Key);
                }
            }
            DispatchGameInputs(currentFrame);
            if (recording) replay.RecordInputForPlayer1(currentFrame);
        }

        void DispatchRecordedInputForPlayer1() {
            if (InputSource.Replay != currentSource) return;
            if (!replay.loading.IsLoaded) return;
            if (null == currentFrame) currentFrame = replay.GetNextInputFrame();
            if (!replay.HasMoreFrames) {
                Debug.Log("Replay has ended!");
                Debug.Break();
                return;
            }
            if ((currentFrame.Time - Universal.gameplayDeltaTime).LT(Universal.gameTime)) {
                DispatchGameInputs(currentFrame);
                // this frame is used, move on
                currentFrame = null;
            } else {
                // <--load next frame-->
            }
        }
    }
}