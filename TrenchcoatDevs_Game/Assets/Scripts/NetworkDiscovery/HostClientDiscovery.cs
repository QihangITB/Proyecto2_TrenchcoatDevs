using System;
using System.Collections.Generic;
using System.Net;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.Events;

public class HostClientDiscovery : NetworkDiscovery<DiscoveryBroadcastData, DiscoveryResponseData>
{
    const string DefaultPlayerName = "Player";
    [Serializable]
    public class ServerFoundEvent : UnityEvent<IPEndPoint, DiscoveryResponseData>
    {
    };

    NetworkManager m_NetworkManager;

    private static string _playerName;
    public string ServerName = "EnterName";

    public ServerFoundEvent OnServerFound;
    public UnityEvent OnClientConnected;

    public static string PlayerName
    {
        get
        {
            if (_playerName == null)
            {
                if (PlayerPrefs.HasKey(nameof(PlayerName)))
                {
                    PlayerName = PlayerPrefs.GetString(nameof(PlayerName));
                }
                else
                {
                    PlayerName = DefaultPlayerName;
                }
            }
            return _playerName;
        }
        private set
        {
            _playerName = value;
        }
    }

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
    int conectedClients = 0;

    public void Update()
    {
        
        
        if (m_NetworkManager.IsHost && m_NetworkManager.ConnectedClients.Count > 0)
        {
            if(conectedClients != m_NetworkManager.ConnectedClients.Count)
            {
                Debug.Log(m_NetworkManager.ConnectedClients.Count);
                foreach (KeyValuePair<ulong,NetworkClient> client in m_NetworkManager.ConnectedClients)
                {
                    Debug.Log($"Connected client: {client.Key}");
                }
            }
            conectedClients = m_NetworkManager.ConnectedClients.Count;
            //OnClientConnected.Invoke();
        }
        
    }
    protected override bool ProcessBroadcast(IPEndPoint sender, DiscoveryBroadcastData broadCast, out DiscoveryResponseData response)
    {
        response = new DiscoveryResponseData()
        {
            ServerName = ServerName,
            Port = ((UnityTransport)m_NetworkManager.NetworkConfig.NetworkTransport).ConnectionData.Port,
            playerName = PlayerName
        };
        return true;
    }
    public static void ChangePlayerName(string newName)
    {
        PlayerName = newName;
        PlayerPrefs.SetString(nameof(PlayerName),newName);
        PlayerPrefs.Save();
    }
    protected override void ResponseReceived(IPEndPoint sender, DiscoveryResponseData response)
    {
        OnServerFound.Invoke(sender, response);
    }
}