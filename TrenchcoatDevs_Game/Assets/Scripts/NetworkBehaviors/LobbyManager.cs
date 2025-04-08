using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    NetworkObject _playerPrefab;
    [SerializeField]
    ScorePointClickable _scoreClickablePrefab;
    [SerializeField]
    float _spawnEvery;
    [SerializeField]
    int _spawnLimit;
    Stack<ScorePointClickable> _pool = new Stack<ScorePointClickable>();
    NetworkManager _networkManager;
    private void Start()
    {
        _networkManager = NetworkManager.Singleton;
        _networkManager.PrefabHandler.AddHandler(_scoreClickablePrefab.gameObject, new NetworkPoolPrefabHandler<ScorePointClickable>(_scoreClickablePrefab, _pool));
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
    void StartSpawnManager()
    {
        NetworkManager netManager = NetworkManager.Singleton;
        
        StartCoroutine(SpawnManager());
    }
    IEnumerator SpawnManager()
    {
        NetworkObject spawnableObject = _scoreClickablePrefab.GetComponent<NetworkObject>();
        NetworkManager netManager = NetworkManager.Singleton;
        while (true)
        {
            yield return new WaitForSeconds(_spawnEvery);
            if (_pool.Count > 0)
            {
                _networkManager.SpawnManager.InstantiateAndSpawn(spawnableObject,default,true);
            }else if (_spawnLimit > 0)
            {
                spawnableObject.Spawn(true);
            }
        }
    }
}
