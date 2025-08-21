using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Camera")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _cameraPoint;

    private Vector3 _velocity;
    private bool _jumpPressed;


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        // FixedUpdateNetwork is only executed on the StateAuthority

        if (_characterController.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }

        Vector3 horizontal = Input.GetAxis("Horizontal") * transform.right;
        Vector3 vertical = Input.GetAxis("Vertical") * transform.forward;
        Vector3 move = _moveSpeed * Runner.DeltaTime * (horizontal + vertical);
        move.y = 0;

        _velocity.y += _gravity * Runner.DeltaTime;
        if (_jumpPressed && _characterController.isGrounded)
        {
            _velocity.y += _jumpForce;
        }

        _characterController.Move(move + _velocity);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _jumpPressed = false;
    }

    // Make sure to always use Spawned instead of Awake/Start when initializing NetworkObjects. In Awake/Start the NetworkObject might not be ready to use yet.
    public override void Spawned()
    {
        // HasStateAuthority is only true for objects the player controls, so only the local player object and not other player objects.
        if (HasStateAuthority)
        {
            _camera = Camera.main;
            var fpCam = _camera.GetComponent<FirstPersonCamera>();
            fpCam.PlayerTransform = transform;
            fpCam.Pivot = _cameraPivot;
            fpCam.Target = _cameraPoint;
        }
    }
}
