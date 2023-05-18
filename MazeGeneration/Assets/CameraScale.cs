using UnityEngine;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    private float ScaleDelta => _maxScale - _minScale;

    [SerializeField] private MazeConfig _mazeConfig;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void Rescale()
    {
        var scale = Scale;
        var size = _minScale + ScaleDelta * scale.sizePercentage;
        _camera.orthographicSize = size;

        var cameraTransform = _camera.transform;
        cameraTransform.position = new Vector3(_mazeConfig.Size.x / 2f, cameraTransform.position.y, _mazeConfig.Size.y / 2f);
    }
    
    private (bool horizontalBias, float sizePercentage) Scale
    {
        get
        {
            var horizontalBias = _mazeConfig.Size.x >= _mazeConfig.Size.y;

            return horizontalBias ? 
                (true, SizePercentageOf(_mazeConfig.Size.x, _mazeConfig.SizeMin.x, _mazeConfig.SizeMax.x)) 
                : (false, SizePercentageOf(_mazeConfig.Size.y, _mazeConfig.SizeMin.y, _mazeConfig.SizeMax.y));
        }
    }

    private static float SizePercentageOf(float size, float sizeMin, float sizeMax)
    {
        var delta = sizeMax - sizeMin;
        return (size - sizeMin) / delta;
    }
}
