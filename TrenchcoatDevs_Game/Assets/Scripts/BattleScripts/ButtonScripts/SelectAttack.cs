using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAttack : MonoBehaviour
{
    public GenericAttack attack;

    public void SelectGenericAttack()
    {
        if(OnlineBattleManager.instance != null)
        {
            OnlineBattleManager.instance.DeActivateTargetButtons();
            OnlineBattleManager.instance.attack = attack;
            OnlineBattleManager.instance.attack.ActivateTargetButtons();
        }
        else
        {
            BattleManager.instance.DeActivateTargetButtons();
            BattleManager.instance.attack = attack;
            BattleManager.instance.attack.ActivateTargetButtons();
        }
        
    }
}
