using UnityEngine;

namespace UI
{
    public class Minimise : MonoBehaviour
    {
        [SerializeField] private Transform _scaleMinimised;
        [SerializeField] private Transform _scaleMaximised;
        [SerializeField] private float _scaleSpeed;
        private Vector3 ScaleDelta => _scaleMaximised.localScale - _scaleMinimised.localScale;
        private float _scalePercentage = 1f;
        
        public bool Minimised { get; set; }

        private void Update()
        {
            // Update scale
            _scalePercentage = Mathf.Clamp01(_scalePercentage + (Minimised ? -_scaleSpeed : _scaleSpeed) * Time.deltaTime);
            transform.localScale = _scaleMinimised.localScale + ScaleDelta * _scalePercentage;
        
            // Deactivate once minimised.
            if ( Minimised && gameObject.activeSelf && _scalePercentage < .0125f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
