using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class ServerListElement : FormHandler
{
    public event Action OnConectionAttempted;
    DiscoveryResponseData _responseData;
    IPAddress _hostIpAddress;
    [SerializeField]
    TMP_Text _hostServerNameText;
    [SerializeField]
    TMP_Text _hostIpAddressText;
    [SerializeField]
    TMP_Text _playerNameText;
    [SerializeField]
    Button _joinButton;

    private Button JoinButton
    {
        get
        {
            return _joinButton;
        }
        set
        {
            _joinButton = value;
        }
    }
    public override void ChangeData()
    {
        _hostServerNameText.text = _responseData.ServerName;
        _hostIpAddressText.text = _hostIpAddress.ToString();
        _playerNameText.text = _responseData.playerName;
    }
    public void ChangeData(IPAddress hostIP, DiscoveryResponseData responseData)
    {
        _hostIpAddress = hostIP;
        _responseData = responseData;
        ChangeData();
    }
    public override void DoAction()
    {
        UnityTransport transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.SetConnectionData(_hostIpAddress.ToString(), _responseData.Port);
        NetworkManager.Singleton.StartClient();
        OnConectionAttempted?.Invoke();
    }
    public void AddAction(UnityAction action)
    {
        JoinButton.onClick.AddListener(action);
    }
    public void DisableButton()
    {
        JoinButton.interactable = false;
    }
    public void EnableButton()
    {
        JoinButton.interactable = true;
    }
}
