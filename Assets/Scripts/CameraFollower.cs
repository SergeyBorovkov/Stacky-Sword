using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
        
    private Vector3 _defaultTargetOffset;
    private Vector3 _currentTargetOffset;
    private Vector3 _zoomedTargetOffset;    
    private float _speed = 4;
    private float _xOffsetRatio = 0.4f;
    private float _zOffsetRatio = 0.2f;
    private bool _isZoomed;
    
    private void Start()
    {
        _defaultTargetOffset = new Vector3(15, 5, -5);

        _currentTargetOffset = _defaultTargetOffset;        

        _zoomedTargetOffset = new Vector3(9, 2, -3.5f);
    }

    private void Update()
    {
        if (_isZoomed == false)
        {
            Vector3 newPosition = Vector3.LerpUnclamped(transform.position, _target.position + 
                new Vector3(_currentTargetOffset.x + _target.position.y * _xOffsetRatio, _currentTargetOffset.y, _currentTargetOffset.z - _target.position.y * _zOffsetRatio),
                Time.deltaTime * _speed);

            transform.position = newPosition;            
        }
        else
        {
            var rotationDirection = _target.position - transform.position;
            
            var endRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * _speed);

            transform.position = Vector3.Lerp(transform.position, _target.position + _zoomedTargetOffset, Time.deltaTime * _speed);            
        }
    }

    public void Zoom()
    {
        _isZoomed = true;
    }
}