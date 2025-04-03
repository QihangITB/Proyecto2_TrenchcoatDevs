using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAttack : MonoBehaviour
{
    public GenericAttack attack;

    public void SelectGenericAttack()
    {
        BattleManager.instance.attack = attack;
        BattleManager.instance.attack.ActivateTargetButtons();
    }
}
