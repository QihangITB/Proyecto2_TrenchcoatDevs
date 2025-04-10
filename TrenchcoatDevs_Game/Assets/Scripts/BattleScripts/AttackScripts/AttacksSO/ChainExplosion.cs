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
            BattleManager.instance.FinishTurn();
        }
    }
    public override void ActivateTargetButtons()
    {
        targetButtons.Add(BattleManager.instance.enemyTeamButton);
        targetButtons[0].GetComponent<Image>().enabled = true;
    }
}
