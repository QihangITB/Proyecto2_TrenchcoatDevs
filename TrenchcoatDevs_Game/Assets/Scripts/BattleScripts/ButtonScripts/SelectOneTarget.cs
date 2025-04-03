using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOneTarget : MonoBehaviour
{
    public CharacterHolder target;

    public void SelectTarget()
    {
        Debug.Log("Target selected");
        BattleManager.instance.target = target;
        BattleManager.instance.UseAttack();
    }
}
