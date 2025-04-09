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
    NetworkManager _networkManager;
    private void Start()
    {
        _networkManager = NetworkManager.Singleton;
        if (_networkManager.IsServer)
        {
            SpawnPlayer();
            StartClickableSpawnManager();
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
    void StartClickableSpawnManager()
    {   
        StartCoroutine(SpawnManager());
    }
    IEnumerator SpawnManager()
    {
        NetworkObject spawnableObject = _scoreClickablePrefab.GetComponent<NetworkObject>();
        NetworkManager netManager = _networkManager;
        Stack<ScorePointClickable> pool = netManager.GetComponent<ScorePointPoolHandler>().GetStack(_scoreClickablePrefab);
        while (true)
        {
            yield return new WaitForSeconds(_spawnEvery);
            Camera cam = Camera.main;
            float spawnY = Random.Range
                (cam.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            float spawnX = Random.Range
                (cam.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
            Vector2 randomPosition = new Vector2(spawnX, spawnY);
            
            if (pool.Count > 0)
            {
                _networkManager.SpawnManager.InstantiateAndSpawn(spawnableObject,default,true,default,default,randomPosition);
            }else if (_spawnLimit > 0)
            {
                _networkManager.SpawnManager.InstantiateAndSpawn(spawnableObject, default, true, default, default, randomPosition);
                _spawnLimit--;
            }
        }
    }
}
