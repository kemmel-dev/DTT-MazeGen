using System;
using System.Linq;
using Character;
using Ghost;
using Maze.Builder;
using Maze.ContentsEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze
{
    public class PlayMaze : MonoBehaviour
    {

        [SerializeField] private GhostObject _ghostObject;
        [SerializeField] private Player _player;
        [SerializeField] private MazeBuilder _mazeBuilder;
        [SerializeField] private MazeContentsEditor _mazeContentsEditor;

        [SerializeField] private GameObject _generationUI;
        [SerializeField] private GameObject _editorUI;
        [SerializeField] private GameObject _restartUI;
        [SerializeField] private GameObject _hintUI;
        
        private bool _startedPlaying;
        
        public void StartPlaying()
        {
            var start = _mazeContentsEditor.Start.position;
            var finish = _mazeContentsEditor.Finish.position;
            var key = _mazeContentsEditor.Key.position;
            _ghostObject.ChangeObject(null);
            _mazeBuilder.Build3DMaze();
            _mazeBuilder.PlaceObjects(start, finish, key);
            _player.InitializePlayer(start);
            _mazeContentsEditor.enabled = false;
            _generationUI.SetActive(false);
            _editorUI.SetActive(false);
            _restartUI.SetActive(true);
            _hintUI.SetActive(false);
            _startedPlaying = true;
            
        }

        private void Update()
        {
            if (_startedPlaying && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}