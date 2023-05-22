using Character;
using Ghost;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze
{
    public class PlayMaze : MonoBehaviour
    {

        [SerializeField] private GhostObject _ghostObject;
        [SerializeField] private Player _player;
        [Header("Toggle UI Elements")]
        [SerializeField] private GameObject _generationUI;
        [SerializeField] private GameObject _editorUI;
        [SerializeField] private GameObject _restartUI;
        [SerializeField] private GameObject _hintUI;

        private MazeConfig _mazeConfig;
        private bool _startedPlaying;

        private void Awake()
        {
            _mazeConfig = MazeConfig.Instance;
        }

        public void StartPlaying()
        {
            // Grab maze object positions from AlterMazeContents
            var start = _mazeConfig.AlterMazeContents.Start.position;
            var finish = _mazeConfig.AlterMazeContents.Finish.position;
            var key = _mazeConfig.AlterMazeContents.Key.position;
            


            
            // Build maze and place objects
            _mazeConfig.MazeBuilder.Build3DMaze();
            _mazeConfig.MazeBuilder.PlaceObjects(start, finish, key);
            
            // Initialization
            _player.InitializePlayer(start);
            
            // Disable required scripts and UI elements.
            _ghostObject.ChangeObject(null);
            _mazeConfig.AlterMazeContents.enabled = false;
            _generationUI.SetActive(false);
            _editorUI.SetActive(false);
            _restartUI.SetActive(true);
            _hintUI.SetActive(false);
            
            // Mark as started.
            _startedPlaying = true;
        }

        private void Update()
        {
            CheckForRestart();
        }

        /// <summary>
        /// Restarts when the "Escape" key is pressed.
        /// </summary>
        private void CheckForRestart()
        {
            if (_startedPlaying && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}