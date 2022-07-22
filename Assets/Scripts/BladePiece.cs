using System.Collections;
using UnityEngine;

public class BladePiece : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;    
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private float _defaultBoxColliderX;

    private bool _isConnected;
    private Vector3 _randomDirection;
    private int _randomMin = -10;
    private int _randomMax = 10;    
    private float _scaleDecreaseFactor = 0.3f;
    private float _scaleIncreaseFactor = 1.35f;
    private float _destroyPause = 0.5f;

    public bool IsConnected => _isConnected;

    private void Awake()
    {
        _rigidBody.isKinematic = false;

        _isConnected = true;

        _randomDirection = new Vector3(Random.Range(_randomMin, _randomMax), Random.Range(_randomMin, _randomMax), Random.Range(_randomMin, _randomMax));
    }

    private void Update()
    {
        if (_isConnected)     
            ZeroPositionZ();
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.TryGetComponent<GroundSide>(out GroundSide SideGround))
        {
            gameObject.transform.SetParent(SideGround.transform);            

            DestroyConnectedBlade();
        }
    }

    public void MoveDown(float height)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - height, transform.localPosition.z);
    }
    
    public void StartChangeScale()
    {
        StartCoroutine(nameof(ChangeScale));
    }

    public void DestroyConnectedBlade()
    {
        if (_isConnected)
        {
            _isConnected = false;

            DefaultBoxColliderSizeX();

            _rigidBody.constraints = RigidbodyConstraints.None;

            _rigidBody.rotation = Random.rotation;
            
            _rigidBody.velocity = _randomDirection;
        }

        Invoke(nameof(Destroy), _destroyPause);
    }

    private void ZeroPositionZ()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
    }

    private IEnumerator ChangeScale()
    {
        var defaultScale = transform.localScale;

        var minScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z) * _scaleDecreaseFactor;

        while (defaultScale.x > minScale.x)
        {
            transform.localScale = minScale;

            minScale *= _scaleIncreaseFactor;

            yield return null;
        }

        transform.localScale = defaultScale;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void DefaultBoxColliderSizeX()
    {
        _boxCollider.size = new Vector3(_defaultBoxColliderX, _boxCollider.size.y, _boxCollider.size.z);
    }
}