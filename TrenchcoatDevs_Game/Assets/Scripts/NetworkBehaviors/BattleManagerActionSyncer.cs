using Unity.Netcode;

public class BattleManagerActionSyncer : NetworkBehaviour
{
    private BattleManagerActionSyncer _instance;
    private CharacterHolder[] _characterHolders;
    public BattleManagerActionSyncer Instance
    {
        get { return _instance; }
    }
    public CharacterHolder[] CharacterHolders
    {
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
    public void StartRoundOf()
    {

    }
}
