using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Inputs {
    public class InputFrame
    {
        float time;
        List<GameAction> gameInputs = new();
        MenuAction ?menuInput = null;
        public bool HasNoGameInput => gameInputs.Count == 0;

        public void AddGameInput(GameAction newInput) => gameInputs.Add(newInput);
        public void SetMenuInput(MenuAction newInput) => menuInput = newInput;
        public bool HasInput(GameAction toCheck) => gameInputs.Contains(toCheck);

    }
}