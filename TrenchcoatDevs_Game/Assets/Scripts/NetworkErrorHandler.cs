using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
public class NetworkErrorHandler : MonoBehaviour
{
    const string ClientErrorMsg = "The client has disconected";
    const string HostErrorMsg = "The host has closed their session";
    NetworkManager _networkManager;
    [SerializeField]
    Canvas _errorCanvas;
    [SerializeField]
    ErrorWindow _errorWindow;
    [SerializeField]
    string _onErrorScene;
    private static NetworkErrorHandler _instance;

    public static NetworkErrorHandler Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
        
    }
    private void OnEnable()
    {
        _networkManager.OnClientDisconnectCallback += OnClientDisconect;
    }
    private void OnDisable()
    {
        _networkManager.OnClientDisconnectCallback -= OnClientDisconect;
    }
    void OnClientDisconect(ulong client)
    {
        string errorMsg = !_networkManager.IsHost ? HostErrorMsg : ClientErrorMsg;
        _errorCanvas.enabled = true;
        _errorWindow.ChangeData(errorMsg);
        _errorWindow.OnDoAction += ErrorHandling;
    }
    void ErrorHandling()
    {
        _errorCanvas.enabled =false;
        _errorWindow.OnDoAction -= ErrorHandling;
        SceneController.Instance.DestroyNetworkManager();
        SceneController.Instance.LoadScene(_onErrorScene);
        enabled = false;
    }
}
