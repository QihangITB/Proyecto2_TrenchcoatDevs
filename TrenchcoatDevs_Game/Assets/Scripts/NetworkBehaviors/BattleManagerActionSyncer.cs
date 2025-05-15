using System.Collections.Generic;
using Unity.Netcode;

public class BattleManagerActionSyncer : NetworkBehaviour
{
    private static BattleManagerActionSyncer _instance;
    private List<CharacterHolder> _characterHolders;
    public static BattleManagerActionSyncer Instance
    {
        get { return _instance; }
    }
    public List<CharacterHolder> CharacterHolders
    {
        private get { return _characterHolders; }
        set { _characterHolders = value; }
    }
    public override void OnNetworkSpawn()
    {
        if(Instance == null)
        {
            _instance = this;
        }
        else
        {
            GetComponent<NetworkObject>().Despawn();
        }
    }
    public void StartRoundOf(CharacterHolder character)
    {
        StartRoundOfClientRpc(CharacterHolders.IndexOf(character));
    }
    [ClientRpc(RequireOwnership = false)]
    private void StartRoundOfClientRpc(int characterIndex)
    {
        OnlineBattleManager.instance.StartTurn(CharacterHolders[characterIndex]);
    }
}
