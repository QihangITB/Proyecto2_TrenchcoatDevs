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
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        NetworkObject instance = Instantiate(_playerPrefab);
        instance.Spawn(true);
    }
}
