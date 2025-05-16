using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Attacks/BasicAttack")]
public class BasicAttackScript : GenericAttack
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
        foreach (CharacterHolder target in targets)
        {
            target.TakeDamage(user.attack);
        }
        PerformFinishTurn();
    }
}