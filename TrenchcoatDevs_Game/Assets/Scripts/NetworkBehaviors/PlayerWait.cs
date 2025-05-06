using Unity.Netcode;
using UnityEngine.Events;
using System;
using UnityEngine;
using UnityEditor;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Collections;


public class PlayerWait : NetworkBehaviour
{
    private static PlayerWait _instance;
    private NetworkList<bool> _playersReady = new NetworkList<bool>(new bool[] {false,false});
    public UnityEvent OnAllPlayersReady;

    public static PlayerWait Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public override void OnNetworkSpawn()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            GetComponent<NetworkObject>().Despawn();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void MarkReadyServerRpc(ulong clientId)
    {
        _playersReady[((int)clientId)] = true;
        if (AreAllReady())
        {
            OnAllPlayersReady.Invoke();
            ReturnAllToUnready();
        }
    }
    private bool AreAllReady()
    {
        foreach (bool check in _playersReady)
        {
            if (!check)
            {
                return false;
            }
        }
        return true;
    }
    private void ReturnAllToUnready()
    {
        for (int i = 0; i < _playersReady.Count; i++) 
        {
            _playersReady[i] = false;
        }
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        _instance = null;
        try
        {
            _playersReady.Clear();
            _playersReady.Dispose();
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);

            //UnityEngine.Debug.Log(e);
        }
        
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        
        try
        {
            _playersReady.Clear();
            _playersReady.Dispose();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            //UnityEngine.Debug.Log(e);
        }
    }
    private void OnDisable()
    {
        try
        {
            _playersReady.Clear();
            _playersReady.Dispose();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            //UnityEngine.Debug.Log(e);
        }
    }
}
