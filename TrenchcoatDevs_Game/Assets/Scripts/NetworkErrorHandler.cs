using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class NetworkErrorHandler : MonoBehaviour
{
    NetworkManager _networkManager;
    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
        
    }
    private void OnEnable()
    {
        _networkManager.OnClientDisconnectCallback += OnClientDisconect;
    }
    private void OnDisable()
    {
        _networkManager.OnClientDisconnectCallback -= OnClientDisconect;
    }
    private void Update()
    {
        if (_networkManager.IsClient && _networkManager.IsServer == false && _networkManager.IsConnectedClient == false)
        {
            OnHostDisconect();
        }
    }
    void OnClientDisconect(ulong client)
    {
        Debug.Log("A client has been disconected");
    }
    void OnHostDisconect()
    {
        Debug.Log("Host disconnected");
    }
}
