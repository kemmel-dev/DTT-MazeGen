using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MazeBuilder))]
    public class MazeBuilderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mazeBuilder = (MazeBuilder)target;

            if (GUILayout.Button("Build maze"))
            {
                mazeBuilder.BuildMaze(true);
            }
        }
    }
}