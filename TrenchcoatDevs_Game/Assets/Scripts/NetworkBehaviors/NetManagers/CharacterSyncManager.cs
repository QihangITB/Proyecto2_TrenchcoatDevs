using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;

public class CharacterSyncManager : NetworkBehaviour
{
    private static Dictionary<ulong, CharacterSyncManager> _instances;

    private FightAssetsIndexer _assetsIndexer;
    private APlayer _firstSlotChar;
    private APlayer _secondSlotChar;
    private APlayer _thirdSlotChar;
    private APlayer[] _playerIndexer;
    private NetworkVariable<APlayerNetStruct> _firstSlotSyncer = new NetworkVariable<APlayerNetStruct>();
    private NetworkVariable<APlayerNetStruct> _secondSlotSyncer = new NetworkVariable<APlayerNetStruct>();
    private NetworkVariable<APlayerNetStruct> _thirdSlotSyncer = new NetworkVariable<APlayerNetStruct>();
    private NetworkVariable<APlayerNetStruct>[] _charSyncerIndexer;
    public static CharacterSyncManager OwnerInstance
    {
        get
        {
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            return _instances[clientId];
        }
    }
    public static CharacterSyncManager OtherUserInstance
    {
        get
        {
            CharacterSyncManager manager;
            ulong clientId = NetworkManager.Singleton.LocalClientId;
            manager = _instances.Where(obj => obj.Key != clientId).First().Value;
            return manager;
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
        }
    }
    public void SetCharSlot(int slot, APlayer character)
    {
        _playerIndexer[slot] = character;
    }
}
