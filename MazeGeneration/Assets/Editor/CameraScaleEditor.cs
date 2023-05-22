using CameraTools;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CameraScaler))]
    public class CameraScaleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!GUILayout.Button("Rescale")) return;
            
            var cameraScale = (CameraScaler)target;
            cameraScale.Rescale();
        }
    }
}