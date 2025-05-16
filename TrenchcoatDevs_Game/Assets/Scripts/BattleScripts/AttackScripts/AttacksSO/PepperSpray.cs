using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PepperSpray", menuName = "Attacks/PepperSpray")]

public class PepperSpray : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle())
        {
            foreach (GameObject button in OnlineBattleManager.instance.enemyButtons)
            {
                if (button.GetComponent<CharacterHolder>().character != null)
                {
                    button.GetComponent<Image>().enabled = true;
                }
            }
        }
        else
        {
            foreach (GameObject button in BattleManager.instance.enemyButtons)
            {
                if (button.GetComponent<CharacterHolder>().character != null)
                {
                    button.GetComponent<Image>().enabled = true;
                }
            }
        }
        
    }
    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            PerformFinishTurn();
            return;
        }
        foreach (CharacterHolder target in targets)
        {
            if (target.precisionModifier - 2 <= 5) 
            {
                target.precisionModifier = 5;
                Debug.Log(target.gameObject + "'s precision can't get lower");
            }
            else
            {
                target.precisionModifier -= 2;
                Debug.Log(target.gameObject + "'s precision has gone down");
            }
        }
        PerformFinishTurn();
    }
}
