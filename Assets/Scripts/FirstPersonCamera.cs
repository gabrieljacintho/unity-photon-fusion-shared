using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _mouseSensitivity = 10f;

    private float _verticalRotation;
    private float _horizontalRotation;

    public Transform Target
    {
        get => _target;
        set => _target = value;
    }


    private void LateUpdate()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = _target.position;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _verticalRotation -= mouseY * _mouseSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -70f, 70f);

        _horizontalRotation += mouseX * _mouseSensitivity;

        transform.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0f);
    }
}
