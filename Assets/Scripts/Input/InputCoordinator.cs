using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs {
    public class InputCoordinator : Universal.SingletonBehaviour<InputCoordinator>
    {
        public InputSource currentSource;
        public bool inGame;
        public InputMapping controls;
        // We are starting with a single player game, so a single frame should be sufficient
        private InputFrame currentFrame;
        private List<IFrameProcessor> inputRecipients;

        protected override void Awake() {
            base.Awake();
            inputRecipients = new();
        }

        public void RegisterRecipient(IFrameProcessor recipient) {
            if (inputRecipients.Contains(recipient)) return;
            inputRecipients.Add(recipient);
        }

        void FixedUpdate() {
            currentFrame = new(Universal.frameTime);
            CaptureDirectInputForPlayer1();
        }

        void CaptureDirectInputForPlayer1() {
            if (InputSource.Player != currentSource) return;
            if (inGame) {
                foreach (KeyValuePair<GameAction, KeyCode> kvp in controls.gameControls) {
                    if (Input.GetKey(kvp.Value)) {
                       currentFrame.AddGameInput(kvp.Key);
                    }
                }
                foreach (IFrameProcessor recipient in inputRecipients) {
                    recipient.ProcessInputFrame(currentFrame);
                }
            }
        }
    }
}