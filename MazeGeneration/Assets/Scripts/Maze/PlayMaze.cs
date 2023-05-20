using Maze.Builder;
using Maze.ContentsEditor;
using UnityEngine;

namespace Maze
{
    public class PlayMaze : MonoBehaviour
    {

        private Camera _camera;

        [SerializeField] private Player.Player _player;
        [SerializeField] private MazeBuilder _mazeBuilder;
        [SerializeField] private MazeContentsEditor _mazeContentsEditor;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void StartPlaying()
        {
            var start = _mazeContentsEditor.Start.position;
            var finish = _mazeContentsEditor.Finish.position;
            _mazeBuilder.Build3DMaze();
            MovePlayerTo(start);
            ActivatePlayer();
        }

        private void ActivatePlayer()
        {
            _player.gameObject.SetActive(true);
        }

        private void MovePlayerTo(Vector3 pos)
        {
            _player.transform.position = pos;
        }
    }
}