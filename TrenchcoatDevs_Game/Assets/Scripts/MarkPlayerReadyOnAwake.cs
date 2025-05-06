using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MarkPlayerReadyOnAwake : MonoBehaviour
{
    private void Start()
    {
        NetworkManager netManager = NetworkManager.Singleton;
        if(netManager.IsClient || netManager.IsHost)
        {
            PlayerWait.Instance.MarkReadyServerRpc(netManager.LocalClientId);
        }
        
    }
}
