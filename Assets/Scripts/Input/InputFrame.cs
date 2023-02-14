using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputs {
    // We are starting with a single player game, so a single frame represents a single player
    public class InputFrame
    {
        float time;
        // We assume any number of gameplay acctions could be done in one frame
        List<GameAction> gameInputs = new();
        // We assume only one menu action can be done in one frame
        MenuAction ?menuInput = null;
        public bool HasNoGameInput => gameInputs.Count == 0;

        public void AddGameInput(GameAction newInput) => gameInputs.Add(newInput);
        public void SetMenuInput(MenuAction newInput) => menuInput = newInput;
        public bool HasInput(GameAction toCheck) => gameInputs.Contains(toCheck);
        public InputFrame(float frametime) {
            time = frametime;
        }
    }
}