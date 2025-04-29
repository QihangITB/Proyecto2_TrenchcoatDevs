using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void NetworkLoadScene(string sceneName)
    {
        NetworkManager manager = NetworkManager.Singleton;
        if (manager.IsServer)
        {
            manager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
    }
    public void DestroyNetworkManager()
    {
        if(NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
