using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectMultipleTargets : MonoBehaviour
{
    public List<CharacterHolder> targets;

    public void SelectTargets()
    {
        Debug.Log("Target selected");
        BattleManager.instance.targets.Clear();
        foreach (CharacterHolder target in targets)
        {
            if  (target.GetComponent<CharacterHolder>().character!=null)
            {
                BattleManager.instance.targets.Add(target);
            }
        }
        BattleManager.instance.UseAreaAttack();
    }
}
