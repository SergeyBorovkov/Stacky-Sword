using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _garbageTemplate;    

    private Vector3 _randomVelocity;
    private float _minY = 8;
    private float _maxY = 16;
    private float _minZ = -6;
    private float _maxZ = -1;    

    private void Start()
    {
        _randomVelocity = new Vector3(0, Random.Range(_minY,_maxY), Random.Range(_minZ, _maxZ));
        
        Deactivate();        
    }

    public void Activate()
    {        
        _garbageTemplate.SetActive(true);

        _rigidbody.isKinematic = false;                

        _rigidbody.rotation = Random.rotation;

        _rigidbody.velocity = _randomVelocity;        

        Invoke(nameof(Deactivate), 1f);
    }

    public void Deactivate()
    {
        _garbageTemplate.SetActive(false);

        _rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BladePiece>())        
            Activate();        
    }    
}