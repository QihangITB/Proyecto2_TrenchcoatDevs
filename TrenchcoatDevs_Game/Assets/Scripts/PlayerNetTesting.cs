using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetTesting : MonoBehaviour
{
    private static PlayerNetTesting _instance;
    [SerializeField]
    CharacterSyncManager _characterSyncPrefab;
    NetworkManager _netManager;
    List<CharacterOutOfBattle> _outOfFightCharacters;
    [SerializeField]
    List<CharacterOutOfBattle> _outOfFightCharactersHost;
    [SerializeField]
    List<CharacterOutOfBattle> _outOfFightCharactersClient;
    List<CharacterOutOfBattle> _otherOutOfFightChars;
    
    List<APlayer> _localPlayerChars;
    List<APlayer> _otherPlayerChars;

    public static PlayerNetTesting Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _netManager = NetworkManager.Singleton;
    }
    private void Start()
    {
        AllocateCharacters();
    }
    public void AllocateCharacters()
    {
        if (_netManager.IsHost)
        {
            _outOfFightCharacters = _outOfFightCharactersHost;
        }
        else
        {
            _outOfFightCharacters = _outOfFightCharactersClient;
        }
        if (_netManager.IsServer)
        {
            foreach (ulong clientId in _netManager.ConnectedClients.Keys)
            {
                NetworkObject netObject = _characterSyncPrefab.GetComponent<NetworkObject>();
                _netManager.SpawnManager.InstantiateAndSpawn(netObject, clientId, true);
            }
        }
        _localPlayerChars = new List<APlayer>();
        foreach(CharacterOutOfBattle outOfFightChar in _outOfFightCharacters)
        {
            _localPlayerChars.Add(outOfFightChar.character);
        }
        StartCoroutine(WaitForInstance());
        StartCoroutine(WaitForOtherPlayerInstance());
    }
    IEnumerator WaitForInstance()
    {
        yield return new WaitUntil(()=>CharacterSyncManager.OwnerInstance!=null);
        CharacterSyncManager syncer = CharacterSyncManager.OwnerInstance;
        for(int i = 0; i < _outOfFightCharacters.Count; i++)
        {
            syncer.SetCharSlot(i, _outOfFightCharacters[i]);
        }
        AttemptStartCombat();
        
    }
    IEnumerator WaitForOtherPlayerInstance()
    {
        yield return new WaitUntil(()=>CharacterSyncManager.OtherUserInstance!=null);
        CharacterSyncManager syncer = CharacterSyncManager.OtherUserInstance;
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        syncer.allSlotsFilled.AddListener(FillEnemySlots);
        syncer.AttemptEventInvoke();
    }
    private void AttemptStartCombat()
    {
        if(_localPlayerChars != null && _otherPlayerChars != null)
        {
            OnlineBattleManager.instance.CharacterAllocation(_localPlayerChars, _otherPlayerChars, _outOfFightCharacters,_otherOutOfFightChars);
        }
    }
    private void FillEnemySlots(CharacterOutOfBattle[] playersSO)
    {
        _otherOutOfFightChars = playersSO.ToList();
        _otherPlayerChars = new List<APlayer>();
        foreach (CharacterOutOfBattle outOfFight in _otherOutOfFightChars) 
        {
            _otherPlayerChars.Add(outOfFight.character);
        }
        CharacterSyncManager.OtherUserInstance.allSlotsFilled.RemoveListener(FillEnemySlots);
        AttemptStartCombat();
    }
}

