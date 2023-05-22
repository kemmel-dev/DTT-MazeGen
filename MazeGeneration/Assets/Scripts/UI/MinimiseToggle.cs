using UnityEngine;

namespace UI
{
    public class MinimiseToggle : MonoBehaviour
    {
        [SerializeField] private Minimise _objectToMinimise;
        [SerializeField] private GameObject _verticalLineForMaximise;

        private bool _minimized;

        public void Toggle()
        {
            _minimized = !_minimized;
            _objectToMinimise.gameObject.SetActive(true);
            _objectToMinimise.Minimised = _minimized;
            _verticalLineForMaximise.SetActive(_minimized);
        }
    }
}
