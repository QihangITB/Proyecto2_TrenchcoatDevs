using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetButtonsTest : MonoBehaviour
{
    [SerializeField]
    private Button _startHost;
    [SerializeField]
    private Button _startClient;
    private void Start()
    {
        _startHost.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        _startClient.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        } );
    }
}
