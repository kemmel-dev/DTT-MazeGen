using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GoButton : MonoBehaviour
    {

        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button.interactable = false;
        }

        public bool Interactable
        { 
            set => _button.interactable = value;
        }

    }
}
