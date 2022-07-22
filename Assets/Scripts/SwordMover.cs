using UnityEngine;

public class SwordMover : MonoBehaviour
{    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _force;         
    [SerializeField] private float _maxVelocity;
    [SerializeField] private PhysicMaterial _frictionMaterial;
        
    private bool _isStopped = false;
    private float _maxFriction = 10000;

    private void Start()
    {
        _frictionMaterial.dynamicFriction = 0;

        _frictionMaterial.staticFriction = _frictionMaterial.dynamicFriction;

        _frictionMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.z < _maxVelocity && _isStopped == false)        
            _rigidbody.AddRelativeForce(Vector3.forward * _force * Time.fixedDeltaTime, ForceMode.VelocityChange);        

        if (_rigidbody.velocity.z < 0)
            _rigidbody.AddRelativeForce(Vector3.down * _force * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    
    public void Stop()
    {
        _isStopped = true;

        _frictionMaterial.dynamicFriction = _maxFriction;

        _frictionMaterial.staticFriction = _frictionMaterial.dynamicFriction;

        _frictionMaterial.frictionCombine = PhysicMaterialCombine.Maximum;

        _rigidbody.velocity = Vector3.zero;        
    }
}