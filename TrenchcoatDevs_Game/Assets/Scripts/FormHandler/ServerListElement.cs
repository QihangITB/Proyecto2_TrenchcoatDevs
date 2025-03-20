using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;

public class ServerListElement : FormHandler
{
    DiscoveryResponseData _responseData;
    IPAddress _hostIpAddress;
    [SerializeField]
    TMP_Text _hostServerNameText;
    [SerializeField]
    TMP_Text _hostIpAddressText;
    public override void ChangeData()
    {
        _hostServerNameText.text = _responseData.ServerName;
        _hostIpAddressText.text = _hostIpAddress.ToString();
    }
    public void ChangeData(IPAddress hostIP, DiscoveryResponseData responseData)
    {
        _hostIpAddress = hostIP;
        _responseData = responseData;
    }
    public override void DoAction()
    {
        UnityTransport transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.SetConnectionData(_hostIpAddress.ToString(), _responseData.Port);
        NetworkManager.Singleton.StartClient();
    }
}
