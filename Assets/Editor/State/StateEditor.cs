using System.Collections;
using UnityEditor;
using UnityEngine;

namespace State {
    public class StateEditor
    {
        [MenuItem("Assets/Create/Player Controller Settings")]
        public static void CreatePlayerControllerSettings() {
            PlayerControllerSettings asset = ScriptableObject.CreateInstance<PlayerControllerSettings>();
            string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            if (string.IsNullOrWhiteSpace(path)) path = "Assets";
            path += "/PlayerControllerSettings.asset";
            Debug.Log(path);

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}