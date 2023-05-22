using System;
using Ghost;
using Maze.ContentsEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maze.Content
{
    [RequireComponent(typeof(MazeConfig))]
    public class AlterMazeContents : MonoBehaviour
    {
        [SerializeField] private GoButton _goButton;
        [SerializeField] private GhostObject _ghostObject;

        private MazeConfig _mazeConfig;
        private Camera _cam;
        private MazeObject _currentObjectType = MazeObject.None;
        
        public Transform Start { get; private set; }
        public Transform Finish { get; private set; }
        public Transform Key { get; private set; }
        private bool ActiveSelection => _currentObjectType != MazeObject.None;
        private bool AllPlaced => Start != null && Finish != null && Key != null;

        private Vector3 WorldPointUnderMouse
        {
            get
            {
                var worldPointUnderMouse = _cam.ScreenToWorldPoint(Input.mousePosition);
                worldPointUnderMouse.y = 0;
                return worldPointUnderMouse;
            }
        }


        private void Awake()
        {
            _cam = Camera.main;
            _mazeConfig = MazeConfig.Instance;
        }

        private void Update()
        {
            if (ActiveSelection && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceSelection();
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                ClearSelection();
            }
            
            if (!ActiveSelection) return;
            _ghostObject.SetPosition(WorldPointUnderMouse);
        }

        private void PlaceSelection()
        {
            var mazeParent = _mazeConfig.MazeBuilder.transform.GetChild(0);
            if (mazeParent == null)
                throw new NullReferenceException("Tried placing an object but there was no maze!");
            
            switch (_currentObjectType)
            {
                case MazeObject.None:
                    throw new NullReferenceException("Tried placing an object while selection was None.");
                case MazeObject.Start:
                    if (Start != null)
                        Destroy(Start.gameObject);
                    Start = Instantiate(_mazeConfig.StartGhostPrefab, WorldPointUnderMouse, Quaternion.identity, mazeParent).transform;
                    break;
                case MazeObject.Finish:
                    if (Finish != null)
                        Destroy(Finish.gameObject);
                    Finish = Instantiate(_mazeConfig.FinishGhostPrefab, WorldPointUnderMouse, Quaternion.identity, mazeParent).transform;
                    break;
                case MazeObject.Key:
                    if (Key != null)
                        Destroy(Finish.gameObject);
                    Key = Instantiate(_mazeConfig.KeyGhostPrefab, WorldPointUnderMouse, Quaternion.identity, mazeParent).transform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _goButton.RefreshEnabled(AllPlaced);
        }

        public void PickStart()
        {
            PickPrefab(MazeObject.Start, _mazeConfig.StartGhostPrefab);
        }

        public void PickFinish()
        {
            PickPrefab(MazeObject.Finish, _mazeConfig.FinishGhostPrefab);
        }

        public void PickKey()
        {
            PickPrefab(MazeObject.Key, _mazeConfig.KeyGhostPrefab);
        }

        private void PickPrefab(MazeObject objectType, GameObject prefab) 
        {
            _currentObjectType = objectType;
            _ghostObject.ChangeObject(prefab);
        }

        private void ClearSelection()
        {
            _currentObjectType = MazeObject.None;
            _ghostObject.ChangeObject(null);
            _ghostObject.GhostVisible = false;
        }
    }
}