using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInputs : MonoBehaviour
{
    [SerializeField]
    private ShipEngine _shipEngine;

    [SerializeField]
    private float _minInputThreshold = 0.1f;

    public void OnLinearInput(InputAction.CallbackContext context)
    {
        Vector3 direction = Vector3.zero;
        if(!context.canceled)
        {
            direction = ConvertVectorToDirection(context.ReadValue<Vector3>());
        }

        _shipEngine.LinearMovement(direction);
    }

    public void OnAngularInput(InputAction.CallbackContext context)
    {
        Vector3 direction = Vector3.zero;
        if(!context.canceled)
        {
            direction = ConvertVectorToDirection(context.ReadValue<Vector3>());
        }

        _shipEngine.AngularMovement(direction);
    }

    public void OnSwitchSpeedForce(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

    }

    public Vector3 ConvertVectorToDirection(Vector3 direction)
    {
        var finalDirection = Vector3.zero;
        if(direction.x > _minInputThreshold)
        {
            finalDirection.x += direction.x;
        }
        else if(direction.x < -_minInputThreshold)
        {
            finalDirection.x += direction.x;
        } else if(direction.z > _minInputThreshold)
        {
            finalDirection.z += direction.z;
        } else if(direction.z < -_minInputThreshold)
        {
            finalDirection.z += direction.z;
        } else if(direction.y > _minInputThreshold)
        {
            finalDirection.y += direction.y;
        } else if(direction.y < -_minInputThreshold)
        {
            finalDirection.y += direction.y;
        }

        return finalDirection;
    }
}