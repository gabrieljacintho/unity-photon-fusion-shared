using Fusion;
using UnityEngine;

public class RaycastAttack : NetworkBehaviour
{
    [SerializeField] private Transform _direction;
    [SerializeField] private float _damage = 10f;


    private void Update()
    {
        if (!HasStateAuthority)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new Ray(_direction.position, _direction.forward);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.TryGetComponent<Health>(out var health))
                {
                    health.DealDamageRpc(_damage);
                }
            }
        }
    }
}
