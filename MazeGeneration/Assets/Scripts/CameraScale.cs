using UnityEngine;

public class CameraScale : MonoBehaviour
{
    [SerializeField] private float _minScaleVertical;
    [SerializeField] private float _maxScaleVertical;
    [SerializeField] private float _minScaleHorizontal = 3;
    [SerializeField] private float _maxScaleHorizontal = 72;
    private float ScaleDeltaVertical => _maxScaleVertical - _minScaleVertical;
    private float ScaleDeltaHorizontal => _maxScaleHorizontal - _minScaleHorizontal;

    [SerializeField] private MazeConfig _mazeConfig;

    private Camera _camera;
    
    private Camera Camera
    {
        get
        {
            if (_camera != null) return _camera;
            _camera = GetComponent<Camera>();
            return _camera;

        }
    }

    public void Rescale()
    {
        var scale = Scale;
        
        var size = scale.horizontalBias
            ? _minScaleHorizontal + ScaleDeltaHorizontal * scale.sizePercentage
            : _minScaleVertical + ScaleDeltaVertical * scale.sizePercentage;
        Camera.orthographicSize = size;

        var cameraTransform = Camera.transform;
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
