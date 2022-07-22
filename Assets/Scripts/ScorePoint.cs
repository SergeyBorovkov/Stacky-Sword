using UnityEngine;

public class ScorePoint : MonoBehaviour
{
    [SerializeField] private int _randomMinX;
    [SerializeField] private int _randomMaxX;
    [SerializeField] private int _randomMinY;
    [SerializeField] private int _randomMaxY;
    [SerializeField] private float _speed;
    [SerializeField] private float _activeSeconds;

    private Vector2 _randomDirection;
    private int _maxHeight;
    private int _maxWidth;
    private Camera _camera;

    public bool IsActive { get; private set; }

    private void Start()
    {
        _camera = Camera.main;

        _maxHeight = _camera.pixelHeight;

        _maxWidth = _camera.pixelWidth;

        _randomDirection = new Vector2(Random.Range(_randomMinX, _randomMaxX), Random.Range(_randomMinY, _randomMaxY));

        Deactivate();
    }

    private void Update()
    {
        if (IsActive)        
            transform.Translate(_randomDirection * Time.deltaTime * _speed);        
    }

    public void Activate(Vector3 position)
    {
        Vector2 newPosition = new Vector2(position.x - _maxWidth * 0.5f, position.y - _maxHeight * 0.5f);

        transform.localPosition = newPosition;        

        gameObject.SetActive(true);

        IsActive = true;

        Invoke(nameof(Deactivate), _activeSeconds);
    }

    public void Deactivate()
    {
        ResetPosition();

        gameObject.SetActive(false);

        IsActive = false;
    }

    private void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }
}