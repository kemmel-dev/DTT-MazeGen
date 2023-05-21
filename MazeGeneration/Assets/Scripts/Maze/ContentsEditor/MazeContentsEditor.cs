using System;
using System.Collections.Generic;
using Ghost;
using Maze.Builder;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maze.ContentsEditor
{
    public class MazeContentsEditor : MonoBehaviour
    {

        [SerializeField] private GoButton _goButton;
        [SerializeField] private MazeBuilder _mazeBuilder;
        [SerializeField] private GameObject _startPrefab;
        [SerializeField] private GameObject _finishPrefab;
        [SerializeField] private GameObject _keyPrefab;
        [SerializeField] private GhostObject _ghostObject;

        public Transform Start { get; private set; }
        public Transform Finish { get; private set; }
        public List<Transform> Keys { get; private set; } = new ();
        public bool AllPlaced => Start != null && Finish != null && Keys.Count > 0;
        
        private Camera _cam;

        private MazeObject _currentObjectType = MazeObject.None;
        private bool ActiveSelection => _currentObjectType != MazeObject.None;
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
            var mazeParent = _mazeBuilder.transform.GetChild(0);
            if (mazeParent == null)
                throw new NullReferenceException("Tried placing an object but there was no maze!");
            
            switch (_currentObjectType)
            {
                case MazeObject.None:
                    throw new NullReferenceException("Tried placing an object while selection was None.");
                case MazeObject.Start:
                    if (Start != null)
                        Destroy(Start.gameObject);
                    Start = Instantiate(_startPrefab, WorldPointUnderMouse, Quaternion.identity, mazeParent).transform;
                    break;
                case MazeObject.Finish:
                    if (Finish != null)
                        Destroy(Finish.gameObject);
                    Finish = Instantiate(_finishPrefab, WorldPointUnderMouse, Quaternion.identity, mazeParent).transform;
                    break;
                case MazeObject.Key:
                    Keys.Add(Instantiate(_keyPrefab, WorldPointUnderMouse, Quaternion.identity, mazeParent).transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _goButton.RefreshEnabled(AllPlaced);
        }

        public void PickStart()
        {
            PickPrefab(MazeObject.Start, _startPrefab);
        }

        public void PickFinish()
        {
            PickPrefab(MazeObject.Finish, _finishPrefab);
        }

        public void PickKey()
        {
            PickPrefab(MazeObject.Key, _keyPrefab);
        }

        private void PickPrefab(MazeObject objectType, GameObject prefab) 
        {
            _currentObjectType = objectType;
            _ghostObject.ChangeObject(prefab);
        }

        public void ClearSelection()
        {
            _currentObjectType = MazeObject.None;
            _ghostObject.ChangeObject(null);
            _ghostObject.GhostVisible = false;
        }
    }
}