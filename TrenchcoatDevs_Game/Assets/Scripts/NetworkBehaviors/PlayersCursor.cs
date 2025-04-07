using System.Collections;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersCursor : NetworkBehaviour
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
    [ServerRpc]
    void AddToScoreServerRpc()
    {
        _minigameScore.Value = _minigameScore.Value + 1;
        UpdateScoreClientRpc();
    }
    [ClientRpc]
    void UpdateScoreClientRpc()
    {
        _scoreFeedback.text = _minigameScore.Value.ToString();
    }
    [ServerRpc]
    void HitFeedbackServerRpc(Ray rayInfo)
    {
        Debug.Log($"Client {OwnerClientId} tried to all serverRpc");
        if (Physics.Raycast(rayInfo, out RaycastHit hitTarget))
        {
            AddToScoreServerRpc();
        }
        
    }
    IEnumerator WaitForName()
    {
        yield return new WaitUntil(()=> !string.IsNullOrEmpty(playerName.Value.ToString()));
        _playerText.text = playerName.Value.ToString();
    }
}
