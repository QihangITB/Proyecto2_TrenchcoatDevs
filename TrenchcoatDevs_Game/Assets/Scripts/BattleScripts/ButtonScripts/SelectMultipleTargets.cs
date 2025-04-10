using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMultipleTargets : MonoBehaviour
{
    public List<CharacterHolder> targets;

    public void SelectTargets()
    {
        Debug.Log("Target selected");
        BattleManager.instance.targets.Clear();
        foreach (CharacterHolder target in targets)
        {
            BattleManager.instance.targets.Add(target);
        }
        BattleManager.instance.UseAreaAttack();
    }
}
