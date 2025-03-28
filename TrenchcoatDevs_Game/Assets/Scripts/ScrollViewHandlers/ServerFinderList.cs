using System.Net;
using UnityEngine;

public class ServerFinderList : ScrollViewHandler<ServerListElement>
{
    [SerializeField]
    HostClientDiscovery _discovery;

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
            
        }
    }
    void EnableAllEvents()
    {

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
        return newSection;
    }
}
