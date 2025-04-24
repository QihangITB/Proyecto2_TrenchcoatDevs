using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SeedSync : NetworkBehaviour
{
    NetworkVariable<int> _seed = new NetworkVariable<int>();
    private void OnNetworkInstantiate()
    {
        if (IsServer)
        {
            _seed.Value = Convert.ToInt32(DateTime.Now.Ticks);
        }

    }
    public int GetSyncedSeed()
    {
        return _seed.Value;
    }
}
