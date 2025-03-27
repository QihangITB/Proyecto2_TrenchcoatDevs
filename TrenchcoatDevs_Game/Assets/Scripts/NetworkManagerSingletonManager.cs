using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class NetworkManagerSingletonManager : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(WaitForSingleton());
    }
    IEnumerator WaitForSingleton()
    {
        NetworkManager networkManager = GetComponent<NetworkManager>();
        yield return new WaitUntil(()=>NetworkManager.Singleton != null);
        if(networkManager != NetworkManager.Singleton)
        {
            Destroy(gameObject);
        }
    }
}
