using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ChainExplosion", menuName = "Attacks/ChainExplosion")]
public class ChainExplosion : GenericAreaAttack
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
                    target.TakeDamage(user.attack);
                }
            }
            PerformFinishTurn();
        }
    }
    public override void ActivateTargetButtons()
    {
        if (IsOnlineBattle()) 
        {
            targetButtons[0] = OnlineBattleManager.instance.enemyTeamButton;
            targetButtons[0].GetComponent<Image>().enabled = true;
        }
        else
        {
            targetButtons[0] = BattleManager.instance.enemyTeamButton;
            targetButtons[0].GetComponent<Image>().enabled = true;
        }
        
    }
}
