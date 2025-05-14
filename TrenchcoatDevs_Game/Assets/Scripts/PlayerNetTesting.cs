using Unity.Netcode;
using UnityEngine;

public class PlayerNetTesting : MonoBehaviour
{
    [SerializeField]
    CharacterSyncManager _characterSyncPrefab;
    NetworkManager _netManager;

    private void Awake()
    {
        _netManager = NetworkManager.Singleton;
    }
    private void Start()
    {
        foreach (ulong clientId in _netManager.ConnectedClients.Keys)
        {
            _netManager.SpawnManager.InstantiateAndSpawn(_characterSyncPrefab.GetComponent<NetworkObject>(), clientId, true);
        }
    }
}

