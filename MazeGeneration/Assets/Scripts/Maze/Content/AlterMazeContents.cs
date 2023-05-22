using System;
using Ghost;
using UI;
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
        
        /// <summary>
        /// Transform of placed Start object. Null if not placed yet.
        /// </summary>
        public Transform Start { get; private set; }
        
        /// <summary>
        /// Transform of placed Finish object. Null if not placed yet.
        /// </summary>
        public Transform Finish { get; private set; }
        
        /// <summary>
        /// Transform of placed Key object. Null if not placed yet.
        /// </summary>
        public Transform Key { get; private set; }
        
        /// <summary>
        /// Returns whether an object is currently selected to be placed.
        /// </summary>
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

        /// <summary>
        /// Place currently selected object.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when function is called without there being a maze.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when MazeObject enum value exceeds range.</exception>
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

        /// <summary>
        /// Picks the start prefab to be placed.
        /// Used by button events.
        /// </summary>
        public void PickStart()
        {
            PickPrefab(MazeObject.Start, _mazeConfig.StartGhostPrefab);
        }

        /// <summary>
        /// Picks the finish prefab to be placed.
        /// Used by button events.
        /// </summary>
        public void PickFinish()
        {
            PickPrefab(MazeObject.Finish, _mazeConfig.FinishGhostPrefab);
        }

        /// <summary>
        /// Picks the key prefab to be placed.
        /// Used by button events.
        /// </summary>
        public void PickKey()
        {
            PickPrefab(MazeObject.Key, _mazeConfig.KeyGhostPrefab);
        }

        /// <summary>
        /// Picks a prefab to be placed.
        /// </summary>
        /// <param name="objectType">The enum value corresponding to the type of object</param>
        /// <param name="prefab">The prefab that corresponds to the object</param>
        private void PickPrefab(MazeObject objectType, GameObject prefab) 
        {
            _currentObjectType = objectType;
            _ghostObject.ChangeObject(prefab);
        }
        
        /// <summary>
        /// Clears the active selection 
        /// </summary>
        private void ClearSelection()
        {
            _currentObjectType = MazeObject.None;
            _ghostObject.ChangeObject(null);
        }
    }
}