using System.Net;
using UnityEngine;

public class ServerFinderList : ScrollViewHandler<ServerListElement>
{
    [SerializeField]
    HostClientDiscovery _discovery;

    void Awake()
    {
        
    }
    void OnServerFound(IPEndPoint sender, DiscoveryResponseData response)
    {

    }
}
