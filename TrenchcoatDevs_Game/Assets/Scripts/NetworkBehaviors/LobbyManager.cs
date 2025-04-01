using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    NetworkVariable<FixedString512Bytes> _playerName = new NetworkVariable<FixedString512Bytes>(HostClientDiscovery.PlayerName,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    NetworkObject _playerTagPrefab;
    Transform _contentArea;
    private void OnNetworkInstantiate()
    {
        
    }
}
