using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SeedSync : NetworkBehaviour
{
    NetworkVariable<int> _seed = new NetworkVariable<int>();
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _seed.Value = unchecked((int)DateTime.Now.Ticks);
        }

    }
    public int GetSyncedSeed()
    {
        return _seed.Value;
    }
}
