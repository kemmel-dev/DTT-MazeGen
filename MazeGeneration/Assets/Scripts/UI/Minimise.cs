using UnityEngine;

namespace UI
{
    public class Minimise : MonoBehaviour
    {
        [SerializeField] private Transform _scaleMinimised;
        [SerializeField] private Transform _scaleMaximised;
        [SerializeField] private float _scaleSpeed;
        private Vector3 ScaleDelta => _scaleMaximised.localScale - _scaleMinimised.localScale;
        public bool Minimised { get; set; }

        private float _scalePercentage = 1f;

        private void Update()
        {
            _scalePercentage = Mathf.Clamp01(_scalePercentage + (Minimised ? -_scaleSpeed : _scaleSpeed) * Time.deltaTime);
            transform.localScale = _scaleMinimised.localScale + ScaleDelta * _scalePercentage;
        
            if ( Minimised && gameObject.activeSelf && _scalePercentage < .0125f)
            {
                gameObject.SetActive(false);
            }
        }

        public void ToggleMinimized()
        {
            Minimised = !Minimised;
        }
    
    }
}
