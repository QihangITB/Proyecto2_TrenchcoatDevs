using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public abstract class PrefabPoolHandlerInstantiator<TBehavior> : MonoBehaviour where TBehavior : MonoBehaviour
{
    [SerializeField]
    List<TBehavior> _netPrefabs;
    Dictionary<TBehavior, Stack<TBehavior>> _poolList = new Dictionary<TBehavior, Stack<TBehavior>>();
    NetworkManager _networkManager;

    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
    }
    private void Start()
    {
        foreach(TBehavior net in _netPrefabs)
        {
            _poolList.Add(net,new Stack<TBehavior>());
            _networkManager.PrefabHandler.AddHandler(net.gameObject, new NetworkPoolPrefabHandler<TBehavior>(net.GetComponent<IPoolable<TBehavior>>(), _poolList[net]));
        }
    }
    public Stack<TBehavior> GetStack(TBehavior prefab)
    {
        return _poolList[prefab];
    }
}
