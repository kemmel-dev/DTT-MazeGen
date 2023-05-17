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

            if (GUILayout.Button("Build maze"))
            {
                var mazeBuilder = (MazeBuilder)target;
                mazeBuilder.BuildMaze(mazeBuilder.GetComponent<MazeGenerator>().Generate());
            }
        }
    }
}