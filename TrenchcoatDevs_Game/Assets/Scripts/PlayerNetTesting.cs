using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetTesting : MonoBehaviour
{
    [SerializeField]
    CharacterSyncManager _characterSyncPrefab;
    NetworkManager _netManager;
    [SerializeField]
    List<CharacterOutOfBattle> _outOfFightCharacters;

    private void Awake()
    {
        _netManager = NetworkManager.Singleton;
    }
    private void Start()
    {
        if (_netManager.IsServer)
        {
            foreach (ulong clientId in _netManager.ConnectedClients.Keys)
            {
                NetworkObject netObject = _characterSyncPrefab.GetComponent<NetworkObject>();
                _netManager.SpawnManager.InstantiateAndSpawn(netObject, clientId, true);
            }
            
        }
        StartCoroutine(WaitForInstance());

    }
    IEnumerator WaitForInstance()
    {
        yield return new WaitUntil(()=>CharacterSyncManager.OwnerInstance!=null);
        CharacterSyncManager syncer = CharacterSyncManager.OwnerInstance;
        for(int i = 0; i < _outOfFightCharacters.Count; i++)
        {
            syncer.SetCharSlot(i, _outOfFightCharacters[i].character);
        }
        
    }
}

