using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HostServerFormHandler : FormHandler
{
    [SerializeField]
    TMP_InputField _serverName;
    [SerializeField]
    HostClientDiscovery _discovery;
    [SerializeField]
    string nextSceneOnPlayerJoined;


    private void Start()
    {
        ChangeData();
    }
    public override void ChangeData()
    {
        _discovery.ServerName = _serverName.text;
    }

    public override void DoAction()
    {
        ChangeData();
        NetworkManager.Singleton.StartHost();
        _discovery.StartServer();
    }
    public void StopFinder()
    {
        NetworkManager.Singleton.Shutdown();
        _discovery.StopDiscovery();
    }
}
