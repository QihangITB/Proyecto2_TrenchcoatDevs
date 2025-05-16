using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EatMore", menuName = "Attacks/EatMore")]
public class EatMore : GenericAreaAttack
{
    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            PerformFinishTurn();
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                if (target != null)
                {
                    target.Heal((target.maxHP / 3)*user.healingModifier, true);
                }
            }
            PerformFinishTurn();
        }
    }
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle()) 
        {
            targetButtons[0] = OnlineBattleManager.instance.playerTeamButton;
            targetButtons[0].GetComponent<Image>().enabled = true;
        }
        else
        {
            targetButtons[0] = BattleManager.instance.playerTeamButton;
            targetButtons[0].GetComponent<Image>().enabled = true;
        }
        
        
    }
}
