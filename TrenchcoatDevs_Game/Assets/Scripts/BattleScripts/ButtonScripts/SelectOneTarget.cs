using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOneTarget : MonoBehaviour
{
    public CharacterHolder target;

    public void SelectTarget()
    {
        if(OnlineBattleManager.instance != null)
        {
            Debug.Log("Target selected");
            OnlineBattleManager.instance.targets.Clear();
            OnlineBattleManager.instance.targets.Add(target);
            OnlineBattleManager.instance.UseAttack();
        }
        else
        {
            Debug.Log("Target selected");
            BattleManager.instance.targets.Clear();
            BattleManager.instance.targets.Add(target);
            BattleManager.instance.UseAttack();
        }
        
    }
}
