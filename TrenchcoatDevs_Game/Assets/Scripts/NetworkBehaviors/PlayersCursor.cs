using System.Collections;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersCursor : NetworkBehaviour, INetworkPrefabInstanceHandler
{
    NetworkVariable<FixedString512Bytes> playerName = new NetworkVariable<FixedString512Bytes>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    NetworkVariable<uint> _minigameScore = new NetworkVariable<uint>();
    [SerializeField]
    SpriteRenderer _mouseSprite;
    [SerializeField]
    TMP_Text _playerText;
    [SerializeField]
    TMP_Text _scoreFeedback;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            playerName.Value = new FixedString512Bytes(HostClientDiscovery.GetPlayerName());
            _mouseSprite.enabled = false;
        }
        else
        {
            StartCoroutine(WaitForName());
        }
        _scoreFeedback.text = _minigameScore.Value.ToString();
        _minigameScore.OnValueChanged += (prevV, newV) => { _scoreFeedback.text = newV.ToString(); };
    }
    void Update()
    {
        TrackCursor();  
    }
    void TrackCursor()
    {
        if (IsOwner)
        {
            Vector2 cursorPosition = Input.mousePosition;
            cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            transform.position = cursorPosition;
            Camera cam = Camera.main;
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                HitFeedbackServerRpc(cam.ScreenPointToRay(Input.mousePosition));
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    void AddToScoreServerRpc(uint points)
    {
        _minigameScore.Value+=points;
    }
    [ServerRpc]
    void HitFeedbackServerRpc(Ray rayInfo)
    {
        if (Physics.Raycast(rayInfo, out RaycastHit hitTarget) && hitTarget.transform.TryGetComponent(out ScorePointClickable clickable))
        {
            AddToScoreServerRpc(clickable.Points);
        }
        
    }
    IEnumerator WaitForName()
    {
        yield return new WaitUntil(()=> !string.IsNullOrEmpty(playerName.Value.ToString()));
        _playerText.text = playerName.Value.ToString();
    }

    public NetworkObject Instantiate(ulong ownerClientId, Vector3 position, Quaternion rotation)
    {
        throw new System.NotImplementedException();
    }

    public void Destroy(NetworkObject networkObject)
    {
        throw new System.NotImplementedException();
    }
}
