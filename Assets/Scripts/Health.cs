using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(HealthChanged))]
    public float NetworkedHealth { get; set; } = 100f;


    /*
     * RpcSources.All allows anyone to call the RPC. By default, only the InputAuthority (same as StateAuthority in Shared mode) can call an RPC.
     * 
     * Asking the State Authority to modify Networked Properties is the most common use case for RPCs.
     */
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(float damage)
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        NetworkedHealth -= damage;
    }

    private void HealthChanged()
    {
        Debug.Log($"Health changed to: {NetworkedHealth}");
    }
}
