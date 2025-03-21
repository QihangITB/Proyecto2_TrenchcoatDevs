using System.Net;
using UnityEngine;

public class ServerFinderList : ScrollViewHandler<ServerListElement>
{
    [SerializeField]
    HostClientDiscovery _discovery;

    private void Start()
    {
        _discovery.StartClient();
        _discovery.ClientBroadcast(new DiscoveryBroadcastData());
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
    public ServerListElement AddSectionToList(IPAddress hostAddress, DiscoveryResponseData data)
    {
        ServerListElement newSection = AddSectionToList();
        newSection.ChangeData(hostAddress, data);
        return newSection;
    }
}
