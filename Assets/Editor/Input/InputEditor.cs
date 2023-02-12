using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Inputs {
    public class InputEditor
    {
        [MenuItem("Assets/Create/Custom Input Mapping")]
        public static void CreateInputMapping() {
            InputMapping asset = ScriptableObject.CreateInstance<InputMapping>();
            string path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            if (string.IsNullOrWhiteSpace(path)) path = "Assets";
            path += "/InputMapping.asset";
            Debug.Log(path);

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}