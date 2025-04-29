using TMPro;
using Unity.Netcode;
using UnityEngine;

public class HostServerFormHandler : FormHandler
{
    [SerializeField]
    TMP_InputField _serverName;
    [SerializeField]
    HostClientDiscovery _discovery;
    [SerializeField]
    GameObject _startHostObject;
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
