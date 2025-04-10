using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAreaAttack : MonoBehaviour
{
    public GenericAreaAttack attack;

    public void SelectGenericAreaAttack()
    {
        BattleManager.instance.DeActivateTargetButtons();
        BattleManager.instance.areaAttack = attack;
        BattleManager.instance.areaAttack.ActivateTargetButtons();
    }
}
