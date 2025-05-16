using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAreaAttack : MonoBehaviour
{
    public GenericAreaAttack attack;

    public void SelectGenericAreaAttack()
    {
        if(OnlineBattleManager.instance != null)
        {
            OnlineBattleManager.instance.DeActivateTargetButtons();
            OnlineBattleManager.instance.areaAttack = attack;
            OnlineBattleManager.instance.areaAttack.ActivateTargetButtons();
        }
        else
        {
            BattleManager.instance.DeActivateTargetButtons();
            BattleManager.instance.areaAttack = attack;
            BattleManager.instance.areaAttack.ActivateTargetButtons();
        }
        
    }
}
