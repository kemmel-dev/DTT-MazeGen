using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(MazeConfig))]
    public class MazeConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var mazeConfig = (MazeConfig)target;

            if (GUILayout.Button("Randomize size"))
            {
                mazeConfig.Size = new Vector2Int(
                    Random.Range(mazeConfig.SizeMin.x, mazeConfig.SizeMax.x + 1),
                    Random.Range(mazeConfig.SizeMin.y, mazeConfig.SizeMax.y + 1)
                );

            }
        }
    }
}