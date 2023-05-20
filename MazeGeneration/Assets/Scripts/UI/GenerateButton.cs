﻿using Maze;
using Maze.Builder;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GenerateButton : MonoBehaviour
    {

        [SerializeField] private Slider _stepTimeSlider;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;

        [SerializeField] private MazeBuilder _mazeBuilder;

        [SerializeField] private TMP_InputField _inputFieldX; 
        [SerializeField] private TMP_InputField _inputFieldY;

        [SerializeField] private Color _textColorEnabled;
        [SerializeField] private Color _textColorDisabled;

        private void Update()
        {
            var inputValid = ValidInput;
            _button.interactable = inputValid;
            _buttonText.color = inputValid ? _textColorEnabled : _textColorDisabled;
        }

        public void OnGenerate()
        {
            if (!ValidInput) return;
        
            var mazeConfig = _mazeBuilder.GetComponent<MazeConfig>();
            mazeConfig.StepTime = _stepTimeSlider.value;
            mazeConfig.Size = new Vector2Int(X, Y);
            _mazeBuilder.BuildMaze(mazeConfig.StepTime == 0);
        }

        public bool ValidInput => X > 0 && Y > 0;

        public int X
        {
            get
            {
                int x;
                if (int.TryParse(_inputFieldX.text, out x))
                    return x;
                return -1;
            }
        }
    
        public int Y
        {
            get
            {
                int y;
                if (int.TryParse(_inputFieldY.text, out y))
                    return y;
                return -1;
            }
        }
    }
}