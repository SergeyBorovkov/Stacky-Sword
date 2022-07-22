using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;        
        
    private Vector3 _defaultTargetOffset;
    private Vector3 _zoomedTargetOffset;    
    private float _speed = 4;
    private bool _isZoomed;
    
    private void Start()
    {
        _defaultTargetOffset = new Vector3(20, 1, -12);

        _zoomedTargetOffset = new Vector3(9, 2, -3.5f);
    }

    private void Update()
    {
        if (_isZoomed == false)
        {
            Vector3 newPosition = Vector3.LerpUnclamped(transform.position, _target.position + _defaultTargetOffset, Time.deltaTime * _speed);

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