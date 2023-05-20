using Character;
using Ghost;
using Maze.Builder;
using Maze.ContentsEditor;
using UnityEngine;

namespace Maze
{
    public class PlayMaze : MonoBehaviour
    {

        [SerializeField] private GhostObject _ghostObject;
        [SerializeField] private Player _player;
        [SerializeField] private MazeBuilder _mazeBuilder;
        [SerializeField] private MazeContentsEditor _mazeContentsEditor;

        public void StartPlaying()
        {
            var start = _mazeContentsEditor.Start.position;
            var finish = _mazeContentsEditor.Finish.position;
            _ghostObject.ChangeObject(null);
            _mazeBuilder.Build3DMaze();
            _player.InitializePlayer(start);
        }
    }
}