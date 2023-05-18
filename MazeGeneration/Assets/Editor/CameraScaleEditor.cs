﻿using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CameraScale))]
    public class CameraScaleEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Rescale"))
            {
                var cameraScale = (CameraScale)target;
                cameraScale.Rescale();
            }
        }
    }
}