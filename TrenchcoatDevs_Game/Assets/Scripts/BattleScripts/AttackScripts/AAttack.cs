using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class AAttack : ScriptableObject
{
    public string attackName;
    public string description;
    public int cost;
    public abstract void Effect(List<CharacterHolder> targets, CharacterHolder user);
    protected void PerformFinishTurn()
    {
        if (OnlineBattleManager.instance != null)
        {
            OnlineBattleManager.instance.FinishTurn();
        }
        else
        {
            BattleManager.instance.FinishTurn();
        }
    }
    protected bool IsOnlineBattle()
    {
        return OnlineBattleManager.instance != null;
    }
}
