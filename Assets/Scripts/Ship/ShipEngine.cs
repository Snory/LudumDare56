using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    private Vector3 _lastLinearDirection;
    private Vector3 _lastAngularDirection;

    [SerializeField]
    private float _currentLinearSpeedForce;

    [SerializeField]
    private float _currentAngularSpeedForce;

    [SerializeField]
    private float _linearDamping;

    [SerializeField]
    private float _angularDamping;

    [SerializeField]
    private float _maxLinearSpeed;

    [SerializeField]
    private float _maxAngularSpeed;

    public void Floating()
    {
        _rigidbody.linearDamping = _linearDamping;
    }

    public void LinearMovement(Vector3 direction)
    {
         _lastLinearDirection = direction;

        if (direction == Vector3.zero)
        {
            Floating();
        } else
        {
            Moving();
        }
    }

    private void Moving()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.linearDamping = 0f;
    }

    public void AngularMovement(Vector3 direction)
    {
        if(direction == Vector3.zero)
        {
            _lastAngularDirection = Vector3.zero;
            _rigidbody.angularDamping = _angularDamping;
            return;
        }
        _rigidbody.angularDamping = 0f;
        
        var finalDirection = Vector3.zero;
        if(direction.x > 0)
        {
            finalDirection+= transform.right;
        }

        if(direction.x < 0)
        {
            finalDirection+= -transform.right;
        }

        if(direction.y > 0)
        {
            finalDirection+= -transform.up;
        }

        if(direction.y < 0)
        {
            finalDirection+= transform.up;
        }

        if(direction.z > 0)
        {
            finalDirection+= transform.forward;
        }

        if(direction.z < 0)
        {
            finalDirection+= -transform.forward;
        }

        _lastAngularDirection = finalDirection;

    }

    private void Start()
    {
        _rigidbody.angularDamping = _angularDamping;
        _rigidbody.linearDamping = _linearDamping;
    }

    private void Update()
    {
        AngularInDirection();
        MoveInDirection();
    }

    private void MoveInDirection()
    {
        if (_lastLinearDirection == Vector3.zero) 
        {
            return;
        }

        var direction = transform.TransformDirection(_lastLinearDirection);
        _rigidbody.AddForce(direction * _currentLinearSpeedForce, ForceMode.Acceleration);

        if (_rigidbody.linearVelocity.magnitude > _maxLinearSpeed)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _maxLinearSpeed;
            return;
        }

    }

    private void AngularInDirection()
    {
        if (_lastAngularDirection == Vector3.zero)
        {
            return;
        }

        _rigidbody.AddTorque(_lastAngularDirection * _currentAngularSpeedForce, ForceMode.Acceleration);

        if (_rigidbody.angularVelocity.magnitude > _maxAngularSpeed)
        {
            _rigidbody.angularVelocity = _rigidbody.angularVelocity.normalized * _maxAngularSpeed;
            return;
        }

    }
}