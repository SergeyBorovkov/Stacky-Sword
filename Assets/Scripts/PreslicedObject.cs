using UnityEngine;

public class PreslicedObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;    
    [SerializeField] private SlicedObject _slicedObject;
    [SerializeField] private Transform _precliced;
    [SerializeField] private bool _isChangingMaterial;
               
    private ScorePointShower _scoreShower;
    private Vector3 _screenPosition;
    private Camera _camera;
    private float _destoyPause = 3;

    public Material Material { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;

        _scoreShower = GameObject.FindObjectOfType<ScorePointShower>();
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BladePiece>(out BladePiece bladePiece))
        {
            if (_slicedObject != null)
            {
                _slicedObject.Activate();

                _screenPosition = _camera.WorldToScreenPoint(transform.position);

                _scoreShower.Show(_screenPosition);
            }

            if (_precliced != null)
            {
                _precliced.gameObject.SetActive(false);                
            }

            Invoke(nameof(Destroy), _destoyPause);
        }
    }

    public void ChangeMaterial(Material material)
    {
        if (_isChangingMaterial)        
            _meshRenderer.material = material;

        Material = material;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}