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
        public float Time => time;
        public void AddGameInput(GameAction newInput) => gameInputs.Add(newInput);
        public void SetMenuInput(MenuAction newInput) => menuInput = newInput;
        public bool HasInput(GameAction toCheck) => gameInputs.Contains(toCheck);
        public InputFrame(float frametime) {
            time = frametime;
        }

        public string ToString() {
            string str = $"{time}";
            foreach (GameAction action in gameInputs) str += $":{(int)action}";
            return str;
        }

        public static InputFrame FromString(string data) {
            Queue<string> dataparts = new(data.Split(":"));
            float t = float.Parse(dataparts.Dequeue());
            InputFrame result = new(t);
            while (dataparts.Count > 0) {
                result.AddGameInput((GameAction)int.Parse(dataparts.Dequeue()));
            }
            return result;
        }
    }
}