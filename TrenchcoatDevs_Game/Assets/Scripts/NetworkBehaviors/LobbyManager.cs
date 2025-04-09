using Unity.Netcode;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    NetworkObject _playerPrefab;
    NetworkManager _networkManager;
    private void Start()
    {
        _networkManager = NetworkManager.Singleton;
        if (_networkManager.IsServer)
        {
            SpawnPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void SpawnPlayer()
    {
        foreach(ulong client in _networkManager.ConnectedClients.Keys)
        {
            _networkManager.SpawnManager.InstantiateAndSpawn(_playerPrefab,client,true);
        }
    }
}
