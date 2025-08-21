using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _playerPrefab;


    /*
     * When an object gets spawned with Runner.Spawn it gets automatically replicated to all other clients,
     * so it should only be called when PlayerJoined is called for the local player's join.
     * */
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Runner.Spawn(_playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
