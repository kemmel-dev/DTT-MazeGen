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
            var start = _mazeConfig.AlterMazeContents.Start.position;
            var finish = _mazeConfig.AlterMazeContents.Finish.position;
            var key = _mazeConfig.AlterMazeContents.Key.position;
            _ghostObject.ChangeObject(null);
            _mazeConfig.MazeBuilder.Build3DMaze();
            _mazeConfig.MazeBuilder.PlaceObjects(start, finish, key);
            _player.InitializePlayer(start);
            _mazeConfig.AlterMazeContents.enabled = false;
            _generationUI.SetActive(false);
            _editorUI.SetActive(false);
            _restartUI.SetActive(true);
            _hintUI.SetActive(false);
            _startedPlaying = true;
            
        }

        private void Update()
        {
            CheckForRestart();
        }

        private void CheckForRestart()
        {
            if (_startedPlaying && Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}