using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inputs {
    // These may turn into their own classes in the future to support parametrization
    public enum GameAction {
        Accelerate  = 1 << 0,
        Decelerate  = 1 << 1,
        TurnLeft    = 1 << 2,
        TurnRight   = 1 << 3,
        FireWeapon  = 1 << 4,
        PauseMenu   = 1 << 5,
    }
    public enum MenuAction {
        SelectUp,
        SelectDown,
        SelectEnter,
        DismissMenu,
    }

    public enum InputSource {
        Player,
        Replay,
    }

    public class InputMapping : ScriptableObject
    {
        // The value could be lists!
        // Also consider the Input.GetAxis controls
        public Dictionary<GameAction, KeyCode> gameControls = new(){
            {GameAction.Accelerate, KeyCode.UpArrow},
            {GameAction.Decelerate, KeyCode.DownArrow},
            {GameAction.TurnLeft, KeyCode.LeftArrow},
            {GameAction.TurnRight, KeyCode.RightArrow},
            {GameAction.FireWeapon, KeyCode.Space},
            {GameAction.PauseMenu, KeyCode.Escape},
        };

        public Dictionary<MenuAction, KeyCode> menuControls = new(){
            {MenuAction.SelectUp, KeyCode.UpArrow},
            {MenuAction.SelectDown, KeyCode.DownArrow},
            {MenuAction.SelectEnter, KeyCode.Return},
            {MenuAction.DismissMenu, KeyCode.Escape},
        };
    }
}
