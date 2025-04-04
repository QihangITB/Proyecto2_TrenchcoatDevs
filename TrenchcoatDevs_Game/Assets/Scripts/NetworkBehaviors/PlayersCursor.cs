using System.Collections;
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
        }
    }
    IEnumerator WaitForName()
    {
        yield return new WaitUntil(()=> !string.IsNullOrEmpty(playerName.Value.ToString()));
        _playerText.text = playerName.Value.ToString();
    }
}
