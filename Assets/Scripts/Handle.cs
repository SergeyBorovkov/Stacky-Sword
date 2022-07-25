using System;
using UnityEngine;

public class Handle : MonoBehaviour
{
    public event Action IsHitObstacle;
    public event Action IsGrounded;
    public float Height;
    public float LocalHeight;


    private bool _isHitted;


    private void Start()
    {
        Height = transform.position.y;

        LocalHeight = transform.localPosition.y;
    }

    private void Update()
    {
        Height = transform.position.y;

        LocalHeight = transform.localPosition.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UpObstacle>())
        {
            IsHitObstacle?.Invoke();
            _isHitted = true;            
        }

        if ((other.GetComponent<BaseGround>() || other.GetComponent<LowObstacle>() || other.GetComponent<MiddleObstacle>()) && _isHitted)
        {
            IsGrounded?.Invoke();            
        }
    }    

    public void MoveUp (float distance)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
    }
}