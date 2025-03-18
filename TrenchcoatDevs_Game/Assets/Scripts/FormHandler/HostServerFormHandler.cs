using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class HostServerFormHandler : FormHandler
{
    [SerializeField]
    TMP_InputField _serverName;
    [SerializeField]
    HostClientDiscovery _discovery;

    public override void ChangeData()
    {
        _discovery.ServerName = _serverName.name;
    }

    public override void DoAction()
    {
        ChangeData();
        NetworkManager.Singleton.StartHost();
    }
}
