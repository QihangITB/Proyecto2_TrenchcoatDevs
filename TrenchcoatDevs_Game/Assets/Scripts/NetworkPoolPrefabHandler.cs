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
            T gameObject;
            do
            {
                gameObject = _pool.Pop();
            } while (gameObject == null);
            

            

            returnObject = gameObject.GetComponent<NetworkObject>();
            returnObject.gameObject.SetActive(true);
        }
        catch (InvalidOperationException)
        {
            //If stack is empty, do this
            returnObject = NetworkObject.Instantiate(_prefab.GetComponent().GetComponent<NetworkObject>());

            IPoolable<T> poolable = returnObject.GetComponent<IPoolable<T>>();
            poolable.OriginPool = _pool; 
        }
        returnObject.transform.position = position;
        returnObject.transform.rotation = rotation;
        return returnObject;
    }
}
