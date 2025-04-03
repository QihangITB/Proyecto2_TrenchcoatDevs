using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NetworkButton : MonoBehaviour
{
    [SerializeField]
    bool _onlyHostCanInteract;
    private void Start()
    {
        Button interactable = GetComponent<Button>();
        if (_onlyHostCanInteract)
        {
            interactable.interactable = NetworkManager.Singleton.IsHost;
        }
    }
}
