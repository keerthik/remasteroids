using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs {
    public class InputCoordinator : Universal.SingletonBehaviour<InputCoordinator>
    {
        public InputSource currentSource;
        public bool inGame;
        public bool recording;
        public InputMapping controls;
        // We are starting with a single player game, so a single frame should be sufficient
        private InputFrame currentFrame;
        private List<IFrameProcessor> gameInputRecipients;
        // public float gameInputFrameDeltaTime => Universal.gameTime - currentFrame.Time;

        protected override void Awake() {
            base.Awake();
            gameInputRecipients = new();
        }

        public void RegisterRecipient(IFrameProcessor recipient) {
            if (gameInputRecipients.Contains(recipient)) return;
            gameInputRecipients.Add(recipient);
        }

        void FixedUpdate() {
            if (inGame) {
                CaptureDirectInputForPlayer1();
                DispatchRecordedInputForPlayer1();
            }
        }

        void RecordInputForPlayer1() {
            // Save currentSource to file
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
            RecordInputForPlayer1();
        }

        void DispatchRecordedInputForPlayer1() {
            if (InputSource.Replay != currentSource) return;
            // <--Load next inputframe here-->
            currentFrame = new(Universal.gameTime); // not like this
            if ((currentFrame.Time - Universal.gameplayDeltaTime).LT(Universal.gameTime)) {
                DispatchGameInputs(currentFrame);
            } else {
                // <--load next frame-->
            }

        }
    }
}