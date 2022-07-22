using UnityEngine;

public class SlicedObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer _leftPartRenderer;
    [SerializeField] private MeshRenderer _rightPartRenderer;
    [SerializeField] private Rigidbody _leftPartRigidbody;
    [SerializeField] private Rigidbody _rightPartRigidbody;    
    [SerializeField] private PreslicedObject _preslicedObject;
    [SerializeField] private bool _isLastMaterialChangeable;

    private Vector3 _leftPartVelocity = new Vector3(-1, 0, -0.5f);
    private Vector3 _rightPartVelocity = new Vector3(1, 0, -0.5f);
    private float _force = 25;

    private void Start()
    {
        if (_isLastMaterialChangeable)
        {
            _leftPartRenderer.materials = GetArrayWithNewLastMaterial(_leftPartRenderer.materials, _preslicedObject.Material);

            _rightPartRenderer.materials = GetArrayWithNewLastMaterial(_rightPartRenderer.materials, _preslicedObject.Material);
        }

        gameObject.SetActive(false);        
    }

    public void Activate()
    {
        gameObject.SetActive(true);

        var randomRot = Random.rotation;

        _rightPartRigidbody.rotation = randomRot;

        _leftPartRigidbody.velocity = _leftPartVelocity * _force;

        _rightPartRigidbody.velocity = _rightPartVelocity * _force;
    }    

    private Material [] GetArrayWithNewLastMaterial(Material [] materialArray, Material newMaterial)
    {
        Material[] tempArray = materialArray;

        tempArray[tempArray.Length - 1] = newMaterial;

        return tempArray;
    }
}