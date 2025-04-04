using System;
using System.Net;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ServerFinderList : ScrollViewHandler<ServerListElement>
{
    const string JoinError = "Could not connect to the server, it may be shutdown or full";
    [SerializeField]
    HostClientDiscovery _discovery;
    [SerializeField]
    TMP_Text _errorMessage;

    private void Start()
    {
        StartDiscovery();
    }
    private void OnEnable()
    {
        _discovery.OnServerFound.AddListener(OnServerFound);
    }
    private void OnDisable()
    {
        _discovery.OnServerFound.RemoveListener(OnServerFound);
    }
    private void OnDestroy()
    {
        try
        {
            NetworkManager.Singleton.OnClientStopped -= OnFailJoinAction;
        }
        catch (NullReferenceException)
        {

        }
        if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.GetComponent<NetworkErrorHandler>().enabled = true;
        }
    }
    void OnServerFound(IPEndPoint sender, DiscoveryResponseData response)
    {
        AddSectionToList(sender.Address, response);
    }
    void StartDiscovery()
    {
        _discovery.StartClient();
        _discovery.ClientBroadcast(new DiscoveryBroadcastData());
    }
    void DisableAllEvents()
    {
        foreach(ServerListElement row in ScrollViewElements)
        {
            row.DisableButton();
        }
    }
    void EnableAllEvents()
    {
        foreach (ServerListElement row in ScrollViewElements)
        {
            row.EnableButton();
        }
    }
    void OnJoinAction()
    {
        DisableAllEvents();
        _errorMessage.text = string.Empty;
        NetworkManager.Singleton.OnClientStopped += OnFailJoinAction;
    }
    void OnFailJoinAction(bool b)
    {
        EnableAllEvents();
        _errorMessage.text = JoinError;
        NetworkManager.Singleton.OnClientStopped -= OnFailJoinAction;
    }
    public void RefreshList()
    {
        EmptyList();
        StartDiscovery();
    }
    public ServerListElement AddSectionToList(IPAddress hostAddress, DiscoveryResponseData data)
    {
        ServerListElement newSection = AddSectionToList();
        newSection.ChangeData(hostAddress, data);
        newSection.AddAction(OnJoinAction);
        return newSection;
    }
}
