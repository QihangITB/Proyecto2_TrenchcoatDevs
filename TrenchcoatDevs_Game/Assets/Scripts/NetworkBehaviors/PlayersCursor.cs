using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayersCursor : NetworkBehaviour
{
    NetworkVariable<FixedString512Bytes> playerName = new NetworkVariable<FixedString512Bytes>(default,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    [SerializeField]
    SpriteRenderer _mouseSprite;
    [SerializeField]
    TMP_Text _playerText;
    private void OnNetworkInstantiate()
    {
        playerName.Value = new FixedString512Bytes(HostClientDiscovery.GetPlayerName());
        if (IsOwner)
        {
            _mouseSprite.enabled = false;
        }
        _playerText.text = playerName.Value.ToString();
    }
    void Update()
    {
        TrackCursorClientRpc();  
    }
    [ClientRpc]
    public void TrackCursorClientRpc()
    {
        if (IsOwner)
        {
            Vector2 cursorPosition = Input.mousePosition;
            cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
            transform.position = cursorPosition;
        }
    }
}
