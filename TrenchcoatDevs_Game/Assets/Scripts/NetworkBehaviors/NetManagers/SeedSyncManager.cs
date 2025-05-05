using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class SeedSyncManager : MonoBehaviour
{
    NetworkManager _networkManager;
    [SerializeField]
    SeedSync _seedSyncPrefab;
    private static SeedSync _seedSyncer;

    public static SeedSync SeedSyncer
    {
        get
        {
            return _seedSyncer;
        }
        private set
        {
            _seedSyncer = value;
        }
    }
    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
    }
    private void OnEnable()
    {
        _networkManager.OnServerStarted += OnServerStart;
        _networkManager.OnClientStarted += OnClientStart;
    }
    private void OnDisable()
    {
        _networkManager.OnServerStarted -= OnServerStart;
        _networkManager.OnClientStarted -= OnClientStart;
    }
    private void OnServerStart()
    {
        if(SeedSyncer == null)
        {
            NetworkObject netObject = _seedSyncPrefab.GetComponent<NetworkObject>();
            SeedSyncer = _networkManager.SpawnManager.InstantiateAndSpawn(netObject).GetComponent<SeedSync>();
        }
    }
    private void OnClientStart()
    {
        if (SeedSyncer == null)
        {
            StartCoroutine(WaitSyncerSpawn());
        }
    }
    private IEnumerator WaitSyncerSpawn()
    {
        SeedSync seedSyncer = GameObject.FindAnyObjectByType<SeedSync>();
        while (seedSyncer == null && _networkManager.IsClient)
        {
            yield return null;
            seedSyncer = GameObject.FindAnyObjectByType<SeedSync>();
        }
        if (_networkManager.IsClient)
        {
            SeedSyncer = seedSyncer;
        }
    }
}
