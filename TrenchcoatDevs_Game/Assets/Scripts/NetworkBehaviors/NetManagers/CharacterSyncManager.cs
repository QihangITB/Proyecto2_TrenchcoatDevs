using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSyncManager : NetworkBehaviour
{
    private static Dictionary<ulong, CharacterSyncManager> _instances = new Dictionary<ulong, CharacterSyncManager>();
    [SerializeField]
    private FightAssetsIndexer _assetsIndexer;
    [SerializeField]
    private CharacterOutOfBattle _emptyOutOfBattle;
    private CharacterOutOfBattle _firstSlotChar;
    private CharacterOutOfBattle _secondSlotChar;
    private CharacterOutOfBattle _thirdSlotChar;
    private CharacterOutOfBattle[] _playerIndexer;
    private NetworkVariable<APlayerNetStruct> _firstSlotSyncer = new NetworkVariable<APlayerNetStruct>(default,default,NetworkVariableWritePermission.Owner);
    private NetworkVariable<APlayerNetStruct> _secondSlotSyncer = new NetworkVariable<APlayerNetStruct>(default, default, NetworkVariableWritePermission.Owner);
    private NetworkVariable<APlayerNetStruct> _thirdSlotSyncer = new NetworkVariable<APlayerNetStruct>(default, default, NetworkVariableWritePermission.Owner);
    private NetworkVariable<APlayerNetStruct>[] _charSyncerIndexer;
    public UnityEvent<CharacterOutOfBattle[]> allSlotsFilled;
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
    public int CharSlots
    {
        get
        {
            return _charSyncerIndexer.Length;
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
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            _instances.Add(OwnerClientId, this);
            _playerIndexer = new CharacterOutOfBattle[]
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

            for(int i = 0; i<_charSyncerIndexer.Length; i++)
            {
                
                int actualIndex = i;
                _charSyncerIndexer[i].OnValueChanged += (next, prev) =>
                {
                    AttemptEventInvoke();
                    
                };
            }
            AttemptEventInvoke();
        }
    }
    private void FillNewNetChar(int slot)
    {
        if (!IsOwner)
        {
            APlayer netChar = (APlayer)ScriptableObject.CreateInstance(_charSyncerIndexer[slot].Value.typeName.ToString());
            CharacterOutOfBattle outOfBattle = Instantiate(_emptyOutOfBattle);
            SetNetAPlayer(ref netChar, _charSyncerIndexer[slot].Value);
            outOfBattle.AddCharacter(netChar, _charSyncerIndexer[slot].Value.level);
            _playerIndexer[slot] = outOfBattle;
        }
        
    }
    private void InitializeAllPlayersSlots()
    {
        for(int i = 0; i < _charSyncerIndexer.Length; i++)
        {
            FillNewNetChar(i);
        }
    }
    private bool AreNetCharsInitialized()
    {
        foreach(NetworkVariable<APlayerNetStruct> playerChar in _charSyncerIndexer)
        {
            if (!playerChar.Value.initialized)
            {
                return false;
            }
        }
        return true;
    }
    public void AttemptEventInvoke()
    {
        if (AreNetCharsInitialized())
        {
            InitializeAllPlayersSlots();
            allSlotsFilled.Invoke(_playerIndexer);
        }
    }
    private void SetNetAPlayer(ref APlayer playChar, APlayerNetStruct data)
    {
        if (!data.isNull)
        {
            playChar.characterName = data.characterName.ToString();
            playChar.health = data.health;
            playChar.description = data.description;
            playChar.damage = data.damage;
            playChar.defense = data.defense;
            playChar.maxHealth = data.maxHealth;
            playChar.maxStamina = data.maxStamina;
            playChar.stamina = data.stamina;
            playChar.speed = data.speed;
            playChar.basicAttack = (GenericAttack)_assetsIndexer.GetAttack(data.basicAttackIndex);
            playChar.sprite = _assetsIndexer.GetSprite(data.spriteIndex);
            if (playChar.attacks == null)
            {
                playChar.attacks = new List<AAttack>();
            }
            if (playChar.passives == null)
            {
                playChar.passives = new List<APassive>();
            }
            foreach (int index in data.attacksIndex)
            {
                playChar.attacks.Add(_assetsIndexer.GetAttack(index));
            }
            foreach (int index in data.passivesindexes)
            {
                playChar.passives.Add(_assetsIndexer.GetPassive(index));
            }
        }
        else
        {
            playChar = null;
        }
        
        
    }
    public void SetCharSlot(int slot, CharacterOutOfBattle character)
    {
        if (IsOwner)
        {
            _playerIndexer[slot] = character;
            _charSyncerIndexer[slot].Value = new APlayerNetStruct(character, _assetsIndexer);
            AttemptEventInvoke();
        }
    }
}
