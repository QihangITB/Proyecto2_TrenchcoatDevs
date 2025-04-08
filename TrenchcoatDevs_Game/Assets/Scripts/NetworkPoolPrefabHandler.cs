using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPoolPrefabHandler<T> : INetworkPrefabInstanceHandler where T : MonoBehaviour
{
    IPoolable<T> _prefab;
    Stack<T> _pool;

    public NetworkPoolPrefabHandler(IPoolable<T> prefab, Stack<T> pool)
    {
        _prefab = prefab;
        _pool = pool;
    }
    public NetworkPoolPrefabHandler(IPoolable<T> prefab) : this(prefab,new Stack<T>())
    {}

    public void Destroy(NetworkObject networkObject)
    {
        networkObject.GetComponent<IPoolable<T>>().Return();
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        NetworkObject returnObject;
        try
        {
            T gameObject = _pool.Pop();

            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;

            returnObject = gameObject.GetComponent<NetworkObject>();
        }
        catch (InvalidOperationException)
        {
            //If stack is empty, do this
            returnObject = Instantiate(ownerClientId,position,rotation);
            
            IPoolable<T> poolable = returnObject.GetComponent<IPoolable<T>>();
            poolable.OriginPool = _pool; 
        }
        return returnObject;
    }
}
