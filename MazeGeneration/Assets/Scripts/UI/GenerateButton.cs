using System;
using Maze;
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
        [SerializeField] private TMP_InputField _inputFieldX; 
        [SerializeField] private TMP_InputField _inputFieldY;
        [SerializeField] private Color _textColorEnabled;
        [SerializeField] private Color _textColorDisabled;
        
        private MazeConfig _mazeConfig;

        private void Awake()
        {
            _mazeConfig = MazeConfig.Instance;
        }

        private void Update()
        {
            var inputValid = ValidInput;
            _button.interactable = inputValid;
            _buttonText.color = inputValid ? _textColorEnabled : _textColorDisabled;
        }

        /// <summary>
        /// Executes required actions when the generate button is clicked.
        /// </summary>
        public void OnGenerate()
        {
            if (!ValidInput) return;
        
            // Update maze config.
            var mazeConfig = _mazeConfig.MazeBuilder.GetComponent<MazeConfig>();
            mazeConfig.StepTime = _stepTimeSlider.value;
            mazeConfig.Size = new Vector2Int(X, Y);
            
            // Build the 2d maze.
            _mazeConfig.MazeBuilder.Build2DMaze(mazeConfig.StepTime == 0);
        }

        /// <summary>
        /// Returns true if X and Y field contain values that fit in the maze size.
        /// </summary>
        private bool ValidInput
        {
            get
            {
                var x = X;
                var y = Y;
                return x >= _mazeConfig.SizeMin.x && y >= _mazeConfig.SizeMin.y && x <= _mazeConfig.SizeMax.x && y <= _mazeConfig.SizeMax.y;
            }
        }

        /// <summary>
        /// Return the value of the X input field (or -1 if the contents cant be parsed to an int). 
        /// </summary>
        private int X
        {
            get
            {
                int x;
                if (int.TryParse(_inputFieldX.text, out x))
                    return x;
                return -1;
            }
        }
    
        /// <summary>
        /// Return the value of the Y input field (or -1 if the contents cant be parsed to an int). 
        /// </summary>
        private int Y
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
