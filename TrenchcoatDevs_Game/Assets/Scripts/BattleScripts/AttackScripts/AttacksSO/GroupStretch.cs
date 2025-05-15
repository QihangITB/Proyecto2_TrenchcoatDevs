using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GroupStretch", menuName = "Attacks/GroupStretch")]
public class GroupStretch : GenericAreaAttack
{
    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            BattleManager.instance.FinishTurn();
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                if (target != null)
                {
                    target.GetRested();
                }
            }
            BattleManager.instance.FinishTurn();
        }
    }
    public override void ActivateTargetButtons()
    {
        targetButtons[0] = BattleManager.instance.playerTeamButton;
        targetButtons[0].GetComponent<Image>().enabled = true;
    }
}
