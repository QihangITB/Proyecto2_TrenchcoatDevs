using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PoisonDmgAttack", menuName = "Attacks/PoisonDmgAttack")]
public class PoisonDmgAttack : GenericAttack
{
    public override void ActivateTargetButtons()
    {
        targetButtons.Clear();
        targetButtons = BattleManager.instance.enemyButtons;
        foreach (GameObject button in targetButtons)
        {
            if (button.GetComponent<CharacterHolder>().character != null)
            {
                button.GetComponent<Image>().enabled = true;
            }
        }
    }

    public override void Effect(List<CharacterHolder> targets, CharacterHolder user)
    {
        foreach (CharacterHolder target in targets)
        {
            if (target.isPoisoned)
            {
                target.TakeDamage(user.attack*2);
            }
            else
            {
                target.TakeDamage(user.attack);
            }
        }
        BattleManager.instance.FinishTurn();
    }
}
