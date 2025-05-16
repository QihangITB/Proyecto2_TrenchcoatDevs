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
        if(OnlineBattleManager.instance != null)
        {
            Debug.Log("Target selected");
            OnlineBattleManager.instance.targets.Clear();
            foreach (CharacterHolder target in targets)
            {
                if (target.GetComponent<CharacterHolder>().character != null)
                {
                    OnlineBattleManager.instance.targets.Add(target);
                }
            }
            OnlineBattleManager.instance.UseAreaAttack();
        }
        else
        {
            Debug.Log("Target selected");
            BattleManager.instance.targets.Clear();
            foreach (CharacterHolder target in targets)
            {
                if (target.GetComponent<CharacterHolder>().character != null)
                {
                    BattleManager.instance.targets.Add(target);
                }
            }
            BattleManager.instance.UseAreaAttack();
        }
        
    }
}
