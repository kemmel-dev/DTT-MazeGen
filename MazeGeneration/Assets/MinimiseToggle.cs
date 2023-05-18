using UnityEngine;

public class MinimiseToggle : MonoBehaviour
{
    public Minimise _objectToMinimise;
    public GameObject _verticalLineForMaximise;

    private bool _minimized;

    public void Toggle()
    {
        _minimized = !_minimized;
        _objectToMinimise.gameObject.SetActive(true);
        _objectToMinimise.Minimised = _minimized;
        _verticalLineForMaximise.SetActive(_minimized);
    }
}
