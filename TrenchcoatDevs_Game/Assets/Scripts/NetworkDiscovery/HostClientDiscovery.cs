using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

public class HostClientDiscovery : NetworkDiscovery<DiscoveryBroadcastData, DiscoveryResponseData>
{
    [Serializable]
    public class ServerFoundEvent : UnityEvent<IPEndPoint, DiscoveryResponseData>
    {
    };

    NetworkManager m_NetworkManager;
    

    public string ServerName = "EnterName";

    public ServerFoundEvent OnServerFound;
    public UnityEvent OnClientConnected;
   

    public void Start()
    {
        if(NetworkManager.Singleton != null)
        {
            m_NetworkManager = NetworkManager.Singleton;
        }
        else
        {
            Destroy(this);
        }
        
    }

    public void Update()
    {
        if (m_NetworkManager.IsHost && m_NetworkManager.ConnectedClients.Count > 0)
        {
            OnClientConnected.Invoke();
        }
    }
    protected override bool ProcessBroadcast(IPEndPoint sender, DiscoveryBroadcastData broadCast, out DiscoveryResponseData response)
    {
        response = new DiscoveryResponseData()
        {
            ServerName = ServerName,
            Port = ((UnityTransport) m_NetworkManager.NetworkConfig.NetworkTransport).ConnectionData.Port,
        };
        return true;
    }

    protected override void ResponseReceived(IPEndPoint sender, DiscoveryResponseData response)
    {
        OnServerFound.Invoke(sender, response);
    }
}