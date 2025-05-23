using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BattleManagerActionSyncer : NetworkBehaviour
{
    [SerializeField]
    private FightAssetsIndexer _assetsIndexer;
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
    public void UseSingleAttack(AAttack attack, List<CharacterHolder> targets, CharacterHolder user) 
    {
        int index = _assetsIndexer.GetAttackIndex(attack);
        List<int> targetsIndex = new List<int>();
        foreach (CharacterHolder target in targets)
        {
            targetsIndex.Add(_characterHolders.IndexOf(target));
        }
        int userIndex = _characterHolders.IndexOf(user);
        UseSingleAttackServerRpc(index, targetsIndex.ToArray(), userIndex);
    }
    public void UseAreaAttack(AAttack attack, List<CharacterHolder> targets, CharacterHolder user)
    {
        int index = _assetsIndexer.GetAttackIndex(attack);
        List<int> targetsIndex = new List<int>();
        foreach(CharacterHolder target in targets)
        {
            targetsIndex.Add(_characterHolders.IndexOf(target));
        }
        int userIndex = _characterHolders.IndexOf(user);
        UseAreaAttackServerRpc(index,targetsIndex.ToArray(),userIndex);
    }
    [ServerRpc(RequireOwnership = false)]
    private void StartRoundOfServerRpc(int characterIndex)
    {
        StartRoundOfClientRpc(characterIndex);
    }
    
    [ClientRpc(RequireOwnership = false)]
    private void StartRoundOfClientRpc(int characterIndex)
    {
        OnlineBattleManager.instance.StartTurn(CharacterHolders[characterIndex]);
    }
    [ServerRpc(RequireOwnership = false)]
    private void UseSingleAttackServerRpc(int attackIndex, int[] targetsIndex, int userIndex)
    {
        UseSingleAttackClientRpc(attackIndex, targetsIndex, userIndex);
    }
    [ClientRpc(RequireOwnership = false)]
    private void UseSingleAttackClientRpc(int attackIndex, int[] targetsIndex, int userIndex)
    {
        CharacterHolder user = _characterHolders[userIndex];
        AAttack attack = _assetsIndexer.GetAttack(attackIndex);
        List<CharacterHolder> targets = new List<CharacterHolder>();
        foreach (int target in targetsIndex)
        {
            targets.Add(_characterHolders[target]);
        }
        OnlineBattleManager.instance.attack = (GenericAttack)attack;
        OnlineBattleManager.instance.targets = targets;
        OnlineBattleManager.instance.user = user;
        OnlineBattleManager.instance.PerformAttack();
    }
    [ServerRpc(RequireOwnership = false)]
    private void UseAreaAttackServerRpc(int attackIndex, int[] targetsIndex, int userIndex)
    {
        UseAreaAttackClientRpc(attackIndex, targetsIndex, userIndex);
    }
    [ClientRpc(RequireOwnership = false)]
    private void UseAreaAttackClientRpc(int attackIndex, int[]targetsIndex, int userIndex)
    {
        CharacterHolder user = _characterHolders[userIndex];
        AAttack attack = _assetsIndexer.GetAttack(attackIndex);
        List<CharacterHolder> targets = new List<CharacterHolder>();
        foreach (int target in targetsIndex)
        {
            targets.Add(_characterHolders[target]);
        }
        OnlineBattleManager.instance.areaAttack = (GenericAreaAttack)attack;
        OnlineBattleManager.instance.targets = targets;
        OnlineBattleManager.instance.user = user;
        OnlineBattleManager.instance.PerformAreaAttack();
    }
}
