using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CharacterSyncManager : NetworkBehaviour
{
    private static Dictionary<ulong, CharacterSyncManager> _instances = new Dictionary<ulong, CharacterSyncManager>();
    [SerializeField]
    private FightAssetsIndexer _assetsIndexer;
    private APlayer _firstSlotChar;
    private APlayer _secondSlotChar;
    private APlayer _thirdSlotChar;
    private APlayer[] _playerIndexer;
    private NetworkVariable<APlayerNetStruct> _firstSlotSyncer = new NetworkVariable<APlayerNetStruct>(default,default,NetworkVariableWritePermission.Owner);
    private NetworkVariable<APlayerNetStruct> _secondSlotSyncer = new NetworkVariable<APlayerNetStruct>(default, default, NetworkVariableWritePermission.Owner);
    private NetworkVariable<APlayerNetStruct> _thirdSlotSyncer = new NetworkVariable<APlayerNetStruct>(default, default, NetworkVariableWritePermission.Owner);
    private NetworkVariable<APlayerNetStruct>[] _charSyncerIndexer;
    public static CharacterSyncManager OwnerInstance
    {
        get
        {
            try
            {
                ulong clientId = NetworkManager.Singleton.LocalClientId;
                return _instances[clientId];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
            
        }
    }
    public static CharacterSyncManager OtherUserInstance
    {
        get
        {
            try
            {
                CharacterSyncManager manager;
                ulong clientId = NetworkManager.Singleton.LocalClientId;
                manager = _instances.Where(obj => obj.Key != clientId).First().Value;
                return manager;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            
        }
    }
    public override void OnNetworkSpawn()
    {
        if (_instances.ContainsKey(OwnerClientId))
        {
            GetComponent<NetworkObject>().Despawn();
        }
        else 
        {
           
            _instances.Add(OwnerClientId, this);
            _playerIndexer = new APlayer[]
            {
                _firstSlotChar,
                _secondSlotChar,
                _thirdSlotChar,
            };
            _charSyncerIndexer = new NetworkVariable<APlayerNetStruct>[]
            {
                _firstSlotSyncer,
                _secondSlotSyncer,
                _thirdSlotSyncer,
            };
            
            foreach(NetworkVariable<APlayerNetStruct> netVar in _charSyncerIndexer)
            {
                netVar.OnValueChanged += (prev, next) =>
                {
                    Debug.Log($"Client {OwnerClientId} CharName = ${next.characterName}");
                    foreach(int index in next.passivesindexes)
                    {
                        Debug.Log($"Client {OwnerClientId} passive number = ${index}");
                    }
                    foreach(int index in next.attacksIndex)
                    {
                        Debug.Log($"Client {OwnerClientId} attack number = ${index}");
                    }
                };
            }
        }
    }
    
    public void SetCharSlot(int slot, APlayer character)
    {
        if (IsOwner)
        {
            _playerIndexer[slot] = character;
            _charSyncerIndexer[slot].Value = new APlayerNetStruct(character, _assetsIndexer);
        }
    }
    
    public void SetCharSlot(APlayer character)
    {
        if (IsOwner)
        {
            int slot = _playerIndexer.ToList().IndexOf(character);
            SetCharSlot(slot, character);
        }
        
    }
    public void SetOnChangeEvent(int slotEvent,NetworkVariable<APlayerNetStruct>.OnValueChangedDelegate onChange)
    {
        _charSyncerIndexer[slotEvent].OnValueChanged += onChange;
    }
    public void UnsetOnChangeEvent(int slotEvent, NetworkVariable<APlayerNetStruct>.OnValueChangedDelegate onChange)
    {
        _charSyncerIndexer[slotEvent].OnValueChanged -= onChange;
    }
}
