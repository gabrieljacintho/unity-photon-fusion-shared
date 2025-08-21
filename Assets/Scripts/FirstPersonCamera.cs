using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _pivot;
    [SerializeField] private Transform _target;
    [SerializeField] private float _mouseSensitivity = 90f;

    private float _verticalRotation;
    private float _horizontalRotation;

    public Transform PlayerTransform
    {
        get => _playerTransform;
        set => _playerTransform = value;
    }
    public Transform Pivot
    {
        get => _pivot;
        set => _pivot = value;
    }
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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _verticalRotation -= mouseY * _mouseSensitivity * Time.deltaTime;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -30f, 30f);

        _horizontalRotation += mouseX * _mouseSensitivity * Time.deltaTime;

        _playerTransform.rotation = Quaternion.Euler(0f, _horizontalRotation, 0f);
        _pivot.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        transform.SetPositionAndRotation(_target.position, _target.rotation);
    }
}
