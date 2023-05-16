using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MazeGen))]
    public class MazeGenEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Refresh maze"))
            {
                MazeGen mazeGen = (MazeGen)target;
                mazeGen.RefreshMaze();
            }
        }
    }
}