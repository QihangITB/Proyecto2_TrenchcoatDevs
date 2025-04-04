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

        

    }
    void Update()
    {
        TrackCursorClientRpc();  
    }
    [ClientRpc]
    void TrackCursorClientRpc()
    {
        if (IsOwner)
        {
            Vector2 cursorPosition = Input.mousePosition;
            cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            transform.position = cursorPosition;
            Camera cam = Camera.main;
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hitTarget))
                {
                    HitFeedbackTestServerRpc();
                }
            }
        }
    }
    [ServerRpc]
    void AddToScoreServerRpc()
    {
        _minigameScore.Value++;
        UpdateScoreClientRpc();
    }
    [ClientRpc]
    void UpdateScoreClientRpc()
    {
        _scoreFeedback.text = _minigameScore.Value.ToString();
    }
    [ServerRpc]
    void HitFeedbackTestServerRpc()
    {
        Debug.Log($"Player with id {OwnerClientId} has hited something");
    }
    IEnumerator WaitForName()
    {
        yield return new WaitUntil(()=> !string.IsNullOrEmpty(playerName.Value.ToString()));
        _playerText.text = playerName.Value.ToString();
    }
}
