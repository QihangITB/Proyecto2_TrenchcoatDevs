using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DohaBattery", menuName = "Attacks/DohaBattery")]
public class DohaBattery : GenericAreaAttack
{
    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        if (user.stamina < cost)
        {
            Debug.Log("Not enough stamina");
            return;
        }
        else
        {
            user.UseStamina(cost);
            foreach (CharacterHolder target in targets)
            {
                if (target != null)
                {
                    target.TakeDamage(user.attack*2);
                }
            }
            user.TakeDamage(user.maxHP / 40);
            BattleManager.instance.FinishTurn();
        }
    }
    public override void ActivateTargetButtons()
    {
        targetButtons[0] = BattleManager.instance.enemyTeamButton;
        targetButtons[0].GetComponent<Image>().enabled = true;
    }
}
