using Character;
using Maze.Builder;
using Maze.ContentsEditor;
using UnityEngine;

namespace Maze
{
    public class PlayMaze : MonoBehaviour
    {

        [SerializeField] private Player _player;
        [SerializeField] private MazeBuilder _mazeBuilder;
        [SerializeField] private MazeContentsEditor _mazeContentsEditor;

        public void StartPlaying()
        {
            var start = _mazeContentsEditor.Start.position;
            var finish = _mazeContentsEditor.Finish.position;
            _mazeBuilder.Build3DMaze();
            _player.InitializePlayer(start);
        }
    }
}